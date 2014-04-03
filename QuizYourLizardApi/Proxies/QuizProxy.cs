using Microsoft.Practices.Unity;
using QuizYourLizardApi.CrossCutting;
using QuizYourLizardApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;

namespace QuizYourLizardApi.Proxies
{
    public interface IQuizProxy
    {
        HttpClient Client { get; set; }
        List<QuizModel> GetAllQuizzes();
        HttpResponseMessage CreateNewQuiz(QuizModel quiz);
        QuizModel GetQuizById(Guid id);
        HttpResponseMessage UpdateQuiz(QuizModel quiz);
        HttpResponseMessage DeleteQuiz(Guid id);
    }

    public class QuizProxy : IQuizProxy
    {
        [Dependency]
        public HttpClient Client { get; set; }


        public List<QuizModel> GetAllQuizzes()
        {
            var model = Client.GetAsync(Constants.QuizApiUri).Result
                    .Content.ReadAsAsync<List<QuizModel>>().Result;

            return model;
        }

        public HttpResponseMessage CreateNewQuiz(QuizModel quiz)
        {
            var result = Client.PostAsync(Constants.QuizApiUri, quiz
                        , new JsonMediaTypeFormatter()).Result;

            return result;
        }

        public QuizModel GetQuizById(Guid id)
        {
            var model = Client.GetAsync(string.Format(@"{0}/{1}", Constants.QuizApiUri, id)).Result
                .Content.ReadAsAsync<QuizModel>().Result;

            return model;
        }

        public HttpResponseMessage UpdateQuiz(QuizModel quiz)
        {
            var result = Client.PutAsync(string.Format(@"{0}/{1}", Constants.QuizApiUri, quiz.Id), quiz
                   , new JsonMediaTypeFormatter()).Result;

            return result;
        }

        public HttpResponseMessage DeleteQuiz(Guid id)
        {
            var result = Client.DeleteAsync(string.Format(@"{0}/{1}", Constants.QuizApiUri, id)).Result;

            return result;
        }
    }
}