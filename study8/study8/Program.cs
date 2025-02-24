using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace study8
{
    class Program
    {
        static void Main(string[] args)
        {
            // 두 값을 비교하여 관계를 평가한다.

            //int x = 5, y = 10;

            //Console.WriteLine(x == y);      // false
            //Console.WriteLine(x != y);      // true
            //Console.WriteLine(x < y);       // true
            //Console.WriteLine(x > y);       // false
            //Console.WriteLine(x >= y);      // false
            //Console.WriteLine(x <= y);      // true

            // 논리 연산자

            //bool a = true;
            //bool b = false;

            //Console.WriteLine(a && b);
            //Console.WriteLine(a || b);

            //b = !a;

            //Console.WriteLine(b);

            // 비트 연산자
            //int x = 5;      // 0101
            //int y = 3;      // 0011

            //Console.WriteLine(x & y);       // AND
            //Console.WriteLine(x | y);       // OR
            //Console.WriteLine(x ^ y);       // XOR
            //Console.WriteLine(~x);          // NOT

            //int value = 4;  // 0100

            //Console.WriteLine(value << 1);      // 왼쪽 이동 : 8 (1000)
            //Console.WriteLine(value >> 1);      // 왼쪽 이동 : 2 (0010)
            //Console.WriteLine(value << 9);      // 왼쪽 이동 : 2048

            //int a = 10, b = 20;

            //int max;

            //max = (a < b) ? a : b;      // 삼항 연산자

            //Console.WriteLine(max);

            //int key = 1;

            //string str = (1 == key) ? "문이 열렸습니다" : "문을 열지 못했습니다";

            //Console.WriteLine(str);

            //int result = 10 + 2 * 5;    // 곱셈이 덧셈보다 우선
            //Console.WriteLine(result);

            //int adjustedResult = (10 + 2) * 5;  // 괄호 우선순위 먼저 계산
            //Console.WriteLine(adjustedResult);

            //int score = 85;
            //if (score >= 90)
            //{
            //    Console.WriteLine("A 학점");
            //}
            //else
            //{
            //    Console.WriteLine("B 학점");
            //}

            //string gameID = "멋멋";

            //if (0 == string.Compare(gameID, "멋멋"))
            //{
            //    Console.WriteLine("아이디가 일치합니다");
            //}
            //else
            //{
            //    Console.WriteLine("아이디가 일치하지 않습니다");
            //}

            //int score = 65;
            //if (score >= 90)
            //{
            //    Console.WriteLine("A 학점");
            //}
            //else if(score >= 80)
            //{
            //    Console.WriteLine("B 학점");
            //}
            //else if (score >= 70)
            //{
            //    Console.WriteLine("C 학점");
            //}
            //else
            //{
            //    Console.WriteLine("F 학점");
            //}



            // 1단계
            // 가지고 있는 소지금을 입력하세요 : 
            // 0 ~ 100  무한의대검 +1
            // 101 ~ 200 카타나 +2
            // 201 ~ 300 진은검 +3
            // 301 ~ 400 집판검 +4
            // 401 ~ 500 엑스칼리버 +5
            // 501 ~ 600 유령검 +6
            // 601 넘어가면 전설의검 +7

            //string nickName = "pgj";
            //string weaponStr = "";
            //int gold = 0;
            //int upgradeNum = 0;

            //Console.Write("소지 금액 임력: ");
            //gold = int.Parse(Console.ReadLine());

            //if (600 < gold)
            //{
            //    upgradeNum = 7;
            //    weaponStr = "전설의검";
            //}
            //else if (500 < gold)
            //{
            //    upgradeNum = 6;
            //    weaponStr = "유령검";
            //}
            //else if (400 < gold)
            //{
            //    upgradeNum = 5;
            //    weaponStr = "엑스칼리버";
            //}
            //else if (300 < gold)
            //{
            //    upgradeNum = 4;
            //    weaponStr = "집판검";
            //}
            //else if (200 < gold)
            //{
            //    upgradeNum = 3;
            //    weaponStr = "진은검";
            //}
            //else if (100 < gold)
            //{
            //    upgradeNum = 2;
            //    weaponStr = "카타나";
            //}
            //else 
            //{
            //    upgradeNum = 1;
            //    weaponStr = "무한의대검";
            //}

            //Console.WriteLine("====================================");


            //// 2단계
            //// 캐릭터 멋사검존
            //// 무기 : 가지고 있는 무기 이름 표시하기
            //// 공격력 : 100 + 1
            //Console.WriteLine($"캐릭터 이름 : {nickName}");
            //Console.WriteLine("무기 : " + weaponStr);
            //Console.WriteLine("공격력 : 100 + " + upgradeNum);
            //Console.WriteLine("----------------------------");



            // 2025.02.24 오후 문제
            // 문제 1. 세 정수의 최대값 구하기
            int num1, num2, num3, maxNum;

            Console.Write("정수 입력 (1/3) : ");
            num1 = int.Parse(Console.ReadLine());
            Console.WriteLine("---------------------------");

            Console.Write("정수 입력 (2/3) : ");
            num2 = int.Parse(Console.ReadLine());
            Console.WriteLine("---------------------------");

            Console.Write("정수 입력 (3/3) : ");
            num3 = int.Parse(Console.ReadLine());
            Console.WriteLine("============================");

            if (num1 > num2)        // num1 최대 상태
            {
                if (num1 > num3)    // num1 최대
                {
                    maxNum = num1;
                }
                else                // num3 최대
                {
                    maxNum = num3;
                }
            }
            else                    // num2 최대 상태
            {
                if (num2 > num3)    // num2 최대
                {
                    maxNum = num2;
                }
                else                // num3 최대
                {
                    maxNum = num3;
                }
            }

            Console.WriteLine("최대값 : " + maxNum);
            Console.WriteLine("============================");
            Console.WriteLine("");



            // 문제 2. 점수에 따른 학점 평가
            Console.Write("점수에 따른 학점 평가");

            Console.Write("점수 입력 : ");
            num1 = int.Parse(Console.ReadLine());
            Console.WriteLine("---------------------------");

            if (num1 >= 90)
            {
                Console.WriteLine("A 학점");
            }
            else if (num1 >= 80)
            {
                Console.WriteLine("B 학점");
            }
            else if (num1 >= 70)
            {
                Console.WriteLine("C 학점");
            }
            else if (num1 >= 60)
            {
                Console.WriteLine("D 학점");
            }
            else
            {
                Console.WriteLine("F 학점");
            }

            Console.WriteLine("============================");
            Console.WriteLine("");



            // 문제 3. 간단한 사칙연산 계산기
            float sum = 0.0f;

            Console.Write("간단한 사칙연산 계산기");
            Console.WriteLine("");

            Console.Write("계산할 두 개의 정수를 입력 (1/2) : ");
            num1 = int.Parse(Console.ReadLine());
            Console.WriteLine("---------------------------");

            Console.Write("계산할 두 개의 정수를 입력 (2/2) : ");
            num2 = int.Parse(Console.ReadLine());
            Console.WriteLine("---------------------------");

            Console.Write("계산할 연산자 입력 (+, -, *, /) : ");
            string symbolStr = Console.ReadLine();
            if (0 == string.Compare("+", symbolStr))
            {
                sum = num1 + num2;
            }
            else if (0 == string.Compare("-", symbolStr))
            {
                sum = num1 - num2;
            }
            else if (0 == string.Compare("*", symbolStr))
            {
                sum = num1 * num2;
            }
            else if (0 == string.Compare("/", symbolStr))
            {
                sum = (float)num1 / num2;
            }
            else
            {
                Console.WriteLine("연산자 입력 오류");
            }

            Console.WriteLine("결과 : " + sum);

            Console.WriteLine("---------------------------");
        }
    }
}
