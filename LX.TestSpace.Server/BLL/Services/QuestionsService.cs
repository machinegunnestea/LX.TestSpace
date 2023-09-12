using AutoMapper;
using Firebase.Storage;
using LX.TestSpace.Server.BLL.Interfaces;
using LX.TestSpace.Server.BLL.Models;
using LX.TestSpace.Server.DAL.Entities;
using LX.TestSpace.Server.DAL.Interfaces;
using LX.TestSpace.Server.DAL.Repositories;

namespace LX.TestSpace.Server.BLL.Services
{
    public class QuestionsService : IQuestionService
    {
        private readonly IQuestionsRepository questionsRepository;
        private readonly IMapper mapper;
        private readonly FirebaseStorage firebaseStorage;

        public QuestionsService(IQuestionsRepository questionsRepository, IMapper mapper, FirebaseStorage firebaseStorage)
        {
            this.questionsRepository = questionsRepository;
            this.mapper = mapper;
            this.firebaseStorage = firebaseStorage;
        }

        public async Task CreateAsync(QuestionDTO entity)
        {
            await questionsRepository.CreateAsync(mapper.Map<Question>(entity));
        }

        public async Task<QuestionDTO> CreateAndReturnAsync(QuestionDTO entity)
        {
            var tempQuestion = await questionsRepository.CreateAndReturnAsync(mapper.Map<Question>(entity));
            return mapper.Map<QuestionDTO>(tempQuestion);
        }

        private async Task<string> GetImageUrl(int id, Stream imageStream, string imageExtansion)
        {
            return await firebaseStorage
                .Child("questionImages")
                .Child($"{id}" + $"{imageExtansion}")
                .PutAsync(imageStream);
        }

        public async Task<string> AttachImageToQuestionAsync(int id, Stream imageStream, string imageExtansion)
        {
            var question = await questionsRepository.GetByIdAsync(id);
            var imageUrl = await GetImageUrl(question.Id, imageStream, imageExtansion);
            question.ImagePath = imageUrl;
            await questionsRepository.UpdateAsync(question);
            return imageUrl;
        }

        public async Task DeleteAsync(QuestionDTO entity)
        {
            await questionsRepository.DeleteAsync(mapper.Map<Question>(entity));
        }

        public async Task<IEnumerable<QuestionDTO>> GetAsync()
        {
            return mapper.Map<IEnumerable<QuestionDTO>>(await questionsRepository.GetAllAsync());
        }

        public async Task UpdateAsync(QuestionDTO entity)
        {
            await questionsRepository.UpdateAsync(mapper.Map<Question>(entity));
        }

        public async Task<QuestionDTO> GetByIdAsync(int id)
        {
            return mapper.Map<QuestionDTO>(await questionsRepository.GetByIdAsync(id));
        }
    }
}
