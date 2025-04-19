using AiPlayground.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AiPlayground.DataAccess.Repositories;

public class PlatformRepository : BaseRepository<Platform>
{
    public PlatformRepository(AiPlaygroundContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Platform>> GetAllAsync()
    {
        try
        {
            return await _context
                .Platforms
                .Include(p => p.Models)
                .ToListAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"Error retrieving all platforms with models: {e.Message}", e);
        }
    }

    public override async Task<Platform?> GetByIdAsync(int id)
    {
        try
        {
            return await _context
                .Platforms
                .Include(p =>p.Models)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        catch (Exception e)
        {
            throw new Exception($"Error retrieving platform with ID {id}: {e.Message}", e);
        }
    }
}