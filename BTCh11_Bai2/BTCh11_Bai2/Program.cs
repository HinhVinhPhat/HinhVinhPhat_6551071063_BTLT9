using System.Globalization;
using BTCh11_Bai2.Config;
using Serilog;

namespace BTCh11_Bai2;

static class Program
{
    private const string FriendlyMsg = "Đã xảy ra lỗi, thông tin đã được ghi lại";

    [STAThread]
    static void Main()
    {
        var vi = new CultureInfo("vi-VN");
        CultureInfo.DefaultThreadCurrentCulture = vi;
        CultureInfo.DefaultThreadCurrentUICulture = vi;
        CultureInfo.CurrentCulture = vi;

        // Log theo ngày: Logs\log-yyyyMMdd.txt (cạnh file .exe)
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File(Path.Combine(AppContext.BaseDirectory, "Logs", "log-.txt"),
                          rollingInterval: RollingInterval.Day, shared: true)
            .CreateLogger();

        // Xử lý lỗi toàn cục
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (_, e) => HandleUnhandled(e.Exception, "Application.ThreadException");
        AppDomain.CurrentDomain.UnhandledException += (_, e) =>
            HandleUnhandled(e.ExceptionObject as Exception, "AppDomain.UnhandledException");
        TaskScheduler.UnobservedTaskException += (_, e) =>
        {
            Log.Error(e.Exception, "TaskScheduler.UnobservedTaskException");
            e.SetObserved();
        };

        try
        {
            ApplicationConfiguration.Initialize();
            var cfg = AppConfig.Load();
            Log.Information("Ứng dụng khởi động");
            Application.Run(new FormKhachThue(cfg));
        }
        catch (Exception ex)
        {
            HandleUnhandled(ex, "Main");
        }
        finally
        {
            Log.Information("Ứng dụng đóng");
            Log.CloseAndFlush();
        }
    }

    private static void HandleUnhandled(Exception ex, string source)
    {
        Log.Error(ex, "Lỗi chưa bắt ({Source})", source); // có đầy đủ stack trace
        MessageBox.Show(FriendlyMsg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}
