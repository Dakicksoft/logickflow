using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Logickflow.Core.Audit;
using Logickflow.Core.Configurations;
using Logickflow.Core.Exceptions;
using Logickflow.Core.Factories;
using Logickflow.Core.Security;

namespace Logickflow.Core
{
    /// <summary>
    /// Internal default implementation of workflow instance
    /// <remarks>Workflow business aggregation root</remarks>
    /// </summary>
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    internal class WorkflowInstance : IWorkflowInstance
    {
        private readonly IWorkflowTemplate _workflowTemplate;
        private readonly IForm _form;
        private readonly IApprover _owner;
        private ICollection<AuditTrailEntry> _auditTrailEntries;
        private readonly WorkflowExecutionContext _executionContext;
        private bool _isDirty = true;
        private bool _isNew;
        private IActivityInstance _originateActivityInstance;

        public WorkflowInstance(IWorkflowTemplate workflowTemplate, IForm form, IApprover owner, WorkflowExecutionContext context)
            : this(workflowTemplate, form, context)
        {
            _owner = owner;
        }

        public WorkflowInstance(IWorkflowTemplate workflowTemplate, IForm form, WorkflowExecutionContext context)
        {
            WorkflowInstanceId = CreateWorkflowInstanceId();
            _workflowTemplate = workflowTemplate;
            _form = form;
            _executionContext = context;
            _isNew = true;
            Status = WorkflowInstanceStatus.New;
        }

        private static string CreateWorkflowInstanceId()
        {
            return Guid.NewGuid().ToString();
        }

        public string WorkflowInstanceId { get; internal set; }

        public IWorkflowTemplate WorkflowTemplate
        {
            get { return _workflowTemplate; }
        }

        public IForm Form
        {
            get { return _form; }
        }

        public IApprover Owner
        {
            get { return _owner; }
        }

        public ICollection<AuditTrailEntry> AuditTrails
        {
            get { return _auditTrailEntries ?? (_auditTrailEntries = new List<AuditTrailEntry>()); }
        }

        public DateTime ExpireOn
        {
            get { return DateTime.MaxValue; }
        }

        public long InstanceVersion { get; set; }

        public IActivityInstance Current { get; set; }

        public IActivityInstance OriginateActivity
        {
            get
            {
                return _originateActivityInstance;
            }
        }

        public WorkflowExecutionContext ExecutionContext
        {
            get { return _executionContext; }
        }

        private static void AssertOperation(IActivityInstance activityInstance, OperationCode operationCode)
        {
            if (activityInstance.ActivityTemplate.AllowedActions.All(p => p.OperationCode != operationCode))
                throw new InvalidOperationException("The current node does not allow this operation");
        }

        private void AssertPrivilege(IActivityInstance activityInstance)
        {
            if (EnvConfiguration.PermissionCheckOff)
                return;

            if (!_executionContext.Approver.Roles.Contains(activityInstance.ActivityTemplate.RequiredRole))
                throw new IllegalStateException("User does not have permission to perform this operation");
        }

        private static IAction GetCurrentAction(IActivityInstance activityInstance, OperationCode operationCode)
        {
            var ret = activityInstance.ActivityTemplate.AllowedActions.FirstOrDefault(p => p.OperationCode == operationCode);
            if (ret == null)
                throw new IllegalStateException("Failed to obtain the corresponding Action information");
            return ret;
        }

        private static ActivityInstance NewActivityInstance(IActivityTemplate activityTemplate)
        {
            var builder = new ActivityInstanceBuilder(activityTemplate);
            return builder.Build();
        }

        #region Operations
        public void Submit(string comment)
        {
            AssertOperation(Current, OperationCode.Submit);
            //Permission verification is not performed when submitting, and it may be added later. Some users may not have the permission to submit specific processes
            AssertPrivilege(Current);

            var action = GetCurrentAction(Current, OperationCode.Submit);

            //Determine whether this node loops back
            if (action.Transit.ActivityTemplateId != Current.ActivityTemplate.ActivityTemplateId)
            {
                //Loopback of the non-local node, create the next node instance
                var nextActivityInstance = NewActivityInstance(action.Transit);

                //Mark the activity as completed
                _originateActivityInstance = Current;
                _originateActivityInstance.MarkFinish();
                //_originateActivityInstance.Tail.MarkFinished();

                //Replace the current node
                Current = nextActivityInstance;
            }

            //Create an Action instance accordingly
            var actionRecord = new ActionRecord()
            {
                ActivityInstanceId = Current.ActivityInstanceId,
                RequiredRole = Current.ActivityTemplate.RequiredRole.Id
            };
            Current.AddAction(actionRecord);

            //Current node replacement
            var auditTrail = new AuditTrailEntry() { IsNew = true };
            //Add audit log
            AuditTrails.Add(auditTrail);

            _isDirty = true;
        }

        public void Approve(string comment)
        {
            AssertOperation(Current, OperationCode.Approve);
            AssertPrivilege(Current);

            //Build the next Activity Instance
            var activityTemplate = Current.ActivityTemplate.AllowedActions.FirstOrDefault(p => p.OperationCode == OperationCode.Approve)
                .Transit;
            var nextActivityInstance = NewActivityInstance(activityTemplate);

            _originateActivityInstance = Current;
            _originateActivityInstance.MarkFinish();

            Current = nextActivityInstance;

            _isDirty = true;
        }

        public void Cancel(string comment)
        {
            AssertOperation(Current, OperationCode.Cancel);
            AssertPrivilege(Current);
            //TODO: implement
        }

        public void Reject(string comment)
        {
            AssertOperation(Current, OperationCode.Reject);
            AssertPrivilege(Current);

            var activityTemplate = Current.ActivityTemplate.AllowedActions.FirstOrDefault(p => p.OperationCode == OperationCode.Reject)
                .Transit;
            var nextActivityInstance = NewActivityInstance(activityTemplate);
            _originateActivityInstance = Current;
            Current = nextActivityInstance;
        }

        public void Assign(AssignSpecification assignSpecification)
        {
            AssertOperation(Current, OperationCode.Assign);
            AssertPrivilege(Current);
            //TODO: implement
        }

        public void Delegate(AssignSpecification assignSpecification)
        {
            AssertOperation(Current, OperationCode.Delegate);
            AssertPrivilege(Current);
            //TODO: implement
        }
        #endregion

        public bool IsNew
        {
            get { return _isNew; }
        }

        public bool IsTransient
        {
            get { return false; }
        }

        public bool IsDirty { get { return _isDirty; } }

        public void MarkOld()
        {
            _isNew = false;
            _isDirty = false;

            if (_originateActivityInstance != null)
                _originateActivityInstance.MarkOld();
            Current.MarkOld();
        }

        public override string ToString()
        {
            return string.Format("WorkflowInstance<Uid:{0}>", WorkflowInstanceId);
        }

        public override int GetHashCode()
        {
            return WorkflowInstanceId.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var instance = obj as WorkflowInstance;
            if (instance != null)
            {
                return WorkflowInstanceId == instance.WorkflowInstanceId;
            }
            return false;
        }

        public WorkflowInstanceStatus Status { get; internal set; }

    }
}