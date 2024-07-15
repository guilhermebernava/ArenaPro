namespace ArenaPro.Application.Abstractions;
public interface IServices<P, R>
{
    Task<R> ExecuteAsync(P parameter);
}

public interface IServices<R>
{
    Task<R> ExecuteAsync();
}
