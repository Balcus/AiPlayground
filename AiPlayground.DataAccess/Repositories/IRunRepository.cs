using AiPlayground.DataAccess.Entities;

namespace AiPlayground.DataAccess.Repositories;

public interface IRunRepository : IRepository<Run>
{
    Task<List<Run>> GetAllByModelId(int modelId);
    Task<List<Run>> GetAllByPromptId(int promptId);
    Task<List<Run>> GetAll();
}