using Mapping;

namespace PublicApi.DTO.v1.Mapper
{
    public class GameMapper : IMapper<Domain.App.Game, PublicApi.DTO.v1.Game>
    {
        public PublicApi.DTO.v1.Game Map(Domain.App.Game game)
        {
            return new Game()
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                Language = game.Language,
                PlayedGamesCount = game.PlayedGames?.Count ?? 0,
            };
        }

        public Domain.App.Game Map(PublicApi.DTO.v1.Game game)
        {
            return new Domain.App.Game()
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                Language = game.Language,
            };
        }
    }
}
