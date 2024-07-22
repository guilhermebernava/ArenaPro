namespace ArenaPro.Application.Utils;
public static class ExceptionUtils
{
    public static string CreateError(string entityName) => GenericError("CREATE", entityName);
    public static string DeleteError(string entityName, int? id = null) => GenericError("DELETE", entityName, id);
    public static string GetError(string entityName, int? id = null) => GenericError("GET", entityName, id);
    public static string UpdateError(string entityName, int? id = null) => GenericError("UPDATE", entityName, id);
    private static string GenericError(string errorType, string entityName, int? id = null)
    {
        var errorMessage = $"Could not {errorType} this {entityName}";
        if (id != null)
        {
            errorMessage += $" with this ID - {id}";
        }
        return errorMessage;
    }
}
