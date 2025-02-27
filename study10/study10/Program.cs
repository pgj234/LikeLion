using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace study10
{
    class Program
    {
        static void Main(string[] args)
        {
            // 배열
            // 0부터 시작

            //int[] num = new int[3];     // 3개 메모리 만들겠다

            //num[0] = 10;
            //num[1] = 20;
            //num[2] = 30;

            //for (int i=0; i<3; i++)
            //{
            //    Console.WriteLine(num[i]);
            //}

            //int[] array = { 1, 2, 3 };              // 간단한 선언과 초기화
            //int[] array2 = new int[3];              // 크기만 지정
            //int[] array3 = new int[] { 1, 2, 3 };   // 초기화와 함께 선언

            //for (int i=0; i<3; i++)
            //{
            //    Console.WriteLine(array3[i]);
            //}

            //string[] fruits = { "사과", "바나나", "오렌지" };

            //for (int i=0; i<3; i++)
            //{
            //    Console.WriteLine(fruits[i]);
            //}

            // 3명의 국어 영어, 수학 점수를 입력받고
            // 총점과 평균을 출력
            //int[] iKor = new int[3];
            //int[] iEng = new int[3];
            //int[] iMath = new int[3];

            //int[] sum = new int[3];
            //float[] avg = new float[3];

            //for (int i=0; i<3; i++)
            //{
            //    Console.Write($"학생 {i+1} 국어 점수 입력 : ");
            //    iKor[i] = int.Parse(Console.ReadLine());
            //    Console.Write($"학생 {i+1} 영어 점수 입력 : ");
            //    iEng[i] = int.Parse(Console.ReadLine());
            //    Console.Write($"학생 {i+1} 수학 점수 입력 : ");
            //    iMath[i] = int.Parse(Console.ReadLine());

            //    Console.WriteLine("--------------------------------");

            //    sum[i] = iKor[i] + iEng[i] + iMath[i];
            //    avg[i] = (float)sum[i] / sum.Length;
            //}

            //for (int i=0; i<3; i++)
            //{
            //    Console.WriteLine($"학생 {i+1} 총점 : {sum[i]}, 평균 : {avg[i].ToString("F2")}");
            //}


            // 1차원 배열
            //int[] scoreArray = new int[3];

            //scoreArray[0] = 90;
            //scoreArray[1] = 85;
            //scoreArray[2] = 88;

            //for (int i=0; i<scoreArray.Length; i++)
            //{
            //    Console.WriteLine($"점수 {i + 1} : {scoreArray[i]}");
            //}

            //double value = 123.456789;
            //// 소수점 자리수를 설정하는 포맷
            //Console.WriteLine(value.ToString("F2"));
            //Console.WriteLine($"소수점 둘째자리 : {value:F2}");
            //Console.WriteLine(string.Format("소수점 셋쨰자리 : {0:F3}", value));
            //Console.WriteLine(value.ToString("F0"));    // 소수점 없이 정수 출력

            //double value = 123_124124.12323;    // _는 그냥 구분만 하는 것
            //Console.WriteLine(value);


            // 2차원 배열 선언
            //int[,] matrix = new int[3, 3] { { 1, 2, 3}, { 4, 5, 6 }, { 7, 8, 8} };

            //for (int i=0; i<matrix.GetLength(0); i++)
            //{
            //    for (int j = 0; j < matrix.GetLength(1); j++)
            //    {
            //        Console.Write($"{matrix[i, j]}");
            //    }
            //    Console.WriteLine();
            //}


            // 가변 배열
            //int[][] matrix = new int[3][];

            //matrix[0] = new int[4];
            //matrix[1] = new int[2];
            //matrix[2] = new int[2];


            //// 값 입력
            //matrix[0][0] = 1;
            //matrix[0][1] = 2;
            //matrix[0][2] = 3;
            //matrix[0][3] = 4;

            //matrix[1][0] = 2;
            //matrix[1][1] = 4;

            //matrix[2][0] = 3;
            //matrix[2][1] = 6;

            //for (int i=0; i<matrix.Length; i++)
            //{
            //    for (int j=0; j < matrix[i].Length; j++)
            //    {
            //        Console.Write(matrix[i][j] + " ");
            //    }
            //    Console.WriteLine();
            //}

            //Console.WriteLine("가변 배열");
            //int[][] jaggedArray = new int[3][];

            //jaggedArray[0] = new int[2] { 1, 2 };
            //jaggedArray[1] = new int[5] { 2, 4, 6, 8, 10 };
            //jaggedArray[2] = new int[3] { 3, 6, 9 };

            //for (int i=0; i<jaggedArray.Length; i++)
            //{
            //    for (int j=0; j < jaggedArray[i].Length; j++)
            //    {
            //        Console.Write(jaggedArray[i][j]);
            //    }
            //    Console.WriteLine();
            //}

            //Console.WriteLine("var 키워드 사용");
            //var numbers = new[] { 1, 2, 3, 4, 5 };

            //Console.WriteLine($"배열 타입 : {numbers.GetType()}");
        }
    }
}
