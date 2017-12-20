using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Data;
using Core.DTO;
using Infrastructure.Data.Repository;

namespace Web.Controllers
{
    public class LichSuCongTacController : Controller
    {
        //
        // GET: /LichSuCongTac/

        SalaryContext db = new SalaryContext();
        public ActionResult Index(string id)
        {

            try
            {
                int id_nhanvien = Int32.Parse(id);
                //
                nhanvien a = new NhanVienRepository(db).getNhanVien(id_nhanvien);
                ViewBag.ma_nhanvien = a.ma;
                ViewBag.ten_nhanvien = a.ten;
                ViewBag.ngayvaolam_nhanvien = a.ngay_vao_lam;
                //
 
                ViewBag.ngachs = new LichSuNgachRepository(db).getLichSuNgachQuery(id_nhanvien);
                //

                ViewBag.chucvus = new LichSuChucVuRepository(db).getQueryLichSuChucVu(id_nhanvien);

                return View();

            }
            catch (Exception ex)
            {
                return Redirect("/Home");
            }



        }
    }
}