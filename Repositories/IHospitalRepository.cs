using EC4clase1.Models;

namespace EC4clase1.Repositories
{
    public interface IHospitalRepository
    {
        Task<IEnumerable<Hospital>> GetAll();
        Task<Hospital?> GetOne(Guid id);
        Task Add(Hospital hospital);
    }
}
