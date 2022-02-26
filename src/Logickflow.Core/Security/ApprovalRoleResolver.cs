using System;

namespace Logickflow.Core.Security
{
    public class ApprovalRoleResolver
    {
        public static IApproverRole Resolve(string descriptor)
        {
            if (string.IsNullOrWhiteSpace(descriptor))
            {
                throw new ArgumentNullException($"{nameof(descriptor)} is null or empty!");
            }

            return new ApproverRole()
            {
                Description = descriptor,
                Name = descriptor.Split(":")[1],
                Id = descriptor.Split(":")[1],
            };

        }
    }
}