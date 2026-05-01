using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class PotViewService
{

    /// <summary>
    /// Создание ViewModel
    /// </summary>
    /// <param name="pot"> Электролизёр </param>
    /// <param name="deviation"> Отклонение</param>
    /// <param name="amperage"> Сила тока </param>
    /// <param name="averageAmperage"> Выход по току</param>
    /// <param name="lastTask"> Последнее расчётное задание </param>
    /// <param name="metalMarkName"> Имя марки металла внутри электролизёра </param>
    /// <returns> ViewModel электролизера </returns>
    public ViewDeviationAndTaskPot BuildPotView(
        PotDto pot,
        DeviationDto deviation,
        double amperage,
        double averageAmperage,
        CalculatedTaskDto? lastTask,
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