using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apex_apigateway_ocelot.Dtos
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<Album> Albums { get; set; } = new List<Album>();
    }
}
