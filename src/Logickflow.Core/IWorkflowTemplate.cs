using System.Collections.Generic;

namespace Logickflow.Core
{
    public interface IWorkflowTemplate
    {
        /// <summary>
        /// Unique ID of process template
        /// </summary>
        string TemplateUuid { get; }

        /// <summary>
        /// Process template status
        /// </summary>
        int Status { get; }

        /// <summary>
        /// Collection of nodes in the process
        /// </summary>
        IEnumerable<IActivityTemplate> Activities { get; }

    }
}