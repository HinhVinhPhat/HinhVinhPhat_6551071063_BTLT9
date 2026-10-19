using Microsoft.Extensions.Configuration;

namespace BTCh11_Bai2.Config;

public class ConnectionStringsConfig
{
    public string QL_NhaTro { get; set; } = "";
}

public class ProvinceApiConfig
{
    public string BaseUrl { get; set; } = "https://provinces.open-api.vn/api/v2/";
    public int TimeoutSeconds { get; set; } = 10;
}

/// <summary>Lớp cấu hình có kiểu (strongly-typed) bind từ appsettings.json</summary>
public class AppConfig
{
    public ConnectionStringsConfig ConnectionStrings { get; set; } = new();
    public ProvinceApiConfig ProvinceApi { get; set; } = new();

    public static AppConfig Load()
    {
        var cfg = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .Build();
        return cfg.Get<AppConfig>() ?? new AppConfig();
    }
}
