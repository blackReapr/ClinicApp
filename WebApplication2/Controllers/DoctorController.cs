using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data.Data;
using WebApplication2.Data.Entities;
using WebApplication2.Dtos;
using WebApplication2.Dtos.DoctorDtos;
using WebApplication2.Dtos.StudentDtos;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DoctorController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string? query, int page=1)
        {
            IQueryable<Doctor> doctors = _context.Doctors.Include(d => d.Department).AsQueryable();
            if (query != null)
            {
                doctors = doctors.Where(d => d.Name.ToLower().Contains(query.ToLower()));
            }
            int count = doctors.Count();
            doctors = doctors.Skip(page - 1).Take(2);
            PaginationDto<DoctorReturnDto> paginationDto = new()
            {
                items = _mapper.Map<List<DoctorReturnDto>>(doctors),
                PageCount = count
            };
            return Ok(paginationDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Doctor? doctor = await _context.Doctors.Include(d => d.Department).FirstOrDefaultAsync(d => d.Id == id);
            if (doctor == null) return NotFound();
            return Ok(_mapper.Map<DoctorReturnDto>(doctor));
        }

        [HttpPost("")]
        public async Task<IActionResult> Post(DoctorCreateDto doctorCreateDto)
        {
            Doctor doctor = _mapper.Map<Doctor>(doctorCreateDto);
            Department? department = await _context.Departments.Include(d => d.Doctors).FirstOrDefaultAsync(d => d.Id == doctor.DepartmentId);
            if (department == null) return BadRequest();
            if (department.Doctors.Count == department.Limit) return Conflict();
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DoctorCreateDto doctorCreateDto)
        {
            Doctor? doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
            if (doctor == null) return NotFound();
            if (doctor.DepartmentId != doctorCreateDto.DepartmentId)
            {
                Department? department = await _context.Departments.Include(d => d.Doctors).FirstOrDefaultAsync(d => d.Id == doctorCreateDto.DepartmentId);
                if (department == null) return BadRequest();
                if (department.Limit == department.Doctors.Count) return Conflict();
            }
            _mapper.Map(doctorCreateDto, doctor);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            Doctor? doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
            if (doctor == null) return NotFound();
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
