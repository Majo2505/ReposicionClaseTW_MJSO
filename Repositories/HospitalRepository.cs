using EC4clase1.Data;
using EC4clase1.Models;
using Microsoft.EntityFrameworkCore;

namespace EC4clase1.Repositories
{
    public class HospitalRepository : IHospitalRepository
    {
        private readonly AppDbContext _db;
        public HospitalRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task Add(Hospital hospital)
        {
            await _db.Hospitals.AddAsync(hospital);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Hospital hospital)
        {
            _db.Hospitals.Remove(hospital);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Hospital>> GetAll()
        {
            return await _db.Hospitals.ToListAsync();
        }

        public async Task<IEnumerable<Hospital>> GetByTypes(int[] types)
        {
            return await _db.Hospitals.Where(h => types.Contains(h.Type)).ToListAsync();
        }

        public async Task<Hospital?> GetOne(Guid id)
        {
            return await _db.Hospitals.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Hospital hospital)
        {
            _db.Hospitals.Update(hospital);
            await _db.SaveChangesAsync();
        }
    }
}
