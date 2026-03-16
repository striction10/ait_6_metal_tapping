using RUSAL.MetalTapping.BLL.Application.Contracts;
using RUSAL.MetalTapping.BLL.Domain.Entities;
using RUSAL.MetalTapping.BLL.Domain.Interfaces;

namespace RUSAL.MetalTapping.BLL.Application.UseCases
{
    public class ProcessCalculatedTaskUseCase
    {
        private readonly ICalculatedTaskRepository _calculatedTaskRepository;

        public ProcessCalculatedTaskUseCase(ICalculatedTaskRepository calculatedTaskRepository)
        {
            _calculatedTaskRepository = calculatedTaskRepository;
        }

        public async Task<ProcessCalculatedTaskResponse> ExecuteAsync(ProcessCalculatedTaskRequest model)
        {
            var existingTask = await _calculatedTaskRepository.GetCalculatedTaskWithPotIdAsync(model.potId);

            if (existingTask == null)
            {
                var newTask = new CalculatedTask
                {
                    Id = Guid.NewGuid(),
                    PotId = model.potId,
                    CalculatedTaskForPot = model.calculatedTask,
                    RoundCalculatedTaskForPot = model.roundedCalculatedTask ?? 0,
                    CreatedAt = DateTime.UtcNow
                };

                await _calculatedTaskRepository.CreateAsync(newTask);

                return new ProcessCalculatedTaskResponse(
                    calculatedTask: model.calculatedTask,
                    roundCalculatedTask: newTask.RoundCalculatedTaskForPot
                );
            }

            existingTask.CalculatedTaskForPot = model.calculatedTask;
            existingTask.RoundCalculatedTaskForPot = model.roundedCalculatedTask ?? 0;
            existingTask.CreatedAt = DateTime.UtcNow;

            await _calculatedTaskRepository.UpdateAsync(existingTask);

            return new ProcessCalculatedTaskResponse(
                calculatedTask: model.calculatedTask,
                roundCalculatedTask: existingTask.RoundCalculatedTaskForPot
            );
        }
    }
}
