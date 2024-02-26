using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SKTest.Db.Entities
{
    public class User
    {
        public static User Create(int Id, string FirstName, string LastName, string Email)
        {
            return new User
            {
                UserId = Id,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email
            };
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }

    }
}
