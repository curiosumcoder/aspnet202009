using System;
using Xunit;

namespace WA90.Tests
{
    public class Hello_Tests
    {
        [Fact(Skip = "Demostraci�n")]
        //[Fact]
        public void SayHelloWorld()
        {
            // Arrange
            bool variableBoleana;

            // Act
            variableBoleana = false;

            // Assert
            Assert.True(variableBoleana, "No es verdadero");
        }
    }
}
