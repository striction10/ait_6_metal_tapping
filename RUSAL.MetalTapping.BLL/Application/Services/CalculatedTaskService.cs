using RUSAL.MetalTapping.BLL.Domain.Enums;
namespace RUSAL.MetalTapping.BLL.Application.Services;

public class CalculatedTaskService
{
                                                                                                                                                                                                                                                           
    /// <summary>
    /// Расчёт параметра "расчётное задание"
    /// </summary>
    /// <param name="amperage"> Выход по току у электролизёра </param>
    /// <param name="avgAmperage"> Общая сила тока в корпусе </param>
    /// <returns> Расчётное задание электролизёра </returns>
    public double CalculatedTask(double amperage, double avgAmperage)
    {
        return Math.Round(
            amperage * avgAmperage * CalculateConstants.K / 100 * CalculateConstants.hoursCount,
            2);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="task"></param>
    /// <param name="castingRatio"></param>
    /// <returns> ЗПР электролизёра </returns>
    public double CalculateRoundedTask(double task, int castingRatio)
    {
        return task * castingRatio / 100;
    }
}