using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Logickflow.Core.Factories;
using Logickflow.Core.Security;

// ReSharper disable PossibleNullReferenceException

namespace Logickflow.Core.Utils
{
    public class XmlWorkflowTemplateParser
    {
        private const int DefaultTemplateStatus = 1;

        /// <summary>
        /// Parse from string
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static IWorkflowTemplate ParseFromString(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                throw new ArgumentException();

            var stream = PrepareStream(xml);

            var xdoc = XDocument.Load(stream);

            return DoParse(xdoc);
        }

        /// <summary>
        /// Parse from file
        /// </summary>
        /// <param name="uri">XML file path</param>
        /// <returns></returns>
        public static IWorkflowTemplate Parse(string uri)
        {
            var xdoc = XDocument.Load(uri);
            return DoParse(xdoc);
        }

        private static Stream PrepareStream(string source)
        {
            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream, Encoding.UTF8);
            streamWriter.Write(source);
            streamWriter.Flush();
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// Parsing
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        private static IWorkflowTemplate DoParse(XDocument xdoc)
        {
            if (xdoc == null)
                throw new ArgumentException();

            var root = xdoc.Element("WorkflowTemplate");

            var templateUuid = root.Element("Identifier").Value;
            //Create WorkflowTemplate object
            var workflowTemplate = new WorkflowTemplate(templateUuid, DefaultTemplateStatus, null);

            //Build ActivityTemplates
            //Note that the deferred execution caused by Lambda expressions may be the cause of some internal null pointers
            var query = from item in root.Element("Activities").Elements("Activity")
                        select BuildActivityTemplate(item);

            var builder = new WorkflowTemplateBuilder();

            return builder.SetActivityTemplates(query.ToList())
                .SetWorkflowTemplate(workflowTemplate)
                .Build();
        }

        private static ActivityTemplate BuildActivityTemplate(XElement element)
        {
            var activityTemplate = new ActivityTemplate();
            var activityTemplateId = Convert.ToInt32(element.Attribute("id").Value);
            var beginActivity = Convert.ToBoolean(element.Attribute("beginActivity")?.Value);
            var finalActivity = Convert.ToBoolean(element.Attribute("finalActivity")?.Value);
            var activityTemplateName = element.Element("Name").Value;

            activityTemplate.BeginActivity = beginActivity;
            activityTemplate.FinalActivity = finalActivity;
            activityTemplate.ActivityTemplateId = activityTemplateId;
            activityTemplate.Name = activityTemplateName;

            var descriptor = element.Element("Approver").Attribute("descriptor")?.Value;

            activityTemplate.RequiredRole = ApprovalRoleResolver.Resolve(descriptor);
            //activityTemplate.RequiredRole = new ApproverRole()
            //{
            //    Id = "role123",
            //    Name = "Test role",
            //    Description = "For testing only"
            //};
            var actionElements = element.Elements("Actions");
            foreach (var actionElem in actionElements.Elements("Action"))
            {
                var operation = (OperationCode)Enum.Parse(typeof(OperationCode), actionElem.Attribute("operationCode").Value);
                var transitTo = 0;
                if (actionElem.Attribute("transit") != null)
                {
                    transitTo = Convert.ToInt32(actionElem.Attribute("transit").Value);
                }
                var action = new Action(operation, transitTo);
                activityTemplate.Actions.Add(action);
            }
            return activityTemplate;
        }
    }
}