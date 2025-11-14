using CreditCardApi.Data;
using CreditCardApi.DTOs;
using CreditCardApi.Interface;
using CreditCardApi.Models;
using CreditCardApi.Utils;
using Microsoft.EntityFrameworkCore;

namespace CreditCardApi.Services;

public class CreditCardService : ICreditCardService
{
    private readonly AppDbContext _context;

    public CreditCardService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Regist a new credit card after validatin input.
    /// </summary>
    /// <param name="dto">CreditCardDto</param>
    /// <returns> a new valid credit card </returns>
    public async Task<CreditCard> RegisterCardAsync(CreditCardDto dto)
    {
        var card = new CreditCard
        {
            CardHolder = dto.CardHolder,
            EncryptedNumber = EncryptionHelper.Encrypt(dto.CardNumber),
            Expiry = dto.Expiry,
            EncryptedCvc = EncryptionHelper.Encrypt(dto.Cvc)
        };

        _context.CreditCards.Add(card);
        await _context.SaveChangesAsync();

        return card;
    }
    /// <summary>
    /// Get all exist credit card information
    /// </summary>
    /// <returns> json including credit cards information </returns>
    public async Task<IEnumerable<CreditCard>> GetAllAsync() =>
        await _context.CreditCards.AsNoTracking().ToListAsync();

    /// <summary>
    /// Get a vaild credit card by input id
    /// </summary>
    /// <param name="id">Credit card Id</param>
    /// <returns>json including one credit card information </returns>
    public async Task<CreditCard?> GetByIdAsync(int id) =>
        await _context.CreditCards.FindAsync(id);
}