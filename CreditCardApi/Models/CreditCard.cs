namespace CreditCardApi.Models;

public class CreditCard
{
    public int Id {get; set;}
    public string CardHolder {get; set;} = string.Empty;
    public string EncryptedNumber {get; set;} = string.Empty;
    public string Expiry {get; set;} = string.Empty;
    public string EncryptedCvc {get; set;} = string.Empty;
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
}