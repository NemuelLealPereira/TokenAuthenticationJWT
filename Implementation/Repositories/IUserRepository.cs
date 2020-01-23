using Implementation.Entities;

namespace Implementation.Repositories
{
    public interface IUserRepository
    {
        User GetUser(string username);
    }
}
