using System.Linq;
using Logickflow.Core.Exceptions;

namespace Logickflow.Core.Utils
{
  public sealed class WorkflowTemplateValidator
  {
    /// <summary>
    /// Verification process template
    /// </summary>
    /// <param name="workflowTemplate"></param>
    /// <returns></returns>
    public static void Validate(IWorkflowTemplate workflowTemplate)
    {
      //Determine the uniqueness of the first and last nodes
      if (workflowTemplate.Activities.Count(p => p.BeginActivity) > 1
                || workflowTemplate.Activities.Count(q => q.FinalActivity) > 1)
        throw new IllegalWorkflowTemplateException();

      //TODO:Ensure that there are no orphaned nodes

    }
  }
}