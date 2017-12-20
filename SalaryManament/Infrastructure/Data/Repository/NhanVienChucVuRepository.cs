using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO;
using Infrastructure.Data;

namespace Infrastructure.Data.Repository
{
    public class NhanVienChucVuRepository
    {
        protected readonly SalaryContext context;
        public NhanVienChucVuRepository(SalaryContext context)
        {
            this.context = context;
        }


        public List<nhanvien_chucvu> getListNhanVienChucVu(int id)
        {
            var query2 = from b in context.nhanvien_chucvu
                         where b.id_nhanvien == id
                         select b;
            return query2.ToList();
        }
        public void AddNhanVienChucVu(nhanvien_chucvu c)
        {
            context.nhanvien_chucvu.Add(c);
        }

        public void AddRangeNhanVienChucVu(IEnumerable<nhanvien_chucvu> entities)
        {
            context.nhanvien_chucvu.AddRange(entities);
        }

        public void removeNhanVienChucVuRange(IEnumerable<nhanvien_chucvu> entities)
        {
            context.nhanvien_chucvu.RemoveRange(entities);
        }

        public IEnumerable<nhanvien_chucvu> getQueryNgayChucVu(DateTime ngaychucvu)
        {
            var query0 = from b in context.nhanvien_chucvu
                         where b.ngay == ngaychucvu
                         select b;
            return query0;
        }

    }
}
