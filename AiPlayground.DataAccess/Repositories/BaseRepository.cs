
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace AiPlayground.DataAccess.Repositories;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly AiPlaygroundContext _context;
    
    public BaseRepository(AiPlaygroundContext context)
    {
        _context = context;
    }
    
    public virtual async Task<TEntity?> GetByIdAsync(int id)
    {
        try
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
        catch (DbUpdateException e)
        {
            throw new Exception($"Database error while retrieving {typeof(TEntity).Name} with ID {id}: {e.InnerException?.Message ?? e.Message}");
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to retrieve {typeof(TEntity).Name} with ID {id}: {e.Message}");
        }
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        try
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
        catch (DbUpdateException e)
        {
            throw new Exception($"Database error while retrieving all {typeof(TEntity).Name} entities: {e.InnerException?.Message ?? e.Message}");
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to retrieve all {typeof(TEntity).Name} entities: {e.Message}");
        }
    }
    
    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        try
        { 
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (DbUpdateException e)
        {
            throw new Exception($"Database error while adding new {typeof(TEntity).Name}: {e.InnerException?.Message ?? e.Message}");
        }
        catch (ValidationException e)
        {
            throw new Exception($"Validation failed while adding {typeof(TEntity).Name}: {e.Message}");
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to add new {typeof(TEntity).Name}: {e.Message}");
        }
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        try
        {
             _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (DbUpdateConcurrencyException e)
        {
            throw new Exception($"Concurrency conflict while updating {typeof(TEntity).Name}: The record was modified by another user");
        }
        catch (DbUpdateException e)
        {
            throw new Exception($"Database error while updating {typeof(TEntity).Name}: {e.InnerException?.Message ?? e.Message}");
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to update {typeof(TEntity).Name}: {e.Message}");
        }
    }

    public virtual async Task DeleteAsync(int id)
    {
        try
        {
            var entity = await  _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                throw new Exception($"Entity with id {id} not found");
            }
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            throw new Exception($"Database error while deleting {typeof(TEntity).Name} with ID {id}: {e.InnerException?.Message ?? e.Message}");
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to delete {typeof(TEntity).Name} with ID {id}: {e.Message}");
        }
    }
}