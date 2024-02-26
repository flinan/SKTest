using Microsoft.EntityFrameworkCore;
using SKTest.Db.Entities;
using SKTest.Db.Repositories;
using SKTest.Infrastructure.Data;
namespace SKTest.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private CodeTestContext _context;
        private bool _disposed = false;

        public UserRepository(CodeTestContext context) { _context = context; }


        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User? GetUserById(int userId)
        {
            return _context.Users.Find(userId);
        }

        public void UpdateUser(User user)
        {
            if (user.UserId == 0)
            {
                _context.Users.Add(user);
                return;
            }

            var userDb = GetUserById(user.UserId);
            if (userDb != null)
            {
                userDb.FirstName = user.FirstName;
                userDb.LastName = user.LastName;
                userDb.Email = user.Email;
            }
            // context.Entry(user).State = EntityState.Modified;
        }

        public void DeleteUser(int userId)
        {
            var user = GetUserById(userId);
            if (user == null) { return; }
            _context.Users.Remove(user);
        }

        public bool IsEmailUsedByAnotherUser(string email, int userId)
        {
            return _context.Users.FirstOrDefault((User u) => u.Email.ToUpper() == email.ToUpper() && u.UserId != userId) != null;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
