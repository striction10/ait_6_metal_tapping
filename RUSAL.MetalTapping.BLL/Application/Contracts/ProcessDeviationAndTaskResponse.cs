namespace RUSAL.MetalTapping.BLL.Application.Contracts;

/// <summary>
/// Ответ с результатами обработки отклонения и расчёта задания
/// </summary>
/// <param name="deviation"> Величина отклонения уровня металла </param>
/// <param name="calculatedTask"> Рассчитанное задание </param>
/// <param name="roundCalculatedTask"> ЗПР </param>
public record ProcessDeviationAndTaskResponse(double deviation, double? calculatedTask = null, double? roundCalculatedTask = null);
