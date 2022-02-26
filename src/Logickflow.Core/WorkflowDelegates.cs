using Logickflow.Core.Events;

namespace Logickflow.Core
{
  /// <summary>
  /// Process status update processing commission
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="args"></param>
  public delegate void WorkflowStateChangedEventHandler(object sender, WorkflowStateChangedEvent args);
}