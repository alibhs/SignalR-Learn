using SignalR_Learn.Business;
using SignalR_Learn.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region SignalR Ýçin Gerekenler
builder.Services.AddTransient<MyBusiness>(); //MyBusiness sýnýfýný talep ettiðimizde websoketin çalýþmasýný saðlar.
builder.Services.AddSignalR();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHub<MyHub>("/myHub");
app.MapHub<MessageHub>("/messageHub");

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
