using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Data;
using Core.DTO;
using System.Globalization;
using Infrastructure.Data.Repository;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        SalaryContext db = new SalaryContext();
        public ActionResult Index()
        {

            ViewBag.chucvu = new ChucVuRepository(db).getChucVuList();
            ViewBag.Ngach = new NgachRepository(db).getNgachList();
            return View();
        }

        public JsonResult TakeData(string ma, string ten, int page, string row_perpage)
        {

            List<nhanvien> list = new NhanVienRepository(db).getNhanVienList(ma, ten);
            List<nhanvien2> list2 = new List<nhanvien2>();
            for (int i = 0; i < list.Count; i++)
            {
                var id_nhanvien = list[i].id;
                var MaxDate = new NhanVienRepository(db).getMaxDate(id_nhanvien);
                var list_chucvu = new ChucVuRepository(db).getTenChucVuList(ten, id_nhanvien, MaxDate);
                var ngach = new NhanVienRepository(db).getNhanVienNgach(id_nhanvien);
                var bac = new NhanVienRepository(db).getNhanVienBac(id_nhanvien);
                nhanvien2 h = new nhanvien2(id_nhanvien, list[i].ma, list[i].ten, list[i].gioi_tinh, list[i].ngay_sinh, list[i].dan_toc, list[i].ngay_vao_lam, list[i].dia_chi, list[i].so_cmnd, list_chucvu, ngach, bac);
                list2.Add(h);
            }

            int rowperpage = Int32.Parse(row_perpage);

            int offset = (page - 1) * rowperpage;

            var data = list2.Skip(offset).Take(rowperpage);

            var count = list2.Count();

            var result = new { data = data, count = count };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            var ma = Request.Form["ma"];
            var ten = Request.Form["ten"]; ;
            var gioitinh = Request.Form["gioitinh"];
            var dantoc = Request.Form["dantoc"];
            var ngaysinh = Request.Form["ngaysinh"]; ;
            var ngayvaolam = Request.Form["ngayvaolam"];
            var ngayvaolam2 = DateTime.ParseExact(ngayvaolam, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var diachi = Request.Form["diachi"];
            var cmnd = Request.Form["cmnd"]; ;
            string[] chucvu = Request.Form.GetValues("chucvu");
            var ngach = Int32.Parse(Request.Form["ngach"]);
            var bac = Request.Form["bac"];

            nhanvien a = new nhanvien { ma = ma, ten = ten, gioi_tinh = gioitinh, dan_toc = dantoc, ngay_sinh = ngaysinh, ngay_vao_lam = ngayvaolam, dia_chi = diachi, so_cmnd = cmnd };
            new NhanVienRepository(db).addNhanVien(a);
            db.SaveChanges();
            nhanvien_ngach b = new nhanvien_ngach { id_ngach = ngach, id_nhanvien = a.id, bac = bac, ngay = ngayvaolam2 };
            new NhanVienNgachRepository(db).AddNhanVienNgach(b);
            db.SaveChanges();
            if (chucvu == null)
            {
                nhanvien_chucvu c = new nhanvien_chucvu { id_chucvu = 0, id_nhanvien = a.id, ngay = ngayvaolam2, totnhat = "true" };
                new NhanVienChucVuRepository(db).AddNhanVienChucVu(c);
                db.SaveChanges();
            }
            else
            {
                List<nhanvien_chucvu> list2 = new List<nhanvien_chucvu>();
                for (int i = 0; i < chucvu.Length; i++)
                {
                    nhanvien_chucvu c = new nhanvien_chucvu { id_chucvu = Int16.Parse(chucvu[i]), id_nhanvien = a.id, ngay = ngayvaolam2, totnhat = "false" };
                    list2.Add(c);
                }
                int k = 0;
                var min = 100;
                for (int i = 0; i < list2.Count; i++)
                {
                    if (list2[i].id_chucvu < min)
                    {
                        k = i;
                        min = list2[i].id_chucvu;
                    }
                }
                list2[k].totnhat = "true";
                new NhanVienChucVuRepository(db).AddRangeNhanVienChucVu(list2);
                db.SaveChanges();
            }
            return Redirect("/Home");
        }



        public ActionResult UpdateCaNhan()
        {
            var id = Int32.Parse(Request.Form["id"]);
            var ma = Request.Form["ma"];
            var ten = Request.Form["ten"]; ;
            var gioitinh = Request.Form["gioitinh"];
            var dantoc = Request.Form["dantoc"];
            var ngaysinh = Request.Form["ngaysinh"]; ;
            var ngayvaolam = Request.Form["ngayvaolam"];
            var diachi = Request.Form["diachi"];
            var cmnd = Request.Form["cmnd"]; ;
            //

            nhanvien a = new NhanVienRepository(db).getNhanVien(id);
            a.ma = ma;
            a.ten = ten;
            a.gioi_tinh = gioitinh;
            a.dan_toc = dantoc;
            a.ngay_sinh = ngaysinh;
            a.ngay_vao_lam = ngayvaolam;
            a.dia_chi = diachi;
            a.so_cmnd = cmnd;
            db.SaveChanges();


            return Redirect("/Home");
        }

        public ActionResult UpdateChucVu()
        {
            var id = Int32.Parse(Request.Form["id"]);
            string[] chucvu = Request.Form.GetValues("chucvu");
            var ngaychucvu = Request.Form["ngaychucvu"];
            var ngaychucvu2 = DateTime.ParseExact(ngaychucvu, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var query0 = new NhanVienChucVuRepository(db).getQueryNgayChucVu(ngaychucvu2);
            if (query0 != null)
            {
                new NhanVienChucVuRepository(db).removeNhanVienChucVuRange(query0);
                db.SaveChanges();
            }

            if (chucvu != null)
            {
                List<nhanvien_chucvu> list2 = new List<nhanvien_chucvu>();
                for (int i = 0; i < chucvu.Length; i++)
                {
                    if (Int16.Parse(chucvu[i]) != 0)
                    {
                        nhanvien_chucvu c = new nhanvien_chucvu { id_chucvu = Int16.Parse(chucvu[i]), id_nhanvien = id, ngay = ngaychucvu2, totnhat = "false" };
                        list2.Add(c);
                    }

                }
                int k = 0;
                var min = 100;
                for (int i = 0; i < list2.Count; i++)
                {
                    if (list2[i].id_chucvu < min)
                    {
                        k = i;
                        min = list2[i].id_chucvu;
                    }
                }
                list2[k].totnhat = "true";
                new NhanVienChucVuRepository(db).AddRangeNhanVienChucVu(list2);
                db.SaveChanges();
            }


            return Redirect("/Home");
        }

        public ActionResult UpdateNgach()
        {
            var id = Int32.Parse(Request.Form["id"]);
            var ngach = Int32.Parse(Request.Form["ngach"]);
            var bac = Request.Form["bac"];
            var ngay = Request.Form["ngay"];
            var ngay2 = DateTime.ParseExact(ngay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //
            var query0 = new NhanVienNgachRepository(db).getQueryNgay(ngay2);
            if (query0 != null)
            {
                new NhanVienNgachRepository(db).removeNhanVienNgachRange(query0);
                db.SaveChanges();
            }
            //
            if (ngach != 0)
            {
                nhanvien_ngach c = new nhanvien_ngach { id_ngach = ngach, id_nhanvien = id, bac = bac, ngay = ngay2 };
                new NhanVienNgachRepository(db).AddNhanVienNgach(c);
                db.SaveChanges();
            }

            return Redirect("/Home");
        }
        public ActionResult Delete()
        {
            var id = Int32.Parse(Request.Form["id"]);

            //

            nhanvien a = new NhanVienRepository(db).getNhanVien(id);
            new NhanVienRepository(db).removeNhanVien(a);
            //

            List<nhanvien_chucvu> list = new NhanVienChucVuRepository(db).getListNhanVienChucVu(id);
            new NhanVienChucVuRepository(db).removeNhanVienChucVuRange(list);
            //

            List<nhanvien_ngach> list2 = new NhanVienNgachRepository(db).getListNhanVienNgach(id);
            new NhanVienNgachRepository(db).removeNhanVienNgachRange(list2);

            db.SaveChanges();


            return Redirect("/Home");
        }

        public JsonResult CheckAccount(string manhanvien)
        {
            var query = new NhanVienRepository(db).getQueryMaNhanVien(manhanvien);

            var count = query.Count();

            var result = new { count = count };

            return Json(result, JsonRequestBehavior.AllowGet);
        } 
    }
}