using System;
using data.Models;

namespace data.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetProfile(string userId);
    Task<User?> EditProfile(User user);
    Task<bool> DeleteAccount(string userId);
    Task<bool> EditPassword(string userId, string newPassword);
    Task<User?> GetUserById(string userId);
}
