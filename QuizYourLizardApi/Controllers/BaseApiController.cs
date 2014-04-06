using QuizYourLizardApi.Models;
using QuizYourLizardApi.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QuizYourLizardApi.Controllers
{
    public interface IBaseApiController<T>
        where T : PersistentEntity
    {
        IEnumerable<T> Get();
        T Get(Guid id);
        void Post([FromBody]T value);
        void Put(Guid id, [FromBody]T value);
        void Delete(Guid id);
    }

    public abstract class BaseApiController<C, T> : ApiController, IBaseApiController<T>
    where T : PersistentEntity
    where C : DbContext, new()
    {
        protected IGenericAccessor<C, T> Accessor { get; set; }

        public IEnumerable<T> Get()
        {
            var returnValue = Accessor.Repository.GetAllAsync();
            return returnValue.ToList();
        }
        public T Get(Guid id)
        {
            return Accessor.Repository.FindBy(x => x.Id == id).SingleOrDefault();
        }
        public void Post([FromBody]T value)
        {
            Accessor.Repository.Add(value);
            Accessor.Commit();
        }
        public void Put(Guid id, [FromBody]T value)
        {
            value.Id = id;
            
            Accessor.Repository.Edit(value);
            Accessor.Commit();
        }
        public void Delete(Guid id)
        {
            var itemToDelete = Accessor.Repository.FindBy(x => x.Id == id).SingleOrDefault();

            Accessor.Repository.Delete(itemToDelete);
            Accessor.Commit();
        }
    }
}
