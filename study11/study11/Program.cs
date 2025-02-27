using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace study11
{
    class Program
    {
        static void Loading()
        {
            Console.WriteLine("로딩 중.");
            Thread.Sleep(1000);
            Console.WriteLine("로딩 중..");
            Thread.Sleep(1000);
            Console.WriteLine("로딩 중...");
            Thread.Sleep(1000);
        }

        static void AttackFunction(int _atk)
        {
            Console.WriteLine("공격력 : " + _atk);
        }

        static int BaseAtk()
        {
            // 기본 공격력 10
            int atk = 10;

            return atk;
        }

        static void Main(string[] args)
        {
            //int atk = 0;
            //int bAtk = 0;
            //Console.WriteLine("캐릭터의 공격력을 입력 : ");
            //atk = int.Parse(Console.ReadLine());

            //bAtk = BaseAtk();

            //AttackFunction(bAtk + atk);

            // 두 수를 더하는 함수 만들어서 오류를 해결하세요.
            //int result = Add(10, 20);
            //Console.WriteLine($"10 + 20 = {result}");

            // 놓친 foreach 반복문
            //string[] fruits = { "사과", "바나나", "체리" };
            //foreach (string fruit in fruits)
            //{
            //    Console.WriteLine(fruit);
            //}
        }
        //static int Add(int a, int b)
        //{
        //    return a + b;
        //}
    }
}
