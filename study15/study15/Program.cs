using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace study15
{
    class Program
    {
        // 일반적인 함수
        static int Add(int a, int b)
        {
            return a + b;
        }

        // 화살표 함수 (람다)
        static int AddArrow(int a, int b) => a + b;

        // 중괄호 {} return 생략 가능 (화살표 함수 (람다))

        // 일반적인 함수
        static void PrintMsg()
        {
            Console.WriteLine("하이요");
        }

        // 화살표 함수
        static void PrintMsgArrow() => Console.WriteLine("하이요");

        static void Main(string[] args)
        {
            // C# 화살표 함수
            // C#에서 화살표 함수 => 람다 표현식
            // 간결한 방식으로 함수를 정의할 수 있다
            Console.WriteLine(Add(3, 5));
            Console.WriteLine(AddArrow(3, 5));

            PrintMsg();
            PrintMsgArrow();
        }
    }
}
