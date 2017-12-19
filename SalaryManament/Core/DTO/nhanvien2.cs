using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class nhanvien2
    {
         public int id { get; set; }
        public string ma { get; set; }
        public string ten { get; set; }
        public string gioi_tinh { get; set; }
        public string ngay_sinh { get; set; }
        public string dan_toc { get; set; }
        public string ngay_vao_lam { get; set; }
        public string dia_chi { get; set; }
        public string so_cmnd { get; set; }
        public string[] chucvu { get; set; }
        public string ngach { get; set; }
        public string bac { get; set; }


         public nhanvien2(int id, string ma, string ten, string gioi_tinh, string ngay_sinh, string dan_toc, string ngay_vao_lam, string dia_chi, string so_cmnd, string[] chucvu, string ngach, string bac)
        {
            this.id = id;
            this.ma = ma;
            this.ten = ten;
            this.gioi_tinh = gioi_tinh;
            this.ngay_sinh = ngay_sinh;
            this.dan_toc = dan_toc;
            this.ngay_vao_lam = ngay_vao_lam;
            this.dia_chi = dia_chi;
            this.so_cmnd = so_cmnd;
            this.chucvu = chucvu;
            this.ngach = ngach;
            this.bac = bac;

        }
    }
}
