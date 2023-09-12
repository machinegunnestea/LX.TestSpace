using AutoMapper;
using LX.TestSpace.Server.BLL.Interfaces;
using LX.TestSpace.Server.BLL.Models;
using LX.TestSpace.Server.DAL.Entities;
using LX.TestSpace.Server.DAL.Interfaces;
using LX.TestSpace.Server.DAL.Repositories;

namespace LX.TestSpace.Server.BLL.Services
{
    public class AnswersService : IAnswersService
    {
        private readonly IAnswersRepository _answersRepository;
        private readonly IMapper mapper;

        public AnswersService(IAnswersRepository answersRepository, IMapper mapper)
        {
            _answersRepository = answersRepository;
            this.mapper = mapper;
        }

        public async Task CreateAsync(AnswerDTO entity)
        {
            await _answersRepository.CreateAsync(mapper.Map<Answer>(entity));
        }

        public async Task DeleteAsync(AnswerDTO entity)
        {
            await _answersRepository.DeleteAsync(mapper.Map<Answer>(entity));
        }

        public async Task<IEnumerable<AnswerDTO>> GetAsync()
        {
            return mapper.Map<IEnumerable<AnswerDTO>>(await _answersRepository.GetAllAsync());
        }

        public async Task UpdateAsync(AnswerDTO entity)
        {
            await _answersRepository.UpdateAsync(mapper.Map<Answer>(entity));
        }
        public async Task<AnswerDTO> GetByIdAsync(int id)
        {
            return mapper.Map<AnswerDTO>(await _answersRepository.GetByIdAsync(id));
        }
    }
}
