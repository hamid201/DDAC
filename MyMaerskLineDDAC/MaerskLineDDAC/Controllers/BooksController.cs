using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MaerskLineDDAC.Models;
using Microsoft.AspNet.Identity;

namespace MaerskLineDDAC.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private MaerskLineEntities db = new MaerskLineEntities();
        private static Book mBook;

        // GET: Books
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Cargo1).Include(b => b.Ship1).Include(b => b.Warehouse1);
            return View(books.ToList());
        }

        public ActionResult CargoManifest()
        {
            var books = db.Books.Include(b => b.Cargo1).Include(b => b.Ship1).Include(b => b.Warehouse1);
            return View(books.ToList());
        }


        public ActionResult Confirm(Book book)
        {
            mBook = book;
            book.Cargo1 = db.Cargos.Find(book.Cargo);
            book.Ship1 = db.Ships.Find(book.Ship);
            book.Warehouse1 = db.Warehouses.Find(book.Warehouse);
            if (book == null)
            {
                return HttpNotFound();
            }
            
            return View(book);
                
        }

        public ActionResult Save()
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(mBook);
                db.SaveChanges();

                Cargo cargo = db.Cargos.Find(mBook.Cargo);
                cargo.CargoStatus = "Move To WareHouse";
                db.Entry(cargo).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("ViewSchedule", "Ships");
            }

            ViewBag.Cargo = new SelectList(db.Cargos.Where(item => item.CargoStatus == "Customer Holding"), "CargoId", "CargoName");
            ViewBag.Ship = new SelectList(db.Ships, "ShipId", "ShipName", mBook.Ship);
            ViewBag.Warehouse = new SelectList(db.Warehouses, "WarehouseId", "WarehouseName", mBook.Warehouse); 

            return View();
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.Cargo = new SelectList(db.Cargos.Where(item => item.CargoStatus == "Customer Holding"), "CargoId", "CargoName");
            ViewBag.Ship = new SelectList(db.Ships, "ShipId", "ShipName");
            ViewBag.Warehouse = new SelectList(db.Warehouses, "WarehouseId", "WarehouseName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cargo,Ship,Warehouse")] Book book)
        {
            var userName = User.Identity.GetUserName();
            book.Agent = userName;
           
            /*
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();

                Cargo cargo = db.Cargos.Find(book.Cargo);
                cargo.CargoStatus = "Move To WareHouse";
                db.Entry(cargo).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }*/

            return RedirectToAction("Confirm", "Books", book);
    
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cargo = new SelectList(db.Cargos.Where(item => item.CargoStatus == "Customer Holding"), "CargoId", "CargoName");
            ViewBag.Ship = new SelectList(db.Ships, "ShipId", "ShipName", book.Ship);
            ViewBag.Warehouse = new SelectList(db.Warehouses, "WarehouseId", "WarehouseName", book.Warehouse);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId,Cargo,Ship,Warehouse")] Book book)
        {
            var userName = User.Identity.GetUserName();
            book.Agent = userName;

            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cargo = new SelectList(db.Cargos.Where(item => item.CargoStatus == "Customer Holding"), "CargoId", "CargoName");
            ViewBag.Ship = new SelectList(db.Ships, "ShipId", "ShipName", book.Ship);
            ViewBag.Warehouse = new SelectList(db.Warehouses, "WarehouseId", "WarehouseName", book.Warehouse);
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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
