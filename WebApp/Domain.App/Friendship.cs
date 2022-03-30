using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
