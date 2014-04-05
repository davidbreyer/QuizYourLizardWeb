using QuizYourLizardApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizYourLizardApi.Repositories
{
    public interface IQuizAccessor : IDisposable, IGenericAccessor<QuizContext, QuizModel>
    {
        IGenericRepository<QuizContext, QuestionModel> QuestionRepository { get; set; }
        IGenericRepository<QuizContext, AnswerModel> AnswerRepository { get; set; }
    }
    //Sample of a custom accessor
    public class QuizAccessor : IDisposable, IQuizAccessor
    {
        public IUnitOfWork<QuizContext> UnitOfWork { get; set; }
        public IGenericRepository<QuizContext, QuizModel> Repository { get; set; }
        public IGenericRepository<QuizContext, QuestionModel> QuestionRepository { get; set; }
        public IGenericRepository<QuizContext, AnswerModel> AnswerRepository { get; set; }

        public QuizAccessor(IUnitOfWork<QuizContext> unitOfWork
            , IGenericRepository<QuizContext, QuizModel> quizRepository
            , IGenericRepository<QuizContext, QuestionModel> questionRepository
            , IGenericRepository<QuizContext, AnswerModel> answerRepository)
        {
            this.UnitOfWork = unitOfWork;
            this.Repository = quizRepository;
            this.QuestionRepository = questionRepository;
            this.AnswerRepository = answerRepository;

            //Add shared context, so all repositories will share the Unit Of Work.
            this.Repository.Context = unitOfWork.DbContext;
            this.QuestionRepository.Context = unitOfWork.DbContext;
            this.AnswerRepository.Context = unitOfWork.DbContext;
        }

        public void Commit()
        {
            UnitOfWork.Commit();
        }

        //Implementing IDisposable correctly http://msdn.microsoft.com/en-us/library/ms244737.aspx
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //This is where the repositories would normally be
                //disposed. But the Unity HierarchicalLifetimeManager should
                //handle that for us.
            }
        }
    }
}