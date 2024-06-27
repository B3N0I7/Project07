using Microsoft.EntityFrameworkCore;
using PostTradesThree.Data;
using PostTradesThree.Domain;

namespace PostTradesThree.Repositories
{
    public class CurvePointRepository : ICurvePointRepository
    {
        private readonly PttDbContext _pttDbContext;

        public CurvePointRepository(PttDbContext pttDbContext)
        {
            _pttDbContext = pttDbContext;
        }
        public async Task<IEnumerable<CurvePoint>> GetAllCurvePointsAsync()
        {
            return await _pttDbContext.CurvePoints.ToListAsync();
        }

        public async Task<CurvePoint> GetCurvePointByIdAsync(int id)
        {
            return await _pttDbContext.CurvePoints.FindAsync(id) ?? throw new InvalidOperationException();
        }

        public async Task<int> CreateCurvePointAsync(CurvePoint curvePoint)
        {
            _pttDbContext.CurvePoints.Add(curvePoint);

            return await _pttDbContext.SaveChangesAsync();
        }

        public async Task UpdateCurvePointAsync(CurvePoint curvePoint)
        {
            _pttDbContext.Entry(curvePoint).State = EntityState.Modified;

            await _pttDbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteCurvePointByIdAsync(int id)
        {
            var curvePoint = await _pttDbContext.CurvePoints.FindAsync(id);
            if (curvePoint != null)
            {
                _pttDbContext.CurvePoints.Remove(curvePoint);
            }

            return await _pttDbContext.SaveChangesAsync();
        }
    }
}
