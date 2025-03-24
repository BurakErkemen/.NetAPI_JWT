using CoreLayer.DTOs;
using CoreLayer.Models;
using CoreLayer.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.API_JWT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IGenericService<ProductModel,ProductDto> genericService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            return CreateActionResultInstance(await genericService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductDto productDto)
        {
            return CreateActionResultInstance(await genericService.AddAsync(productDto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductDto productDto)
        {
            return CreateActionResultInstance(await genericService.Update(productDto.Id));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return CreateActionResultInstance(await genericService.Delete(id));
        }
    }
}