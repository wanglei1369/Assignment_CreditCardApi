using System.Text;

namespace CreditCardApi.Utils;

public static class EncryptionHelper
{
    public static string Encrypt(string input) => 
        Convert.ToBase64String(Encoding.UTF8.GetBytes(input));

    public static string Decrypt(string encrypted) =>
        Encoding.UTF8.GetString(Convert.FromBase64String(encrypted));    
}