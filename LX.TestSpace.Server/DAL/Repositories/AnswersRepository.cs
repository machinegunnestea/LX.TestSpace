using LX.TestSpace.Server.DAL.DbContext;
using LX.TestSpace.Server.DAL.Entities;
using LX.TestSpace.Server.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LX.TestSpace.Server.DAL.Repositories
{
    public class AnswersRepository : IAnswersRepository
    {
        private readonly TestSpaceDbContext testSpaceDbContext;

        public AnswersRepository(TestSpaceDbContext testSpaceDbContext)
        {
            this.testSpaceDbContext = testSpaceDbContext;
        }

        public async Task CreateAsync(Answer entity)
        {
            await this.testSpaceDbContext.Answers.AddAsync(entity);
            await this.testSpaceDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Answer entity)
        {
            this.testSpaceDbContext.Answers.Remove(entity);
            await this.testSpaceDbContext.SaveChangesAsync();
        }

        public async Task DeleteManyAsync(IEnumerable<Answer> entities)
        {
            this.testSpaceDbContext.Answers.RemoveRange(entities);
            await this.testSpaceDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Answer>> GetAllAsync()
        {
            return await this.testSpaceDbContext.Answers
                .Include(x => x.Question)
                .ToListAsync();
        }

        public async Task UpdateAsync(Answer entity)
        {
            testSpaceDbContext.Entry(testSpaceDbContext.Find<Answer>(entity.Id))
                .CurrentValues.SetValues(entity);
            await testSpaceDbContext.SaveChangesAsync();
        }

        public async Task<Answer> GetByIdAsync(int id)
        {
            var result = await this.testSpaceDbContext.Answers.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
    }
}
