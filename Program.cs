using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flychess
{
    public class Program
    {
        //names[0]存玩家a姓名，1存b的
        public static string[] names= new string[2];
        public static int[] map= new int[100];
        public static int[] playerpos = { 0, 0 };
        public static void Main(string[] args)
        {
            input(0, "A"); input(1, "B");
            Console.Clear();
            showUI();
            initmap();
            drawmap();
            string winner = startplay();
            Console.WriteLine("{0} won!! game end", winner);
            Console.ReadKey();
        }
        public static int ReadInt(int min, int max){
            while(true){
                try
                {
                    int number = Convert.ToInt32(Console.ReadLine());
                    if (number < min || number > max)
                    {
                        Console.WriteLine("over range");
                        continue;
                    }
                    return number;
                }
                catch
                {
                    Console.WriteLine("not a number,retype");
                }
            }
        }
        public static void showUI()
        {
            Console.WriteLine("*******************************");
            Console.WriteLine("*                             *");
            Console.WriteLine("*     my fly chess            *");
            Console.WriteLine("*                             *");
            Console.WriteLine("*******************************");
            Console.WriteLine("game start");
            Console.WriteLine("{0} represent as A", names[0]);
            Console.WriteLine("{0} represent as B", names[1]);
            Console.WriteLine("if A,B at the same position,use<> to represent");
        }
        public static void input(int No,string Na)
        {
            Console.WriteLine("please input player {0}'s name:",Na);
            names[No] = Console.ReadLine();
            //判断用户B输入的内容是否为空
            while (names[No] == "" || names[1] == names[0])
            {
                if (names[No] == "")
                {
                    Console.WriteLine("player{0}'s name should not be null",Na);
                }
                else
                {
                    Console.WriteLine("player {0}'s name has been used!!",Na);
                }
                names[1] = Console.ReadLine();
            }
            Console.WriteLine("player {0}'s name has been confirmed as: {1}",Na ,names[No]);
        }
        public static void initmap()
        {
            int[] luckyturn ={6,23,40,55,69,83,98};
            int[] landmine = { 5,13,17,33,38,50,64,80,94 };
            int[] pause = { 9,27,60,93 };
            int[] timetunnel = {20,25,45,63,72,88,90,99};
            int i = 0;
            for(i=0;i<luckyturn.Length;i++){
                map[luckyturn[i]] = 1;
            }
            for (i = 0; i < landmine.Length; i++)
            {
                map[landmine[i]] = 2;
            }
            for (i = 0; i < pause.Length; i++)
            {
                map[pause[i]] = 3;
            }
            for (i = 0; i < timetunnel.Length; i++)
            {
                map[timetunnel[i]] = 4;
            }
        }
        public static void drawmap()
        {

            for (int i = 0; i <= 29; i++)
            {
                drawmapdetail(i);
            }
            Console.WriteLine("");
            for (int i = 30; i <= 34; i++)
            {
                for (int j = 0; j <= 28; j++) { 
                    Console.Write("  ");
                }
                drawmapdetail(i);
                Console.WriteLine("");
            }
            for (int i = 64; i >= 35; i--)
            {
                drawmapdetail(i);
            }
            Console.WriteLine("");
            for (int i = 65; i <= 69; i++)
            {
                drawmapdetail(i);
                Console.WriteLine("");
            }
            for (int i = 70; i <= 99; i++)
            {
                drawmapdetail(i);
            }
            Console.WriteLine("");
        }
        public static void drawmapdetail(int i)
        {
            string result = "";
            if (i == playerpos[0] && i == playerpos[1])
            {
                 Console.ForegroundColor = ConsoleColor.Yellow;
                 result="<>";
            }
            else if (i == playerpos[0])
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                result="Ａ";
            }
            else if (i == playerpos[1])
            {
                Console.ForegroundColor = ConsoleColor.Red;
                result="Ｂ";
            }
            else
            {
                switch (map[i])
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.White;
                        result="□";
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Red;
                        result="●";
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Green;
                        result="★";
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        result="▲";
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        result="卐";
                        break;
                }
            }
            Console.Write(result);
            Console.ResetColor();
        }
        public static string startplay()
        {
            Random r = new Random();
            while (playerpos[0] < 99 && playerpos[1] < 99)
            {
                //A开始掷骰子
                playprocess(r, 0);
                //B开始掷骰子
                playprocess(r, 1);
            }
            return playerpos[0] < playerpos[1] ? names[1] : names[0];
        }
        public static void playprocess(Random ran, int player)
        {
            Console.WriteLine("{0} enter anythin to start roll:", names[player]);
            Console.ReadKey(false);
            playerpos[player] += ran.Next(1, 7);
            checkpos();
            exucute();
            poseffect(player);
            Console.WriteLine("{0} get the pos: {1}", names[player], playerpos[player]);
            exucute();
        }
        public static void checkpos()
        {
            for (int i = 0; i < playerpos.Length; i++)
            {
                if (playerpos[i] < 0)
                {
                    playerpos[i] = 0;
                }
                if (playerpos[i] > 99)
                {
                    playerpos[i] = 99;
                }
            }
        }
        public static void exucute()
        {   
            Console.WriteLine("type anything to start move");
            Console.ReadKey(false);
            Console.Clear();
            showUI();
            drawmap();
        }
        public static void poseffect(int who)
        {
            int other;
            other = (who == 0 ? 1 : 0);
            if (playerpos[0] >= 99 || playerpos[1] >= 99)
            {
                return;
            }
            if (playerpos[who] == playerpos[other])
            {
                playerpos[other] = 0;
                Console.WriteLine("{0} have meet {1},{2} get back to origin",who,other,other);
            }else{
                switch (map[playerpos[who]])
                {
                case 0:
                    break;
                case 1:
                    Console.WriteLine("{0} have reached lucky pos!!!!!!!!!!!!!!!!!",who);
                    Console.WriteLine("1--change pos; 2--boom opponent");
                    int userselect = ReadInt(1, 2);
                    if (userselect == 1)
                    {
                        int temp = playerpos[who];
                        playerpos[who]=playerpos[1];
                        playerpos[1] = temp;
                        poseffect(who);
                    }
                    else
                    {
                        playerpos[other] = playerpos[other] - 6;
                        checkpos();
                    }
                    break;
                case 2:
                    Console.WriteLine("{0} have reached bomb!!!!!!!!!!!!!!!!!",who);
                    playerpos[who] = playerpos[who] - 6;
                    checkpos();
                    poseffect(who);
                    break;
                case 3:
                    Console.WriteLine("{0} have reached pause!!!!!!!!!!!!!!!!!1",who);
                    break;
                case 4:
                    Console.WriteLine("{0} have reached tunnel!!!!!!!!!!!!!!!!",who);
                    playerpos[who] = playerpos[who] + 10;
                    checkpos();
                    poseffect(who);
                    break;
                }
            }
        }
    }
}
