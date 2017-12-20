using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.DTO;

namespace Infrastructure.Data.Repository
{
    public class ChucVuRepository
    {
         protected readonly SalaryContext context;
         public ChucVuRepository(SalaryContext context)
        {
            this.context = context;
        }
        public List<chucvu> getChucVuList()
        {
            return context.chucvu.ToList();
        }

        public string[] getTenChucVuList(string ten,int id_nhanvien,DateTime MaxDate)
        {
            string[] list_chucvu = new string[10];
            int v = 0;
            var query3 = from b in context.nhanvien_chucvu
                         join c in context.chucvu on b.id_chucvu equals c.id
                         where b.id_nhanvien == id_nhanvien && b.ngay == MaxDate
                         select new
                         {
                             ten = c.chuc_vu
                         };
            if (query3.Count() != 0)
            {

                foreach (var item in query3)
                {
                    list_chucvu[v] = item.ten;
                    v++;
                }
            }
            return list_chucvu;
        }
    }
}
