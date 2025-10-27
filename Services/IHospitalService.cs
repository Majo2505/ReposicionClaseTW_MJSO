using EC4clase1.Models;
using EC4clase1.Models.dtos;

namespace EC4clase1.Services
{
    public interface IHospitalService
    {
        Task<IEnumerable<Hospital>> GetAll(); //me devuelve una lista de hospitales
        Task<Hospital> GetOne(Guid id); //me devuelve UN objeto de hospital
        Task<Hospital> CreateHospital(CreateHospitalDto dto);
    }
}
