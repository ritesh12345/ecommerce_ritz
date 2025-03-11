﻿using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductRepository _productRepository) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type, string? sort)
        {
            return Ok(await _productRepository.GetProductsAsync(brand,type, sort));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            _productRepository.AddProduct(product);
            if(await _productRepository.SaveChangesAsync())
            {
                return CreatedAtAction("GetProduct", new { id = product.Id }, product );
            }
            return BadRequest("Problem creating product");
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (product.Id != id || !_productRepository.ProductExists(id)) return BadRequest("Cannot update this product");
            _productRepository.UpdateProduct(product);
            if(await _productRepository.SaveChangesAsync())
            {
                return NoContent();
            }
            return BadRequest("Problem updating the product");
            
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            _productRepository.DeleteProduct(product);
            if (await _productRepository.SaveChangesAsync())
            {
                return NoContent();
            }
            return BadRequest("Problem deleting the product");
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            return Ok(await _productRepository.GetBrandsAsync());
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            return Ok(await _productRepository.GetTypesAsync());
        }
    }
}
