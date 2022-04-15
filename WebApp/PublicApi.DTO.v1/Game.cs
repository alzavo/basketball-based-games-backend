namespace PublicApi.DTO.v1
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Language { get; set; } = null!;
        public int PlayedGamesCount { get; set; }
    }
}
