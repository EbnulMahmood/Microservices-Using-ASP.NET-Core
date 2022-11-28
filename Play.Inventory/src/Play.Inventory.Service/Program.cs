using MassTransit;
using Play.Common.Service.IRepositories;
using Play.Common.Service.MongoDB;
using Play.Inventory.Service.Consumers;
using Play.Inventory.Service.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMongo(builder.Configuration);

builder.Services.AddSingleton(typeof(IRepository<>), typeof(MongoRepository<>));

builder.Services.AddCatalogHttpClient("http://localhost:5189");

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((cxt, cfg) =>
    {
        cfg.ReceiveEndpoint("catalog-items", e =>
        {
            e.Consumer<CatalogItemCreatedConsumer>(cxt);
            e.Consumer<CatalogItemUpdatedConsumer>(cxt);
            e.Consumer<CatalogItemDeletedConsumer>(cxt);
        });
    });
});

builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});
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
