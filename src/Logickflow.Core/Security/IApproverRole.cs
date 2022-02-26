namespace Logickflow.Core.Security
{
  public interface IApproverRole
  {
    /// <summary>
    /// Unique identification of approval role
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Name
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Details
    /// </summary>
    string Description { get; }
  }
}