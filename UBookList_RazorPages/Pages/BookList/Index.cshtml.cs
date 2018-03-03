using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UBookList_RazorPages.Models;

namespace UBookList_RazorPages.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;

        public IndexModel(AppDbContext db)
        {
            _db = db;
        }

        [TempData]
        public string Message { get; set; }

        public IEnumerable<Book> Books { get; set; }

        public async Task OnGet()
        {
            Books = await _db.Books.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var book = await _db.Books.FindAsync(id);
            if(book==null)
            {
                Message = "Book now found in booklist";
                return RedirectToPage();
            }

            _db.Books.Remove(book);
            await _db.SaveChangesAsync();

            Message = "Book deleted successfully";
            return RedirectToPage();
        }
    }
}