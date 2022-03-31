using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicApi.DTO.v1
{
    public class JwtResponse
    {
        public string Token { get; set; } = null!;
        public string UserName { get; set; } = null!;
    }
}
