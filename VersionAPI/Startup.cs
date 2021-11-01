namespace VersionAPI
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Reflection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.OpenApi.Models;

    /// <summary>
    /// Startup
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        /// <summary>
        /// Common
        /// </summary>
        public const string CommonVersion = "common";

        /// <summary>
        /// New
        /// </summary>
        public const string MajorVersionNew = "v2";

        /// <summary>
        /// Current
        /// </summary>
        public const string MajorVersionCurrent = "v1";

        /// <summary>
        /// API Controller Root-Name
        /// </summary>
        public const string ApiDemoName = "Dummy";

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// CTOR
        /// </summary>
        /// <param name="configuration">IConfiguration</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets property
        /// </summary>
        /// <value>IConfiguration</value>
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            _ = services.AddControllers();
            _ = services.AddHealthChecks();

            _ = services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                    policy.AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowAnyOrigin());
            });

            _ = services.AddMvc(
                config =>
                {
                    config.Filters.Add(typeof(Libs.GlobalExceptionFilter));
                });

            var releaseNotes = new Uri("https://myAPI.ey.com/releasenotes/" + MajorVersionCurrent);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(MajorVersionCurrent, this.MakeOpenApiInfo(ApiDemoName, Startup.MajorVersionCurrent, "Current API", releaseNotes));
                c.SwaggerDoc(MajorVersionNew, this.MakeOpenApiInfo(ApiDemoName, Startup.MajorVersionNew, "Future API", releaseNotes));
                c.SwaggerDoc(CommonVersion, this.MakeOpenApiInfo(ApiDemoName, Startup.CommonVersion , "Version Common Methods", releaseNotes));

                // This code allow you to use XML-comments
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
            });
        }

        private OpenApiInfo MakeOpenApiInfo(string title, string version, string description, Uri releaseNotes)
        {
            var oai= new OpenApiInfo { Title = title, Version = version, Contact = new OpenApiContact { Email = Program.ProgramMetadata.TeamEMail, Name = Program.ProgramMetadata.TeamName }, Description = description };
            if (releaseNotes != null) oai.Contact.Url = releaseNotes;
            return oai;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="env">IWebHostEnvironment</param>
        /// <param name="logger"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                logger.LogInformation("In Development.");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                logger.LogInformation("Not Development.");
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{MajorVersionCurrent}/swagger.json", $"Dummy API {MajorVersionCurrent}");
                c.SwaggerEndpoint($"/swagger/{MajorVersionNew}/swagger.json", $"Dummy API {MajorVersionNew}");
                c.SwaggerEndpoint($"/swagger/{CommonVersion}/swagger.json", $"API {CommonVersion}");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
