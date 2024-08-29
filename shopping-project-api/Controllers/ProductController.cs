using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shopping_project.Data.Entities;
using shopping_project.Data.Entities.ViewModels;

namespace shopping_project_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ShoppingContext _context;
        public ProductController()
        {
            _context = new ShoppingContext();
        }
        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();
            try
            {
                TblCategory category = new TblCategory()
                {
                    Name = name
                };
                await _context.TblCategories.AddAsync(category);
                await _context.SaveChangesAsync();
                return Ok("Category Added Successfully");
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Error retrieving Category from database");
            }
        }

        [HttpGet("AllCategory")]
        public async Task<ActionResult<List<string>>> AllCat()
        {
            List<string> categories = await _context.TblCategories
                .Select(p => p.Name).ToListAsync();
            if (categories.Count > 0)
                return Ok(categories);
            return NoContent();
        }
        [HttpPut("EditCategory/{id}/{name}")]
        public async Task<IActionResult> EditCat(int id,  string name)
        {
            TblCategory? category = await _context.TblCategories.SingleOrDefaultAsync(i => i.Id == id);
            if (category == null)
            {
                return BadRequest("کتگوری یافت نشد");
            }
            try
            {
                category.Name = name;
                _context.TblCategories.Update(category);
                await _context.SaveChangesAsync();
                return Ok("Category Updated Successfully");
            }
            catch
            {
                return StatusCode(500, "Error while updating pealese try again");
            }
        }

        [HttpDelete("DelCat/{id}")]
        public async Task<IActionResult> DelCat(int id)
        {
            var category = await _context.TblCategories.SingleOrDefaultAsync(i => i.Id == id);
            if (category == null) return BadRequest();
            try
            {
                _context.TblCategories.Remove(category);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("All/{categoryId}")]
        public async Task<ActionResult<List<TblProduct>>> GetProductsByCategory(int categoryId)
        {
            List<TblProduct> products = await _context.TblProducts.Where(i => i.CategoryId == categoryId)
                .Select(p => new TblProduct
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    Color = p.Color
                })
                .ToListAsync();
            if(products.Count == 0)
                return NoContent();
            return Ok(products);
        }

        [HttpGet("product/{id}")]
        public async Task<ActionResult<TblProduct>> GetProductById(int id)
        {
            TblProduct? product = await _context.TblProducts.SingleOrDefaultAsync(i => i.Id == id);
            if(product == null) return BadRequest();
            try
            {
                Product showProduct = new Product(product);
                return Ok(showProduct);
            }
            catch
            {
                return StatusCode(500, "Error!! pealese try again");
            }
        }
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if(product == null) return BadRequest();
            if(_context.TblProducts.Any(i => i.Name == product.Name.Trim()))
            {
                return BadRequest("محصول دیگری با این نام وجود دارد");
            }
            try
            {
                TblProduct productToAdd = new TblProduct()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Color = product.Color,
                    Stock = product.Stock,
                    CategoryId = product.CategoryId,
                    Price = product.Price
                };
                await _context.TblProducts.AddAsync(productToAdd);
                await _context.SaveChangesAsync();
                return Ok("Prduct Added Successfully");
            }
            catch(DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Error while Adding pealese try again");
            }
        }

        [HttpPut("EditProduct")]
        public async Task<IActionResult> EditProduct(Product product)
        {
            if(product == null) return BadRequest();
            TblProduct? ProductToEdit = await _context.TblProducts.SingleOrDefaultAsync(i => i.Id == product.Id);
            if (ProductToEdit == null) return BadRequest();
            try
            {
                ProductToEdit.Name = product.Name;
                ProductToEdit.Description = product.Description;
                ProductToEdit.Color = product.Color;
                ProductToEdit.Stock = product.Stock;
                ProductToEdit.CategoryId = product.CategoryId;
                ProductToEdit.Price = product.Price;
                _context.TblProducts.Update(ProductToEdit);
                await _context.SaveChangesAsync();
                return Ok("Prduct Updated Successfully");
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Error while Updating pealese try again");
            }
        }

        [HttpDelete("DelProduct/{id}")]
        public async Task<IActionResult> DelProduct(int id)
        {
            var product = await _context.TblProducts.SingleOrDefaultAsync(i => i.Id == id);
            if (product == null) return BadRequest();
            try
            {
                _context.TblProducts.Remove(product);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
