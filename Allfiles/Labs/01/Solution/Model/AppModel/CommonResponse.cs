using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI.Model
{
    public class AppCommonResponse 
    {
        public DateTime? DataAsOF { get; set; }
        public string PortGroup { get; set; }
        public DateTime? CreatedDate { get; set; }

    }

    public class AppCommonRequst
    {
        public DateTime? DataAsOF { get; set; }
        public string PortGroup { get; set; }


    }

  

}
