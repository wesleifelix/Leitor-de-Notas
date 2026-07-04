using LeitorDeNotas.ClearArch.IoC;

namespace ClearArch.NotasApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddLeitorDeNotasServices();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.UseSwagger(); // Esse cara precisa do AddSwaggerGen() feito lá em cima
                app.UseSwaggerUI(c =>
                {
                    // Força o Swagger UI a procurar o arquivo correto de definição da API
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Leitor de Notas API v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
