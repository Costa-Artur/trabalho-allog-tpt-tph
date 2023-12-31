using Univai.Api.Entities;

namespace Univali.Api.Repositories;

public class UserRepository : IUserRepository
{
    public UserRepository() {}

    public UserEntity? Get(string username, string password)
    {
        var users = new List<UserEntity>();

        users.Add(new UserEntity {Id = 1, Username = "Pablo", Password = "123"});

        return users
            .Where(u => 
                u.Username.ToUpper() == username.ToUpper()
                && u.Password == password
            ).FirstOrDefault();
    }
}