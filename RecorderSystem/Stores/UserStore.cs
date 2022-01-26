using Dapper;
using Microsoft.AspNetCore.Identity;
using RecorderSystem.Entities;
using System.Data.Common;
using System.Data.SqlClient;

namespace RecorderSystem.Stores
{
    public class UserStore : IUserStore<User>, IUserPasswordStore<User>
    {
        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            using var connection = GetOpenConnection();

            await connection.ExecuteAsync(
                    "insert into AdminUsers([Id]," +
                    "[UserName]," +
                    "[NormalizedUserName]," +
                    "[PasswordHash]) " +
                    "Values(@id,@userName,@normalizedUserName,@passwordHash)",
                    new
                    {
                        id = user.Id,
                        userName = user.UserName,
                        normalizedUserName = user.NormalizedUserName,
                        passwordHash = user.PasswordHash
                    }
                );

            return IdentityResult.Success;
        }

        public static DbConnection GetOpenConnection()
        {
            var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;" +
                                               "database=RecorderSystem;" +
                                               "trusted_connection=yes;");

            connection.Open();

            return connection;
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            // TODO:
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            using var connection = GetOpenConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(
                "Select * From AdminUsers where Id = @id", new { id = userId });
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            using var connection = GetOpenConnection();

            return await connection.QueryFirstOrDefaultAsync<User>(
                "select * From AdminUsers where NormalizedUserName = @name",
                new { name = normalizedUserName });
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.NormalizedUserName);

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.Id);

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.UserName);

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            using var connection = GetOpenConnection();
            await connection.ExecuteAsync(
                    "update AdminUsers " +
                    "set [Id] = @id," +
                    "[UserName] = @userName," +
                    "[NormalizedUserName] = @normalizedUserName," +
                    "[PasswordHash] = @passwordHash " +
                    "where [Id] = @id",
                    new
                    {
                        id = user.Id,
                        userName = user.UserName,
                        normalizedUserName = user.NormalizedUserName,
                        passwordHash = user.PasswordHash
                    });

            return IdentityResult.Success;
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.PasswordHash);
        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.PasswordHash != null);
    }
}
