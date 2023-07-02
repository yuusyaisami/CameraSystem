/* 2DCameraSystem Example
 * 作者: yuisami
 * 目的: CameraSystemの例を紹介
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
        static extern short GetAsyncKeyState(int vKey); //キー入力の状態を取得するための関数
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; //環境依存の文字を出力する場合必要
            Console.SetBufferSize(1000, 5000);  // 幅: 1000 文字、高さ: 5000 行に設定する (小さい数値にするとバッファがオーバーする)
            Camera.Create("map",90,90); //90x90のマップを作る、名前に意味はない
            Camera.character_size = 2;  //文字の大きさを指定する　全角の文字を使う場合は2にする、または半角２文字も設定は2にする。
            Camera.character[0] = "・"; //マップの中身が0に設定されていたら、・を表示する
            Camera.character[1] = "○○"; //以下同じ
            Camera.character[2] = "□□";
            Camera.character[3] = "△△";
            Camera.character[4] = "××";
            Camera.character[5] = "・";
            Camera.character[6] = "・";
            Camera.character[7] = "🥎";
            Camera.map[5][5] = 7; //マップのx5, y5のところに7番(Camera.character[7])の文字を出力する

            Random random = new Random(); //今回はランダムで上記の文字が出力されるようにするために、ランダム関数が必要なのでインスタンスを作る
            int y = 0, x = 0;//表示させるマップの左上の座標
            while (true)
            {
                //キーの状態によってマップの座標を変える
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
                Thread.Sleep(32);//30fps
                int r = random.Next(0, 7); //
                int pxr = random.Next(0, 90);
                int pyr = random.Next(0, 90);
                Camera.map[pyr][pxr] = r;
            }
        }
    }
    

}
