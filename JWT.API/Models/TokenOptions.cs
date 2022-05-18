using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.API.Models
{
    public class TokenOptions
    {
        public string Isssuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public int ExpireTime { get; set; }
    }
}
