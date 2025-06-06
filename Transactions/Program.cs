using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using Transactions.Core.Aplication.Transfers.Handlers;
using Transactions.Core.Domain.Aggregates.BatchAggregates;
using Transactions.Core.Infrastructure.Data;
using Transactions.Core.Infrastructure.Repository;
using Transactions.Domain.Services;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddOpenApi();
builder.Services.AddHttpClient<BankAccountService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<TransactionService>();
builder.Services.AddScoped<IBatchTranferItem, BatchTransferItemRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDefault"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();
