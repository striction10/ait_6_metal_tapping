namespace RUSAL.MetalTapping.BLL.Application.Contracts;

/// <summary>
/// Запрос на создание заказа на выливку металла
/// </summary>
/// <param name="metalMarkName"> Наименование марки металла </param>
/// <param name="requiredMetalWeight"> Требуемый вес металла </param>
public record OrderRequest(string metalMarkName, double requiredMetalWeight);
