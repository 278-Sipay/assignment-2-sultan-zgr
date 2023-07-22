using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SipayApi.Data.DBContext;
using SipayApi.Data.Repository;
using SipayApi.DataLayer.Entities;
using SipayApi.Schema;

namespace SipayApi.Api;

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
        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sipay Api Collection", Version = "v1" });
        });


        //dbcontext
            var dbConfig = Configuration.GetConnectionString("MsSqlConnection");
            services.AddDbContext<SimDbContext>(opts =>
            opts.UseSqlServer(dbConfig));


        services.AddScoped<ITransactionRepository, TransactionRepository>();
   

        //AutoMappere

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        services.AddSingleton(config.CreateMapper());





    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sipay v1"));
        }

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
