using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Data;
using Core.DTO;
using System.Globalization;


namespace Web.Controllers
{
    public class HomeController : Controller
    {
        SalaryContext db = new SalaryContext();
        public ActionResult Index()
        {

            ViewBag.chucvu = db.chucvu.ToList();
            ViewBag.Ngach = db.ngach.ToList();
            return View();
        }

        public JsonResult TakeData(string ma, string ten, int page, string row_perpage)
        {

            var query = from b in db.nhanvien
                        where b.ma.Contains(ma) && b.ten.Contains(ten)
                        select b;
            List<nhanvien> list = query.ToList<nhanvien>();

            List<nhanvien2> list2 = new List<nhanvien2>();
            for (int i = 0; i < list.Count; i++)
            {
                var id_nhanvien = list[i].id;
                string ngach = "";
                string bac = "";
                var query2 = (from b in db.nhanvien_ngach
                              join c in db.ngach on b.id_ngach equals c.id
                              where b.id_nhanvien == id_nhanvien
                              select new
                              {
                                  ngach = c.ngach1,
                                  bac = b.bac,
                                  ngay = b.ngay
                              }).OrderByDescending(x => x.ngay);
                if (query2.Count() != 0)
                {
                    ngach = query2.FirstOrDefault().ngach;
                    bac = query2.FirstOrDefault().bac;
                }


                //
                string[] list_chucvu = new string[10];
                var MaxDate = (from d in db.nhanvien_chucvu
                               where d.id_nhanvien == id_nhanvien
                               select d.ngay).Max();

                var query3 = from b in db.nhanvien_chucvu
                             join c in db.chucvu on b.id_chucvu equals c.id
                             where b.id_nhanvien == id_nhanvien && b.ngay == MaxDate
                             select new
                             {
                                 ten = c.chuc_vu
                             };
                int v = 0;
                //
                if (query3.Count() != 0)
                {

                    foreach (var item in query3)
                    {
                        list_chucvu[v] = item.ten;
                        v++;
                    }
                }


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

            //

            nhanvien a = new nhanvien { ma = ma, ten = ten, gioi_tinh = gioitinh, dan_toc = dantoc, ngay_sinh = ngaysinh, ngay_vao_lam = ngayvaolam, dia_chi = diachi, so_cmnd = cmnd };
            db.nhanvien.Add(a);
            db.SaveChanges();
            nhanvien_ngach b = new nhanvien_ngach { id_ngach = ngach, id_nhanvien = a.id, bac = bac, ngay = ngayvaolam2 };
            db.nhanvien_ngach.Add(b);
            db.SaveChanges();
            if (chucvu == null)
            {
                nhanvien_chucvu c = new nhanvien_chucvu { id_chucvu = 0, id_nhanvien = a.id, ngay = ngayvaolam2, totnhat = "true" };
                db.nhanvien_chucvu.Add(c);
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
                db.nhanvien_chucvu.AddRange(list2);
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

            var query = from b in db.nhanvien
                        where b.id == id
                        select b;
            nhanvien a = query.FirstOrDefault<nhanvien>();
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

            var query0 = from b in db.nhanvien_chucvu
                         where b.ngay == ngaychucvu2
                         select b;
            if (query0 != null)
            {
                db.nhanvien_chucvu.RemoveRange(query0);
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
                db.nhanvien_chucvu.AddRange(list2);
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
            var query0 = from b in db.nhanvien_ngach
                         where b.ngay == ngay2
                         select b;
            if (query0 != null)
            {
                db.nhanvien_ngach.RemoveRange(query0);
                db.SaveChanges();
            }
            //
            if (ngach != 0)
            {
                nhanvien_ngach c = new nhanvien_ngach { id_ngach = ngach, id_nhanvien = id, bac = bac, ngay = ngay2 };
                db.nhanvien_ngach.Add(c);
                db.SaveChanges();
            }

            return Redirect("/Home");
        }
        public ActionResult Delete()
        {
            var id = Int32.Parse(Request.Form["id"]);

            //
            var query = from b in db.nhanvien
                        where b.id == id
                        select b;
            nhanvien a = query.FirstOrDefault<nhanvien>();
            db.nhanvien.Remove(a);
            //
            var query2 = from b in db.nhanvien_chucvu
                         where b.id_nhanvien == id
                         select b;
            List<nhanvien_chucvu> list = query2.ToList();
            db.nhanvien_chucvu.RemoveRange(list);
            //
            var query3 = from b in db.nhanvien_ngach
                         where b.id_nhanvien == id
                         select b;
            List<nhanvien_ngach> list2 = query3.ToList();
            db.nhanvien_ngach.RemoveRange(list2);

            db.SaveChanges();


            return Redirect("/Home");
        }

        public JsonResult CheckAccount(string manhanvien)
        {
            var query = from b in db.nhanvien
                        where b.ma == manhanvien
                        select b;

            var count = query.Count();

            var result = new { count = count };

            return Json(result, JsonRequestBehavior.AllowGet);
        } 
    }
}