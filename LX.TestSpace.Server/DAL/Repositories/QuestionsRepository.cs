using LX.TestSpace.Server.DAL.DbContext;
using LX.TestSpace.Server.DAL.Entities;
using LX.TestSpace.Server.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LX.TestSpace.Server.DAL.Repositories
{
    public class QuestionsRepository : IQuestionsRepository
    {
        private readonly TestSpaceDbContext testSpaceDbContext;

        public QuestionsRepository(TestSpaceDbContext testSpaceDbContext)
        {
            this.testSpaceDbContext = testSpaceDbContext;
        }

        public async Task CreateAsync(Question entity)
        {
            await this.testSpaceDbContext.Questions.AddAsync(entity);
            await this.testSpaceDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Question entity)
        {
            this.testSpaceDbContext.Questions.Remove(entity);
            await this.testSpaceDbContext.SaveChangesAsync();
        }

        public async Task DeleteManyAsync(IEnumerable<Question> entities)
        {
            this.testSpaceDbContext.Questions.RemoveRange(entities);
            await this.testSpaceDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Question>> GetAllAsync()
        {
            return await this.testSpaceDbContext.Questions
                .Include(x => x.Test)
                .Include(x => x.Answers)
                .ToListAsync();
        }

        public async Task UpdateAsync(Question entity)
        {
            testSpaceDbContext.Entry(testSpaceDbContext.Find<Question>(entity.Id))
                .CurrentValues.SetValues(entity);
            await this.testSpaceDbContext.SaveChangesAsync();
        }

        public async Task<Question> GetByIdAsync(int id)
        {
            var result = await this.testSpaceDbContext.Questions.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<Question> CreateAndReturnAsync(Question entity)
        {
            var result = await this.testSpaceDbContext.Questions.AddAsync(entity);
            await this.testSpaceDbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
