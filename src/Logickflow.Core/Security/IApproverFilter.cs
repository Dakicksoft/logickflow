using System.Collections.Generic;

namespace Logickflow.Core.Security
{
  /// <summary>
  /// Approver filter, filter out the approver specified by the current process in Activity through conditions
  /// </summary>
  public interface IApproverFilter
  {
    List<IApprover> Filter(string filterName, params string[] parameters);
  }
}