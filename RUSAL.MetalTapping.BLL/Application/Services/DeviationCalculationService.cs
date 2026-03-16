using RUSAL.MetalTapping.BLL.Domain.Entities;

namespace RUSAL.MetalTapping.BLL.Application.Services
{
    public class DeviationCalculationService
    {
        public double CalculateDeviation(double target, double actual)
            => target - actual;

        public bool IsValidDeviation(double deviationAmount, IEnumerable<DeviationValues> values)
            => values.Any(v => v.Value == deviationAmount);

        public int? GetCastingRatio(double deviationAmount, IEnumerable<DeviationValues> values)
            => values.FirstOrDefault(v => v.Value == deviationAmount)?.CastingRatio;
    }
}
