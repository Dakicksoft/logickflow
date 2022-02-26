namespace Logickflow.Core.Audit
{
    /// <summary>
    /// Approval record
    /// </summary>
    public class AuditTrailEntry
    {
        public string OperatorId { get; internal set; }

        public string WorkflowInstanceId { get; internal set; }

        internal bool IsNew { get; set; }
    }
}