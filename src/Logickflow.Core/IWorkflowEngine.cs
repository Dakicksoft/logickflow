using System.Collections.Generic;

namespace Logickflow.Core
{
    /// <summary>
    /// Workflow engine
    /// </summary>
    public interface IWorkflowEngine
    {
        /// <summary>
        /// Process status update event
        /// </summary>
        event WorkflowStateChangedEventHandler OnWorkflowStateChanged;

        IWorkflowSession NewSession();

        void RegisterUserCredentialsProvider(IUserCredentialsProvider sessionProvider);

        void Initialize();

        IEnumerable<IWorkflowTemplate> AvailableWorkflowTemplates { get; }

        IWorkflowTemplate LoadWorkflowTemplate(string workflowTemplateId);

        IUserCredentialsProvider RegisteredUserCredentialsProviderProvider { get; }
    }
}