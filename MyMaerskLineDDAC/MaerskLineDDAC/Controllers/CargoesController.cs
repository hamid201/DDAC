using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MaerskLineDDAC.Models;

namespace MaerskLineDDAC.Controllers
{
    [Authorize]
    public class CargoesController : Controller
    {
        private MaerskLineEntities db = new MaerskLineEntities();

        // GET: Cargoes
        public ActionResult Index()
        {
            var cargos = db.Cargos.Include(c => c.Customer1);
            db.Ships.Where(item => item.ShippedDate == DateTime.Today);

            foreach (var ship in db.Ships.Where(item => item.ShippedDate == DateTime.Today))
            {
                foreach (var book in db.Books.Where(item => item.Ship == ship.ShipId))
                {
                    foreach (var cargo in db.Cargos.Where(item => item.CargoId == book.Cargo))
                    {
                        cargo.CargoStatus = "In Transit";
                    }
                }
            }
            db.SaveChanges();

            return View(cargos.ToList());
        }

        // GET: Cargoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = db.Cargos.Find(id);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            return View(cargo);
        }

        // GET: Cargoes/Details/5
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = db.Cargos.Find(id);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            return View(cargo);
        }

        // GET: Cargoes/Create
        public ActionResult Create()
        {
            ViewBag.Customer = new SelectList(db.Customers, "CustomerId", "CustomerName");
            return View();
        }

        // POST: Cargoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CargoName,CargoLength,CargoWidth,CargoHeight,CargoWeight,Customer")] Cargo cargo)
        {
            cargo.CargoStatus = "Customer Holding";
            if (ModelState.IsValid)
            {
                db.Cargos.Add(cargo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Customer = new SelectList(db.Customers, "CustomerId", "CustomerName", cargo.Customer);
            return View(cargo);
        }

        // GET: Cargoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = db.Cargos.Find(id);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            ViewBag.Customer = new SelectList(db.Customers, "CustomerId", "CustomerName", cargo.Customer);
            return View(cargo);
        }

        // POST: Cargoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CargoId,CargoName,CargoLength,CargoWidth,CargoHeight,CargoWeight,Customer")] Cargo cargo)
        {
            cargo.CargoStatus = "Customer Holding";
            if (ModelState.IsValid)
            {
                db.Entry(cargo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Customer = new SelectList(db.Customers, "CustomerId", "CustomerName", cargo.Customer);
            return View(cargo);
        }

        // GET: Cargoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = db.Cargos.Find(id);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            return View(cargo);
        }

        // POST: Cargoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cargo cargo = db.Cargos.Find(id);
            db.Cargos.Remove(cargo);
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
