using EC4clase1.Models;

namespace EC4clase1.Repositories
{
    public interface IHospitalRepository
    {
        Task<IEnumerable<Hospital>> GetAll();
        Task<IEnumerable<Hospital>> GetByTypes(int[] types);
        Task<Hospital?> GetOne(Guid id);
        Task Add(Hospital hospital);
        Task Update(Hospital hospital);
        Task Delete(Hospital hospital);
    }
}
