/* 2DCameraSystem
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CameraSystem;

namespace CameraSystem
{
    internal class Program
    {
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.SetBufferSize(1000, 5000); // 幅: 100 文字、高さ: 500 行に設定する
            Camera.Create("map",90,90);
            Camera.character_size = 2;
            Camera.character[0] = "・";
            Camera.character[1] = "○○";
            Camera.character[2] = "□□";
            Camera.character[3] = "△△";
            Camera.character[4] = "××";
            Camera.character[5] = "・";
            Camera.character[6] = "・";
            Random random = new Random();
            int y = 0, x = 0;
            while (true)
            {
                if ((GetAsyncKeyState((int)ConsoleKey.W) & 0x8000) != 0)
                {
                    if (y > 0)
                        y--;
                }
                if ((GetAsyncKeyState((int)ConsoleKey.S) & 0x8000) != 0)
                {
                    y++;
                }
                if ((GetAsyncKeyState((int)ConsoleKey.D) & 0x8000) != 0)
                {
                    x++;
                }
                if ((GetAsyncKeyState((int)ConsoleKey.A) & 0x8000) != 0)
                {
                    if (x > 0)
                        x--;
                }
                Camera.CameraReView(x,y,60,60,10,1);//カメラの表示
                Thread.Sleep(32);
                int r = random.Next(0, 7);
                int pxr = random.Next(0, 90);
                int pyr = random.Next(0, 90);
                Camera.map[pyr][pxr] = r;
            }
        }
    }
    

}
