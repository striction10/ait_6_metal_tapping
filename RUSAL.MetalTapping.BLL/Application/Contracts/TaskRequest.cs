namespace RUSAL.MetalTapping.BLL.Application.Contracts;

/// <summary>
/// Запрос на получение заданий по выливке для указанной даты и корпуса
/// </summary>
/// <param name="date"> Дата, на которую запрашиваются задания </param>
/// <param name="buildingId"> Идентификатор корпуса </param>
public record TaskRequest(DateTime date, Guid buildingId);
