using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.DTO;
using System.Data.Entity;

namespace Infrastructure.Data.Repository
{
    public class NhanVienRepository
    {
        protected readonly SalaryContext context;
        public NhanVienRepository(SalaryContext context)
        {
            this.context = context;
        }

        public List<nhanvien> getNhanVienList(string ma, string ten)
        {
            var query = from b in context.nhanvien
                        where b.ma.Contains(ma) && b.ten.Contains(ten)
                        select b;
            return query.ToList<nhanvien>();
        }

        public nhanvien getNhanVien(int id)
        {
            var query = from b in context.nhanvien
                        where b.id == id
                        select b;
            return query.FirstOrDefault<nhanvien>();
        }

        public string getNhanVienNgach(int id_nhanvien)
        {
            string ngach_result = "";
            var query2 = (from b in context.nhanvien_ngach
                          join c in context.ngach on b.id_ngach equals c.id
                          where b.id_nhanvien == id_nhanvien
                          select new
                          {
                              ngach = c.ngach1,
                              bac = b.bac,
                              ngay = b.ngay
                          }).OrderByDescending(x => x.ngay);
            if (query2.Count() != 0)
            {
                ngach_result = query2.FirstOrDefault().ngach;
            }
            return ngach_result;
        }

        public string getNhanVienBac(int id_nhanvien)
        {
            string bac_result = "";
            var query2 = (from b in context.nhanvien_ngach
                          join c in context.ngach on b.id_ngach equals c.id
                          where b.id_nhanvien == id_nhanvien
                          select new
                          {
                              ngach = c.ngach1,
                              bac = b.bac,
                              ngay = b.ngay
                          }).OrderByDescending(x => x.ngay);
            if (query2.Count() != 0)
            {
                bac_result = query2.FirstOrDefault().bac;
            }
            return bac_result;
        }

        public DateTime getMaxDate(int id_nhanvien)
        {
            var MaxDate = (from d in context.nhanvien_chucvu
                           where d.id_nhanvien == id_nhanvien
                           select d.ngay).Max();
            return MaxDate??DateTime.Now;
        }

        public void addNhanVien(nhanvien a)
        {
            context.nhanvien.Add(a);
        }

        public void removeNhanVien(nhanvien a)
        {
            context.nhanvien.Remove(a);
        }

        public IEnumerable<nhanvien> getQueryMaNhanVien(string ma)
        {
            var query = from b in context.nhanvien
                        where b.ma == ma
                        select b;
            return query;
        }

    }
}
