using MetaquotesApi;
using Xunit;

namespace MetaquotesApiTests;

public class SrvRandAnswerTests
{
    [Fact]
    public void CalculatesFine()
    {
        Assert.Equal("77fe51827f7fa69dd80fbec9aa33f1bb",
            SrvRandAnswer.Calculate("Password1", "73007dc7184747ce0f7c98516ef1c851 "));
    }
}