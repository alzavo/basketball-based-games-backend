using Mapping;

namespace PublicApi.DTO.v1.Mapper
{
    public class UserMapper : IMapper<Domain.App.User, PublicApi.DTO.v1.User>
    {
        public PublicApi.DTO.v1.User Map(Domain.App.User user)
        {
            return new PublicApi.DTO.v1.User()
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
