﻿using Microsoft.Practices.Unity;
using QuizYourLizardApi.CrossCutting;
using QuizYourLizardApi.Models;
using QuizYourLizardApi.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;

namespace QuizYourLizardApi.Proxies
{
    public interface IBaseProxy<T>
        where T : ClientEntity
    {
        HttpClient Client { get; set; }
        List<T> GetAllEntities();
        HttpResponseMessage CreateNewEntity(T entity);
        T GetEntityById(Guid id);
        HttpResponseMessage UpdateEntity(T entity);
        HttpResponseMessage DeleteEntity(Guid id);

    }

    public class BaseProxy<T> : IBaseProxy<T>
        where T : ClientEntity, new()
    {
        [Dependency]
        public HttpClient Client { get; set; }
        public string ApiUri { get; set; }

        public BaseProxy()
        {
            var entity = new T();
            ApiUri = entity.ApiUri;
        }

        public virtual List<T> GetAllEntities()
        {
            var model = Client.GetAsync(ApiUri).Result
                    .Content.ReadAsAsync<List<T>>().Result;

            return model;
        }

        public virtual HttpResponseMessage CreateNewEntity(T entity)
        {
            var result = Client.PostAsync(ApiUri, entity
                        , new JsonMediaTypeFormatter()).Result;

            return result;
        }

        public virtual T GetEntityById(Guid id)
        {
            var model = Client.GetAsync(string.Format(@"{0}/{1}", ApiUri, id)).Result
                .Content.ReadAsAsync<T>().Result;

            return model;
        }

        public virtual HttpResponseMessage UpdateEntity(T entity)
        {
            var result = Client.PutAsync(string.Format(@"{0}/{1}", ApiUri, entity.Id), entity
                   , new JsonMediaTypeFormatter()).Result;

            return result;
        }

        public virtual HttpResponseMessage DeleteEntity(Guid id)
        {
            var result = Client.DeleteAsync(string.Format(@"{0}/{1}", ApiUri, id)).Result;

            return result;
        }
    }
}