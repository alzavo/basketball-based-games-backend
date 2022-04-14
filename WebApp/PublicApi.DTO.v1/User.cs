namespace PublicApi.DTO.v1
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public int PlayedGamesCount { get; set; }
        public int PersonalFriendshipsCount { get; set; }
        public int FriendshipsWithUserCount { get; set; }
    }
}
