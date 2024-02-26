using SKTest.Db.Entities;
using SKTest.Server.Dtos;

namespace SKTest.Server.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetAllUsers();
        void UpdateUser(UserDto user);
        void DeleteUser(int userId);

        bool IsEmailUsedByAnotherUser(string email, int userId);

    }
}
