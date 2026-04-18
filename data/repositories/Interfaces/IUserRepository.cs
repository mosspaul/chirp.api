using System;
using data.Models;

namespace data.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> SignUp(User user);
    Task<bool> Login(string username, string password);
    Task<User?> GetProfile(int userId);
    Task<User?> EditProfile(User user);
    Task<bool> DeleteAccount(int userId);
    Task<bool> EditPassword(int userId, string newPassword);
}
