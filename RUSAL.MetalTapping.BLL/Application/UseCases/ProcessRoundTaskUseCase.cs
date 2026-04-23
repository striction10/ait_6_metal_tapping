using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;
namespace RUSAL.MetalTapping.BLL.Application.UseCases;

public class ProcessRoundTaskUseCase(ICalculatedTaskRepository calculatedTaskRepository)
{
    private readonly ICalculatedTaskRepository _calculatedTaskRepository = calculatedTaskRepository;

    /// <summary>
    /// Создание расчетного задания для конкретных параметров электролизёра
    /// </summary>
    /// <param name="model"> Данные для создания расчётного задания </param>
    public async Task ExecuteAsync(ProcessRoundTaskRequest model)
    {
        var existingTask = await _calculatedTaskRepository.GetCalculatedTaskWithPotIdAsync(model.potId);

        if (existingTask == null)
        {
            var newTask = new CalculatedTask
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