using QuizYourLizardApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace QuizYourLizardApi.Repositories
{
    public interface IGenericRepository<C, T> :
             IDisposable
        where T : PersistentEntity
        where C : DbContext, new()
{
    IQueryable<T> GetAll();
    List<T> GetAllAsync();
    IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
    void Add(T entity);
    void Delete(T entity);
    void Edit(T entity);
    List<T> GetPageAsync(int page, int pageSize);

    C Context { get; set; }
}

public class GenericRepository<C, T> :
            IDisposable, IGenericRepository<C, T>
    where T : PersistentEntity
    where C : DbContext, new()
{
    private C _entities;
    public C Context
    {
        get { return _entities; }
        set { _entities = value; }
    }

    public virtual IQueryable<T> GetAll()
    {
        IQueryable<T> query = _entities.Set<T>();
        return query;
    }

    public virtual List<T> GetAllAsync()
    {
        _entities.Configuration.LazyLoadingEnabled = false;
        return _entities.Set<T>().ToListAsync<T>().Result;
    }

    public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
    {
        IQueryable<T> query = _entities.Set<T>().Where(predicate);
        return query;
    }

    public virtual void Add(T entity)
    {
        _entities.Set<T>().Add(entity);
    }

    public virtual void Delete(T entity)
    {
        _entities.Set<T>().Remove(entity);
    }

    public virtual void Edit(T entity)
    {
        _entities.Entry(entity).State = System.Data.Entity.EntityState.Modified;
    }
    
    public List<T> GetPageAsync(int page, int pageSize)
    {
        var internalPage = page - 1;
        return _entities.Set<T>().OrderBy(e => e.Id).Skip(pageSize * internalPage).Take(pageSize).ToListAsync<T>().Result;
    }

    public virtual void Save()
    {
        _entities.SaveChanges();
    }

    //Implementing IDisposable correctly http://msdn.microsoft.com/en-us/library/ms244737.aspx
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // The bulk of the clean-up code is implemented in Dispose(bool)
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_entities != null)
            {
                _entities.Dispose();
                _entities = null;
            }
        }
    }
}

}