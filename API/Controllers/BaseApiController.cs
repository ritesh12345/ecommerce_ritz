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
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        public async Task<ActionResult> CreatePagedResult<T>(IGenericRepository<T> _repo, ISpecification<T> spec, int pageindex, int pagesize) where T : BaseEntity
        {
            var data = await _repo.GetListAsync(spec);
            var count = await _repo.CountAsync(spec);
            var pagination = new Pagination<T>(pageindex, pagesize, count, data);
            return Ok(pagination);
        }

    }
    
}
