using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Model
{
    public class Appsetting
    {
        public string ConnectionStrings { get; set; }
        public string Secret { get; set; }
        public string Environment { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
