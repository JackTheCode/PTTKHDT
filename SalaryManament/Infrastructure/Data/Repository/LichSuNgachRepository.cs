using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO;
using Infrastructure.Data;

namespace Infrastructure.Data.Repository
{
    public class LichSuNgachRepository
    {
        protected readonly SalaryContext context;
        public LichSuNgachRepository(SalaryContext context)
        {
            this.context = context;
        }

        public IOrderedQueryable<lichsungach> getLichSuNgachQuery(int id_nhanvien)
        {
            var query2 = (from b in context.nhanvien_ngach
                          join c in context.ngach on b.id_ngach equals c.id
                          where b.id_nhanvien == id_nhanvien
                          select new lichsungach()
                          {
                              ten = c.ngach1,
                              bac = b.bac,
                              ngay = (DateTime)b.ngay
                          }).OrderBy(x => x.ngay);
            return query2;
        }
    }
}
