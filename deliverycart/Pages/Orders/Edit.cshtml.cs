#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using deliverycart.Data;
using deliverycart.Models;

namespace deliverycart.Pages_Orders
{
    public class EditModel : PageModel
    {
        private readonly deliverycart.Data.Assignment3XTestsContext _context;

        public EditModel(deliverycart.Data.Assignment3XTestsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Order
                .Include(o => o.Customer)
                .Include(o => o.Item).FirstOrDefaultAsync(m => m.OrderID == id);

            if (Order == null)
            {
                return NotFound();
            }
           ViewData["CustomerID"] = new SelectList(_context.Set<Customer>(), "CustomerID", "CustomerID");
           ViewData["ItemID"] = new SelectList(_context.Set<Item>(), "ItemID", "ItemID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(Order.OrderID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderID == id);
        }
    }
}
