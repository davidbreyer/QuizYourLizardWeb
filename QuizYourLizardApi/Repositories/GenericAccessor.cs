using QuizYourLizardApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuizYourLizardApi.Repositories
{
    public interface IGenericAccessor<C, T> :
             IDisposable
        where T : PersistentEntity
        where C : DbContext, new()
    {
        IGenericRepository<C, T> Repository { get; set; }
        void Commit();
    }

    public class GenericAccessor<C, T> :
             IDisposable, IGenericAccessor<C,T>
        where T : PersistentEntity
        where C : DbContext, new()
    {
        public IUnitOfWork<C> UnitOfWork { get; set; }
        public IGenericRepository<C,T> Repository { get; set; }

        public GenericAccessor(IUnitOfWork<C> unitOfWork, IGenericRepository<C, T> repository)
        {
            this.UnitOfWork = unitOfWork;
            this.Repository = repository;
            this.Repository.Context = unitOfWork.DbContext;
        }

        public void Commit()
        {
            UnitOfWork.Commit();
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
                //_entities.Dispose();
            }
        }
    }
}