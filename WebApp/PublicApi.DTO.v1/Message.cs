using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicApi.DTO.v1
{
    public class Message
    {
        public Message()
        {
        }

        public Message(params string[] messages)
        {
            Messages = messages;
        }

        public IList<string> Messages { get; set; } = new List<string>();
    }
}
