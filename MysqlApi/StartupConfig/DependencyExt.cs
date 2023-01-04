using MysqlApiLibrary.DataAccess;
using MysqlApiLibrary.DataAccess._100Main;
using MysqlApiLibrary.DataAccess.Login;

namespace MysqlApi.StartupConfig; 

public static class DependencyExt
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        
        builder.Services.AddSwaggerGen();

    }

    public static void AddInjectionServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IMysqlDataAccess, MysqlDataAccess>();
        builder.Services.AddSingleton<ILoginAccess, LoginAccess>();
        builder.Services.AddSingleton<ISchemaAccess, SchemaAccess>();
        builder.Services.AddSingleton<IMenuUsersAccess, MenuUsersAccess>();
    }

    public static void AddCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(p => p.AddPolicy("FreeForAll", build =>
        {
            build.WithOrigins("*")
            .AllowAnyMethod()
            .AllowAnyHeader();
        }));

    }


    public static void AddAuthenticationServices(this WebApplicationBuilder builder)
    {

    }


}
