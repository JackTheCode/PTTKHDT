using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class nhanvien_ngach
    {
        public Int32 id { get; set; }
        public Int32 id_ngach { get; set; }
        public Int32 id_nhanvien { get; set; }
        public String bac { get; set; }
        public DateTime ngay { get; set; }
    }
}
