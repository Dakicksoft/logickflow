using System;
using System.Collections.Generic;
using Logickflow.Core.Audit;
using Logickflow.Core.Security;

namespace Logickflow.Core
{
    /// <summary>
    /// Workflow example
    /// </summary>
    public interface IWorkflowInstance : IBusinessBase
    {
        /// <summary>
        /// The unique identifier of the process instance
        /// </summary>
        string WorkflowInstanceId { get; }

        /// <summary>
        /// Create a workflow template for this process
        /// </summary>
        IWorkflowTemplate WorkflowTemplate { get; }

        /// <summary>
        /// Approval form
        /// </summary>
        IForm Form { get; }

        /// <summary>
        /// Approval of applicants
        /// </summary>
        IApprover Owner { get; }

        /// <summary>
        /// Approval history
        /// </summary>
        ICollection<AuditTrailEntry> AuditTrails { get; }

        /// <summary>
        /// Default expiration time
        /// </summary>
        DateTime ExpireOn { get; }

        /// <summary>
        /// Current node
        /// </summary>
        IActivityInstance Current { get; }

        IActivityInstance OriginateActivity { get; }

        WorkflowInstanceStatus Status { get; }

        #region Operations
        void Submit(string comment);

        void Approve(string comment);

        void Cancel(string comment);

        void Reject(string comment);

        void Assign(AssignSpecification assignSpecification);

        void Delegate(AssignSpecification assignSpecification);
        #endregion
    }
}