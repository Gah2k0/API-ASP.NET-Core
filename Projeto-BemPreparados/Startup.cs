using Aplicacao.Interface;
using Aplicacao.Servico;
using Dominio.Interface.Provedor;
using Dominio.Mensagem;
using Dominio.Validacoes;
using Dominio.Validacoes.Interface;
using Dominio.Interface.Repositorios;
using Infraestrutura.Conexao;
using Infraestrutura.Fila;
using Infraestrutura.Repositorios;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;


namespace Projeto_BemPreparados
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
            services.AddCors(options => 
            {
                options.AddPolicy("CorsPolicy", builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddControllers(); 
            services.AddScoped<IMensagem, Mensagem>();
            services.AddScoped<IAutenticacao, AutenticacaoService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICliente, ClienteService>();
            services.AddScoped<IConveniada, ConveniadaService>();
            services.AddScoped<ITreinaProposta, TreinaPropostaService>();
            services.AddScoped<IValidacaoCliente, ValidacaoCliente>();
            services.AddScoped<IValidacaoProposta, ValidacaoProposta>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<Aplicacao.Dto.UsuarioLogado>();
            services.AddHttpContextAccessor();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // mapper

            services.AddScoped<ConexaoDb>();

            services.AddScoped<ITreinaUsuariosRepositorio, TreinaUsuariosRepositorio>();
            services.AddScoped<ITreinaClientesRepositorio, TreinaClientesRepositorio>();
            services.AddScoped<ITreinaConveniadasRepositorio, TreinaConveniadasRepositorio>();
            services.AddScoped<ITreinaPropostasRepositorio, TreinaPropostasRepositorio>();

            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("Secret"));
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
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme.",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
            });

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(Configuration.GetValue<string>("ServidorFila:Host")
                        , Configuration.GetValue<ushort>("ServidorFila:Port"), "/", h =>
                    {
                        h.Username(Configuration.GetValue<string>("ServidorFila:Username"));
                        h.Password(Configuration.GetValue<string>("ServidorFila:Password"));
                    });
                });
            });

            services.AddTransient<IFilaProvedor, MassTransitProvedor>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Projeto_BemPreparados v1"));
            }

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
