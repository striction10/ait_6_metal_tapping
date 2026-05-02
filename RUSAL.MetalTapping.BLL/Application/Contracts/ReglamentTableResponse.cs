using RUSAL.MetalTapping.BLL.Domain.DTOs;

namespace RUSAL.MetalTapping.BLL.Application.Contracts;

/// <summary>
/// Ответ с таблицей регламентов, содержащей ковши и коэффициенты выливки
/// </summary>
/// <param name="pots"> Список ковшей с отклонениями и коэффициентами выливки </param>
public record ReglamentTableResponse(List<PotDeviationDto> pots);
