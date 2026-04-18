using System;
using System.Reflection;
using System.Threading.Tasks;
using data.DbContexts;
using data.Models;
using data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace data.Repositories;

public class UserRepository : IUserRepository
{
    public readonly ChirpDbContext _db;
    public UserRepository(ChirpDbContext db)
    {
        _db = db;
    }
    public async Task<bool> DeleteAccount(int userId)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user != null)
        {
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> EditPassword(int userId, string newPassword)
    {
        var userToUpdatePassword = await GetUserById(userId);
        if (userToUpdatePassword != null)
        {
            userToUpdatePassword.PasswordHash = newPassword;
            await _db.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<User?> EditProfile(User user)
    {
        var userToUpdate = await GetUserById(user.Id);
        if (userToUpdate == null)
        {
            return null;
        }
        MapUser(userToUpdate, user);
        await _db.SaveChangesAsync();
        return userToUpdate;
    }

    public async Task<User?> GetProfile(int userId)
    {
        return await GetUserById(userId);
    }

    public async Task<bool> Login(string username, string password)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user != null)
        {
            user.PasswordHash = password;
            await _db.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<User?> SignUp(User user)
    {
        if (await CheckUniqueUsernameAndEmail(user))
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return user;
        }
        return null;
    }

    private async Task<User?> GetUserById(int userId)
    {
        return await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }

    private void MapUser(User userToUpdate, User newUserProfile)

    {
        userToUpdate.Username = newUserProfile.Username;
        userToUpdate.Email = newUserProfile.Email;
        userToUpdate.LastName = newUserProfile.LastName;
        userToUpdate.FirstName = newUserProfile.FirstName;
    }
    private async Task<bool> CheckUniqueUsernameAndEmail(User newUser)
    {
        var existingUser = await _db.Users.FirstOrDefaultAsync(u => u.Username == newUser.Username || u.Email == newUser.Email);
        return existingUser == null;
    }
}

