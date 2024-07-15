using FluentValidation.Results;
using System.Text;

namespace ArenaPro.Application.Utils;
public static class FluentValidationUtils
{
    public static string GetValidationErrors(ValidationResult result)
    {
        StringBuilder sb = new StringBuilder();

        foreach (var failure in result.Errors)
        {
            sb.AppendLine($"Property {failure.PropertyName} failed validation. Error was: {failure.ErrorMessage}");
        }

        return sb.ToString();
    }
}
