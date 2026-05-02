using RUSAL.MetalTapping.BLL.Domain.DTOs;

namespace RUSAL.MetalTapping.BLL.Application.Services;

/// <summary>
/// Сервис расчёта отклонений уровня металла и определения коэффициентов выливки.
/// </summary>
public class DeviationCalculationService
{
    /// <summary>
    /// Расчёт отклонения актуального уровня металла от заданного.
    /// </summary>
    /// <param name="target"> Заданный уровень металла в электролизёре. </param>
    /// <param name="actual"> Актуальный уровень металла в электролизёре. </param>
    /// <returns> Отклонение от заданного уровня металла. </returns>
    public double CalculateDeviation(double target, double actual)
        => target - actual;

    /// <summary>
    /// Является ли отклонение регламентным.
    /// </summary>
    /// <param name="deviationAmount"> Отклонение от заданного уровня металла. </param>
    /// <param name="values"> Список регламентных отклонений. </param>
    /// <returns> Статус отклонения. </returns>
    public bool IsValidDeviation(double deviationAmount, IEnumerable<DeviationValuesDto> values)
        => values.Any(v => v.Value == deviationAmount);

    /// <summary>
    /// Получение регламентного процента выливки в зависимости от регламентного значения отклонения.
    /// </summary>
    /// <param name="deviationAmount"> Значение отклонения. </param>
    /// <param name="values"> Список регламентных отклонений. </param>
    /// <returns> Значение регламентного процента выливки. </returns>
    public int? GetCastingRatio(double deviationAmount, IEnumerable<DeviationValuesDto> values)
        => values.FirstOrDefault(v => v.Value == deviationAmount)?.CastingRatio;
}
