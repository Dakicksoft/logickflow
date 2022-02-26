using System;
using System.Collections.Generic;

namespace Logickflow.Core
{
    public interface IActivityInstance : IBusinessBase
    {
        string ActivityInstanceId { get; }

        IActivityTemplate ActivityTemplate { get; }

        /// <summary>
        /// Current status of the process
        /// </summary>
        ActivityInstanceStatus Status { get; }

        /// <summary>
        /// Process creation time
        /// </summary>
        DateTime CreatedOn { get; }

        /// <summary>
        /// Process expiration time
        /// </summary>
        DateTime ExpireOn { get; }

        /// <summary>
        /// Last modification time of the process
        /// </summary>
        DateTime LastUpdatedOn { get; }

        /// <summary>
        /// Mark node instance complete
        /// </summary>
        void MarkFinish();

        /// <summary>
        /// Action collection
        /// </summary>
        IEnumerable<ActionRecord> ActionRecords { get; }

        void AddAction(ActionRecord record);
    }
}