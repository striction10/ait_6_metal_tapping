using RUSAL.MetalTapping.BLL.Domain.Enums;

namespace RUSAL.MetalTapping.BLL.Application.Services
{
    public class CalculatedTaskService
    {
        public double CalculatedTask(double amperage, double avgAmperage)
        {
            return Math.Round(
                amperage * avgAmperage * CalculateConstants.K / 100 * CalculateConstants.hoursCount,
                2);
        }

        public double CalculateRoundedTask(double task, int castingRatio)
        {
            return task * castingRatio / 100;
        }
    }
}