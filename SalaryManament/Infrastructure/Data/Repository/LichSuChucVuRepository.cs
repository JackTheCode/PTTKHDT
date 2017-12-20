using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO;
using Infrastructure.Data;

namespace Infrastructure.Data.Repository
{
    public class LichSuChucVuRepository
    {
        protected readonly SalaryContext context;
        public LichSuChucVuRepository(SalaryContext context)
        {
            this.context = context;
        }
        public IOrderedQueryable<lichsuchucvu> getQueryLichSuChucVu(int id_nhanvien)
        {
            var query3 = (from b in context.nhanvien_chucvu
                          join c in context.chucvu on b.id_chucvu equals c.id
                          where b.id_nhanvien == id_nhanvien
                          select new lichsuchucvu
                          {
                              ten = c.chuc_vu,
                              ngay = (DateTime)b.ngay
                          }).OrderBy(x => x.ngay);
            return query3;
        }
    }
}
