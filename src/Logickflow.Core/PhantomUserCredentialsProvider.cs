using System.Collections.Generic;
using Logickflow.Core.Security;

namespace Logickflow.Core
{
    public class PhantomUserCredentialsProvider:IUserCredentialsProvider
    {
        private readonly IApprover _approver;

        public PhantomUserCredentialsProvider()
        {
            _approver = new Approver()
            {
                ApproverId = "admin123",
                Roles = new List<IApproverRole>()
                {
                    new ApproverRole()
                    {
                        Description = "Test role",
                        Id = "OWNER",
                        Name = "OWNER"
                    }
                }
            };
        }

        public IApprover Current
        {
            get { return _approver;}
        }
    }
}