using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowHelp
{
    public class WindowHelper
    {
        //Window列表
        private static List<WindowInfo> WindowList;
        //获取所有Window
        public static IReadOnlyList<WindowInfo> FindAllWindows()
        {
            WindowList = new List<WindowInfo>();
            EnumWindows(new EnumWindowsProc(EnumWindowsProcCallback),IntPtr.Zero);
            return WindowList;
        }

        //Window信息结构
        public struct WindowInfo
        {
            public WindowInfo(string title, IntPtr hand, Rectangle bounds) : this()
            {
                Handle = hand;
                Title = title;
                Bounds = bounds;
            }
            //Window标题
            public string Title { set; get; }
            //Window句柄
            public IntPtr Handle { set; get; }
            //Window位置与尺寸
            public Rectangle Bounds { set; get; }
            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("Title:{0}\n", Title);
                sb.AppendFormat("Handle:{0}\n", Handle);
                sb.AppendFormat("Bounds:{0}\n", Bounds);
                return sb.ToString();
            }
        }

        //获取Window类名
        [DllImport("user32")]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        //获取Window标题
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        //获取父Window
        [DllImport("user32")]
        private static extern IntPtr GetParent(IntPtr hWnd);
        //Window是否可见
        [DllImport("user32")]
        private static extern bool IsWindowVisible(IntPtr hWnd);
        //获取Window的位置及大小
        [DllImport("user32.dll", SetLastError = true)]
        private static extern void GetWindowRect(IntPtr parentHandle, out RECT rc);
        //位置大小结构
        private readonly struct RECT
        {
            public readonly int Left;
            public readonly int Top;
            public readonly int Right;
            public readonly int Bottom;
            public int Width() => Right - Left;
            public int Height() => Bottom - Top;
        }
        //枚举所有Window
        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);
        //枚举Windows时的处理方法
        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        //获取所有可见的顶层Window
        private static bool EnumWindowsProcCallback(IntPtr hWnd, IntPtr lParam)
        {
            //仅查找可见的顶层Window
            StringBuilder sb = new StringBuilder(256);
            if (GetParent(hWnd) == IntPtr.Zero && GetWindowText(hWnd, sb, sb.Capacity) > 0 && IsWindowVisible(hWnd))
            {
                //获取Window标题
                var lptrString = new StringBuilder(512);
                GetWindowText(hWnd, lptrString, lptrString.Capacity);
                var title = lptrString.ToString().Trim();

                //获取Window位置和尺寸
                RECT rect = default;
                GetWindowRect(hWnd, out rect);
                var bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);

                //添加到已找到Window到列表
                WindowList.Add(new WindowInfo(title, hWnd, bounds));
            }
            //必须返回true，返回false则终止枚举
            return true;
        }

        //把最小化的窗口还原
        [DllImport("user32.dll", SetLastError = true)]
        private static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);
        //激活窗口
        public static void ActivateWindow(IntPtr hand)
        {
            SwitchToThisWindow(hand, true);
        }
    }

}
