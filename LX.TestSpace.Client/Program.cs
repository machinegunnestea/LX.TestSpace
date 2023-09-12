using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using LX.TestSpace.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using MudBlazor.Services;
using LX.TestSpace.Client.HttpRepository;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using LX.TestSpace.Client.Entities;
using LX.TestSpace.Client.Services.Authorization;
using TextCopy;
using LX.TestSpace.Client.HttpRepository.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.InjectClipboard();
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Local", options.ProviderOptions);
    options.ProviderOptions.DefaultScopes.Add("LX.TestSpace.ServerAPI");
    options.ProviderOptions.DefaultScopes.Add("roles");
    // options.UserOptions.AuthenticationType = "LX.TestSpace.ServerApi";
    options.ProviderOptions.ResponseMode = "fragment";
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.PostLogoutRedirectUri = "https://localhost:7267/authentication/logout-callback";
    // options.UserOptions.RoleClaim = "role";
})
    .AddAccountClaimsPrincipalFactory<RemoteAuthenticationState, CustomProfileFactory>();
builder.Services.AddMudServices();

builder.Services.AddScoped(ApiUrl => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiUrl"))});
builder.Services.AddApiAuthorization();
builder.Services.AddTransient<CustomAuthorizationMessageHandler>();
builder.Services.AddHttpClient("public", client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiUrl")));
builder.Services.AddHttpClient("WebAPI",
        client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiUrl")))
    .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();
builder.Services.AddScoped<IAnswersHttpClient, AnswersHttpClient>();
builder.Services.AddScoped<IQuestionsHttpClient, QuestionsHttpClient>();
builder.Services.AddScoped<ITestResultsHttpClient, TestResultsHttpClient>();
builder.Services.AddScoped<ITestsHttpClient, TestsHttpClient>();
builder.Services.AddScoped<IUserHttpClient, UserHttpClient>();

await builder.Build().RunAsync();
