using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jane.Models
{
    public class Settings
    {
        public required string Token { get; set; }
        public required ulong MarvinID { get; set; }
        public required ulong AnnounceChannel { get; set; }
    }
}