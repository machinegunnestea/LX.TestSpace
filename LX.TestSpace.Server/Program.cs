using System.Security.Claims;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;
using LX.TestSpace.Server.BLL.ConfigurationExtensions;
using LX.TestSpace.Server.DAL.DbContext;
using LX.TestSpace.Server.DAL.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;
using LX.TestSpace.Server.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.ConfigureBLL(builder.Configuration);
builder.Services.AddIdentity<User, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;

        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredUniqueChars = 0;
    })
    .AddEntityFrameworkStores<TestSpaceDbContext>();

builder.Services.AddIdentityServer(
        options => { options.UserInteraction.LoginUrl = "/Identity/Account/Login"; })
    .AddApiAuthorization<User, TestSpaceDbContext>(
        options =>
        {
            options.IdentityResources.Add(new IdentityResource("roles", "Roles",
                new[] { JwtClaimTypes.Role, ClaimTypes.Role }));

            options.Clients.AddSPA("LX.TestSpace.Client", clientBuilder =>
            {
                clientBuilder.WithoutClientSecrets()
                    .WithRedirectUri("https://localhost:7267/authentication/login-callback")
                    .WithLogoutRedirectUri("https://localhost:7267/authentication/logout-callback");
            });

            foreach (var c in options.Clients)
            {
                c.AllowedScopes.Add("roles");
            }

            foreach (var a in options.ApiResources)
            {
                a.UserClaims.Add(JwtClaimTypes.Role);
            }
        });

builder.Services
    .AddAuthentication(IdentityServerConstants.LocalApi.PolicyName)
    .AddLocalApi()
    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

var scope = app.Services.GetService<IServiceScopeFactory>();
await using (var serviceScope = scope.CreateAsyncScope())
{
    var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
    await userManager.CreateAsync(new User
    {
        Email = "1@1.com",
        Name = "1",
        EmailConfirmed = true,
        UserName = "1@1.com",
        LockoutEnabled = false
    }, "123456");

    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await roleManager.CreateAsync(new IdentityRole("User"));
    await roleManager.CreateAsync(new IdentityRole("Admin"));

    var user = await userManager.FindByEmailAsync("1@1.com");
    var userRoles = await userManager.GetRolesAsync(user);
    var roles = roleManager.Roles.Select(role => role.Name);
    await userManager.AddToRolesAsync(user, roles);
}

app.UseCors(options =>
{
    options.AllowCredentials().AllowAnyHeader().AllowAnyMethod()
        .WithOrigins("http://localhost:5282", "https://localhost:7267");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseIdentityServer();
app.MapControllers();
app.MapRazorPages();

app.Run();
