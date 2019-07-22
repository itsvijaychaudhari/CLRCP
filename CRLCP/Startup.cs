using System.Text;
using System.Threading.Tasks;

using CRLCP.Helper;
using CRLCP.Models;
using CRLCP.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CRLCP
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title="CLRCP Web API" , Version="v1"});
            });
            services.AddDbContext<CLRCP_MASTERContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CLRCP_MASTERConectionString")));
            services.AddDbContext<TEXTContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TextConnectionString")));
            services.AddDbContext<TextToSpeechContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TextToSpeechConnectionString")));
            services.AddDbContext<IMAGEContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IMAGEConnectionString")));
            services.AddDbContext<ImageToTextContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ImageToTextConnectionString")));
            services.AddDbContext<TextToTextContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TextToTextConnectionString")));
            services.AddDbContext<VALIDATION_INFOContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ValidationInfoConnectionString")));
           
            services.AddScoped<IUserRepository, UserService>();
           
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;  
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
                        var EmailId = context.Principal.Identity.Name;
                        var user = userService.GetByEmailId(EmailId);
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();
                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CLRCP Web API");
                });
            }
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CLRCP Web API");
            });
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
