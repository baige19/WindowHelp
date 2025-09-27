using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowHelp
{
    public class InputHelper
    {
        [DllImport("user32.dll",EntryPoint = "SendMessageA")]
        private static extern int SendMessage(IntPtr hwnd,uint Msg,int wParam,int lParam);
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_MOUSEMOVE = 0x0200;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_LBUTTONUP = 0x0202;
        private const int WM_LBUTTONBLICLK = 0x0203;
        private const int WM_RBUTTONDOWN = 0x0204;
        private const int WM_RBUTTONUP = 0x0205;
        private const int WM_RBUTTONBLICLK = 0x0206;

        //移动鼠标
        public static void MouseMove(IntPtr hand, int x,  int y)
        {
            int lparm = (y << 16) + x;
            SendMessage(hand, WM_MOUSEMOVE, 0, lparm);

        }

        //左键按下
        public static void MouseLeftDown(IntPtr hand, int x, int y)
        {
            int lparm = (y << 16) + x;
            SendMessage(hand, WM_MOUSEMOVE, 0, lparm);
            SendMessage(hand, WM_LBUTTONDOWN, 0, lparm);
        }

        //左键松开
        public static void MouseLeftUp(IntPtr hand, int x, int y)
        {
            int lparm = (y << 16) + x;
            SendMessage(hand, WM_MOUSEMOVE, 0, lparm);
            SendMessage(hand, WM_LBUTTONUP, 0, lparm);
        }
        
        //右键按下
        public static void MouseRightDown(IntPtr hand, int x, int y)
        {
            int lparm = (y << 16) + x;
            SendMessage(hand, WM_MOUSEMOVE, 0, lparm);
            SendMessage(hand, WM_RBUTTONDOWN, 0, lparm);
        }

        //油键松开
        public static void MouseRightUp(IntPtr hand, int x, int y)
        {
            int lparm = (y << 16) + x;
            SendMessage(hand, WM_MOUSEMOVE, 0, lparm);
            SendMessage(hand, WM_RBUTTONUP, 0, lparm);
        }

        //右键点击
        public static void MouseRightClick(IntPtr hand, int x, int y)
        {
            int lparm = (y << 16) + x;
            SendMessage(hand, WM_MOUSEMOVE, 0, lparm);
            SendMessage(hand, WM_RBUTTONDOWN, 0, lparm);
            SendMessage(hand, WM_RBUTTONUP, 0, lparm);
        }

        //左键点击
        public static void MouseLefClick(IntPtr hand, int x, int y)
        {
            int lparm = (y << 16) + x;
            SendMessage(hand, WM_MOUSEMOVE, 0, lparm);
            SendMessage(hand, WM_LBUTTONDOWN, 0, lparm);
            SendMessage(hand, WM_LBUTTONUP, 0, lparm);
        }

        //左键双击
        public static void MouseLefDoubleClick(IntPtr hand, int x, int y)
        {
            int lparm = (y << 16) + x;
            SendMessage(hand, WM_MOUSEMOVE, 0, lparm);
            SendMessage(hand, WM_LBUTTONBLICLK, 0, lparm);
        }

        //模拟按键输入
        public static void KeybordInput(IntPtr hand, string str)
        {
            bool isCapsLockOn = Control.IsKeyLocked(Keys.CapsLock);
            Thread.Sleep(50);
            foreach(char c in str)
            {
                bool isupper = char.IsUpper(c);
                if(isupper && !isCapsLockOn || ! isupper && isCapsLockOn)
                {
                    KeyPress(hand, 0x14);
                }
                byte key = (byte)char.ToUpper(c);
                KeyPress(hand, key);
                Thread.Sleep(50);
            }
        }

        //按键按下
        public static void KeyDown(IntPtr hand,byte key)
        {
            SendMessage(hand, WM_KEYDOWN, (int)key, 0);
        }

        //按键抬起
        public static void KeyUp(IntPtr hand, byte key)
        {
            SendMessage(hand, WM_KEYUP, (int)key, 0);
        }

        //按键
        public static void KeyPress(IntPtr hand, byte key)
        {
            SendMessage(hand, WM_KEYDOWN, (int)key, 0);
            SendMessage(hand, WM_KEYUP, (int)key, 0);
        }
    }
}
