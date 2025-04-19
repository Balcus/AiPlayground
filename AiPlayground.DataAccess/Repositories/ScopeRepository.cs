using AiPlayground.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AiPlayground.DataAccess.Repositories;

public class ScopeRepository : BaseRepository<Scope>
{
    public ScopeRepository(AiPlaygroundContext context) : base(context)
    {
    }

    public override async Task<Scope?> GetByIdAsync(int id)
    {
        try
        {
            return await _context
                .Scope
                .Include(s => s.Prompts)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        catch (Exception e)
        {
            throw new Exception($"Error retrieving entity with ID {id}: {e.Message}", e);
        }
    }
}
