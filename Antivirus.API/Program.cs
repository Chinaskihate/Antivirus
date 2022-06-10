var builder = WebApplication.CreateBuilder(args);
RegisterServices(builder.Services, builder.Configuration);
var app = builder.Build();
Configure(app, app.Environment);

app.Run();

void RegisterServices(IServiceCollection services, IConfiguration config)
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddControllers();
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

    app.UseEndpoints(endpoints => endpoints.MapControllers());
}