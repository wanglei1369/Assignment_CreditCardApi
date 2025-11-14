using CreditCardApi.Data;
using CreditCardApi.DTOs;
using CreditCardApi.Services;
using CreditCardApiTests.Utils;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CreditCardApiTests;

public class CreditCardServiceTests
{
    [Fact]
    public async Task RegisterCard_Should_Save_To_Database()
    {
        using var context = DbHelper.CreateContext("TestDb_Register");
        var service = new CreditCardService(context);

        var dto = new CreditCardDto
        {
            CardHolder = "John Doe",
            CardNumber = "4111111111111111",
            Expiry = "12/27",
            Cvc = "123"
        };

        // Act
        var result = await service.RegisterCardAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("John Doe", result.CardHolder);
        Assert.NotEmpty(result.EncryptedNumber);
        Assert.NotEmpty(result.EncryptedCvc);

        // Verify it is actually saved
        var savedCard = await context.CreditCards.FindAsync(result.Id);
        Assert.NotNull(savedCard);
        Assert.Equal(result.Id, savedCard!.Id);
       
    }

        [Fact]
    public async Task GetAllCards_Should_Return_AllRegisteredCards()
    {
        using var context = DbHelper.CreateContext("TestDb_GetAll");
        var service = new CreditCardService(context);

        var dto1 = new CreditCardDto { CardHolder = "Allen", CardNumber="42842743569722", Expiry="01/26", Cvc="111" };
        var dto2 = new CreditCardDto { CardHolder = "Bob", CardNumber="4284274335235", Expiry="02/26", Cvc="222" };

        await service.RegisterCardAsync(dto1);
        await service.RegisterCardAsync(dto2);

        // Act
        var allCards = await service.GetAllAsync();

        // Assert
        Assert.Equal(2, allCards.Count());
    }

    [Fact]
    public async Task GetById_Should_Return_CorrectCard()
    {
        using var context = DbHelper.CreateContext("TestDb_GetById");
        var service = new CreditCardService(context);

        var dto = new CreditCardDto { CardHolder="Charlie", CardNumber="4284274993722", Expiry="03/26", Cvc="333" };
        var savedCard = await service.RegisterCardAsync(dto);

        // Act
        var card = await service.GetByIdAsync(savedCard.Id);

        // Assert
        Assert.NotNull(card);
        Assert.Equal("Charlie", card!.CardHolder);
    }
}