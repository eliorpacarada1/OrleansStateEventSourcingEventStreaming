using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using OrleansTest.Grains;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseOrleans(options =>
{
    options.Configure((Action<ClusterOptions>)(o =>
    {
        o.ClusterId = "dev";
        o.ServiceId = "dev";
    }));
    options.UseAdoNetClustering(x =>
    {
        x.Invariant = "Npgsql";
        x.ConnectionString = builder.Configuration.GetConnectionString("DatabaseConnectionString");
    });
    options.AddAdoNetGrainStorage("orleansStorage", x =>
    {
        x.Invariant = "Npgsql";
        x.ConnectionString = builder.Configuration.GetConnectionString("DatabaseConnectionString");
        x.UseJsonFormat = true;
    });
    options.ConfigureEndpoints
    (
        siloPort: 11111,
        gatewayPort: 30000,
        listenOnAnyHostAddress: true
    );
    options.AddSimpleMessageStreamProvider("betProvider")
           .AddMemoryGrainStorage("betStorage");
    options.ConfigureApplicationParts
    (
        parts => parts.AddApplicationPart(typeof(BetGrain).Assembly).WithReferences()
    );
    options.AddLogStorageBasedLogConsistencyProvider("testLogStorage");
    options.UseDashboard(options =>
    {
        options.Username = "USERNAME";
        options.Password = "PASSWORD";
        options.Host = "*";
        options.Port = 8080;
        options.HostSelf = true;
        options.CounterUpdateIntervalMs = 1000;
    });
});

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