using Microsoft.Practices.Unity;
using QuizYourLizardApi.CrossCutting;
using QuizYourLizardApi.Repositories;
using System;
using System.Net.Http;
using System.Web.Http;
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

            //Uri url = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
            //string _endPoint = url.GetLeftPart(UriPartial.Authority);
            //string _endPoint = "http://localhost:29323";

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IQuizRepository, QuizRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IQuestionRepository, QuestionRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IAnswerRepository, AnswerRepository>(new HierarchicalLifetimeManager());
            //container.RegisterType<IBaseClient, BaseClient>(new HierarchicalLifetimeManager(), new InjectionProperty("BaseAddress", new Uri(_endPoint)));
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}