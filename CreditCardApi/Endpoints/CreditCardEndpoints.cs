using CreditCardApi.DTOs;
using CreditCardApi.Interface;
using CreditCardApi.Utils;
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
    {
        return Results.Ok(await service.GetAllAsync());
    }

    /// <summary>
    /// Get api/cards/{id}
    /// </summary>
    /// <param name="id"> Unique credit card identity number</param>
    /// <param name="service">Interface ICreditCardService instance used to access credit card business logic and data</param>
    /// <returns>A credit card entity if found; otherwise, NotFound result</returns>
    private static async Task<IResult> GetCardById(int id, ICreditCardService service)
    {
        var card = await service.GetByIdAsync(id);
        return card is null? Results.NotFound(): Results.Ok(card);
    }

}