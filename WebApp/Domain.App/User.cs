using Microsoft.AspNetCore.Identity;

namespace Domain.App
{
    public class User : IdentityUser<int>
    {
        public ICollection<PlayedGame>? PlayedGames { get; set; }

        // friends which User chooses
        public ICollection<Friendship>? PersonalFriendships { get; set; }

        // people who chooses User to be their friend
        public ICollection<Friendship>? FriendshipsWithUser { get; set; }
    }
}
