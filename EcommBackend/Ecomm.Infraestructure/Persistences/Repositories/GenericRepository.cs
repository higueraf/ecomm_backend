using Ecomm.Domain.Entities;
using Ecomm.Infraestructure.Persistences.Contexts;
using Ecomm.Infraestructure.Persistences.Interfaces;
using Ecomm.Utils.Static;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Ecomm.Infraestructure.Persistences.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly EcommContext _context;
        private readonly DbSet<T> _entity;

        public GenericRepository(EcommContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var getAll = await _entity
                .Where(x => x.State.Equals((int)StateTypes.Active) && x.DeleteBy == null && x.DeleteDate == null)
                .AsNoTracking()
                .ToListAsync();
            return getAll;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var getById = await _entity!.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return getById!;
        }

        public async Task<bool> CreateAsync(T entity)
        {
            entity.CreateBy = Guid.Parse("da94176f-ddb8-4f06-aeb7-05b51b4e3263");
            entity.CreateDate = DateTime.Now;
            await _context.AddAsync(entity);
            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }
        public async Task<bool> UpdateAsync(T entity)
        {
            entity.UpdateBy = Guid.Parse("da94176f-ddb8-4f06-aeb7-05b51b4e3263");
            entity.UpdateDate = DateTime.Now;
            _context.Update(entity);
            _context.Entry(entity).Property(c => c.CreateBy).IsModified = false;
            _context.Entry(entity).Property(c => c.CreateDate).IsModified = false;
            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;

        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            T entity = await GetByIdAsync(id);
            entity.DeleteBy = Guid.Parse("da94176f-ddb8-4f06-aeb7-05b51b4e3263");
            entity.DeleteDate = DateTime.Now;
            _context.Update(entity);
            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;

        }
        public IQueryable<T> GetEntityQuery(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _entity;
            if (filter is not null) query = query.Where(filter);
            return query;
        }

        public IQueryable<T> GetAllQueryable()
        {
            var getAllQuery = GetEntityQuery(x => x.DeleteBy == null && x.DeleteDate == null);
            return getAllQuery;
        }

        public async Task<IEnumerable<T>> GetSelectAsync()
        {
            var getAll = await _entity
                .Where(x => x.State.Equals((int)StateTypes.Active))
                .AsNoTracking()
                .ToListAsync();
            return getAll;
        }
    }
}
