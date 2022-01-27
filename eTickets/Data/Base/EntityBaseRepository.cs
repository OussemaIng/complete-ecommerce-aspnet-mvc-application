﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eTickets.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        public readonly AppDbContext _context;

        public EntityBaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
   

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(ac => ac.Id == id);
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
             return  await _context.Set<T>().ToListAsync();
          
        }

        public async Task<T> GetByIdAsync(int id)
        {
             return  await _context.Set<T>().FirstOrDefaultAsync(ac => ac.Id == id);
           
        }

        public async Task UpdateAsync(int id, T entity)
        {
             EntityEntry entityEntry = _context.Entry<T>(entity);
             entityEntry.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
