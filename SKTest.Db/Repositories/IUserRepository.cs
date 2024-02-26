using SKTest.Db.Entities;

namespace SKTest.Db.Repositories
{
    public interface IUserRepository : IDisposable
    {
        IEnumerable<User> GetAllUsers();

        User? GetUserById(int userId);
        void UpdateUser(User user);
        void DeleteUser(int userId);
        void Save();
        bool IsEmailUsedByAnotherUser(string email, int userId);
    }
}
