using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicApi.DTO.v1.Mapper
{
    public class UserMapper
    {
        public static User Map(Domain.App.User user)
        {
            return new User()
            {
                Id = user.Id,
                UserName = user.UserName,
                PlayedGamesCount = user.PlayedGames?.Count ?? 0,
                PersonalFriendshipsCount = user.PersonalFriendships?.Count ?? 0,
                FriendshipsWithUserCount = user.FriendshipsWithUser?.Count ?? 0,
            };
        }

        public static Domain.App.User Map(UserUpdate user)
        {
            return new Domain.App.User()
            {
                Id = user.Id,
                UserName = user.UserName,
            };
        }
    }
}
