using System.ComponentModel.DataAnnotations;

namespace Domain.App
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; } = null!;

        [StringLength(50)]
        public string UserName { get; set; } = null!;

        public ICollection<PlayedGame>? PlayedGames { get; set; }

        // friends which User chooses
        public ICollection<Friendship>? PersonalFriendships { get; set; }

        // people who chooses User to be their friend
        public ICollection<Friendship>? FriendshipsWithUser { get; set; }
    }
}
