using System.Net;
using TrainingApp;
using TrainingApp.Entities;
using TrainingApp.Services;
using NLog.Web;
using TrainingApp.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddNLog("nlog.config");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TrainingDbContext>();
builder.Services.AddScoped<Seeder>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<ITraineeService, TraineeService>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IExerciseService, ExerciseService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();

builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
    options.HttpsPort = 5001;
});

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

// Seed the database with the initial data

SeedDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


// seed the database with the initial data
void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
        seeder.Seed();
    }
}