using System;

namespace WebAPI.Model
{
    public class CommonEntity
    {
        public int CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool ActiveFlag { get; set; }
    }
}
