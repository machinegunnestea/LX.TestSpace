using System.Linq.Expressions;

using LX.TestSpace.Server.DAL.DbContext;
using LX.TestSpace.Server.DAL.Entities;
using LX.TestSpace.Server.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LX.TestSpace.Server.DAL.Repositories
{
    public class TestsRepository : ITestRepository
    {

        private readonly TestSpaceDbContext testSpaceDbContext;

        public TestsRepository(TestSpaceDbContext testSpaceDbContext)
        {
            this.testSpaceDbContext = testSpaceDbContext;
        }

        public async Task CreateAsync(Test entity)
        {
            await this.testSpaceDbContext.Tests.AddAsync(entity);
            await this.testSpaceDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Test entity)
        {
            var testResult = testSpaceDbContext.TestResults.Where(x => x.TestId == entity.Id).ToList();
            foreach (var item in testResult)
            {
                item.TestId = null;
            }

            testSpaceDbContext.UpdateRange(testResult);
            this.testSpaceDbContext.Tests.Remove(entity);
            await this.testSpaceDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Test>> FilterByCriterion(Expression<Func<Test, bool>> criterion)
        {
            return await this.testSpaceDbContext.Tests
                .Include(t => t.Questions)
                    .ThenInclude(q => q.Answers)
                .Include(t => t.TestResults)
                    .ThenInclude(tr => tr.User)
                .Where(criterion)
                .ToListAsync();
        }

        public async Task<IEnumerable<Test>> GetAllAsync()
        {
            return await this.testSpaceDbContext.Tests
                .Include(t => t.Questions)
                    .ThenInclude(q => q.Answers)
                .Include(t => t.TestResults)
                    .ThenInclude(tr => tr.User)
                .ToListAsync();
        }

        public async Task UpdateAsync(Test entity)
        {

            var test = testSpaceDbContext.Find<Test>(entity.Id);
            testSpaceDbContext.Entry(test).CurrentValues.SetValues(entity);
            test.Questions = entity.Questions;
            await this.testSpaceDbContext.SaveChangesAsync();
        }

        public async Task<Test> GetByIdAsync(int id)
        {
            var result = await this.testSpaceDbContext.Tests
                 .Include(t => t.Questions)
                    .ThenInclude(q => q.Answers)
                 .AsNoTracking()
                 .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<Test> CreateAndReturn(Test test)
        {
            var result = await this.testSpaceDbContext.Tests.AddAsync(test);
            await this.testSpaceDbContext.SaveChangesAsync();
            return result.Entity; 
        }
    }
}
