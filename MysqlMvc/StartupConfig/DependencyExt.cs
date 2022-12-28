using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace GSIA.StartupConfig;

public static class DependencyExt
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
    }

    public static void AddApi(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient("api", opts =>
        {
            opts.BaseAddress = new Uri(builder.Configuration
                .GetValue<string>("ApiAddress")); 
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


        builder.Services.AddAuthentication(ops =>
        {
            ops.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            // ops.DefaultChallengeScheme  = GoogleDefaults.AuthenticationScheme;
        })
            .AddCookie(ops =>
            {
                ops.LoginPath = "/login";

            }); 
            //.AddGoogle(options =>
            //{
            //    options.ClientId        = builder.Configuration.GetValue<string>("GoogleLogin:ClientId");
            //    options.ClientSecret    = builder.Configuration.GetValue<string>("GoogleLogin:ClientSecret"); 
            //    options.CallbackPath    = builder.Configuration.GetValue<string>("GoogleLogin:CallbackPath");
            //    options.AuthorizationEndpoint += "?prompt=consent";
            //})
            
    }


}
