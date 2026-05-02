namespace RUSAL.MetalTapping.BLL.Application.Contracts;

/// <summary>
/// Запрос на получение таблицы регламентов с отклонениями
/// </summary>
/// <param name="buildingId"> Идентификатор корпуса </param>
/// <param name="reglamentId"> Идентификатор регламента </param>
public record ReglamentTableRequest(Guid buildingId, Guid reglamentId);
