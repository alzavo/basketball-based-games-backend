using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicApi.DTO.v1.Mapper
{
    public class FriendshipMapper
    {
        public static Friendship Map(Domain.App.Friendship friendship) 
        {
            return new Friendship()
            {
                Id = friendship.Id,
                UserId = friendship.UserId,
                UserName = friendship.User.UserName,
                FriendId = friendship.FriendId,
                FriendName = friendship.Friend.UserName,
            };
        }

        public static Domain.App.Friendship Map(FriendshipUpdate friendship)
        {
            return new Domain.App.Friendship()
            {
                Id = friendship.Id,
                UserId = friendship.UserId,
                FriendId = friendship.FriendId,
            };
        }

        public static Domain.App.Friendship Map(FriendshipCreate friendship)
        {
            return new Domain.App.Friendship()
            {
                UserId = friendship.UserId,
                FriendId = friendship.FriendId,
            };
        }
    }
}
