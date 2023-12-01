using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using HocAvalon.ViewModels;
using SharpHook;
using SharpHook.Native;
using System.Threading.Tasks;

namespace HocAvalon
{
    public class ShareData
    {
        // các biến được sử dụng chung bởi các views
        public static string transText = string.Empty;
        public static string langSecond = "vi";
    }
}

namespace HocAvalon.Views
{
    public partial class Window1 : Window
    {   
        // Khởi tạo các biến hook và giả lập sự kiện
        TaskPoolGlobalHook hook = new TaskPoolGlobalHook();
        EventSimulator simulator = new EventSimulator();
        // currentText dùng để tránh popup window mở sai thời điểm
        string currentText = string.Empty;
        public Window1()
        {
            InitializeComponent();
            // đăng kí sự kiện và chạy hook
            hook.MouseReleased += OnMouseRelease;
            hook.RunAsync();
        }
        public void OnMouseRelease(object sender, MouseHookEventArgs e)
        {
            Dispatcher.UIThread.Post( async () =>
            {
                // lấy giá trị hiện tại của Clipboard để khi xử lý xong sẽ trả lại
                string temp = await Clipboard.GetTextAsync();
                await Clipboard.ClearAsync();

                await Task.Delay(75);
                // Press Ctrl+C
                simulator.SimulateKeyPress(KeyCode.VcLeftControl);
                simulator.SimulateKeyPress(KeyCode.VcC);
                // Release Ctrl+C
                simulator.SimulateKeyRelease(KeyCode.VcC);
                simulator.SimulateKeyRelease(KeyCode.VcLeftControl);
                await Task.Delay(75);

                // Lấy giá trị text đang có trong Clipboard
                string text = await Clipboard.GetTextAsync();
                // Nếu text ko có giá trị hoặc trùng giá trị trước đó thì ko làm gì
                if (text is null || text == " " || text == currentText)
                {                   
                    await Clipboard.SetTextAsync(temp);
                    return;
                }
                currentText = text;
                // gán text cho transText để đưa đi dịch
                ShareData.transText = text;
                // Mở popup window tại vị trí con chuột đang đứng
                MainWindow mainWindow = new MainWindow();
                mainWindow.Position = new Avalonia.PixelPoint(e.Data.X, e.Data.Y);
                mainWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                mainWindow.Show();

                await Clipboard.SetTextAsync(temp);
            });
        }
        private void Window_Closed(object? sender, System.EventArgs e)
        {
            hook.Dispose();
        }
    }
}
