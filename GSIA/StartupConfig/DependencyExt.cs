using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using LibraryMySql;
using Microsoft.Extensions.Options;
using LibraryMySql.DataAccess.Login;

namespace GSIA.StartupConfig;

public static class DependencyExt
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();


        builder.Services.AddSingleton<IMySqlDataAccess, MySqlDataAccess>();
        builder.Services.AddSingleton<ILoginAccess, LoginAccess>();

    }

    public static void AddAuthenticationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization( ops =>
        {
            ops.FallbackPolicy = new AuthorizationPolicyBuilder() 
            .RequireAuthenticatedUser()
            .Build();
        });


        builder.Services.AddAuthentication( ops =>
        {
            ops.DefaultScheme           = CookieAuthenticationDefaults.AuthenticationScheme;
            ops.DefaultChallengeScheme  = GoogleDefaults.AuthenticationScheme;
        })
            .AddCookie(ops =>
            {
                ops.LoginPath = "/login"; 

            })
            .AddGoogle(options =>
            {
                options.ClientId        = builder.Configuration.GetValue<string>("GoogleLogin:ClientId");
                options.ClientSecret    = builder.Configuration.GetValue<string>("GoogleLogin:ClientSecret"); 
                options.CallbackPath    = builder.Configuration.GetValue<string>("GoogleLogin:CallbackPath");
                options.AuthorizationEndpoint += "?prompt=consent";
            })
            //.AddOpenIdConnect("OpenId", options =>
            //{
            //    options.Authority = "https://accounts.google.com";
            //    options.ClientId = builder.Configuration.GetValue<string>("GoogleLogin:ClientId");
            //    options.ClientSecret = builder.Configuration.GetValue<string>("GoogleLogin:ClientSecret");
            //    options.CallbackPath = builder.Configuration.GetValue<string>("GoogleLogin:CallbackPath");
            //    options.GetClaimsFromUserInfoEndpoint = true;
            //})
            ; 
    }


}
