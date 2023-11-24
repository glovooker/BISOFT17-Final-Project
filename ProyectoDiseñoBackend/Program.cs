using ProyectoDiseñoBackend.Servicios;
using ProyectoDiseñoBackend.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<MongoDBInstance>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    return new MongoDBInstance(configuration);
});

builder.Services.AddSingleton<BandService>();
builder.Services.AddSingleton<ClientService>();
builder.Services.AddSingleton<ProjectService>();
builder.Services.AddSingleton<EmployeeService>();
builder.Services.AddSingleton<AccountService>();
builder.Services.AddSingleton<BillingService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
