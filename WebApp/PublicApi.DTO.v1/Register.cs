using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicApi.DTO.v1
{
    public class Register
    {
        public string Password { get; set; } = null!;
        public string UserName { get; set; } = null!;
    }
}
