using LX.TestSpace.Server.DAL.DbContext;
using LX.TestSpace.Server.DAL.Interfaces;
using LX.TestSpace.Server.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LX.TestSpace.Server.DAL.ConfigurationExtensions
{
    public static class ConfigurationExtensions
    {
        public static void ConfigureDAL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TestSpaceDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                optionsBuilder => optionsBuilder.EnableRetryOnFailure(10));
            });

            services.AddScoped<IAnswersRepository, AnswersRepository>();
            services.AddScoped<IQuestionsRepository, QuestionsRepository>();
            services.AddScoped<ITestRepository, TestsRepository>();
            services.AddScoped<ITestResultsRepository, TestResultsRepository>();
        }
    }
}
