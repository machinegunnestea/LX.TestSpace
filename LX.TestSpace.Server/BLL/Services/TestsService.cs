using AutoMapper;
using IdentityModel;
using LX.TestSpace.Server.BLL.Interfaces;
using LX.TestSpace.Server.BLL.Models;
using LX.TestSpace.Server.DAL.Entities;
using LX.TestSpace.Server.DAL.Interfaces;
using LX.TestSpace.Server.DAL.Repositories;
using Microsoft.Identity.Client;

namespace LX.TestSpace.Server.BLL.Services
{
    public class TestsService : ITestService
    {
        private readonly ITestRepository testRepository;
        private readonly ITestResultsRepository testResultRepository;
        private readonly IMapper mapper;
        private readonly IAnswersRepository answersRepository;
        private readonly IQuestionsRepository questionsRepository;

        public TestsService(ITestRepository testRepository, ITestResultsRepository testResultRepository, IMapper mapper, IAnswersRepository answersRepository, IQuestionsRepository questionsRepository)
        {
            this.testRepository = testRepository;
            this.testResultRepository = testResultRepository;
            this.mapper = mapper;
            this.answersRepository = answersRepository;
            this.questionsRepository = questionsRepository;
        }

        public async Task CreateAsync(TestDTO entity)
        {
            await testRepository.CreateAsync(mapper.Map<Test>(entity));
        }

        public async Task DeleteAsync(TestDTO entity)
        {
            await testRepository.DeleteAsync(mapper.Map<Test>(entity));
        }

        public async Task<IEnumerable<TestDTO>> GetAsync()
        {
            return mapper.Map<IEnumerable<TestDTO>>(await testRepository.GetAllAsync());
        }

        public async Task UpdateAsync(TestDTO entity)
        {
            var test = await testRepository.GetByIdAsync(entity.Id);

            var questionsDtoForRemove = test.Questions.ExceptBy(entity.Questions.Where(x => x.Id != 0).Select(e => e.Id), t => t.Id);
            var allAnswersDto = test.Questions.SelectMany(question => question.Answers);
            var entityAnswersDto = entity.Questions.SelectMany(eQuestion => eQuestion.Answers);
            var answersDtoForRemove = allAnswersDto.ExceptBy(entityAnswersDto.Where(x => x.Id != 0).Select(eAnswer => eAnswer.Id), answer => answer.Id);

            await answersRepository.DeleteManyAsync(answersDtoForRemove);
            await questionsRepository.DeleteManyAsync(questionsDtoForRemove);

            await testRepository.UpdateAsync(mapper.Map<Test>(entity));
        }

        public async Task<TestDTO> GetByIdAsync(int id)
        {
            return mapper.Map<TestDTO>(await testRepository.GetByIdAsync(id));
        }

        public async Task<TestDTO> GetByIdAsync(int id, bool includeDetails)
        {
            var test = mapper.Map<TestDTO>(await testRepository.GetByIdAsync(id));
            if (includeDetails)
            {
                return test;
            }

            var testWithNullIsCorrect = this.SetNullCorrectAnswers(test);
            return testWithNullIsCorrect;
        }

        private TestDTO SetNullCorrectAnswers(TestDTO test)
        {
            foreach (var question in test.Questions)
            {
                foreach (var answer in question.Answers)
                {
                    answer.IsCorrect = null;
                }
            }

            return test;
        }

        public async Task<TestDTO> CreateAndReturnEntityAsync(TestDTO test)
        {
            var result = await testRepository.CreateAndReturn(mapper.Map<Test>(test));
            return mapper.Map<TestDTO>(result);
        }

        public async Task<IEnumerable<TestDTO>> GetPublishedTests()
        {
            return this.mapper.Map<IEnumerable<TestDTO>>(await this.testRepository.FilterByCriterion(x => x.IsPublished));
        }

        public async Task<IEnumerable<TestDTO>> SearchTestByTitle(string testTitle)
        {
            return testTitle == string.Empty ? this.mapper.Map<IEnumerable<TestDTO>>(await this.testRepository.GetAllAsync()) : this.mapper.Map<IEnumerable<TestDTO>>(await this.testRepository.FilterByCriterion(x => x.Name.Contains(testTitle) && x.IsPublished));
        }
    }
}
