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
    public interface IBaseApiController<T, D>
        where T : PersistentEntity
        where D : class
    {
        IEnumerable<D> Get();
        D Get(Guid id);
        void Post([FromBody]T value);
        void Put(Guid id, [FromBody]T value);
        void Delete(Guid id);
    }

    public abstract class BaseApiController<C, T, D> : ApiController, IBaseApiController<T, D>
    where T : PersistentEntity
    where C : DbContext, new()
    where D : class
    {
        protected IGenericAccessor<C, T> Accessor { get; set; }

        public IEnumerable<D> Get()
        {
            var returnValue = Accessor.Repository.GetAllAsync().ToList();
            return AutoMapper.Mapper.Map<IEnumerable<T>, IEnumerable<D>>(returnValue);
        }
        public D Get(Guid id)
        {
            var entity = Accessor.Repository.FindBy(x => x.Id == id).SingleOrDefault();
            var entityPoco = AutoMapper.Mapper.Map<T, D>(entity);

            return entityPoco;
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
