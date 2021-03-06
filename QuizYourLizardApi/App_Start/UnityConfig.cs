using Microsoft.Practices.Unity;
using QuizYourLizardApi.CrossCutting;
using QuizYourLizardApi.Models;
using QuizYourLizardApi.Pocos;
using QuizYourLizardApi.Proxies;
using QuizYourLizardApi.Repositories;
using System;
using System.Configuration;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Unity.WebApi;

namespace QuizYourLizardApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<HttpClient>(
                new InjectionFactory(x =>
                    new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]) }
                )
            );
            container.RegisterType(typeof(IBaseProxy<>), typeof(BaseProxy<>), new HierarchicalLifetimeManager());
            container.RegisterType(typeof(IGenericRepository<,>), typeof(GenericRepository<,>), new HierarchicalLifetimeManager());
            container.RegisterType(typeof(IGenericAccessor<,>), typeof(GenericAccessor<,>), new HierarchicalLifetimeManager());
            container.RegisterType(typeof(IUnitOfWork<>), typeof(UnitOfWork<>), new HierarchicalLifetimeManager());

            //This Unity container will resolve MVC 5 Controllers.
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));

            //This will resolve Web Api controllers.
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

            //AutoMapper Mappings
            AutoMapper.Mapper.CreateMap<QuestionModel, QuestionDto>()
                .ForMember(x=>x.QuizName, opt=>opt.MapFrom(x=>x.Quiz.Name));
            AutoMapper.Mapper.CreateMap<QuestionDto, QuestionModel>();
            AutoMapper.Mapper.CreateMap<AnswerModel, AnswerDto>()
                .ForMember(x => x.QuestionText, opt => opt.MapFrom(x => x.Question.Text));
            AutoMapper.Mapper.CreateMap<AnswerDto, AnswerModel>();
        }
    }
}