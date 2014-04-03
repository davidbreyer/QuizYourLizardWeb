using QuizYourLizardApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuizYourLizardApi.Repositories
{
    public interface IUnitOfWork<C> :
         IDisposable
         where C : DbContext, new()
    {
        C DbContext { get; }
        int Commit();
    }

    public class UnitOfWork<C> :
         IDisposable, IUnitOfWork<C>
         where C : DbContext, new()
    {
        private C context;

        public C DbContext
        {
            get
            {
                if (context == null)
                {
                    context = new C();
                }
                return context;
            }
        }

        public int Commit()
        {
            return context.SaveChangesAsync().Result;
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}