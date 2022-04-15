﻿namespace PublicApi.DTO.v1
{
    public class Friendship
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public int FriendId { get; set; }
        public string FriendName { get; set; } = null!;
    }
}
