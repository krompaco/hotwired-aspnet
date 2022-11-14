using System.IO.Compression;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Net.Http.Headers;
using WebApp.WebSocketManager;

namespace WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.ClearProviders();
        builder.Logging.AddConsole(options => options.LogToStandardErrorThreshold = LogLevel.Trace);

        builder.Services.AddDistributedMemoryCache();

        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(20);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        builder.Services.AddWebSocketManager();

        builder.Services.AddResponseCompression(options =>
        {
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
            options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "image/svg+xml" });
        });

        builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Optimal;
        });

        builder.Services.AddRazorPages()
            .AddSessionStateTempDataProvider()
            .AddViewOptions(options => { options.HtmlHelperOptions.ClientValidationEnabled = false; });

        using var loggerFactory = LoggerFactory.Create(builderInside =>
        {
            builderInside.AddSimpleConsole(i => i.ColorBehavior = LoggerColorBehavior.Disabled);
        });

        var logger = loggerFactory.CreateLogger<Program>();

        var app = builder.Build();

        logger.LogInformation("Starting the app");

        app.Use(async (context, next) =>
        {
            var nonce = Guid.NewGuid().ToString("N");
            context.Items["csp-nonce"] = nonce;

            var param = Guid.NewGuid().ToString("N");
            context.Items["csrf-param"] = param;

            await next();
        });

        app.UseResponseCompression();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseStaticFiles(new StaticFileOptions
        {
            OnPrepareResponse = ctx =>
            {
                const int durationInSeconds = 60 * 60 * 24 * 365;
                ctx.Context.Response.Headers[HeaderNames.CacheControl] = $"public, max-age={durationInSeconds}";
            },
        });

        app.UseSession();

        app.UseStatusCodePagesWithReExecute("/Error", "?statusCode={0}");

        app.UseRouting();

        app.MapRazorPages();

        var webSocketOptions = new WebSocketOptions
        {
            KeepAliveInterval = TimeSpan.FromSeconds(120),
        };
        app.UseWebSockets(webSocketOptions);
        var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
        var serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
        var streamTestHandler = serviceProvider.GetService<StreamTestHandler>();

        if (streamTestHandler != null)
        {
            app.MapWebSocketManager("/streamtesthandler", streamTestHandler);
        }

        app.Run();
    }
}
