using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace study17
{
    //struct Rectangle
    //{
    //    public int Width;
    //    public int Height;

    //    public int GetArea() => Width * Height;
    //}

    struct Student
    {
        public string Name;
        public int KorScore;
        public int EngScore;
        public int MathScore;
    }

    class Program
    {
        // C# 구조체
        // 클래스와 비슷하지만, 값 타입이며 가볍고 빠름
        // 주로 간단한 데이터 묶음을 만들 때 사용

        //struct Point
        //{
        //    // public 어디서든 사용 가능
        //    // private 나만 사용하려고 하는 키워드
        //    public int X;
        //    public int Y;

        //    // 생성자 정의 : 처음 생성할 때 동작하는 함수
        //    public Point(int x, int y)
        //    {
        //        X = x;
        //        Y = y;
        //    }

        //    public void Print()
        //    {
        //        Console.WriteLine($"좌표 : {X}, {Y}");
        //    }
        //}

        // struct에도 생성자 사용 가능 (매개변수를 통한 초기화 가능)
        // 모든 필드를 반드시 초기화해야 함 (클래스와 다름)
        static void Main(string[] args)
        {
            //Point pClass;   // 구조체 선언 (초기화 필요)

            //Point p1 = new Point(5, 15);
            //p1.Print();

            //var rect = new Rectangle { Width = 5, Height = 4 };
            //Rectangle rect;
            //rect.Width = 10;
            //rect.Height = 20;
            //Console.WriteLine($"Area : {rect.GetArea()}");

            //Point[] pointArray = new Point[2];

            //pointArray[0].X = 10;
            //pointArray[0].Y = 10;

            //pointArray[1].X = 20;
            //pointArray[1].Y = 20;

            //foreach (Point point in pointArray)
            //{
            //    Console.WriteLine($"Point : ({point.X}, {point.Y})");
            //}

            Student[] studentArray = new Student[3];
            for (int i=0; i<studentArray.Length; i++)
            {
                Console.Write($"이름 입력 ({i + 1} / {studentArray.Length}) : ");
                studentArray[i].Name = Console.ReadLine();
                Console.Write($"국어 점수 입력 ({i + 1} / {studentArray.Length}) : ");
                studentArray[i].KorScore = int.Parse(Console.ReadLine());
                Console.Write($"영어 점수 입력 ({i + 1} / {studentArray.Length}) : ");
                studentArray[i].EngScore = int.Parse(Console.ReadLine());
                Console.Write($"수학 점수 입력 ({i + 1} / {studentArray.Length}) : ");
                studentArray[i].MathScore = int.Parse(Console.ReadLine());

                Console.WriteLine("------------------------------------");
            }

            Console.Clear();
            Console.WriteLine("이름\t국어\t영어\t수학");

            for (int i = 0; i < studentArray.Length; i++)
            {
                Console.WriteLine($"{studentArray[i].Name}\t{studentArray[i].KorScore}\t{studentArray[i].EngScore}\t{studentArray[i].MathScore}");
            }
        }
    }
}
