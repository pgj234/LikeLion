using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study_4
{
    class Program
    {
        static void Main(string[] args)
        {
            //// 문자열
            //string greeting;    // 문자열 변수를 선언
            //greeting = "Hello, World!";     // 변수에 값을 저장

            //// 변수의 값을 사용
            //Console.WriteLine(greeting);

            //// 변수 선언과 초기화를 한번에 수행
            //int score = 100;                // 정수형 100점
            //double temperature = 36.5;      // 실수형 변수 선언과 초기화
            //string city = "Jeon-Ju";        // 문자열 변수 선언과 초기화

            //// 변수 출력
            //Console.WriteLine(score);
            //Console.WriteLine(temperature);
            //Console.WriteLine(city);

            // 같은 데이터 타입의 변수를 쉼표로 구분해서 선언
            //int x = 10, y = 20, z = 30;     // 정수형 변수 x, y, z 를 선언하고 초기화

            //Console.WriteLine(x);
            //Console.WriteLine(y);
            //Console.WriteLine(z);

            //const double Pi = 3.14159;      // 상수 pi선언 및 초기화
            //const int MaxScore = 100;       // 정수형 상수 선언

            //Console.WriteLine("Pi : " + Pi);
            //Console.WriteLine("Max Score : " + MaxScore);

            // 기본 특성
            int atk = 16755;
            int maxHP = 78103;

            // 전투 특성
            int cri = 36;
            int specialization = 1017;
            int overpowering = 41;
            int spd = 611;
            int patience = 22;
            int skilled = 39;

            Console.WriteLine("----- 기본 특성 -----");
            Console.WriteLine("공격력 : " + atk);
            Console.WriteLine("최대 생명력 : " + maxHP);
            Console.WriteLine("");

            Console.WriteLine("----- 전투 특성 -----");
            Console.WriteLine("치명 : " + cri);
            Console.WriteLine("특화 : " + specialization);
            Console.WriteLine("제압 : " + overpowering);
            Console.WriteLine("신속 : " + spd);
            Console.WriteLine("인내 : " + patience);
            Console.WriteLine("숙련 : " + skilled);
        }
    }
}
