using System;
using System.Collections.Generic;
using Logickflow.Core.Security;

namespace Logickflow.Core
{
    public abstract class AbstractWorkflowSession : IWorkflowSession
    {
        protected AbstractWorkflowSession()
        {

        }

        public abstract IApprover CurrentUser { get; }

        public abstract void SaveInstance(IWorkflowInstance instance);

        public abstract IWorkflowInstance LoadWorkflowInstance(string workflowInstanceId);

        public abstract IWorkflowInstance NewWorkflowInstance(IWorkflowTemplate template, string formType, string formId);
        
        public abstract IEnumerable<IWorkflowInstance> TodoList { get; }


        public IEnumerable<IWorkflowInstance> OwnedInstances
        {
            get { throw new NotImplementedException(); }
        }
    }
}