using PostTradesThree.Domain;

namespace PostTradesThree.Repositories
{
    public interface ICurvePointRepository
    {
        Task<IEnumerable<CurvePoint>> GetAllCurvePointsAsync();
        Task<CurvePoint> GetCurvePointByIdAsync(int id);
        Task<int> CreateCurvePointAsync(CurvePoint curvePoint);
        Task UpdateCurvePointAsync(CurvePoint curvePoint);
        Task<int> DeleteCurvePointByIdAsync(int id);
    }
}
