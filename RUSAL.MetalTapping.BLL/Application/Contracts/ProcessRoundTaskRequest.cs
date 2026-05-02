namespace RUSAL.MetalTapping.BLL.Application.Contracts;

/// <summary>
/// Запрос на обработку ЗПР для электролизёра
/// </summary>
/// <param name="potId"> Идентификатор электролизёра </param>
/// <param name="roundTask"> Значение округлённого задания </param>
public record ProcessRoundTaskRequest(Guid potId, double roundTask);
