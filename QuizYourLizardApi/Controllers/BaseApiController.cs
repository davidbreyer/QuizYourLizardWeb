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
                ModelState.AddModelError("error", "The value passed to this service was null.");
            }

            HttpResponseMessage response;

            try
            {
                if (ModelState.IsValid)
                {
                    var entity = AutoMapper.Mapper.Map<D, T>(value);

                    Accessor.Repository.Add(entity);
                    Accessor.Commit();

                    response = Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }

            return response;
        }
        public HttpResponseMessage Put(Guid id, [FromBody]D value)
        {
            //Validate request
            if (value == null)
            {
                //return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("The value passed to this service was null.") };
                ModelState.AddModelError("error", "The value passed to this service was null.");
            }

            HttpResponseMessage response;

            try
            {
                if (ModelState.IsValid)
                {
                    var entity = AutoMapper.Mapper.Map<D, T>(value);

                    entity.Id = id;
                    if (entity.Id == default(Guid))
                    {
                        Accessor.Repository.Add(entity);
                    }
                    else
                    {
                        Accessor.Repository.Edit(entity);
                    }
                    Accessor.Commit();

                    response = Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }

            return response;
        }
        public HttpResponseMessage Delete(Guid id)
        {
            //Validate Request
            if(id == default(Guid))
            {
                ModelState.AddModelError("error", @"Specified ID is not valid.");
            }

            HttpResponseMessage response;

            try
            {
                var itemToDelete = Accessor.Repository.FindBy(x => x.Id == id).SingleOrDefault();
                if(itemToDelete == null) { ModelState.AddModelError("error", string.Format("No {0} found to delete with ID {1}.", typeof(T).Name, id)); }

                if (ModelState.IsValid)
                {
                    Accessor.Repository.Delete(itemToDelete);
                    Accessor.Commit();

                    response = Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch(Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.GetBaseException().Message);
            }

            return response;
        }
    }
}
