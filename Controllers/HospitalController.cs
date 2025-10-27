
using EC4clase1.Models;
using EC4clase1.Models.dtos;
using EC4clase1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EC4clase1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HospitalController: ControllerBase
    {
        private readonly IHospitalService _service;
        public HospitalController(IHospitalService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllHospitals()
        {
            IEnumerable<Hospital> items = await _service.GetAll();
            return Ok(items);
        }
        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var hospital = await _service.GetOne(id);
            return Ok(hospital);
        }
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateHospital([FromBody] CreateHospitalDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var hospital = await _service.CreateHospital(dto);
            return CreatedAtAction(nameof(GetOne), new { id = hospital.Id }, hospital);
        }

    }
}
