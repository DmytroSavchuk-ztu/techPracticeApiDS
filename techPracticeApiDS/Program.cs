var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

var usersUrl = "http://localhost:5000/api/users"; 

Console.WriteLine($"отримати користувачів: {usersUrl}");

app.UseAuthorization();
app.MapControllers(); 

app.Run();