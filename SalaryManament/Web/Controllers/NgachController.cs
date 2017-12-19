using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.DTO;
using Infrastructure.Data;

namespace SalaryManament.Controllers
{
    public class NgachController : Controller
    {
        private SalaryContext db = new SalaryContext();

        // GET: /Ngach/
        public ActionResult Index()
        {
            return View(db.ngach.ToList());
        }

        // GET: /Ngach/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ngach ngach = db.ngach.Find(id);
            if (ngach == null)
            {
                return HttpNotFound();
            }
            return View(ngach);
        }

        // GET: /Ngach/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Ngach/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,maso,ngach1,nien_hang,C_1,C_2,C_3,C_4,C_5,C_6,C_7,C_8")] ngach ngach)
        {
            if (ModelState.IsValid)
            {
                db.ngach.Add(ngach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ngach);
        }

        // GET: /Ngach/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ngach ngach = db.ngach.Find(id);
            if (ngach == null)
            {
                return HttpNotFound();
            }
            return View(ngach);
        }

        // POST: /Ngach/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,maso,ngach1,nien_hang,C_1,C_2,C_3,C_4,C_5,C_6,C_7,C_8")] ngach ngach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ngach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ngach);
        }

        // GET: /Ngach/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ngach ngach = db.ngach.Find(id);
            if (ngach == null)
            {
                return HttpNotFound();
            }
            return View(ngach);
        }

        // POST: /Ngach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ngach ngach = db.ngach.Find(id);
            db.ngach.Remove(ngach);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
