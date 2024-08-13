using ArenaPro.Domain.Entities.Auth;

namespace ArenaPro.Domain.Abstractions;
public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}
