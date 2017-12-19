using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
    public class lichsuchucvu
    {
        [Key]
        public String ten { get; set; }
        public DateTime ngay { get; set; }
    }
}
