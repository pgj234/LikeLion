using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace study7
{
    class Program
    {
        static void Main(string[] args)
        {
            // 단항 연산자
            //int number = 5;
            //bool flag = true;

            //Console.WriteLine(number);      // 양수 출력 : 5
            //Console.WriteLine(-number);     // 양수 출력 : -5

            //Console.WriteLine(!flag);       // 논리 부정 : false;

            // ~ 비트 반전
            // 10    1010
            //       0101

            //int num = 10;
            //int result = ~num;      // 모든 비트 반전 : 1111 1111 1111 1111 1111 1111 1111 0101

            //Console.WriteLine("원래 값 : " + num);
            //Console.WriteLine("~ 연산자 적용 후 : " + result);

            // 캐스팅
            //double pi = 3.14;
            //int integerPi = (int)pi;    // 실수형 -> 정수형으로 변환

            //Console.WriteLine(integerPi);   // 3

            //int i_Kor = 90;
            //int i_Eng = 75;
            //int i_Math = 58;

            //int sum = 0;
            //float average = 0.0f;

            //sum = i_Kor + i_Eng + i_Math;

            //average = (float)sum / 3;      // 평균

            //Console.WriteLine("총점 : " + sum);
            //Console.WriteLine("평균 : " + average);

            //// 문자열 연결 연산자
            //string firstName = "Alice";
            //string lastName = "Smith";

            //Console.WriteLine(firstName + " " + lastName);

            //int a = 10;

            //a += 5;         // a = a + 5;
            //Console.WriteLine(a);

            //a -= 5;         // a = a - 5;
            //Console.WriteLine(a);

            //a *= 5;         // a = a * 5;
            //Console.WriteLine(a);

            //a /= 5;         // a = a / 5;
            //Console.WriteLine(a);

            //a %= 5;         // a = a % 5;
            //Console.WriteLine(a);



            //// 문제 1. 학점 평균 계산 프로그램
            //int korScore = 0;
            //int engScore = 0;
            //int mathScore = 0;
            //int totalScore = 0;

            //float avg = 0;

            //Console.WriteLine("문제 1. 학점 평균 계산 프로그램");
            //Console.WriteLine("---------------------------------");

            //Console.Write("국어 점수 입력 : ");
            //korScore = int.Parse(Console.ReadLine());
            //Console.WriteLine("---------------------------------");

            //Console.Write("영어 점수 입력 : ");
            //engScore = int.Parse(Console.ReadLine());
            //Console.WriteLine("---------------------------------");

            //Console.Write("수학 점수 입력 : ");
            //mathScore = int.Parse(Console.ReadLine());
            //Console.WriteLine("---------------------------------");

            //totalScore = korScore + engScore + mathScore;
            //avg = (float)totalScore / 3;

            //Console.WriteLine($"총점 : {totalScore}");
            //Console.WriteLine($"평균 : {avg.ToString("F2")}");
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();



            //// 문제 2. 비트 반전(~) 연산자 활용 프로그램
            //int num = 0;

            //Console.WriteLine("문제 2. 비트 반전(~) 연산자 활용 프로그램");
            //Console.WriteLine("---------------------------------");

            //Console.Write("정수 입력 : ");
            //num = int.Parse(Console.ReadLine());
            //Console.WriteLine("---------------------------------");

            //Console.WriteLine($"원래의 값 : {num}");
            //Console.WriteLine($"비트 반전의 값 : {~num}");

            // 증감 연산자

            //int b = 3;

            // 전위(++b), 후위(b++)
            //Console.WriteLine("b의 값은 : " + b++);
            //Console.WriteLine("b의 값은 : " + b);

            //Console.WriteLine("b의 값은 : " + b--);
        }
    }
}
