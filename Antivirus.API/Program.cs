using System.Reflection;
using Antivirus.Application;

var builder = WebApplication.CreateBuilder(args);
RegisterServices(builder.Services, builder.Configuration);
var app = builder.Build();
Configure(app, app.Environment);

app.Run();

void RegisterServices(IServiceCollection services, IConfiguration config)
{
    services.AddApplication();
    services.AddEndpointsApiExplorer();
    services.AddControllers();

    services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
    services.AddSwaggerGen(c =>
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });
    services.AddApiVersioning();
    services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        });
    });
}

void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kasp V1")));
    }

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseCors("AllowAll");

    app.UseEndpoints(endpoints => endpoints.MapControllers());
}