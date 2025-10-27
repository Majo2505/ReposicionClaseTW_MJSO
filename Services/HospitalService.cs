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

        public async Task<bool> DeleteHospital(Guid id)
        {
            var exists = await _repository.GetOne(id);
            if (exists == null) return false;
            await _repository.Delete(exists);
            return true;
        }

        public async Task<IEnumerable<Hospital>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Hospital> GetOne(Guid id)
        {
            var hospital = await _repository.GetOne(id);
            if (hospital == null) return null;
            return hospital;
        }

        public async Task<IEnumerable<Hospital>> GetTypes1and3()
        {
            return await _repository.GetByTypes(new int[] { 1, 3 });
        }

        public async Task<Hospital> UpdateHospital(UpdateHospitalDto dto)
        {
            var hospital = await _repository.GetOne(dto.Id);
            if (hospital == null) return null;
            hospital.Name = dto.Name;
            hospital.Address = dto.Address;
            hospital.Type = dto.Type;
            await _repository.Update(hospital);
            return hospital;
        }
    }
}
