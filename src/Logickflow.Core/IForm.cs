namespace Logickflow.Core
{
    /// <summary>
    /// Abstract form
    /// </summary>
    public interface IForm
    {
        /// <summary>
        /// Form type
        /// </summary>
        string FormType { get; }

        /// <summary>
        /// Form unique identifier
        /// </summary>
        string FormId { get; }
    }
}