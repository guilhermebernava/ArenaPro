namespace ArenaPro.Application.Abstractions;
public interface IServices<P>
{
    Task ExecuteAsync(P parameter);
}

public interface IServices<P,R>
{
    Task<R> ExecuteAsync(P parameter);
}
