using System;
using System.Windows.Forms;


namespace BTCh11_Bai5
{
    internal static class Program
    {
        // Chuỗi kết nối dùng chung toàn project (đổi TenDB và tên server cho đúng máy bạn)
        public static readonly string ConnectionString =
            "Server=.\\SQLEXPRESS;Database=QL_DatTour;Integrated Security=True;TrustServerCertificate=True;";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}