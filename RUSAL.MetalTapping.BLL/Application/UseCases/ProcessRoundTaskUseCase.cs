using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Application.Services;
using RUSAL.MetalTapping.BLL.Domain.DTOs;
namespace RUSAL.MetalTapping.BLL.Application.UseCases;

public class ProcessRoundTaskUseCase(CalculatedTaskService calculatedTaskRepository)
{
    private readonly CalculatedTaskService _calculatedTaskRepository = calculatedTaskRepository;

    /// <summary>
    /// Создание расчетного задания для конкретных параметров электролизёра
    /// </summary>
    /// <param name="model"> Данные для создания расчётного задания </param>
    public async Task ExecuteAsync(ProcessRoundTaskRequest model)
    {
        var existingTask = await _calculatedTaskRepository.GetByPotIdAsync(model.potId);

        if (existingTask == null)
        {
            var newTask = new CalculatedTaskDto
            {
                Id = Guid.NewGuid(),
                PotId = model.potId,
                RoundCalculatedTaskForPot = model.roundTask,
                CreatedAt = DateTime.UtcNow
            };

            await _calculatedTaskRepository.CreateAsync(newTask);

            return;
        }

        existingTask.RoundCalculatedTaskForPot = model.roundTask;
        existingTask.CreatedAt = DateTime.UtcNow;

        await _calculatedTaskRepository.UpdateAsync(existingTask);
    }
}