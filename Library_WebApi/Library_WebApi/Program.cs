using Library_WebApi.Commands;
using Library_WebApi.Context;
using Library_WebApi.Interfaces;
using Library_WebApi.Queries;
using Library_WebApi.Repositories;
using Library_WebApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register all command and query handlers in MediatR 
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(AddBookCommand).Assembly,
                                                                  typeof(BorrowBookCommand).Assembly,
                                                                  typeof(DeleteBookCommand).Assembly,
                                                                  typeof(ReturnBookCommand).Assembly,
                                                                  typeof(GetAllBooksQuery).Assembly));

// Configure the DbContext to use an in-memory database named "LibraryDb".
builder.Services.AddDbContext<LibraryDbContext>(
    opts => opts.UseInMemoryDatabase("LibraryDb")
);

// Register services with Scoped lifecycle
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IMappingService, MappingService>();

// Configure CORS policy to allow requests from the React app
var allowedOrigins = builder.Configuration.GetValue<string>("AllowedOrigins");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        // Allow requests from the specified origin
        builder.WithOrigins(allowedOrigins)
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Apply the CORS policy to the app
app.UseCors("AllowReactApp");

app.MapControllers();

app.Run();
