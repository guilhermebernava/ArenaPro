namespace ArenaPro.Application.Exceptions;

public class RepositoryException : Exception
{
    public string Repository { get; set; }
    public RepositoryException(string message, string repository) : base(message)
    {
        Repository = repository;
    }
}
