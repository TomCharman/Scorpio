using ScorpioData;
using ScorpioData.Services;
using Microsoft.EntityFrameworkCore;
using ScorpioAPI.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options =>
    {
        options.WithOrigins("http://localhost:3000");
        options.AllowCredentials();
    });
});
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICommentService, CommentService>();

var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));
var connectionString = builder.Configuration.GetConnectionString("appDb");
builder.Services.AddDbContext<Context>(options => options.UseMySql(connectionString, serverVersion, mySqlOptions => mySqlOptions.EnableRetryOnFailure())
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors()
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<UpvoteHub>("/hubs/upvote");

app.UseCors(options =>
{
    // This feels dirty
    options.WithOrigins("http://localhost:3000");
    options.AllowAnyMethod();
    options.AllowAnyHeader();
    options.AllowCredentials();
});


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<Context>();
        await Initializer.Initialize(context);
    } catch (Exception e)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(e, "An error occurred creating the DB");
    }
}

app.Run();

