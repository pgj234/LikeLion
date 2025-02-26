using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 모험가키우기
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = new UTF8Encoding();

            Random rand = new Random();
            
            int gold = 500;
            int health = 100;
            int power = 10;
            int input;

            bool isAlive = true;

            Console.WriteLine(" ⚔️ 모험가 키우기 ⚔️ ");
            Thread.Sleep(2000);

            while (isAlive)
            {
                Console.Clear();
                Console.WriteLine($" 현재 상태 : 체력 {health} | 골드 {gold} | 공격력 {power} ");
                Console.WriteLine("\n 1. 탐험하기 🏕️");
                Console.WriteLine("2. 장비 뽑기 🎲 (1000골드)");
                Console.WriteLine("3. 휴식하기 💤 (체력 +20)");
                Console.WriteLine("4. 게임 종료");
                Console.Write("입력 : ");
                
                input = int.Parse(Console.ReadLine());

                if (1 == input)     // 탐험
                {
                    Console.Clear();
                    Console.WriteLine(" 탐험을 떠납니다. ");
                    Thread.Sleep(500);
                    Console.WriteLine(" 탐험을 떠납니다.. ");
                    Thread.Sleep(500);
                    Console.WriteLine(" 탐험을 떠납니다... ");
                    Thread.Sleep(500);
                    Console.WriteLine(" 탐험을 떠납니다.... ");
                    Thread.Sleep(500);

                    int eventChance = rand.Next(0, 100);    // 0 ~ 99 랜덤 이벤트 발생

                    if (30 > eventChance)       // 30% 확률 인카운터
                    {
                        int damage = rand.Next(4, 20);
                        Console.WriteLine($" ⚔️ 몬스터의 습격! (체력 -{damage}) ");
                        health -= damage;
                    }
                    else if (70 > eventChance)      // 40% 확률 보상
                    {
                        int reward = rand.Next(100, 301);   // 100 ~ 300 골드
                        Console.WriteLine($" 💰 보물 발견! (+{reward} 골드) ");
                        gold += reward;
                    }
                    else    // 30% 확률 회복
                    {
                        int heal = rand.Next(10, 31);   // 10 ~ 30 체력 회복
                        Console.WriteLine($" 🌿 신비한 약초를 발견! (+{heal} 체력) ");
                        health += heal;
                    }

                    if (0 >= health)
                    {
                        Console.WriteLine("\n 💀 사망 💀 ");
                        isAlive = false;
                    }

                    Thread.Sleep(1500);
                }
                else if (2 == input)    // 장비 뽑기
                {
                    if (1000 <= gold)
                    {
                        gold -= 1000;
                        Console.Clear();
                        Console.WriteLine(" 🎲 장비를 뽑습니다 (-1000골드) ");
                        Thread.Sleep(1000);

                        int rnd = rand.Next(0, 100);    // 0 ~ 99 랜덤
                        if (0 == rnd)
                        {
                            Console.WriteLine("SSS급 전설의 검 (공격력 +50) 획득!");
                            power += 50;
                        }
                        else if (10 > rnd)
                        {
                            Console.WriteLine("SS급 희귀한 검 (공격력 +30) 획득!");
                            power += 30;
                        }
                        else if (30 > rnd)
                        {
                            Console.WriteLine("S급 희귀한 검 (공격력 +20) 획득!");
                            power += 20;
                        }
                        else
                        {
                            Console.WriteLine("녹슨 검 (공격력 + 5) 획득!");
                            power += 5;
                        }
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Console.WriteLine("골드 부족!");
                        Thread.Sleep(1000);
                    }
                }
                else if (3 == input)    // 휴식
                {
                    Console.WriteLine("휴식! (체력 +20)");
                    health += 20;
                    Thread.Sleep(1000);
                }
                else if (4 == input)    // 게임 종료
                {
                    Console.WriteLine("게임 종료");
                    Environment.Exit(1);
                }
                else
                {
                    Console.WriteLine("잘못된 입력");
                    Thread.Sleep(1000);
                }
            }
            Thread.Sleep(2500);
            Environment.Exit(1);
        }
    }
}