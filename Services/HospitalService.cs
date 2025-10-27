using EC4clase1.Models;
using EC4clase1.Models.dtos;
using EC4clase1.Repositories;

namespace EC4clase1.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly IHospitalRepository _repository;
        public HospitalService(IHospitalRepository repository)
        {
            _repository = repository;
        }
        public async Task<Hospital> CreateHospital(CreateHospitalDto dto)
        {
            var hospital = new Hospital
            {
                Id = dto.Id,
                Name = dto.Name,
                Address = dto.Address,
                Type = dto.Type,
            };
            await _repository.Add(hospital);
            return hospital;
            
        }

        public async Task<IEnumerable<Hospital>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Hospital> GetOne(Guid id)
        {
            return await _repository.GetOne(id);
        }
    }
}
