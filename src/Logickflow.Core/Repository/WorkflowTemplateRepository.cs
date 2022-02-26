using System;
using System.Collections.Generic;
using System.IO;
using Logickflow.Core.Exceptions;
using Logickflow.Core.Utils;

namespace Logickflow.Core.Repository
{
    internal class WorkflowTemplateRepository
    {
        private readonly Dictionary<string, IWorkflowTemplate> _cachedWorkflowTemplates;

        public WorkflowTemplateRepository()
        {
            _cachedWorkflowTemplates = new Dictionary<string, IWorkflowTemplate>();
        }

        public IWorkflowTemplate Find(string workflowTemplateId)
        {
            if (_cachedWorkflowTemplates.ContainsKey(workflowTemplateId)) return _cachedWorkflowTemplates[workflowTemplateId];
            var template = DoLoadTemplate(workflowTemplateId);
            if (template == null)
                throw new IllegalStateException();
            _cachedWorkflowTemplates.Add(workflowTemplateId, template);
            return _cachedWorkflowTemplates[workflowTemplateId];
        }

        public ICollection<IWorkflowTemplate> AvailableWorkflowTemplates
        {
            get
            {
                return new List<IWorkflowTemplate>();
            }
        } 

        private IWorkflowTemplate DoLoadTemplate(string workflowTemplateId)
        {
            //TODO:replace the mock implmentation
            return XmlWorkflowTemplateParser.Parse($"{Directory.GetCurrentDirectory()}\\Templates\\{workflowTemplateId}.xml");
        }
    }
}