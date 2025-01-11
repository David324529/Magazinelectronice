using Example.Data;
using Example.Data.Repositories;
using Example.Api.Clients;
using Example.Domain.Repositories;
using Example.Domain.Workflows;
using Examples.Domain.Workflows;
using MagazinElectronic.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;

namespace Example.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Configurare conexiune la baza de date
            builder.Services.AddDbContext<ShoppingContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Înregistrare repo-uri
            builder.Services.AddTransient<IProductRepository, ProductRepository>();
            builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
            builder.Services.AddTransient<IOrderRepository, OrderRepository>();
            builder.Services.AddTransient<IOrderItemRepository, OrderItemRepository>();

            // Înregistrare workflow-uri
            builder.Services.AddTransient<PlasareComandaWorkflow>();
            builder.Services.AddTransient<FacturareWorkflow>();
            builder.Services.AddTransient<LivrareWorkflow>();

            // Configurare client API pentru interacțiunea cu alte servicii externe
            builder.Services.AddHttpClient<ShoppingApiClient>()
                .ConfigureHttpClient(client =>
                {
                    client.BaseAddress = new Uri("https://api.magazinonline.com");
                })
                .AddPolicyHandler(HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)));

            // Configurare controlere
            builder.Services.AddControllers();

            // Configurare Swagger/OpenAPI pentru documentație API
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MagazinOnline.Api", Version = "v1" });
            });

            WebApplication app = builder.Build();

            // Configurare middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Mapează controlerele pentru a răspunde cererilor HTTP
            app.MapControllers();

            // Lansează aplicația
            app.Run();
        }
    }
}
