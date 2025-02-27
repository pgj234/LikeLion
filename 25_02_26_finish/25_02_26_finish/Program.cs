using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25_02_26_finish
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Q1. 배열 요소 출력");
            //Console.WriteLine("");

            //int[] numArray = new int[5];
            //for (int i=0; i<numArray.Length; i++)
            //{
            //    numArray[i] = i * 10 + 10;
            //    Console.Write($"{numArray[i]} ");
            //}
            //Console.WriteLine("\n\n==========================================\n");

            //Console.WriteLine("Q2. 배열 요소 합 구하기");
            //Console.WriteLine("");

            //numArray[0] = 0;
            //for (int i = 0; i < numArray.Length; i++)
            //{
            //    Console.Write($"정수 입력 ({i+1}/{numArray.Length}) : ");
            //    numArray[0] += int.Parse(Console.ReadLine());
            //}
            //Console.WriteLine("");
            //Console.WriteLine(string.Format("총 합 : {0}", numArray[0]));

            //Console.WriteLine("\n\n==========================================\n");

            //Console.WriteLine("Q3. 최대값 찾기");
            //Console.WriteLine("");

            //numArray = new int[5] { 3, 8, 15, 6, 2 };

            //for (int i = 0; i < numArray.Length; i++)
            //{
            //    if (numArray[0] < numArray[i])      // 새로 비교하는 숫자가 더 큼
            //    {
            //        numArray[0] = numArray[i];
            //    }
            //}

            //Console.WriteLine("최대값 : " + numArray[0]);



            //Console.WriteLine("Q4. 1부터 10까지 출력 (for)");
            //Console.WriteLine();

            //for (int i = 0; i < 10; i++)
            //{
            //    Console.Write($"{i+1} ");
            //}
            //Console.WriteLine("\n\n==========================================\n");

            //Console.WriteLine("Q5. 1~10 중에 짝수만 출력 (while)");
            //Console.WriteLine("");

            //int num = 1;
            //while (num < 11)
            //{
            //    if (0 == num % 2)
            //    {
            //        Console.Write($"{num} ");
            //    }

            //    num++;
            //}

            //Console.WriteLine("\n\n==========================================\n");

            //Console.WriteLine("Q6. 배열 요소 출력 (foreach)");
            //Console.WriteLine();

            //int[] numArray = { 1, 2, 3, 4, 5 };
            //foreach (int n in numArray)
            //{
            //    Console.Write($"{n} ");
            //}
            //Console.WriteLine();



            Console.WriteLine("Q7. 두 수의 합을 구하는 함수");
            Console.WriteLine();

            int input_a = 0, input_b = 0;

            Console.Write("정수 입력 (1/2) : ");
            input_a = int.Parse(Console.ReadLine());

            Console.Write("정수 입력 (2/2) : ");
            input_b = int.Parse(Console.ReadLine());

            Console.WriteLine($"두 수의 합 : {SumCal(input_a, input_b)}");
            Console.WriteLine("\n==========================================\n");

            Console.WriteLine("Q8. 문자열 길이 반환 함수");
            Console.WriteLine();
            Console.Write("문자열 입력 : ");

            string inputStr = Console.ReadLine();
            Console.WriteLine();

            Console.Write("해당 문자열 길이 : " + StringLength(inputStr));

            Console.WriteLine("\n\n==========================================\n");

            Console.WriteLine("Q9. 가장 큰 수 반환 함수");
            Console.WriteLine();

            Console.Write("정수 입력 (1/3) : ");
            input_a = int.Parse(Console.ReadLine());

            Console.Write("정수 입력 (2/3) : ");
            input_b = int.Parse(Console.ReadLine());

            Console.Write("정수 입력 (3/3) : ");
            input_a = MaxNum(new int[] { input_a, input_b, int.Parse(Console.ReadLine()) });

            Console.WriteLine();
            Console.Write($"가장 큰 수 : {input_a}");
            Console.WriteLine();
        }

        // 두 수의 합
        static int SumCal(int a, int b)
        {
            return a + b;
        }

        // 문자열 길이
        static int StringLength(string str)
        {
            return str.Length;
        }

        // 최대값
        static int MaxNum(int[] numArray)
        {
            for (int i=0; i<numArray.Length; i++)
            {
                if (numArray[0] < numArray[i])      // 비교하는 수가 더 큼
                {
                    numArray[0] = numArray[i];
                }
            }

            return numArray[0];
        }
    }
}
