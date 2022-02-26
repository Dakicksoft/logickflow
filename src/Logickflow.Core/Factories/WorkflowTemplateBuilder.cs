using System.Collections.Generic;
using System.Linq;

namespace Logickflow.Core.Factories
{
  internal class WorkflowTemplateBuilder
  {
    private WorkflowTemplate _workflowTemplate;
    private IEnumerable<ActivityTemplate> _activityTemplates;

    class TransitPath
    {
      public ActivityTemplate FromActivityTemplate { get; set; }
      public ActivityTemplate ToActivityTemplate { get; set; }
    }

    public WorkflowTemplateBuilder SetWorkflowTemplate(WorkflowTemplate workflowTemplate)
    {
      _workflowTemplate = workflowTemplate;
      return this;
    }

    public WorkflowTemplateBuilder SetActivityTemplates(IEnumerable<ActivityTemplate> activityTemplates)
    {
      _activityTemplates = activityTemplates;
      return this;
    }

    public IWorkflowTemplate Build()
    {
      var dict = new Dictionary<int, ActivityTemplate>();

      //Set all Activity references to Workflow
      foreach (var activityTemplate in _activityTemplates)
      {
        activityTemplate.WorkflowTemplate = _workflowTemplate;

        dict.Add(activityTemplate.ActivityTemplateId, activityTemplate);
      }

      //Build a circulation path
      foreach (var activity in _activityTemplates)
      {
        foreach (var action in activity.Actions)
        {
          if (action.TargetActivityId > 0)
            action.Transit = dict[action.TargetActivityId];
        }
      }

      _workflowTemplate.AddActivityTemplates(_activityTemplates);

      return _workflowTemplate;
    }
  }
}