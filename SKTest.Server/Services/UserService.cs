using AutoMapper;
using SKTest.Db.Entities;
using SKTest.Db.Repositories;
using SKTest.Server.Dtos;
using SKTest.Server.Interfaces;

namespace SKTest.Server.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public void DeleteUser(int userId)
        {
            _userRepository.DeleteUser(userId);
            _userRepository.Save();
        }

        public List<UserDto> GetAllUsers()
        {
            var users = _userRepository.GetAllUsers().ToList();
            return _mapper.Map<List<UserDto>>(users);
        }

        public void UpdateUser(UserDto user)
        {
            _userRepository.UpdateUser(_mapper.Map<User>(user));
            _userRepository.Save();
        }

        public bool IsEmailUsedByAnotherUser(string email, int userId)
        {
            return _userRepository.IsEmailUsedByAnotherUser(email, userId);
        }
    }
}
