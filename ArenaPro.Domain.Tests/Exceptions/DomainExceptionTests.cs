using ArenaPro.Domain.Validations;

namespace ArenaPro.Domain.Tests.Exceptions;
public class DomainExceptionTests
{
    [Fact]
    public void ItShouldValidateThrowException()
    {
        var test = "123";
        Assert.Throws<DomainException>(() => DomainException.When(test.Length < 5, "Texto pequeno"));
    }

    [Fact]
    public void ItShouldValidateNotThrowException()
    {
        var test = "123";
        DomainException.When(test.Length > 5, "Texto pequeno");
    }
}
