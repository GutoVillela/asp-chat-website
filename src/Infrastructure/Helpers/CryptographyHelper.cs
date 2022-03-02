using DomainCore.Helpers;

namespace Infrastructure.Helpers
{
    public class CryptographyHelper : ICryptographyHelper
    {
        public string DecryptMessage(string messageHash)
        {
            // TODO: ATTENTION: This method isn't really decypting the message
            return messageHash;
        }

        public string EncryptMessage(string messageText)
        {
            // TODO: ATTENTION: This method isn't really encypting the message
            return messageText;
        }
    }
}
