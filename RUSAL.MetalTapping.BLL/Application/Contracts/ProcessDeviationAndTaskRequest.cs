namespace RUSAL.MetalTapping.BLL.Application.Contracts;

/// <summary>
/// Запрос на обработку отклонения и расчёт задания для электролизёра
/// </summary>
/// <param name="potId"> Идентификатор электролизёра </param>
/// <param name="actualMetalLevel"> Фактический уровень металла </param>
public record ProcessDeviationAndTaskRequest(Guid potId, double actualMetalLevel);
