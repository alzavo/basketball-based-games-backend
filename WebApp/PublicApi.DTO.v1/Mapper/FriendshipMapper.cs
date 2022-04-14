using Mapping;


namespace PublicApi.DTO.v1.Mapper
{
    public class FriendshipMapper : IMapper<Domain.App.Friendship, PublicApi.DTO.v1.Friendship>
    {
        public PublicApi.DTO.v1.Friendship Map(Domain.App.Friendship friendship) 
        {
            return new PublicApi.DTO.v1.Friendship()
            {
                Id = friendship.Id,
                UserId = friendship.UserId,
                UserName = friendship.User?.UserName ?? "",
                FriendId = friendship.FriendId,
                FriendName = friendship.Friend?.UserName ?? "",
            };
        }

        public Domain.App.Friendship Map(PublicApi.DTO.v1.Friendship friendship)
        {
            return new Domain.App.Friendship()
            {
                Id = friendship.Id,
                UserId = friendship.UserId,
                FriendId = friendship.FriendId,
            };
        }
    }
}
