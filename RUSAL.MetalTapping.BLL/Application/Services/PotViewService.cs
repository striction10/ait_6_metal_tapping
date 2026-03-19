using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Domain.Entities;

namespace RUSAL.MetalTapping.BLL.Application.Services
{
    public class PotViewService
    {
        public ViewDeviationAndTaskPot BuildPotView(
            Pot pot,
            Deviation deviation,
            double amperage,
            double averageAmperage,
            CalculatedTask? lastTask,
            string metalMarkName)
        {
            var deviationValue = deviation.TargetMetalLevel - deviation.ActualMetalLevel;


            return new ViewDeviationAndTaskPot(
                potId: pot.Id,
                potName: pot.Name,
                targetMetalLevel: deviation.TargetMetalLevel,
                actualMetalLevel: deviation.ActualMetalLevel,
                deviationValue: deviationValue,
                amperage: amperage,
                avgAmperage: averageAmperage,
                calculatedTask: lastTask?.CalculatedTaskForPot,
                roundCalculatedTask: lastTask?.RoundCalculatedTaskForPot,
                metalMarkName: metalMarkName
            );
        }
    }
}