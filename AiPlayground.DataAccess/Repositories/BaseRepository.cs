
using Microsoft.EntityFrameworkCore;
// TODO : Unit of work pattern
namespace AiPlayground.DataAccess.Repositories;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly AiPlaygroundContext _context;
    
    public BaseRepository(AiPlaygroundContext context)
    {
        _context = context;
    }
    
    public async Task<TEntity?> GetByIdAsync(int id)
    {
        try
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
        catch (Exception e)
        {
            throw new Exception("error");
        }
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        try
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
        catch (Exception e)
        {
            throw new Exception("error");
        }
    }

    // use it here !!
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        try
        { 
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception e)
        {
            throw new Exception("error");
        }
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        try
        {
             _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch
        {
            throw new Exception("error");
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var entity = await  _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                throw new Exception("Not found");
            }
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        catch
        {
            throw new Exception("error");
        }
    }
}