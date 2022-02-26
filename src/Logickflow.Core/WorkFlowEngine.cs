using System;
using System.Collections.Generic;
using Logickflow.Core.Exceptions;
using Logickflow.Core.Repository;

namespace Logickflow.Core
{
  /// <summary>
  /// Default workflow engine
  /// </summary>
  internal class WorkFlowEngine : IWorkflowEngine
    {
        private IUserCredentialsProvider _userCredentialsProvider;

        private readonly WorkflowTemplateRepository _workflowTemplateRepository;

        public event WorkflowStateChangedEventHandler OnWorkflowStateChanged;

        public WorkFlowEngine()
        {
            //TODO:Use dependency injection to loose couple
            _workflowTemplateRepository = new WorkflowTemplateRepository();
        }

        public void RegisterUserCredentialsProvider(IUserCredentialsProvider userCredentialsProvider)
        {
            _userCredentialsProvider = userCredentialsProvider;
        }

        public void Initialize()
        {

        }

        public IEnumerable<IWorkflowTemplate> AvailableWorkflowTemplates
        {
            get { return _workflowTemplateRepository.AvailableWorkflowTemplates; }
        }

        public IWorkflowTemplate LoadWorkflowTemplate(string workflowTemplateId)
        {
            return _workflowTemplateRepository.Find(workflowTemplateId);
        }


        public IUserCredentialsProvider RegisteredUserCredentialsProviderProvider
        {
            get
            {
                if (_userCredentialsProvider == null)
                    throw new IllegalStateException("Session Provider not registered");
                return _userCredentialsProvider;
            }
        }


        public IWorkflowSession NewSession()
        {
            var session = new DefaultWorkflowSession();
            return session;
        }
    }
}