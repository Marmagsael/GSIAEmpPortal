using MysqlApi.StartupConfig;

var builder = WebApplication.CreateBuilder(args);
builder.AddServices(); 
builder.AddInjectionServices();
builder.AddCors();
builder.AddAuthenticationServices();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("FreeForAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
