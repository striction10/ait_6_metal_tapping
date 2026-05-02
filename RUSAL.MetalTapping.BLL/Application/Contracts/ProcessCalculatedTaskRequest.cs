namespace RUSAL.MetalTapping.BLL.Application.Contracts;

/// <summary>
/// Запрос на обработку рассчитанного задания для электролизёра
/// </summary>
/// <param name="potId">Идентификатор электролизёра </param>
/// <param name="calculatedTask">Значение рассчётного задания</param>
public record ProcessCalculatedTaskRequest(Guid potId, double calculatedTask);
