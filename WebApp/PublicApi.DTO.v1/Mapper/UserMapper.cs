using Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicApi.DTO.v1.Mapper
{
    public class UserMapper : IMapper<Domain.App.User, PublicApi.DTO.v1.User>
    {
        public User Map(Domain.App.User user)
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

        public Domain.App.User Map(PublicApi.DTO.v1.User user)
        {
            return new Domain.App.User()
            {
                Id = user.Id,
                UserName = user.UserName,
            };
        }
    }
}
