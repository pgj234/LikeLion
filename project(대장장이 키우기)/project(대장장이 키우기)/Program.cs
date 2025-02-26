using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace project대장장이키우기
{
    class Program
    {
        static void Main(string[] args)
        {
            // 랜덤
            Random rand = new Random();

            Console.WriteLine(" 대장장이 키우기 ");

            int money = 100;
            int input;
            int rnd;

            Thread.Sleep(500);

            while (true)
            {
                Console.Clear();
                Console.WriteLine(" 1. 나무 캐기 ");
                Console.WriteLine(" 2. 장비 뽑기 ");
                Console.WriteLine(" 3. 나가기 ");
                Console.Write("입력 : ");

                input = int.Parse(Console.ReadLine());      // input에 키로 눌린 숫자 담기

                if (1 == input)     // 나무 캐기 화면
                {
                    while (true)
                    {
                        Console.WriteLine(" 나무 캐기 (엔터) ");
                        Console.WriteLine(" 뒤로가기 x ");

                        string str = Console.ReadLine();

                        money += 100;
                        Console.WriteLine(" 소지금 : " + money);
                        if ("x" == str)
                        {
                            Console.WriteLine(" 뒤로가기 ");
                            break;
                        }
                    }
                }
                else if (2 == input)
                {
                    // 장비 뽑기
                    if (money >= 1000)      // 돈이 있는지 확인 후 뽑기
                    {
                        money -= 1000;

                        // 20번 뽑기
                        for (int i = 0; i < 20; i++)
                        {
                            rnd = rand.Next(0, 100);

                            if (1 > rnd)       // 1퍼
                            {
                                Console.WriteLine(" 도끼 등급 SSS ");
                            }
                            else if (6 > rnd)
                            {
                                Console.WriteLine(" 도끼 등급 SS ");
                            }
                            else if (17 > rnd)
                            {
                                Console.WriteLine(" 도끼 등급 S ");
                            }
                            else if (38 > rnd)
                            {
                                Console.WriteLine(" 도끼 등급 A ");
                            }
                            else if (69 > rnd)
                            {
                                Console.WriteLine(" 도끼 등급 B ");
                            }
                            else
                            {
                                Console.WriteLine(" 도끼 등급 C ");
                            }

                            Thread.Sleep(300);
                        }
                    }
                    else
                    {
                        Console.WriteLine(" 돈이 부족합니다 \n");
                        Thread.Sleep(1000);
                    }
                }
                else if (3 == input)
                {
                    Console.WriteLine(" 나갑니다 ");
                    Thread.Sleep(1000);
                    Environment.Exit(0);
                }
            }
        }
    }
}