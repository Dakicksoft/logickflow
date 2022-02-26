using Logickflow.Core;
using Logickflow.Core.Exceptions;
using Logickflow.Core.ValueObjects;

namespace Logickflow.Console.Demo
{
  class Program
  {
    static void Main(string[] args)
    {
            var workflowEngine = EngineContext.Current;

            //Register a simulated Session Provider for testing
            //var sessionProvider = new PhantomUserCredentialsProvider();
            //workflowEngine.RegisterUserCredentialsProvider(sessionProvider);

           NewInstanceAndSubmit();

           //ApproveExistInstance();

           // ContractAppralFacade.NewContractDraft(new ApprovableForm(FormType.Contract, "1234567"));

            System.Console.WriteLine("Press any key to continue...");
            System.Console.ReadKey(true);
        }

        static void NewInstanceAndSubmit()
        {
            var workflowEngine = EngineContext.Current;

            //Register a simulated Session Provider for testing
            var sessionProvider = new PhantomUserCredentialsProvider();
            workflowEngine.RegisterUserCredentialsProvider(sessionProvider);

            //Registration event handling methods such as email notifications of process status changes, etc.
            workflowEngine.OnWorkflowStateChanged += (sender, args) =>
            {
                //TODO:
            };

            //Get the session and perform process-related operations through the session
            var session = workflowEngine.NewSession();

            var template = workflowEngine.LoadWorkflowTemplate("WFT123456");

            var instance = session.NewWorkflowInstance(template, "Contract", "123456");

            session.SaveInstance(instance);

            //save
            instance.Submit("submit application");
            session.SaveInstance(instance);

            instance.Approve("examination passed");
            session.SaveInstance(instance);
        }

        static void ApproveExistInstance()
        {
            var workflowEngine = EngineContext.Current;
            //Register a simulated Session Provider for testing
            var sessionProvider = new PhantomUserCredentialsProvider();

            workflowEngine.RegisterUserCredentialsProvider(sessionProvider);

            var session = workflowEngine.NewSession();

            var instance = session.LoadWorkflowInstance("WFT123456");

            if (instance == null)
                throw new IllegalStateException("Process does not exist");

            //See the current node of the process
            var activityInstance = instance.Current;
            //See the template definition information of the current node of the process
            var activityTemplate = activityInstance.ActivityTemplate;
            //What operations are currently allowed
            var actions = activityTemplate.AllowedActions;
            //If you want to see the process template
            var workflowTemplate = activityTemplate.WorkflowTemplate;
        }
    }
}
