using CreditCardApi.DTOs;
using CreditCardApi.Models;

namespace CreditCardApi.Interface;

public interface ICreditCardService
{
    Task<CreditCard> RegisterCardAsync(CreditCardDto dot);
    Task<IEnumerable<CreditCard>> GetAllAsync();
    Task<CreditCard?> GetByIdAsync(int id);
}