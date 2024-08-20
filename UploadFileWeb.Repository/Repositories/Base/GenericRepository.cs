using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UploadFileWeb.Entities;
using UploadFileWeb.Entities.Interfaces.Base;

namespace UploadFileWeb.Repository.Repositories.Base
{
    public class GenericRepository<T, TId> : IGenericRepository<T, TId> where T : class
    {
        protected readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(T entity)
        {
            Type entityType = typeof(T);
            PropertyInfo createDate = entityType.GetProperty("InsertDate");
            if (createDate != null)
                createDate.SetValue(entity, DateTime.Now);
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", bool isTracking = true, int? pageIndex = null, int? take = null)
        {
            int count;
            int index = 1; ;
            int GetMaxPage(int c, int p) => Math.Max(1, (int)Math.Ceiling((double)c / (double)p));
            IQueryable<T> query = _context.Set<T>();

            if (expression != null)
            {
                query = isTracking ? query.Where(expression) : query.Where(expression).AsNoTracking();
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (!string.IsNullOrWhiteSpace(includeProperty))
                        query = query.Include(includeProperty.Trim());
                }
            }

            count = await query.CountAsync();
            int maxPage = 0;
            if (take != null)
            {
                maxPage = GetMaxPage(count, take.Value);
                index = Math.Max(1, pageIndex > maxPage ? maxPage : pageIndex ?? 1);

            }


            if (orderBy != null)
            {
                return take == null ? orderBy(query).ToList() : orderBy(query).Skip((index - 1) * take.Value).Take(take.Value).ToList();
            }
            else
            {
                return take == null ? query.ToList() : query.Skip((index - 1) * take.Value).Take(take.Value).ToList();
            }
        }

        public async Task<IEnumerable<T>> GetAll(string includeProperties = "")
        {
            if (!string.IsNullOrEmpty(includeProperties))
            {
                return await Find(null, null, includeProperties: includeProperties);
            }
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(params object[] keyValues)
        {
            return await _context.Set<T>().FindAsync(keyValues);
        }

        public virtual void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public virtual EntityEntry<T> Update(T entity)
        {
            Type entityType = typeof(T);
            PropertyInfo modifyDate = entityType.GetProperty("ModifyDate");
            if (modifyDate != null)
                modifyDate.SetValue(entity, DateTime.Now);
 
            PropertyInfo insertDate = entityType.GetProperty("InsertDate");
            if (insertDate != null)
                _context.Entry(entity).Property("InsertDate").IsModified = false;
            return _context.Set<T>().Update(entity);
        }
    }
}
