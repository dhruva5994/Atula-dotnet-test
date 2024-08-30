using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

public class ProductController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> ProductList()
    {
        if(ViewBag.Email != "")
        { 
            var products = await _context.Products.Include(p => p.Categories).ToListAsync();
            return View("ProductList", products);
        }
        return RedirectToAction("Account", "Login");
    }

    public IActionResult AddProduct()
    {
        ViewBag.Categories = new MultiSelectList(_context.Categories, "Id", "Name");
        return View("AddProduct");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddProduct(Product product, int[] Categories)
    {
        if (ModelState.IsValid)
        {
            product.Categories =await _context.Categories.Where(c => Categories.Contains(c.Id)).ToListAsync();
            _context.Add(product);
           
            await _context.SaveChangesAsync();
            TempData["Success"] = "Product created successfully!";
            return RedirectToAction(nameof(ProductList));
        }
        return View("AddProduct");
    }

    public async Task<IActionResult> UpdateProduct(int id)
    {
        var product = await _context.Products.Include(p => p.Categories).FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
       // var selectedCategoryIds = product.Categories.Select(c => c.Id).ToArray();

        ViewBag.Categories = new MultiSelectList(_context.Categories, "Id", "Name");
        return View("UpdateProduct",product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateProduct(int id, Product product, int[] Categories)
    {
        if (id != product.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var existingProduct = await _context.Products.Include(p => p.Categories).FirstOrDefaultAsync(p => p.Id == id);
                existingProduct.Name = product.Name;
                existingProduct.Sku = product.Sku;
                existingProduct.Categories =await _context.Categories.Where(c => Categories.Contains(c.Id)).ToListAsync();
                _context.Update(existingProduct);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Product updated successfully!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(ProductList));
        }
        return View("ProductList");
    }

    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return View("DeleteProduct",product);
    }

    [HttpPost, ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _context.Products.FindAsync(id);
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Product deleted successfully!";
        return RedirectToAction(nameof(ProductList));
    }

    private bool ProductExists(int id)
    {
        return _context.Products.Any(e => e.Id == id);
    }
}