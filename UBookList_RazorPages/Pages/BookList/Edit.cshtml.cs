using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UBookList_RazorPages.Models;

namespace UBookList_RazorPages.Pages.BookList
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _db;

        public EditModel(AppDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }

        [TempData]
        public string Message { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var book = await _db.Books.FindAsync(id);
            if(book==null)
            {
                Message = "No book found";
                return RedirectToPage("Index");
            }

            Book = book;

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var book = await _db.Books.FindAsync(Book.Id);
            book.ISBN = Book.ISBN;
            book.Title = Book.Title;
            book.Author = Book.Author;
            book.Availability = Book.Availability;
            book.Price = Book.Price;

            _db.Books.Update(book);
            await _db.SaveChangesAsync();

            Message = "Book updated successfully";
            return RedirectToPage("Index");
        }
    }
}