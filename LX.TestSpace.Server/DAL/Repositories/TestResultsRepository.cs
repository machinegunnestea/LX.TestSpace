using System.Linq.Expressions;
using LX.TestSpace.Server.BLL.Models.Records;
using LX.TestSpace.Server.DAL.DbContext;
using LX.TestSpace.Server.DAL.Entities;
using LX.TestSpace.Server.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LX.TestSpace.Server.DAL.Repositories
{
    public class TestResultsRepository : ITestResultsRepository
    {
        private TestSpaceDbContext testSpaceDbContext;

        public TestResultsRepository(TestSpaceDbContext testSpaceDbContext)
        {
            this.testSpaceDbContext = testSpaceDbContext;
        }

        public async Task<IEnumerable<TestResult>> GetAllAsync()
        {
            return await this.testSpaceDbContext.TestResults
                .Include(t => t.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<TestResult>> GetAllIncludingTestAsync()
        {
            return await this.testSpaceDbContext.TestResults
                .Include(tr => tr.Test)
                    .ThenInclude(t => t.Questions)
                        .ThenInclude(q => q.Answers)
                .Include(t => t.User)
                .ToListAsync();
        }

        public async Task UpdateAsync(TestResult entity)
        {
            testSpaceDbContext.Entry(testSpaceDbContext.Find<TestResult>(entity.Id))
                .CurrentValues.SetValues(entity);
            await testSpaceDbContext.SaveChangesAsync();
        }

        public async Task CreateAsync(TestResult entity)
        {
            await this.testSpaceDbContext.TestResults.AddAsync(entity);
            await this.testSpaceDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TestResult entity)
        {
            this.testSpaceDbContext.TestResults.Remove(entity);
            await this.testSpaceDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TestResult>> FilterByCriterionAsync(Expression<Func<TestResult, bool>> criterion)
        {
            return await this.testSpaceDbContext.TestResults
                .Include(t => t.User)
                .Where(criterion)
                .ToListAsync();
        }

        public async Task<IEnumerable<TestResult>> FilterByCriterionIncludingTestAsync(Expression<Func<TestResult, bool>> criterion)
        {
            return await this.testSpaceDbContext.TestResults
                .Include(t => t.Test)
                .Include(t => t.User)
                .Where(criterion)
                .ToListAsync();
        }

        public async Task<TestResult> GetByIdAsync(int id)
        {
            var result = await this.testSpaceDbContext.TestResults
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<PaginationData<TestResult>> PaginationFilter(int selectedPage, int itemsPerPage)
        {
            var filterData = await this.testSpaceDbContext.TestResults
                .Include(x => x.User)
                .Include(x => x.Test)
                .OrderByDescending(x => x.FinishedAt)
                .Skip(selectedPage * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();
            var countData = this.testSpaceDbContext.TestResults.Count();
            return new PaginationData<TestResult>
            {
                DataCount = countData,
                FilterData = filterData
            };
        }

        public async Task<PaginationData<TestResult>> PaginationFilterByUserName(int selectedPage, int itemsPerPage, string userName)
        {
            var filterData = await this.testSpaceDbContext.TestResults
               .Include(x => x.User)
               .Include(x => x.Test)
               .OrderByDescending(x => x.FinishedAt)
               .Where(x => x.User.Email == userName)
               .ToListAsync();

            var paginationData = filterData
               .Skip(selectedPage * itemsPerPage)
               .Take(itemsPerPage)
               .ToList();

            var countData = filterData.Count;
            return new PaginationData<TestResult>
            {
                DataCount = countData,
                FilterData = paginationData
            };
        }

        public async Task<PaginationData<TestResult>> PaginationFilterByUserId(int selectedPage, int itemsPerPage, string userId)
        {
            var filterData = await this.testSpaceDbContext.TestResults
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.FinishedAt)
                .Skip(selectedPage * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();
            var countData = await this.testSpaceDbContext.TestResults.Where(e => e.UserId == userId).CountAsync();
            return new PaginationData<TestResult>
            {
                DataCount = countData,
                FilterData = filterData,
            };
        }

        public async Task<PaginationData<TestResult>> PaginationFilterByTestId(int selectedPage, int itemsPerPage, int testId)
        {
            var filterData = await this.testSpaceDbContext.TestResults
                .Where(x => x.TestSnapshot.TestId == testId || x.TestId == testId)
                .OrderByDescending(x => x.FinishedAt)
                .Skip(selectedPage * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();
            var countData = await this.testSpaceDbContext
                .TestResults
                .Where(x => x.TestSnapshot.TestId == testId || x.TestId == testId)
                .CountAsync();
            return new PaginationData<TestResult>
            {
                DataCount = countData,
                FilterData = filterData,
            };
        }

        public async Task<PaginationData<Test>> GetTestsFromSnapshot(int selectedPage, int itemsPerPage)
        {
            var data = await this.testSpaceDbContext
                .TestResults
                .AsNoTracking()
                .Select(e => e.TestSnapshot)
                .OrderBy(e => e.Name)
                .ToListAsync();
            data = data.DistinctBy(e => e.TestId)
                .ToList();
            var count = data.Count;
            data = data.Skip(selectedPage * itemsPerPage)
                .Take(itemsPerPage)
                .ToList();
            var result = new PaginationData<Test>
            {
                DataCount = count,
                FilterData = data.Select(e => new Test
                {
                    Id = e.TestId ?? 0,
                    Name = e.Name,
                    Description = e.Description,
                    Duration = e.Duration,
                    UserNameCreate = e.UserNameCreate,
                    CreationDate = e.CreationDate,
                    IsPublished = true,
                }),
            };
            return result;
        }

        public async Task<TestResult> CreateAndReturn(TestResult testResult)
        {
            var result = await this.testSpaceDbContext.TestResults.AddAsync(testResult);
            await this.testSpaceDbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
