using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace study3
{
    class Program
    {
        static void Main(string[] args)
        {
            // 리터럴 : 코드에서 고정된 값을 의미
            //int number = 10;        // 정수형 리터럴
            //double pi = 3.14;       // 실수형 리터럴
            //char letter = 'A';      // 문자형 리터럴
            //string name = "Park";   // 문자열 리터럴

            //Console.WriteLine(number);    // 출력 : 10
            //Console.WriteLine(pi);        // 출력 : 3.14
            //Console.WriteLine(letter);    // 출력 : A
            //Console.WriteLine(name);      // 출력 : Park

            // 캐릭터
            int hp = 100;                   // hp : 100
            double atk = 56.7;              // atk : 56.7
            string charName = "Park";       // 캐릭터 이름 : Park
            char grade = 'S';               // 등급 : S

            Console.WriteLine("캐릭터");
            Console.WriteLine("체력 : " + hp);
            Console.WriteLine("공격력 : " + atk);
            Console.WriteLine("캐릭터이름 : " + charName);
            Console.WriteLine("등급 : " + grade);
        }
    }
}