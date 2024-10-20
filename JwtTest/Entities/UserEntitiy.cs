using System.ComponentModel.DataAnnotations;

namespace JwtTest.Entities
{
    public class UserEntitiy
    {
        public UserEntitiy()
        {
            CreateDate = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public UserType UserType { get; set; }
        public DateTime CreateDate { get; set; }
        public bool  IsDelete { get; set; }



    }
    public enum UserType
    {
        admin,user
    }
}
