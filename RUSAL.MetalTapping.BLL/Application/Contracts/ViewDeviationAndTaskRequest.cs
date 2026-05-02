namespace RUSAL.MetalTapping.BLL.Application.Contracts;

/// <summary>
/// Запрос на получение данных об отклонениях и заданиях для электролизёров
/// </summary>
/// <param name="reglamentId"> Идентификатор регламента </param>
/// <param name="buildingId"> Идентификатор корпуса </param>
public record ViewDeviationAndTaskRequest(Guid reglamentId, Guid buildingId);
