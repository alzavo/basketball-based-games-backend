using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicApi.DTO.v1
{
    public class Friendship
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;

        public int FriendId { get; set; }
        public string FriendName { get; set; } = null!;
    }

    public class FriendshipUpdate
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int FriendId { get; set; }
    }

    public class FriendshipCreate
    {
        public int UserId { get; set; }

        public int FriendId { get; set; }
    }
}
