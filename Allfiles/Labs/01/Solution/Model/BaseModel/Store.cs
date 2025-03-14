using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Model
{
    public class Store : CommonEntity
    {
        [Key]
        public int ID { get; set; }
        public string StoreName { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? OpeningDate { get; set; }
    }
}
