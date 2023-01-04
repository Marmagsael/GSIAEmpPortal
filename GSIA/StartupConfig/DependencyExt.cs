using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using GsiaLibrary.DataAccess.Login;
using GsiaLibrary.DataAccess;
using GsiaLibrary.DataAccess.General;
using GsiaLibrary.DataAccess._100Main;

namespace GSIA.StartupConfig;

public static class DependencyExt
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
    }

    public static void AddInjectedServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IApiAccess, ApiAccess>();
        builder.Services.AddSingleton<ILoginData, LoginData>();
        builder.Services.AddSingleton<ICompanyData, CompanyData>();
        builder.Services.AddSingleton<IMenuData, MenuData>();
    }


    public static void AddHttpClient(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient("api", opt =>
        {
            opt.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiAddress"));
        });
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
