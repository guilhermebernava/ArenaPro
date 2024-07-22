using ArenaPro.Application.Utils;

namespace ArenaPro.Application.Tests.Utils;
public class ExceptionUtilsTests
{
    [Theory]
    [InlineData("User", "CREATE", "Could not CREATE this User")]
    [InlineData("Team", "DELETE", "Could not DELETE this Team")]
    [InlineData("Match", "GET", "Could not GET this Match")]
    [InlineData("Tournament", "UPDATE", "Could not UPDATE this Tournament")]
    public void ItShouldReturnCorrectMessageWithoutId(string entityName, string errorType, string expectedMessage)
    {
        var result = errorType switch
        {
            "CREATE" => ExceptionUtils.CreateError(entityName),
            "DELETE" => ExceptionUtils.DeleteError(entityName),
            "GET" => ExceptionUtils.GetError(entityName),
            "UPDATE" => ExceptionUtils.UpdateError(entityName),
            _ => throw new NotSupportedException("Unsupported error type")
        };

        Assert.Equal(expectedMessage, result);
    }

    [Theory]
    [InlineData("User", "DELETE", 1, "Could not DELETE this User with this ID - 1")]
    [InlineData("Team", "GET", 2, "Could not GET this Team with this ID - 2")]
    [InlineData("Match", "UPDATE", 3, "Could not UPDATE this Match with this ID - 3")]
    public void ItShouldReturnCorrectMessageWithId(string entityName, string errorType, int id, string expectedMessage)
    {
        var result = errorType switch
        {
            "DELETE" => ExceptionUtils.DeleteError(entityName, id),
            "GET" => ExceptionUtils.GetError(entityName, id),
            "UPDATE" => ExceptionUtils.UpdateError(entityName, id),
            _ => throw new NotSupportedException("Unsupported error type")
        };

        Assert.Equal(expectedMessage, result);
    }
}