using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Options.Shikimory
{
    public class ShikimoryClientOptions
    {
        public const String Title = "ShikimoryClientOptions";
        public String ClientName { get; set; }
        public String ClientId { get; set; }
        public String ClientSecret { get; set; }
    }
}
