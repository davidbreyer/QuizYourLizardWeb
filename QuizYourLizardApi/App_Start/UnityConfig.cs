using Microsoft.Practices.Unity;
using QuizYourLizardApi.Repositories;
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
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IQuizRepository, QuizRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IQuestionRepository, QuestionRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IAnswerRepository, AnswerRepository>(new HierarchicalLifetimeManager());
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}