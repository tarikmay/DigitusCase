using UserLoginApp.Business.Helper.Claims;
using UserLoginApp.Business.IOC;
using UserLoginApp.Business.Middlewares;
using UserLoginApp.DataAccess.Conrete.Mongo.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//MongoDbSettings
builder.Services.Configure<MongoSettingsModel>(
    builder.Configuration.GetSection("MongoDbConn"));

//CustomDependencies
builder.Services.AddCustomDependencies(builder.Configuration);

//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//HttpContextAccessor
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

ClaimManager.HttpContextAccessorConfig(app.Services.GetService<IHttpContextAccessor>());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


//UserRequestTime 
app.UseMiddleware<UserRequestTimeMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//OnlineUser
app.UseMiddleware<UserOnlineMiddleware>();




app.Run();
