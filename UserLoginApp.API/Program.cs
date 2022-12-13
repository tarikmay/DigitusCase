using UserLoginApp.Business.Helper.Claims;
using UserLoginApp.Business.Helper.Hubs;
using UserLoginApp.Business.IOC;
using UserLoginApp.Business.Middlewares;
using UserLoginApp.DataAccess.Conrete.Mongo.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//SignalR
builder.Services.AddSignalR();

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
        builder => builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .SetIsOriginAllowed((hosts) => true));
});

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
//SignalR
//builder.Services.AddSignalR();

builder.Services.AddTransient<GetOnlineHub>();


var app = builder.Build();
//https://www.bacancytechnology.com/blog/signalr-in-net-core

ClaimManager.HttpContextAccessorConfig(app.Services.GetService<IHttpContextAccessor>());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors("CORSPolicy");


app.UseRouting();

//UserRequestTime 
app.UseMiddleware<UserRequestTimeMiddleware>();

app.UseAuthentication();

app.UseAuthorization();



//OnlineUser
app.UseMiddleware<UserOnlineMiddleware>();

//SignalR
app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
    endpoints.MapHub<GetOnlineHub>("/OnlineCount");

});




app.Run();
