namespace Logickflow.Core
{
    public interface IBusinessBase
    {
        bool IsNew { get; }

        bool IsDirty { get; }

        bool IsTransient { get; }

        void MarkOld();
    }
}