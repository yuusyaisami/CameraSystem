/* camera system class
 * 作者:        Yuisami
 * 目的:        二次配列を効率よく描写し、手間を省く
 * バージョン:  v1.0.0
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraSystem
{
    public static class Camera
    {
        private static string name = "";
        public static int map_height;
        private static int map_width;

        private static int camera_posi_x;
        private static int camera_posi_y;
        private static int camera_height;
        private static int camera_width;

        public static int[][] map = new int[1024][];
        private static int[][] map_old = new int[1024][];
        /// <summary>
        /// 表示させるマップアイコンを指定
        /// </summary>
        public static string[] character = new string[1024];
        /// <summary>
        /// マップアイコンのサイズを指定(半角は1, 全角は2)
        /// </summary>
        public static int character_size = 2;
        /// <summary>
        /// マップを作成する関数
        /// </summary>
        /// <param name="map_name">マップの名前</param>
        /// <param name="height_size">マップの高さ</param>
        /// <param name="wide_size">マップの横幅</param>
        public static void Create(string map_name, int height_size, int wide_size)
        {
            //マップの初期化
            for (int i = 0; i < 1024; i++)
            {
                map[i] = new int[1024];
                for (int j = 0; j < 1024; j++)
                {
                    map[i][j] = 0;
                }
            }
            //前回値マップの初期化
            for (int i = 0; i < 1024; i++)
            {
                map_old[i] = new int[1024];
                for (int j = 0; j < 1024; j++)
                {
                    map_old[i][j] = 0;
                }
            }
            name = map_name;
            map_height = height_size;
            map_width = wide_size;
        }
        /// <summary>
        /// マップをリセットする関数
        /// </summary>
        public static void Delete()
        {
            for (int i = 0; i < 1024; i++)
            {
                for (int j = 0; j < 1024; j++)
                {
                    map[i][j] = 0;
                    map_old[i][j] = 0;
                }
            }
        }
        /// <summary>
        /// マップを表示する関数
        /// </summary>
        public static void View() //ただ描写するだけの関数
        {
            for (int i = 0; i < map_height; i++)
            {
                string String = "";
                for (int j = 0; j < map_width; j++)
                {
                    for (int k = 0; k < 1024; k++)
                    {
                        if (map[i][j] == k)
                        {
                            String += character[k];
                            break;
                        }
                    }
                }
                Console.WriteLine(String);
            }
        }
        /// <summary>
        /// 前回のマップと変わった部分のみ変更する関数
        /// </summary>
        public static void ReView()//Viewを使用した後に使用するべき関数
        {
            for (int i = 0; i < map_height; i++)
            {
                for (int j = 0; j < map_width; j++)
                {
                    if (map[i][j] != map_old[i][j]) //前回と変更があったら
                    {
                        for (int k = 0; k < 1024; k++)
                        {
                            if (map[i][j] == k)
                            {
                                Console.SetCursorPosition(j * 2, i); //変更点の座標にカーソルを移動
                                Console.WriteLine(character[k]); //書き換える
                                map_old[i][j] = map[i][j]; //前回値マップを更新
                                break;
                            }
                        }
                    }
                }
            }
            Console.SetCursorPosition(0, map_height);
        }
        /// <summary>
        /// map配列をコンソールに表示する関数
        /// </summary>
        /// <param name="first_position_x">mapの座標xを入力(左上)</param>
        /// <param name="first_position_y">mapの座標yを入力(左上)</param>
        /// <param name="height">カメラの高さを入力</param>
        /// <param name="width">カメラの横幅を入力</param>
        /// <param name="cursor_position_x">コンソール描写時の最初の座標x</param>
        /// <param name="cursor_position_y">コンソール描写時の最初の座標y</param>
        /// <param name="template_frame">Template</param>
        public static void CameraReView(int first_position_x, int first_position_y, int height, int width, int cursor_position_x = 0, int cursor_position_y = 0)
        {
            bool changelog_flag = false;
            //カメラの座標が変わったか
            if (first_position_x != camera_posi_x)
            {
                changelog_flag = true; //変わっていたらフラッグを立てる
                camera_posi_x = first_position_x;
            }
            if (first_position_y != camera_posi_y)
            {
                camera_posi_y = first_position_y; //変わっていたらフラッグを立てる
                changelog_flag = true;
            }

            for (int i = 0; ; i++)
            {
                if (map_height >= first_position_y + height - i)
                {
                    height = first_position_y + height - i;
                    break;
                }
            }
            for (int i = 0; ; i++)
            {
                if (map_width >= first_position_x + width - i) //マップのサイズがオーバーしているか
                {
                    width = first_position_x + width - i; //サイズがオーバーしていたら、最大値を入力
                    break;
                }
            }
            if (height != camera_height) //高さが変わったか
            {
                camera_height = height; //更新
                changelog_flag = true; //変わっていたらフラッグを立てる
            }
            if (width != camera_width)
            {
                camera_width = width; 
                changelog_flag = true;//変わっていたらフラッグを立てる
            }
            //前回と変更があったか確認する この関数は残しておくが使わない
            /*
            for (int i = first_position_y; i < height; i++)
            {
                for (int j = first_position_x; j < width; j++)
                {
                    if (map[i][j] != map_old[i][j])
                    {
                        for (int k = 0; k < 1024; k++)
                        {
                            if (map[i][j] == k)
                            {
                                map_old[i][j] = map[i][j];
                                changelog_flag |= true;
                                break;
                            }
                        }
                    }
                }
            }
            */
            //画面をリセットする
            if (changelog_flag)
            {
                Console.Clear();
                Console.SetCursorPosition(0, cursor_position_y);

                for (int i = first_position_y; i < height; i++)
                {
                    string String = "";
                    for(int a = 0; a < cursor_position_x * character_size; a++)
                    {
                        String += " "; //カーソンの移動分だけスペースを置く
                    }
                    for (int j = first_position_x; j < width; j++)
                    {
                        for (int k = 0; k < 1024; k++)
                        {
                            if (map[i][j] == k)
                            {
                                String += character[k]; 
                                break;
                            }
                        }
                        map_old[i][j] = map[i][j];
                    }
                    Console.WriteLine(String); //i行の文字を出力する

                }
            }
            //変更された部分のみ書き換える
            for (int i = first_position_y; i < height; i++)
            {
                for (int j = first_position_x; j < width; j++)
                {
                    if (map[i][j] != map_old[i][j])
                    {
                        for (int k = 0; k < 1024; k++)
                        {
                            if (map[i][j] == k)
                            {
                                Console.SetCursorPosition(((j * character_size) + (cursor_position_x * character_size)) - first_position_x * character_size, (cursor_position_y + i) - first_position_y); //変更された部分のみ書き換える
                                Console.WriteLine(character[k]);
                                map_old[i][j] = map[i][j];
                                break;
                            }
                        }
                    }
                }
            }
            
            Console.SetCursorPosition(0, 0); //意味はないが、最初に戻る
        }
        /// <summary>
        /// 表示させたいテキストを指定の座標に出力する関数
        /// </summary>
        /// <param name="x">テキスト文最初の座標x</param>
        /// <param name="y">テキスト文最初の座標y</param>
        /// <param name="Text">表示させるテキスト</param>
        public static void StandardWriteText(int x, int y, string text)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(text);
            Console.SetCursorPosition(0, map_height);
        }
        //考え中
        public static void WriteTexts(string textname, int x, int y, string text)
        {

        }
    }
}
