using System;
using data.Models;

namespace data.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetProfile(string userId);
    Task<User?> EditProfile(User user);
    Task<bool> DeleteAccount(string userId);
    Task<User?> GetUserById(string userId);
    Task<List<User>> GetUsersWithSimpleFinAccessUrl(CancellationToken ct);
}
