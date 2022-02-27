namespace Shared.Validations
{
    public interface IValidatable<T>
    {
        bool IsValid { get; }
        IReadOnlyCollection<T> Notifications { get; }
    }
}
