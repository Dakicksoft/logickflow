using Logickflow.Core.Security;

namespace Logickflow.Core
{
    public interface IUserCredentialsProvider
    {
        /// <summary>
        /// Through a specific implementation, get the current user information and return to the approval session
        /// </summary>
        IApprover Current { get; }
    }
}