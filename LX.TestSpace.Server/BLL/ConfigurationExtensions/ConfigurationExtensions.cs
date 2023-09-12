using AutoMapper;
using Firebase.Auth;
using Firebase.Storage;
using LX.TestSpace.Server.BLL.Interfaces;
using LX.TestSpace.Server.BLL.Profiles;
using LX.TestSpace.Server.BLL.Services;
using LX.TestSpace.Server.DAL.Entities;
using LX.TestSpace.Server.DAL.Interfaces;
using LX.TestSpace.Server.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Security.Cryptography;
using System.Security.Policy;

namespace LX.TestSpace.Server.BLL.ConfigurationExtensions
{
    public static class ConfigurationExtensions
    {
        public static void ConfigureBLL(this IServiceCollection services, IConfiguration configuration)
        {
            LX.TestSpace.Server.DAL.ConfigurationExtensions.ConfigurationExtensions.ConfigureDAL(services, configuration);

            services.AddAutoMapper(typeof(AnswerProfile));
            services.AddAutoMapper(typeof(QuestionProfile));
            services.AddAutoMapper(typeof(TestProfile));
            services.AddAutoMapper(typeof(TestResultProfile));
            services.AddAutoMapper(typeof(UserProfile));
            services.AddTransient<IAnswersService, AnswersService>();
            services.AddTransient<IQuestionService, QuestionsService>();
            services.AddTransient<ITestService, TestsService>();
            services.AddTransient<DESCryptoServiceProvider>(options =>
            {
                new DESCryptoServiceProvider();
                var des = new DESCryptoServiceProvider();
                des.Key = System.Text.Encoding.ASCII.GetBytes(configuration.GetValue<string>("EncryptionSettings:Key"));
                des.IV = System.Text.Encoding.ASCII.GetBytes(configuration.GetValue<string>("EncryptionSettings:IV"));
                return des;
            });

            services.AddTransient<ITestResultService, TestResultService>();
            services.AddTransient<IUserService, UserService>();
            services.Configure<FirebaseOptions>(configuration.GetSection(nameof(FirebaseOptions)));
            services.AddTransient<FirebaseStorage>(BuildFirebaseStorageClient);

            async Task<string> FirebaseAuth(FirebaseOptions loginDeatils)
            {
                var auth = new FirebaseAuthProvider(new FirebaseConfig(loginDeatils.ApiKey));
                var authorizationResult = await auth.SignInWithEmailAndPasswordAsync(loginDeatils.AuthEmail, loginDeatils.AuthPass);
                return authorizationResult.FirebaseToken;
            }

            FirebaseStorage BuildFirebaseStorageClient(IServiceProvider option)
            {
                var optionScope = option.CreateScope();
                var firebaseOptions = optionScope.ServiceProvider.GetService<IOptions<FirebaseOptions>>();
                return new FirebaseStorage(firebaseOptions.Value.Url, new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => FirebaseAuth(firebaseOptions.Value),
                    ThrowOnCancel = true, 
                });
            }
        }

    }
}
