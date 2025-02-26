using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace study9
{
    class Program
    {
        static void Main(string[] args)
        {
            // switch문

            //int day = 3;

            //switch (day)
            //{
            //    case 1:
            //        Console.WriteLine("월요일");
            //        break;
            //    case 2:
            //        Console.WriteLine("화요일");
            //        break;
            //    case 3:
            //        Console.WriteLine("수요일");
            //        break;
            //    case 4:
            //        Console.WriteLine("목요일");
            //        break;
            //    case 5:
            //        Console.WriteLine("금요일");
            //        break;
            //    default:
            //        Console.WriteLine("주말!");
            //        break;
            //}

            // 캐릭터를 선택하세요 (1.전사 2.마법사 3.도적)
            // 스위치문 사용해보기
            //Console.Write("캐릭터 선택 : ");
            //switch (int.Parse(Console.ReadLine()))
            //{
            //    case 1:
            //        Console.WriteLine("============================");
            //        Console.WriteLine("전사");
            //        Console.WriteLine("공격력 100");
            //        Console.WriteLine("방어력 90");
            //        break;

            //    case 2:
            //        Console.WriteLine("============================");
            //        Console.WriteLine("마법사");
            //        Console.WriteLine("공격력 110");
            //        Console.WriteLine("방어력 80");
            //        break;

            //    case 3:
            //        Console.WriteLine("============================");
            //        Console.WriteLine("도적");
            //        Console.WriteLine("공격력 115");
            //        Console.WriteLine("방어력 70");
            //        break;
            //}

            // for 반복문
            //for (int i = 1; i < 6; i++)
            //{
            //    Console.WriteLine($"숫자 : {i}");
            //}

            // 무한반복
            //for (; ; )
            //{
            //    Console.WriteLine("중꺽마");
            //}

            // 0~9까지 출력 (for)
            //for (int i=0; i<10; i++)
            //{
            //    Console.Write($"{i} ");
            //}

            // 1~10까지 합 구하기
            //int sum = 0;
            //for (int i = 1; i < 11; i++)
            //{
            //    sum += i;
            //}

            //Console.Write($"1 ~ 10 합 : {sum} ");

            // while 반복문
            //int cnt = 1;
            //while (cnt < 6)
            //{
            //    cnt++;

            //    if (3 == cnt)
            //    {
            //        Console.WriteLine("3일 때 반복문 탈출");
            //        break;
            //    }
            //}

            //Console.WriteLine($"cnt : " + cnt);


            // 랜덤
            //Random rand = new Random();     // Random 객체를 생성

            //// 0이상 10미만의 랜덤 정수 생성
            //int randNum = 0;
            //for (int i=0; i<10; i++)
            //{
            //    randNum = rand.Next(5, 15);
            //    Console.WriteLine($"5이상 14까지 : {randNum}");

            //}


            // 대장장이 키우기
            // 도끼 등급 SSS    10%
            // 도끼 등급 SS     40%
            // 도끼 등급 S      50%

            //int num = 0;
            //Random rand = new Random();     // Random 객체를 생성
            //num = rand.Next(0, 100);
            //Console.WriteLine("num : " + num);
            //if (num < 10)
            //{
            //    Console.WriteLine("도끼 등급 SSS");
            //}
            //else if (num < 50)
            //{
            //    Console.WriteLine("도끼 등급 SS");
            //}
            //else if (num < 100)
            //{
            //    Console.WriteLine("도끼 등급 S");
            //}


            // do while 반복문
            // 1회 무조건 실행하고 while 진행
            //int x = 5;

            //do
            //{
            //    Console.WriteLine($"do! {x}");
            //    x--;
            //}
            //while (x > 0);


            // break 문
            // 반복문 탈출
            //for (int i=0; i<10; i++)
            //{
            //    if (5 == i)
            //    {
            //        break;
            //    }

            //    Console.WriteLine(i);
            //}


            // continue
            // 현재 반복을 건너뛰고 다음 반복으로 넘어간다
            //for (int i=0; i<10; i++)
            //{
            //    if (0 == i % 2)
            //    {
            //        continue;
            //    }

            //    Console.WriteLine(i);       // 홀수만 출력
            //}


            // goto
            //int n = 1;

            //start:
            //if (5 >= n)
            //{
            //    Console.WriteLine(n);
            //    n++;

            //        goto start;     // start라는 이름의 레이블로 이동
            //}
        }
    }
}
