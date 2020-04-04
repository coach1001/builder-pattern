using System.Text;
using Autofac;
using CoreDui.Builders;
using CoreDui.FlowControllerFeature;
using CoreDui.Persistance;
using CoreDui.Repositories;
using CoreDuiWebApi.Authentication;
using CoreDuiWebApi.Email;
using CoreDuiWebApi.Email.Templates;
using CoreDuiWebApi.Flow;
using CoreDuiWebApi.Flow.Account.UserLogin;
using CoreDuiWebApi.Flow.Account.UserRegistration;
using CoreDuiWebApi.Flow.TMH1.A1;
using CoreDuiWebApi.Flow.TMH1.A2A3A4;
using CoreDuiWebApi.Flow.TMH1.A7;
using CoreDuiWebApi.Flow.TMH1.TestFlow;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace CoreDuiWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            var moduleRepo = new ModuleRepository();
            var elementMapper = new ElementTypeTemplateMapper();
            var controlMapper = new ControlTypeTemplateMapper();
            var validationMapper = new ValidationAttributeJsConverterMapper();
            var moduleBuilder = new ModuleBuilder(moduleRepo, elementMapper, controlMapper, validationMapper);
            var inMemoryFlowPersistance = new InMemoryFlowPersistance();

            services.AddSingleton<IElementTypeTemplateMapper>(elementMapper);
            services.AddSingleton<IControlTypeTemplateMapper>(controlMapper);
            services.AddSingleton<IValidationAttributeJsConverterMapper>(validationMapper);
            services.AddSingleton<IModuleRepository>(moduleRepo);
            services.AddSingleton<IFlowPersistance>(inMemoryFlowPersistance);
            
            validationMapper.AddValidator("MaxLengthAttribute", CustomAttribuesJsConverters.MaxLengthJsConverter);

            UserRegistrationFlow.RegisterFlow(moduleBuilder);
            UserLoginFlow.RegisterFlow(moduleBuilder);

            A1Flow.RegisterFlow(moduleBuilder);
            A2A3A4Flow.RegisterFlow(moduleBuilder);
            A7Flow.RegisterFlow(moduleBuilder);

            TestFlow.RegisterFlow(moduleBuilder);

            services.AddControllers(c =>
            {
                c.EnableEndpointRouting = false;
            }).ConfigureApplicationPartManager(apm =>
            {
                apm.FeatureProviders.Add(new FlowControllerFeatureProvider(moduleRepo));
            }).AddNewtonsoftJson(j =>
            {
                j.SerializerSettings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()                    
                };
                j.SerializerSettings.Converters.Add(new StringEnumConverter 
                {                     
                    NamingStrategy = new CamelCaseNamingStrategy() 
                });                
                j.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                j.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            var symmetricKey = Encoding.ASCII.GetBytes(Configuration.GetSection("Jwt").GetValue<string>("SymmetricKey"));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;                     
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };                
            });
            services.AddAuthorization(a =>
            {
                a.AddPolicy("IsAccountEnabled", policy => policy.RequireClaim("account_enabled", "True", "true"));
            });

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "Core Dui Example API", Version = "v1" });
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }        
                        },
                        new string[] { }
                    }
                });
            });

            services.AddDbContext<DbLabCalcContext>(o => 
            {
                o.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.Configure<LdapConfig>(Configuration.GetSection("Ldap"));
            services.Configure<JwtConfig>(Configuration.GetSection("Jwt"));
            services.Configure<SmtpConfig>(Configuration.GetSection("Smtp"));
            services.Configure<AppConfig>(Configuration.GetSection("App"));

            services.AddScoped<IEmailClient, GmailEmailClient>();
            services.AddScoped<IAuthenticationService<LdapUser>, LdapAuthenticationService>();
            services.AddScoped<IAuthenticationService<DbUserClient>, DbAuthenticationService>();
            services.AddScoped<IEmailTemplates, EmailTemplates>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core Dui Example API");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
