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


    }
}