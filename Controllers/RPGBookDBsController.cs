using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RPGBookDBsController : Controller
    {
        private RPGBookDBContext db = new RPGBookDBContext();

        // GET: RPGBookDBs
        public ActionResult Index(string sortOrder, string fGame, string fBookType, string fSystem, string fGenre)//, bool fHasPlayed)
        {
            // FILTERING
            ViewBag.Game = (from b in db.RPGBooks
                            where b.Game == fGame || fGame == null || fGame == ""
                            where b.BookType == fBookType || fBookType == null || fBookType == ""
                            where b.System == fSystem || fSystem == null || fSystem == ""
                            where b.Genre == fGenre || fGenre == null || fGenre == ""
                            //where b.HasPlayed == fHasPlayed || fHasPlayed == false
                            select b.Game).Distinct();
            ViewBag.BookType = (from b in db.RPGBooks
                                where b.Game == fGame || fGame == null || fGame == ""
                                where b.BookType == fBookType || fBookType == null || fBookType == ""
                                where b.System == fSystem || fSystem == null || fSystem == ""
                                where b.Genre == fGenre || fGenre == null || fGenre == ""
                                //where b.HasPlayed == fHasPlayed || fHasPlayed == false
                                select b.BookType).Distinct();
            ViewBag.System = (from b in db.RPGBooks
                              where b.Game == fGame || fGame == null || fGame == ""
                              where b.BookType == fBookType || fBookType == null || fBookType == ""
                              where b.System == fSystem || fSystem == null || fSystem == ""
                              where b.Genre == fGenre || fGenre == null || fGenre == ""
                              //where b.HasPlayed == fHasPlayed || fHasPlayed == false
                              select b.System).Distinct();
            ViewBag.Genre = (from b in db.RPGBooks
                             where b.Game == fGame || fGame == null || fGame == ""
                             where b.BookType == fBookType || fBookType == null || fBookType == ""
                             where b.System == fSystem || fSystem == null || fSystem == ""
                             where b.Genre == fGenre || fGenre == null || fGenre == ""
                             //where b.HasPlayed == fHasPlayed || fHasPlayed == false
                             select b.Genre).Distinct();
            ViewBag.HasPlayed = (from b in db.RPGBooks select b.HasPlayed).Distinct();

            // SORTING
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.GameSortParam = sortOrder == "game" ? "game_desc" : "game";
            ViewBag.TypeSortParam = sortOrder == "type" ? "type_desc" : "type";
            ViewBag.SystemSortParam = sortOrder == "system" ? "system_desc" : "system";
            ViewBag.GenreSortParam = sortOrder == "genre" ? "genre_desc" : "genre";

            var books = from b in db.RPGBooks
                        where b.Game == fGame || fGame == null || fGame == ""
                        where b.BookType == fBookType || fBookType == null || fBookType == ""
                        where b.System == fSystem || fSystem == null || fSystem == ""
                        where b.Genre == fGenre || fGenre == null || fGenre == ""
                        //where b.HasPlayed == fHasPlayed || fHasPlayed == false
                        select b;

            switch (sortOrder)
            {
                case "name_desc":
                    books = books.OrderByDescending(b => b.Name);
                    break;
                case "game":
                    books = books.OrderBy(b => b.Game);
                    break;
                case "game_desc":
                    books = books.OrderByDescending(b => b.Game);
                    break;
                case "type":
                    books = books.OrderBy(b => b.BookType);
                    break;
                case "type_desc":
                    books = books.OrderByDescending(b => b.BookType);
                    break;
                case "system":
                    books = books.OrderBy(b => b.System);
                    break;
                case "system_desc":
                    books = books.OrderByDescending(b => b.System);
                    break;
                case "genre":
                    books = books.OrderBy(b => b.Genre);
                    break;
                case "genre_desc":
                    books = books.OrderByDescending(b => b.Genre);
                    break;
                default:
                    books = books.OrderBy(b => b.Name);
                    break;
            }

            // ultimate return
            return View(books.ToList());
        }

        // GET: RPGBookDBs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RPGBookDB rPGBookDB = db.RPGBooks.Find(id);
            if (rPGBookDB == null)
            {
                return HttpNotFound();
            }
            return View(rPGBookDB);
        }

        // GET: RPGBookDBs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RPGBookDBs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Game,BookType,System,ISBN,Genre,HasPlayed,Rating")] RPGBookDB rPGBookDB)
        {
            if (ModelState.IsValid)
            {
                db.RPGBooks.Add(rPGBookDB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rPGBookDB);
        }

        // GET: RPGBookDBs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RPGBookDB rPGBookDB = db.RPGBooks.Find(id);
            if (rPGBookDB == null)
            {
                return HttpNotFound();
            }
            return View(rPGBookDB);
        }

        // POST: RPGBookDBs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Game,BookType,System,ISBN,Genre,HasPlayed,Rating")] RPGBookDB rPGBookDB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rPGBookDB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rPGBookDB);
        }

        // GET: RPGBookDBs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RPGBookDB rPGBookDB = db.RPGBooks.Find(id);
            if (rPGBookDB == null)
            {
                return HttpNotFound();
            }
            return View(rPGBookDB);
        }

        // POST: RPGBookDBs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RPGBookDB rPGBookDB = db.RPGBooks.Find(id);
            db.RPGBooks.Remove(rPGBookDB);
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
