using System.Collections.Generic;
using Logickflow.Core.Security;

namespace Logickflow.Core
{
    public interface IActivityTemplate
    {
        /// <summary>
        /// Name
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Unique ID of ActivityTemplate
        /// </summary>
        int ActivityTemplateId { get; }

        /// <summary>
        /// The workflow template to which the activity belongs
        /// </summary>
        IWorkflowTemplate WorkflowTemplate { get; }

        /// <summary>
        /// Whether to start node
        /// </summary>
        bool BeginActivity { get; }

        /// <summary>
        /// Final node
        /// </summary>
        bool FinalActivity { get; }

        /// <summary>
        /// Approval strategy of the current node
        /// </summary>
        IApprovalPolicy ApprovalPolicy { get; }

        /// <summary>
        /// Allowed operations
        /// </summary>
        IEnumerable<IAction> AllowedActions { get; }

        /// <summary>
        /// Role required for approval
        /// </summary>
        IApproverRole RequiredRole { get; }
    }
}      