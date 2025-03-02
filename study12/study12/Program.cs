using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace study12
{
    class Program
    {
        // 전역 변수
        static int num2 = 9;

        // 1. 매개변수도 반환값도 없는 함수
        static void PrintHello()
        {
            Console.WriteLine("하이");
        }

        // 2. 매개변수만 있는 함수
        static void PrintMessage(string msg)
        {
            Console.WriteLine(msg);
        }

        // 3. 반환값만 있는 함수
        static int GetNum()
        {
            return 41;
        }

        // 4. 매개변수와 반환값이 있는 함수
        static int Add(int a, int b)
        {
            return a + b;
        }

        // 5. 기본값을 가진 매개변수 (디폴트 매개변수)
        static void Greet(string name = "ㅎㅇ")
        {
            Console.WriteLine($"오 {name}");
        }

        // 6. 함수 오버로딩
        /// <summary>
        /// 두 수를 곱하는 함수
        /// </summary>
        /// <param name="a">int, double 오버로딩 해놓음</param>
        /// <param name="b">int, double 오버로딩 해놓음</param>
        /// <returns></returns>
        static int Multiply(int a, int b)
        {
            return a * b;
        }

        static double Multiply(double a, double b)
        {
            return a * b;
        }

        // 7. out 키워드 (여러 값을 반환할 때)
        static void Divide(int a, int b, out int qqq, out int re)
        {
            qqq = a / b;

            re = a % b;
        }

        // 8. ref 키워드 (값을 참조하여 수정)
        static void Increase(ref int num)
        {
            num += 10;
        }

        static void Main(string[] args)
        {
            //PrintHello();
            //PrintMessage("안녕");

            //Console.WriteLine(GetNum());
            //Console.WriteLine(num2);

            //Console.WriteLine(Add(3, 5));

            //Greet();
            //Greet("ㄴㄴ");

            //Console.WriteLine(Multiply(3, 4));
            //Console.WriteLine(Multiply(3.24, 5.251));

            //int q, r;
            //Divide(10, 6, out q, out r);

            //Console.WriteLine($"몫 : {q}, 나머지 : {r}");

            int value = 5;
            Increase(ref value);
            Console.WriteLine(value);
        }
    }
}