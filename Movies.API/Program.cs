using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Movies.API.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<MoviesAPIContext>(options =>
{
    options.UseInMemoryDatabase("Movies");
});

builder.Services.AddAuthentication("Beaer")
    .AddJwtBearer("Beaer", options =>
    {
        options.Authority = "https://localhost:7086";
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CheckClientId", options =>
    {
        options.RequireClaim("client_id", "movieClient");
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var moviesAPIContext = scope.ServiceProvider.GetRequiredService<MoviesAPIContext>();
        MoviesContextSeed.SeedData(moviesAPIContext);
    }
}

SeedDatabase();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
