using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FindABook.Models;
using Microsoft.AspNetCore.Authorization;
using FindABook.Models.UtillityModels;

namespace FindABook.API_s
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookSubscriptionsApi : ControllerBase
    {
        private readonly BooksDbContext _context;

        public BookSubscriptionsApi(BooksDbContext context)
        {
            _context = context;
        }

        // GET: api/BookSubscriptionsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookSubscription>>> GetBookSubscriptions()
        {
            string userId = SecurityHelper.GetUserId(HttpContext);
            
            return await _context.BookSubscriptions.Include(b=>b.Book).Where(a=>a.UserId==userId).ToListAsync();
           
        }

        // GET: api/BookSubscriptionsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookSubscription>> GetBookSubscription(int id)
        {
            var bookSubscription = await _context.BookSubscriptions.FindAsync(id);

            if (bookSubscription == null)
            {
                return NotFound();
            }

            return bookSubscription;
        }

        // PUT: api/BookSubscriptionsApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookSubscription(int id, BookSubscription bookSubscription)
        {
            if (id != bookSubscription.SubscriptionId)
            {
                return BadRequest();
            }

            _context.Entry(bookSubscription).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookSubscriptionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BookSubscriptionsApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BookSubscription>> PostBookSubscription(BookSubscription bookSubscription)
        {
            bookSubscription.UserId=SecurityHelper.GetUserId(HttpContext);
            bookSubscription.LastModified = DateTime.Now;
            bookSubscription.Subscribed = true;
            _context.BookSubscriptions.Add(bookSubscription);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookSubscription", new { id = bookSubscription.SubscriptionId }, bookSubscription);
        }

        // DELETE: api/BookSubscriptionsApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookSubscription>> DeleteBookSubscription(int id)
        {
            var bookSubscription = await _context.BookSubscriptions.FindAsync(id);
            if (bookSubscription == null)
            {
                return NotFound();
            }

            _context.BookSubscriptions.Remove(bookSubscription);
            await _context.SaveChangesAsync();

            return bookSubscription;
        }

        private bool BookSubscriptionExists(int id)
        {
            return _context.BookSubscriptions.Any(e => e.SubscriptionId == id);
        }
    }
}
