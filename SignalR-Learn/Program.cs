using SignalR_Learn.Business;
using SignalR_Learn.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region SignalR ��in Gerekenler
builder.Services.AddTransient<MyBusiness>(); //MyBusiness s�n�f�n� talep etti�imizde websoketin �al��mas�n� sa�lar.
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
