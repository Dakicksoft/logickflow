using System.Collections.Generic;

namespace Logickflow.Core.Security
{
  /// <summary>
  /// Approver
  /// </summary>
  public interface IApprover
  {
    /// <summary>
    /// Approval user Id
    /// </summary>
    string ApproverId { get; }

    /// <summary>
    /// Approval roles owned
    /// </summary>
    List<IApproverRole> Roles { get; }
  }
}