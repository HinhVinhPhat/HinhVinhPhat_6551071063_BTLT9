using Microsoft.Extensions.Configuration;

namespace BTCh11_Bai1.Config;

public class ConnectionStringsSettings
{
    public string QL_ChiTieu { get; set; } = "";
}

/// <summary>Lớp cấu hình có kiểu (strongly-typed), bind từ appsettings.json (+ appsettings.Development.json).</summary>
public class AppSettings
{
    public ConnectionStringsSettings ConnectionStrings { get; set; } = new();

    public static AppSettings Load()
    {
#if DEBUG
        const string defaultEnv = "Development";
#else
        const string defaultEnv = "Production";
#endif
        var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? defaultEnv;

        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            // File này ghi đè appsettings.json khi lập trình; bản publish không có nên bỏ qua
            .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: false)
            .Build();

        var settings = config.Get<AppSettings>() ?? new AppSettings();
        if (string.IsNullOrWhiteSpace(settings.ConnectionStrings.QL_ChiTieu))
            throw new InvalidOperationException("Thiếu ConnectionStrings:QL_ChiTieu trong appsettings.json.");
        return settings;
    }
}
