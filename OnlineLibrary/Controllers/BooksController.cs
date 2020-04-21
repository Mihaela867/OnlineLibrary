using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Data;
using OnlineLibrary.Models;
namespace OnlineLibrary.Controllers
{
    public class BooksController : Controller
    {
        private readonly OnlineLibraryContext _context;
        private readonly IWebHostEnvironment he;
        public BooksController(OnlineLibraryContext context,IWebHostEnvironment e)
        {
            he = e;
            _context = context;
        }
        // GET: Books
        public async Task<IActionResult> Index(string searchString)
        {
            var books = from m in _context.Book
                        select m;
            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString));
            }
            return View(await books.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            book.Reviews = await _context.Review.Where(var => var.BookId == book.Id).ToListAsync();
            decimal sum = 0;
            foreach (var c in book.Reviews)
                sum += c.Rating;
            if (book.Reviews.Count != 0)
                book.Rating = sum / book.Reviews.Count;

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author,ReleaseDate,AddedDate,PublishingHouse,Collection,Rating,Img")] Book book,IFormFile pic)
        {
            if (pic != null)
           {
                var fileName = Path.Combine(he.WebRootPath + "\\Images", Path.GetFileName(pic.FileName));
                pic.CopyTo(new FileStream(fileName, FileMode.Create));
                ViewData["fileLocation"] = "/" + Path.GetFileName(pic.FileName);
                book.Img = Path.GetFileName(pic.FileName);
            }
            _context.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,ReleaseDate,AddedDate,PublishingHouse,Collection,Rating")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }

        public async Task<IActionResult> AddReview(int? id)
        {
            var book = await _context.Book.FindAsync(id);
            
           
            Review x = new Review
            {
                AuthorName = book.Author,
                BookId = book.Id
            };
            book.Reviews.Add(x);
            var y= book.Reviews.Last();
            return View(book.Reviews.Last());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview([Bind("ReviewId,BookId,UserName,AuthorName,Content,Date,Rating")] Review review)
        {
            if (ModelState.IsValid)
            {
                _context.Review.Add(review);
                await _context.SaveChangesAsync();
                var book = _context.Book.FirstOrDefault(book => book.Id == review.BookId);
                return Redirect("~/Books/Details/" + review.BookId.ToString());
            }
            return View();
        }
    }
}
