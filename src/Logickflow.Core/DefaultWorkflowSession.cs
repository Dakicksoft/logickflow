using System.Collections.Generic;
using Logickflow.Core.Factories;
using Logickflow.Core.Repository;
using Logickflow.Core.Security;

namespace Logickflow.Core
{
    internal class DefaultWorkflowSession : AbstractWorkflowSession
    {
        private readonly WorkflowInstanceRepository _workflowInstanceRepository;

        public DefaultWorkflowSession()
        {
            _workflowInstanceRepository = new WorkflowInstanceRepository();
        }

        public override IApprover CurrentUser
        {
            get { return EngineContext.Current.RegisteredUserCredentialsProviderProvider.Current; }
        }

        public override void SaveInstance(IWorkflowInstance instance)
        {
            _workflowInstanceRepository.Save(instance);
        }

        public override IWorkflowInstance LoadWorkflowInstance(string workflowInstanceId)
        {
            return _workflowInstanceRepository.Find(workflowInstanceId);
        }

        public override IWorkflowInstance NewWorkflowInstance(IWorkflowTemplate template, string formType, string formId)
        {
            var instance = WorkflowInstanceFactory.Create(template, new Form(formType, formId), CurrentUser,
                new WorkflowExecutionContext() { Approver = CurrentUser });
            return instance;
        }

        public override IEnumerable<IWorkflowInstance> TodoList
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}