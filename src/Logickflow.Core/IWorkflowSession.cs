using System.Collections.Generic;
using Logickflow.Core.Security;

namespace Logickflow.Core
{
    /// <summary>
    /// Approval streaming session
    /// </summary>
    public interface IWorkflowSession
    {
        IApprover CurrentUser { get; }

        IWorkflowInstance NewWorkflowInstance(IWorkflowTemplate template,string formType,string formId);

        void SaveInstance(IWorkflowInstance instance);

        IWorkflowInstance LoadWorkflowInstance(string workflowInstanceId);

        IEnumerable<IWorkflowInstance> TodoList { get; }

        IEnumerable<IWorkflowInstance> OwnedInstances { get; } 
    }
}