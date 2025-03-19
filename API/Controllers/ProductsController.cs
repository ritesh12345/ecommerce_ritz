using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Core.Specifications.SpecParams;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ProductsController(IGenericRepository<Product> _repo) : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecParams specParams)
        {
            var spec = new ProductSpecification(specParams);
            
            return await CreatePagedResult<Product>(_repo,spec,specParams.PageIndex,specParams.PageSize);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            _repo.Add(product);
            if(await _repo.SaveChangesAsync())
            {
                return CreatedAtAction("GetProduct", new { id = product.Id }, product );
            }
            return BadRequest("Problem creating product");
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (product.Id != id || !_repo.Exists(id)) return BadRequest("Cannot update this product");
            _repo.Update(product);
            if(await _repo.SaveChangesAsync())
            {
                return NoContent();
            }
            return BadRequest("Problem updating the product");
            
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) return NotFound();
            _repo.Delete(product);
            if (await _repo.SaveChangesAsync())
            {
                return NoContent();
            }
            return BadRequest("Problem deleting the product");
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            //TODO : Implementation
            return Ok(await _repo.GetListAsync(new BrandListSpecification()));
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            //TODO : Implementation
            return Ok(await _repo.GetListAsync(new TypeListSpecification()));
        }
    }
}
