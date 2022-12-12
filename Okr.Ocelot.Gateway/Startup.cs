using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Okr.Ocelot.Gateway.MockData;
using System.Text;

namespace Okr.Ocelot.Gateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddOcelot().
                AddDelegatingHandler<FakeHandler>(true).
                AddDelegatingHandler<FakeHandlerTwo>();

            // JWT TOKEN

            var key = Encoding.ASCII.GetBytes(Configuration["Secret"]);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            )
                .AddJwtBearer(x => {
                    x.Audience = "SomeCustomApp";
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.ClaimsIssuer = "mineplaJWT.api.demo";
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                    x.Events = new JwtBearerEvents()
                    {
                        OnTokenValidated = (context) => {
                            //context.Principal.Identity is ClaimsIdentity	                
                            //So casting it to ClaimsIdentity provides all generated claims	                
                            //And for an extra token validation they might be usefull	                
                            var name = context.Principal.Identity.Name;
                            if (string.IsNullOrEmpty(name))
                            {
                                context.Fail("Unauthorized. Please re-login");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            // JWT TOKEN

        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            await app.UseOcelot();

        }
    }
}
