using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Okr.Services.Identity.DbContext;
using Okr.Services.Identity.Mapping;
using Okr.Services.Identity.Repository;
using System.Text;

namespace Okr.Services.Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddDbContext<DbContext.UserDbContext>(o => o.UseInMemoryDatabase("UserInfo"));
            services.AddScoped<IUserService, UserService>();
            services.AddAutoMapper(typeof(UserMapping));

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            SeedDb.Initialize(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);
        }
    }
}

