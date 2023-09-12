using Duende.IdentityServer.EntityFramework.Options;
using LX.TestSpace.Server.DAL.Entities;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace LX.TestSpace.Server.DAL.DbContext
{
    public class TestSpaceDbContext : ApiAuthorizationDbContext<User>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public TestSpaceDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<TestResult>()
                .OwnsOne(testResult => testResult.TestSnapshot, builder => builder.ToJson())
                .OwnsMany(testResult => testResult.QuestionSnapshots, builder =>
                {
                    builder.ToJson();
                    builder.OwnsMany(questionJsons => questionJsons.UserAnswersSnapshots, builder => builder.ToJson());
                })
                .HasOne(t => t.User)
                .WithMany(u => u.TestResults)
                .HasForeignKey(t => t.UserId);
            modelBuilder
                .Entity<Question>(builder =>
                {
                    builder
                    .HasOne(t => t.Test)
                    .WithMany(u => u.Questions)
                    .HasForeignKey(t => t.TestId);
                });
            modelBuilder
               .Entity<Answer>(builder =>
               {
                   builder
                   .HasOne(t => t.Question)
                   .WithMany(u => u.Answers)
                   .HasForeignKey(t => t.QuestionId);
               });
        }
    }
}
