using Mapping;

namespace PublicApi.DTO.v1.Mapper
{
    public class PlayedGameMapper : IMapper<Domain.App.PlayedGame, PublicApi.DTO.v1.PlayedGame>
    {
        public PublicApi.DTO.v1.PlayedGame Map(Domain.App.PlayedGame playedGame)
        {
            return new PublicApi.DTO.v1.PlayedGame()
            {
                Id = playedGame.Id,
                Points = playedGame.Points,
                Place = playedGame.Place,
                UserId = playedGame.UserId,
                UserName = playedGame.User?.UserName ?? "",
                GameId = playedGame.GameId,
                GameName = playedGame.Game?.Name ?? "",
            };
        }

        public Domain.App.PlayedGame Map(PublicApi.DTO.v1.PlayedGame playedGame)
        {
            return new Domain.App.PlayedGame()
            {
                Id = playedGame.Id,
                Points = playedGame.Points,
                Place = playedGame.Place,
                UserId = playedGame.UserId,
                GameId = playedGame.GameId,
            };
        }
    }
}
