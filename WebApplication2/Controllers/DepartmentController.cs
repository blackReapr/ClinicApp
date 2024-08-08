using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data.Data;
using WebApplication2.Data.Entities;
using WebApplication2.Dtos.DepartmentDtos;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DepartmentController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sl = HttpContext;
            IEnumerable<Department> departments = await _context.Departments.Include(d => d.Doctors).ToListAsync();
            return Ok(_mapper.Map<List<DepartmentReturnDto>>(departments));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Department? department = await _context.Departments.Include(d => d.Doctors).FirstOrDefaultAsync(x => x.Id == id);
            if (department == null) return NotFound();
            return Ok(_mapper.Map<DepartmentReturnDto>(department));
        }

        [HttpPost]
        public async Task<IActionResult> Post(DepartmentCreateDto departmentCreateDto)
        {
            Department department = _mapper.Map<Department>(departmentCreateDto);
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DepartmentUpdateDto departmentUpdateDto)
        {
            Department? department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
            if (department == null) return NotFound();
            string oldImage = department.Image;
            _mapper.Map(departmentUpdateDto, department);
            if (department.Image == null) department.Image = oldImage;
            else
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", oldImage);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            Department? department = _context.Departments.FirstOrDefault(x => x.Id == id);
            if (department == null) return NotFound();
            _context.Departments.Remove(department);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", department.Image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
