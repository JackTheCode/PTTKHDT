using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO;
using Infrastructure.Data;

namespace Infrastructure.Data.Repository
{
    public class NhanVienNgachRepository
    {
         protected readonly SalaryContext context;
         public NhanVienNgachRepository(SalaryContext context)
        {
            this.context = context;
        }

        public List<nhanvien_ngach> getListNhanVienNgach(int id)
        {
            var query3 = from b in context.nhanvien_ngach
                         where b.id_nhanvien == id
                         select b;
            return query3.ToList();
        }
        public void AddNhanVienNgach(nhanvien_ngach b)
        {
            context.nhanvien_ngach.Add(b);
        }
        public void removeNhanVienNgachRange(IEnumerable<nhanvien_ngach> entities)
        {
            context.nhanvien_ngach.RemoveRange(entities);
        }

        public IEnumerable<nhanvien_ngach> getQueryNgay(DateTime ngay)
        {
            var query0 = from b in context.nhanvien_ngach
                         where b.ngay == ngay
                         select b;
            return query0;
        }

    }
}
