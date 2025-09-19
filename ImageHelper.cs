using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static WindowHelp.WindowHelper;

namespace WindowHelp
{
    public class ImageHelper
    {
        #region 截图
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetDC(IntPtr hdc);
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateCompatibleDC(IntPtr hdc);
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateCompatibleBitmap(IntPtr hdc,int nWidth,int nHeight);
        [DllImport("gdi32.dll")]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr h);
        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);
        [DllImport("gdi32.dll")]
        private static extern bool DeleteDC(IntPtr hdc);
        [DllImport("user32.dll")]
        private static extern bool ReleaseDC(IntPtr hdc,IntPtr hsor);

        //截取Window界面
        public static Bitmap GetWindowImage(WindowInfo windowInfo)
        {
            int width = (int)(windowInfo.Bounds.Right-windowInfo.Bounds.Left);
            int height = (int)(windowInfo.Bounds.Bottom-windowInfo.Bounds.Top);
            IntPtr hdcSource = GetDC(windowInfo.Handle);
            if (hdcSource == IntPtr.Zero)
            {
                return null;
            }
            IntPtr hdcDest = CreateCompatibleDC(hdcSource);
            IntPtr hBitmap = CreateCompatibleBitmap(hdcSource, width, height);
            IntPtr hOld = SelectObject(hdcDest, hBitmap);
            PrintWindow(windowInfo.Handle, hdcDest, 0);
            SelectObject(hdcDest, hOld);
            DeleteDC(hdcDest);
            ReleaseDC(windowInfo.Handle, hdcSource);
            Bitmap image = Image.FromHbitmap(hBitmap);
            DeleteObject(hBitmap);
            return image;
        }

        #endregion

    }
}
