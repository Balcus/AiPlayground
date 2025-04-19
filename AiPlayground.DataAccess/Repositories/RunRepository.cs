using AiPlayground.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AiPlayground.DataAccess.Repositories;

public class RunRepository : BaseRepository<Run>
{
    public RunRepository(AiPlaygroundContext context) : base(context)
    {
    }

    public async Task<List<Run>> GetAllByPromptId(int promptId)
    {
        try
        {
            return await _context
                .Run
                .Include(r => r.Prompt)
                .Where(r => r.PromptId == promptId)
                .ToListAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"Error retrieving all runs for prompt with ID {promptId}: {e.Message}", e);
        }
    }

    public async Task<List<Run>> GetAllByModelId(int modelId)
    {
        try
        {
            return await _context
                .Run
                .Include(r => r.Model)
                .Where(r => r.ModelId == modelId)
                .ToListAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"Error retrieving all runs for model with ID {modelId}: {e.Message}", e);
        }
    }
}
