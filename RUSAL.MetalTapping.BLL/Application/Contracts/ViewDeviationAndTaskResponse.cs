namespace RUSAL.MetalTapping.BLL.Application.Contracts;

/// <summary>
/// Ответ с данными об отклонениях и заданиях для списка электролизёров
/// </summary>
/// <param name="pots"> Список электролизёров с данными об отклонениях и рассчитанных заданиях </param>
public record ViewDeviationAndTaskResponse(IEnumerable<ViewDeviationAndTaskPot> pots);
