using Univai.Api.Entities;

namespace Univali.Api.Repositories;

public interface IUserRepository
{
    UserEntity? Get(string username, string password);
}