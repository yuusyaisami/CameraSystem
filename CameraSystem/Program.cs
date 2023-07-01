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

namespace CameraSystem
{
    internal class Program
    {
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Map.Create("map",12,12);
            Map.character[0] = "・";
            Map.character[1] = " 𓃟";
            Map.View();
            Map.map[6][0] = 1;
            int y = 0;
            while (true)
            {
                if ((GetAsyncKeyState((int)ConsoleKey.W) & 0x8000) != 0)
                {
                    Map.map[y][1] = 1;
                    y++;
                }
                Map.CameraReView(0,y,12,12);
                Thread.Sleep(1000);
            }
        }
    }
    public static class Map
    {
        public static string name = "";
        public static int map_height;
        public static int map_width;

        public static int camera_posi_x;
        public static int camera_posi_y;
        public static int camera_height;
        public static int camera_width;

        public static int[][] map = new int[1024][];
        public static int[][] map_old = new int[1024][];
        public static string[] character = new string[1024];
        public static void Create(string map_name, int height_size, int wide_size)
        {
            for(int i = 0; i < 1024; i++)
            {
                map[i] = new int[1024];
                for(int j = 0; j < 1024; j++)
                {
                    map[i][j] = 0;
                }
            }
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
        public static void View()
        {
            for(int i = 0; i < map_height; i++)
            {
                string String = "";
                for(int j = 0; j < map_width; j++)
                {
                    for(int k = 0; k < 1024; k++)
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
        public static void ReView()
        {
            for (int i = 0; i < map_height; i++)
            {
                for (int j = 0; j < map_width; j++)
                {
                    if (map[i][j] != map_old[i][j])
                    {
                        for (int k = 0; k < 1024; k++)
                        {
                            if (map[i][j] == k)
                            {
                                Console.SetCursorPosition(j * 2, i);
                                Console.WriteLine(character[k]);
                                map_old[i][j] = map[i][j];
                                break;
                            }
                        }
                    }
                }
            }
            Console.SetCursorPosition(0, map_height);
        }
        public static void CameraReView(int first_position_x, int first_position_y, int height, int width)
        {
            bool changelog_flag = false;
            if (first_position_x != camera_posi_x)
            {
                changelog_flag = true;
                camera_posi_x = first_position_x;
            }
            if(first_position_y != camera_posi_y)
            {
                camera_posi_y = first_position_y;
                changelog_flag = true;
            }

            for (int i = 0;; i++) {
                if (map_height > first_position_y + height - i)
                {
                    height = first_position_y + height - i;
                    break;
                }
            }
            for (int i = 0; ; i++)
            {
                if (map_width > first_position_x + width - i)
                {
                    width = first_position_x + width - i;
                    break;
                }
            }
            if (height != camera_height)
            {
                camera_height = height;
                changelog_flag = true;
            }
            if (width != camera_width)
            {
                camera_width = width;
                changelog_flag = true;
            }
            if (changelog_flag)
            {
                Console.Clear();
                for (int i = first_position_y; i < height + first_position_y; i++)
                {
                    string String = "";
                    for (int j = first_position_x; j < width + first_position_x; j++)
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
                    Console.WriteLine(String);
                }
            }
            for (int i = first_position_y; i < height + first_position_y; i++)
            {
                for (int j = first_position_x; j < width + first_position_x; j++)
                {
                    if (map[i][j] != map_old[i][j])
                    {
                        for (int k = 0; k < 1024; k++)
                        {
                            if (map[i][j] == k)
                            {
                                Console.SetCursorPosition(j * 2, i);
                                Console.WriteLine(character[k]);
                                map_old[i][j] = map[i][j];
                                break;
                            }
                        }
                    }
                }
            }
            Console.SetCursorPosition(0, map_height);
        }
    }
}
