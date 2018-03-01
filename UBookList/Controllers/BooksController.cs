using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UBookList.Models;

namespace UBookList.Controllers
{
    public class BooksController : Controller
    {

        private AppDbContext _db;

        public BooksController(AppDbContext db)
        {
            _db = db;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();

            base.Dispose(disposing);
        }


        public IActionResult Index()
        {
            return View(_db.Books.ToList());
        }


        /// <summary>
        /// get method for creates a book page
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (!ModelState.IsValid)
                return View(book);

            _db.Add(book);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}