using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class nhanvien_chucvu
    {
        public Int32 id { get; set; }
        public Int32 id_chucvu { get; set; }
        public Int32 id_nhanvien { get; set; }
        public String totnhat { get; set; }
        public DateTime ngay { get; set; }


    }
}
