namespace DomainCore.Helpers
{
    public interface ICryptographyHelper
    {
        string EncryptMessage(string messageText);
        string DecryptMessage(string messageHash);
    }
}
