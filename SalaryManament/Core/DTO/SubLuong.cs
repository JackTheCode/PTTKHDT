using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
    public class SubLuong
    {
        [Key]
        public float heso { get; set; }
        public float phucap { get; set; }
        public Int32 count { get; set; }

        public SubLuong(float heso, float phucap, int count)
        {
            this.heso = heso;
            this.phucap = phucap;
            this.count = count;
        }
    }
}
