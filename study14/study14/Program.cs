using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 네임스페이스
// 클래스, 함수, 변수 이름이 충돌하는 것을 방지
namespace dev18
{
    class MyClass
    {
        public static void HelloString()
        {
            Console.WriteLine("안녕하세요 (18팀)");
        }
    }
}

namespace study14
{
    class Program
    {
        public static void HelloString()
        {
            Console.WriteLine("안녕하세요 (기본)");
        }

        static void Main(string[] args)
        {
            HelloString();
            dev18.MyClass.HelloString();
        }
    }
}
