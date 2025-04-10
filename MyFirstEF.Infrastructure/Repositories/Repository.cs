using Microsoft.EntityFrameworkCore;
using MyFirstEF.Application.Interfaces;
using MyFirstEF.Infrastructure.Data;

namespace MyFirstEF.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly MyFirstEFDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(MyFirstEFDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public void Update(T entity) => _dbSet.Update(entity);

    public void Delete(T entity) => _dbSet.Remove(entity);

    public async Task SaveAsync() => await _context.SaveChangesAsync();
}