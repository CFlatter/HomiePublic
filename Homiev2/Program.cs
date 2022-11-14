using Homiev2.Data.Contexts;
using Homiev2.Data.Repositories;
using Homiev2.Domain;
using Homiev2.Shared.Interfaces.Repositories;
using Homiev2.Shared.Interfaces.Services;
using Homiev2.Shared.Models;
using Homiev2.Shared.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ShareCode>(builder.Configuration.GetSection(nameof(ShareCode)));
builder.Services.Configure<Token>(builder.Configuration.GetSection(nameof(Token)));
builder.Services.AddTransient<IHouseholdRepository, HouseholdRepository>();
builder.Services.AddTransient<IHouseholdService, HouseholdService>();
builder.Services.AddTransient<IHouseholdMemberRepository, HouseholdMemberRepository>();
builder.Services.AddTransient<IHouseholdMemberService, HouseholdMemberService>();
builder.Services.AddTransient<IHouseholdCreationService, HouseholdCreationService>();
builder.Services.AddTransient<IChoreFrequencyRepository, ChoreFrequencyRepository>();
builder.Services.AddTransient<IChoreFrequencyService, ChoreFrequencyService>();
builder.Services.AddTransient<IChoreLogRepository, ChoreLogRepository>();
builder.Services.AddTransient<IChoreLogService, ChoreLogService>();
builder.Services.AddTransient<IChoreRepository, ChoreRepository>();
builder.Services.AddTransient<IChoreService, ChoreService>();
builder.Services.AddTransient<IAuthUsersRepository, AuthUsersRepository>();

builder.Services.AddIdentity<AuthUser, IdentityRole>(opt =>
{
    opt.Password.RequiredUniqueChars = 6;
    opt.Password.RequiredLength = 10;
})
    .AddEntityFrameworkStores<Homiev2Context>();

builder.Services.AddAuthentication()
    .AddJwtBearer(cfg =>
    {
        cfg.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = builder.Configuration.GetValue<string>("Token:Issuer"),
            ValidAudience = builder.Configuration.GetValue<string>("Token:Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Token:Key")))
        };
    });

builder.Services.AddDbContext<Homiev2Context>(opt =>
     opt.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
);



//builder.Services.AddDbContext<Homiev2Context>(opt =>
//{
//    var conn = builder.Configuration.GetConnectionString("localdb");
//    opt.UseMySQL(NormalizeAzureInAppConnString(conn))
//    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
//});

//string NormalizeAzureInAppConnString(string raw)
//{
//    string conn = string.Empty;
//    try
//    {
//        var dict =
//             raw.Split(';')
//                 .Where(kvp => kvp.Contains('='))
//                 .Select(kvp => kvp.Split(new char[] { '=' }, 2))
//                 .ToDictionary(kvp => kvp[0].Trim(), kvp => kvp[1].Trim(), StringComparer.InvariantCultureIgnoreCase);
//        var ds = dict["Data Source"];
//        var dsa = ds.Split(":");
//        conn = $"Server={dsa[0]};Port={dsa[1]};Database={dict["Database"]};Uid={dict["User Id"]};Pwd={dict["Password"]};";
//    }
//    catch
//    {
//        throw new Exception("unexpected connection string: datasource is empty or null");
//    }
//    return conn;
//}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler(appBuilder =>
    {
        appBuilder.Run(async context =>
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("An unexpected error occured. Try again later");
        });
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
