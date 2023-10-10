using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics;

class Program
{
    [DllImport("user32.dll")]
    static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    static void Main()
    {
        Process[] processes = Process.GetProcessesByName("nome_do_processo");
        foreach (Process proc in processes)
        {
            IntPtr hWnd = proc.MainWindowHandle;
            Rect rect = new Rect();
            GetWindowRect(hWnd, ref rect);

            int width = rect.Right - rect.Left;
            int height = rect.Bottom - rect.Top;

            using (Bitmap bitmap = new Bitmap(width, height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(rect.Left, rect.Top, 0, 0, new Size(width, height));
                }

                bitmap.Save("screenshot.png", ImageFormat.Png);
            }
        }
    }
}
