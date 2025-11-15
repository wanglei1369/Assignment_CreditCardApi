using CreditCardApi.DTOs;
using CreditCardApi.Interface;
using CreditCardApi.Utils;
using CreditCardApi.Models;
using Microsoft.AspNetCore.Routing.Tree;

namespace CreditCardApi.Endpoints;


// Handles all credit card related REST endpoints
//Part of the modular Minimal API architecture.
public static class CreditCardEndpoints
{
    public static void MapCreditCardEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/cards", CreateCard);
        app.MapGet("/api/cards", GetAllCards);
        app.MapGet("/api/cards/{id}", GetCardById);
    }


    /// <summary>
    /// Post api/Cards --this endpoint will create a new credit card entry
    /// <param name="dto">The credit card data received from the frontend form (CardHolder, CardNumber, Expiry, CVC)</param>
    /// <param name="service">Interface ICreditCardService instance used to access credit card business logic and data</param>
    /// <returns>A credit card entity with encrypted sensitive  information</returns>
    private static async Task<IResult> CreateCard(CreditCardDto dto, ICreditCardService service)
    {
        if (!CreditCardValidator.TryValidate(dto, out var errors))
            return Results.BadRequest(errors);
            
        var result = await service.RegisterCardAsync(dto);
        return Results.Created($"/api/cards/{result.Id}", result);
    }

    /// <summary>
    /// Get api/cards
    /// </summary>
    /// <param name="service">Interface ICreditCardService instance used to access credit card business logic and data</param>
    /// <returns>All credit card entity</returns>
    private static async Task<IResult> GetAllCards(ICreditCardService service)
        => Results.Ok((await service.GetAllAsync()).Select(ToResponseDto));

    /// <summary>
    /// Get api/cards/{id}
    /// </summary>
    /// <param name="id"> Unique credit card identity number</param>
    /// <param name="service">Interface ICreditCardService instance used to access credit card business logic and data</param>
    /// <returns>A credit card entity if found; otherwise, NotFound result</returns>
    private static async Task<IResult> GetCardById(int id, ICreditCardService service)
        =>(await service.GetByIdAsync(id)) is {} card
        ? Results.Ok(ToResponseDto(card))
        : Results.NotFound();
    
    /// <summary>
    /// Encapsulate the parameters into ToResponseDto
    /// </summary>
    /// <param name="card">Credit model</param>
    /// <returns></returns>
    private static CreditCardResponseDto ToResponseDto(CreditCard card)
    {
        return new CreditCardResponseDto
        {
            Id = card.Id,
            CardHolder = card.CardHolder,
            Expiry = card.Expiry,
            MaskedNumber = Mask(card.EncryptedNumber)
        };
    }

    /// <summary>
    /// Mask cark nuber
    /// </summary>
    /// <param name="encryptedCardNumber"> Encrypted number</param>
    /// <returns> Part of card number</returns>
    private static string Mask(string encryptedCardNumber)
    {
        var raw = EncryptionHelper.Decrypt(encryptedCardNumber);

        if (raw.Length <= 4)
            return raw;

        return new string('*', raw.Length - 4) + raw[^4..];
    }
}