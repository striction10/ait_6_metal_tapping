namespace RUSAL.MetalTapping.BLL.Application.Contracts;

/// <summary>
/// Модель представления данных об отклонении и задании для электролизёра
/// </summary>
/// <param name="potId"> Идентификатор ковша </param>
/// <param name="potName"> Наименование ковша </param>
/// <param name="targetMetalLevel"> Целевой уровень металла </param>
/// <param name="actualMetalLevel"> Фактический уровень металла </param>
/// <param name="deviationValue"> Величина отклонения </param>
/// <param name="amperage"> Сила тока </param>
/// <param name="avgAmperage"> Выход по току </param>
/// <param name="calculatedTask"> Рассчитанное задание </param>
/// <param name="roundCalculatedTask"> Округлённое рассчитанное задание </param>
/// <param name="metalMarkName"> Наименование марки металла </param>
public record ViewDeviationAndTaskPot(
    Guid potId,
    string potName,
    double targetMetalLevel,
    double? actualMetalLevel,
    double? deviationValue,
    double amperage,
    double avgAmperage,
    double? calculatedTask,
    double? roundCalculatedTask,
    string metalMarkName);
