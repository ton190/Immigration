using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using FluentValidation;
using Microsoft.Extensions.WebEncoders;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddControllers();
builder.Services.AddLocalization(opt => opt.ResourcesPath = "Resources");
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.Configure<WebEncoderOptions>(opt =>
    opt.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All));
builder.Services.AddScoped<AppState>();
builder.Services.AddScoped<PageNavigation>();
builder.Services.AddMailKit(config => config.UseMailKit(
        builder.Configuration.GetSection("Email").Get<MailKitOptions>()));
builder.Services.AddScoped<RazorViewToStringRenderer>();

RequestLocalizationOptions GetLocalizationOptions()
{
    var cultures = builder?.Configuration.GetSection("Cultures")
        .GetChildren().ToDictionary(x => x.Key, x => x.Value);

    var localizationOptions = new RequestLocalizationOptions();
    var supportedCultures = cultures?.Keys.ToArray();

    if (supportedCultures is not null)
    {
        localizationOptions
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures)
        .SetDefaultCulture("en");
    }

    return localizationOptions;
}

var app = builder.Build();

app.UseStaticFiles();
app.UseRequestLocalization(GetLocalizationOptions());
app.UseRouting();
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
