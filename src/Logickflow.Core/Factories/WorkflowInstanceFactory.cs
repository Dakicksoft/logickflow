using System;
using System.Collections.Generic;
using System.Linq;
using Logickflow.Core.Security;

namespace Logickflow.Core.Factories
{
  internal class WorkflowInstanceFactory
  {
    /// <summary>
    /// Factory method to create workflow instance
    /// </summary>
    /// <param name="template"></param>
    /// <param name="form"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public static IWorkflowInstance Create(IWorkflowTemplate template, IForm form, WorkflowExecutionContext context)
    {
      var instance = new WorkflowInstance(template, form, context);
      return instance;
    }

    /// <summary>
    /// Factory method to create workflow instance (new process)
    /// </summary>
    /// <param name="template"></param>
    /// <param name="form"></param>
    /// <param name="owner"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public static IWorkflowInstance Create(IWorkflowTemplate template, IForm form, IApprover owner, WorkflowExecutionContext context)
    {
      //Create the first activity instance, and the process owner submits it
      var instance = new WorkflowInstance(template, form, owner, context);
      var activityInstance = new ActivityInstance
      {
        ActivityTemplate = template.Activities.First(),
        CreatedOn = DateTime.Now,
        LastUpdatedOn = DateTime.Now,
      };
      instance.Current = activityInstance;
      return instance;
    }
  }
}