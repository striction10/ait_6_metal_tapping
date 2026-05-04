using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Domain.DTOs;

namespace RUSAL.MetalTapping.BLL.Application.UseCases;

/// <summary>
/// Оркестратор сохранения значения расчётного задания для электролизёра.
/// </summary>
/// <param name="calculatedTaskService">Сервис для работы с расчётными заданиями.</param>
public class ProcessCalculatedTaskUseCase(CalculatedTaskService calculatedTaskService)
{
    /// <summary>
    /// Запись значения расчетного задания для электролизёра.
    /// </summary>
    /// <param name="model"> Данные для создания расчётного задания. </param>
    public async Task ExecuteAsync(ProcessCalculatedTaskRequest model)
    {
        var existingTask = await calculatedTaskService.GetByPotIdAsync(model.potId);

        if (existingTask == null)
        {
            var newTask = new CalculatedTaskDto
            {
                Id = Guid.NewGuid(),
                PotId = model.potId,
                CalculatedTaskForPot = model.calculatedTask,
                CreatedAt = DateTime.UtcNow,
            };

            await calculatedTaskService.CreateAsync(newTask);

            return;
        }

        existingTask.CalculatedTaskForPot = model.calculatedTask;
        existingTask.CreatedAt = DateTime.UtcNow;

        await calculatedTaskService.UpdateAsync(existingTask);
    }
}
