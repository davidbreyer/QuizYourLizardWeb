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
        HttpResponseMessage Post([FromBody]D value);
        HttpResponseMessage Put(Guid id, [FromBody]D value);
        HttpResponseMessage Delete(Guid id);
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
        public HttpResponseMessage Post([FromBody]D value)
        {
            //Validate Request
            if (value == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("The value passed to this service was null.") };
            }

            HttpResponseMessage response;

            try
            {
                var entity = AutoMapper.Mapper.Map<D, T>(value);

                Accessor.Repository.Add(entity);
                Accessor.Commit();

                response = Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest);
                response.Content = new StringContent(ex.GetBaseException().Message);
            }

            return response;
        }
        public HttpResponseMessage Put(Guid id, [FromBody]D value)
        {
            //Validate request
            if (value == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("The value passed to this service was null.") };
            }
            if (id == default(Guid))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("The ID of the entity being updated must not be empty. To create a new entity use POST.") };
            }

            HttpResponseMessage response;

            try
            {
                var entity = AutoMapper.Mapper.Map<D, T>(value);

                entity.Id = id;
                Accessor.Repository.Edit(entity);
                Accessor.Commit();

                response = Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest);
                response.Content = new StringContent(ex.GetBaseException().Message);
            }

            return response;
        }
        public HttpResponseMessage Delete(Guid id)
        {
            var itemToDelete = Accessor.Repository.FindBy(x => x.Id == id).SingleOrDefault();

            Accessor.Repository.Delete(itemToDelete);
            Accessor.Commit();

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
