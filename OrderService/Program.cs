using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Extensions;
using OrderService.Services;
using OrderService.Services.IService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DB CONNECTION
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myconnection"));
});

///FROM EXTENSIONS
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.AddAuth();
builder.AddSwaggenGenExtension();

//
builder.Services.AddScoped<IArt, ArtService>();
builder.Services.AddScoped<IBid, BidService>();
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IOrder, OrdersService>();

//
builder.Services.AddHttpClient("User", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURl:AuthService")));
builder.Services.AddHttpClient("Art", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURl:ArtService")));
builder.Services.AddHttpClient("Bid", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURl:BidService")));

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
