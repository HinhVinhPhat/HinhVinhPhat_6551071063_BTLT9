using System.Globalization;
using BTCh11_Bai1.Config;
using BTCh11_Bai1.Forms;
using Serilog;

namespace BTCh11_Bai1;

static class Program
{
    private const string FriendlyMsg = "Đã xảy ra lỗi, thông tin đã được ghi lại.";

    [STAThread]
    static void Main()
    {
        var vi = new CultureInfo("vi-VN");
        CultureInfo.DefaultThreadCurrentCulture = vi;
        CultureInfo.DefaultThreadCurrentUICulture = vi;
        CultureInfo.CurrentCulture = vi;
        CultureInfo.CurrentUICulture = vi;

        // Ghi log theo ngày vào thư mục Logs cạnh file .exe -> Logs\log-yyyyMMdd.txt
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File(Path.Combine(AppContext.BaseDirectory, "Logs", "log-.txt"),
                          rollingInterval: RollingInterval.Day, shared: true)
            .CreateLogger();

        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (_, e) => HandleUnhandled(e.Exception);
        AppDomain.CurrentDomain.UnhandledException += (_, e) => HandleUnhandled(e.ExceptionObject as Exception);

        try
        {
            ApplicationConfiguration.Initialize();
            var settings = AppSettings.Load();
            Log.Information("Ứng dụng khởi động");
            Application.Run(new FormChiTieu(settings));
        }
        catch (Exception ex)
        {
            HandleUnhandled(ex);
        }
        finally
        {
            Log.Information("Ứng dụng đóng");
            Log.CloseAndFlush();
        }
    }

    private static void HandleUnhandled(Exception ex)
    {
        Log.Error(ex, "Lỗi chưa bắt");
        MessageBox.Show(FriendlyMsg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}
