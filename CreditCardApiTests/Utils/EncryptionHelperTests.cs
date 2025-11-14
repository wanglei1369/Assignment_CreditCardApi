using CreditCardApi.Utils;
using Xunit;

namespace CreditCardApiTests.Utils
{
    public class EncryptionHelperTests
    {
        [Fact]
        public void Encrypt_should_Return_Base64_String()
        {
            // Arrange
            var input = "4111111111111111";

            //Act
            var encrypted = EncryptionHelper.Encrypt(input);

            //Assert
            Assert.NotNull(encrypted);
            Assert.NotEqual(input, encrypted);
            Assert.True(IsBase64String(encrypted));

        }

        [Fact]
        public void Decrypt_Should_Return_Original_String()
        {
            //Arrange
            var original = "4111111111111111";
            var encrypted = EncryptionHelper.Encrypt(original);

            //Act
            var decrypted = EncryptionHelper.Decrypt(encrypted);

            //Assert
            Assert.Equal(original, decrypted);

        }

        [Fact]
        public void Encrypt_EmptyString_Should_Return_Base64_OfEmpty()
        {
            //Arrange
            var input = "";

            //Act
            var encrypted = EncryptionHelper.Encrypt(input);

            //Assert
            Assert.Equal("", EncryptionHelper.Decrypt(encrypted));

        }

        [Fact]
        public void Decrypt_InvalidBase64_Should_Throw_FromatException()
        {
            //Arragne
            var invalidBase64 = "@Not_A_Real_Base64String";

            //Act & Assert
            Assert.Throws<FormatException>(()=>
                EncryptionHelper.Decrypt(invalidBase64)
            );
        }

        // method validation of Base64\
        private bool IsBase64String(String base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64,buffer,out _);
        }  
    }
}