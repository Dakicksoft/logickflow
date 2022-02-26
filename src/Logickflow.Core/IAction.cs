namespace Logickflow.Core
{
  public interface IAction
  {
    /// <summary>
    /// Opcode
    /// </summary>
    OperationCode OperationCode { get; }

    /// <summary>
    /// Target Activity
    /// </summary>
    IActivityTemplate Transit { get; }
  }
}