using AutoMapper;
using Fiorella_API_101.Data;
using Fiorella_API_101.DTOs.Blogs;
using Fiorella_API_101.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorella_API_101.Controllers
{
    public class BlogController : BaseController
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BlogController(AppDbContext context,
                              IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BlogCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _context.Blogs.AddAsync(_mapper.Map<Blog>(request));

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Create), request);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<List<BlogDto>>(await _context.Blogs.AsNoTracking().ToListAsync()));
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int id, [FromBody] BlogEditDto request)
        {
            var entity = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (entity == null) return NotFound();

            _mapper.Map(request, entity);

            _context.Blogs.Update(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var entity = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (entity is null) return NotFound();
            return Ok(_mapper.Map<BlogDto>(entity));

        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if (id is null) return BadRequest();
            var entity = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (entity == null) return NotFound();

            _context.Blogs.Remove(entity);

            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}
