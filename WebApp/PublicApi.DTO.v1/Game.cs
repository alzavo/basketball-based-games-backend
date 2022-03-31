using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class Game
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = null!;

        [MaxLength]
        public string Description { get; set; } = null!;

        [StringLength(30)]
        public string Language { get; set; } = null!;
        public int PlayedGamesCount { get; set; }
    }

    public class GameUpdate
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = null!;

        [MaxLength]
        public string Description { get; set; } = null!;

        [StringLength(30)]
        public string Language { get; set; } = null!;
    }

    public class GameCreate
    {
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [MaxLength]
        public string Description { get; set; } = null!;

        [StringLength(30)]
        public string Language { get; set; } = null!;
    }
}
