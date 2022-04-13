using System.ComponentModel.DataAnnotations;

namespace Domain.App
{
    public class Friendship 
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int FriendId { get; set; }
        public User Friend { get; set; } = null!;
    }
}
