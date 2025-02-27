using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bingo
{
    class Program
    {
        static void Main(string[] args)
        {
            //int[] iArray = new int[25];

            //for (int i=0; i<iArray.Length; i++)
            //{
            //    iArray[i] = i + 1;
            //}

            //Random rand = new Random();
            //// 셔플
            //for (int i=0; i<100; i++)
            //{
            //    int iA = rand.Next(0, 25);
            //    int iB = rand.Next(0, 25);
            //    int temp = 0;

            //    temp = iArray[iA];
            //    iArray[iA] = iArray[iB];
            //    iArray[iB] = temp;
            //}

            //int input = 0;      // 플레이어 입력값
            //int iBingo = 0;
            //int iCnt = 0;

            //while (true)
            //{
            //    Console.Clear();

            //    for (int i = 0; i < 5; i++)
            //    {
            //        for (int j = 0; j < 5; j++)
            //        {
            //            if (0 == iArray[i * 5 + j])
            //            {
            //                Console.Write(" * ");
            //            }
            //            else
            //            {
            //                Console.Write(string.Format("{0:D2} ", iArray[i * 5 + j]));
            //            }
            //        }
            //        Console.WriteLine();
            //    }
            //    Console.WriteLine();

            //    Console.WriteLine($"빙고 수 : {iBingo}");
            //    Console.Write("숫자 입력 : ");
            //    input = int.Parse(Console.ReadLine());
            //    iBingo = 0;

            //    for (int i=0; i<iArray.Length; i++)
            //    {
            //        if (input == iArray[i])
            //        {
            //            iArray[i] = 0;
            //            break;
            //        }
            //    }

            //    // 빙고 체크 로직
            //    // 가로 체크
            //    for (int i = 0; i < 5; i++)
            //    {
            //        for (int j=0; j<5; j++)
            //        {
            //            if (0 == iArray[i*5 +j])
            //            {
            //                iCnt++;
            //            }

            //            if (5 == iCnt)
            //            {
            //                iBingo++;
            //            }
            //        }
            //        iCnt = 0;
            //    }

            //    // 세로 체크
            //    for (int i = 0; i < 5; i++)
            //    {
            //        for (int j = 0; j < 5; j++)
            //        {
            //            if (0 == iArray[i + 5 * j])
            //            {
            //                iCnt++;
            //            }

            //            if (5 == iCnt)
            //            {
            //                iBingo++;
            //            }
            //        }
            //        iCnt = 0;
            //    }

            //    // 대각선 오른쪽 체크
            //    // 00 01 02 03 04
            //    // 05 06 07 08 09
            //    // 10 11 12 13 14
            //    // 15 16 17 18 19
            //    // 20 21 22 23 24
            //    for (int i = 0; i < 5; i++)
            //    {
            //        if (0 == iArray[i * 4 + 4])
            //        {
            //            iCnt++;
            //        }

            //        if (5 == iCnt)
            //        {
            //            iBingo++;
            //        }

            //        iCnt = 0;
            //    }

            //    // 대각선 왼쪽 체크
            //    for (int i = 0; i < 5; i++)
            //    {
            //        if (0 == iArray[i * 6])
            //        {
            //            iCnt++;
            //        }

            //        if (5 == iCnt)
            //        {
            //            iBingo++;
            //        }

            //        iCnt = 0;
            //    }

            //    if (4 < iBingo)
            //    {
            //        Console.WriteLine("빙고 성공");
            //        break;
            //    }
            //}


            int[,] board = new int[5, 5];        // 5 x 5 빙고판
            bool[,] marked = new bool[5, 5];     // 선택된 숫자 체크

            int bingoCnt = 0;

            Random ran = new Random();

            // 빙고판 초기화
            int[] numbers = new int[25];

            for (int i=0; i<numbers.Length; i++)
            {
                numbers[i] = i + 1;
            }

            // 랜덤 섞기
            for (int i = 0; i < 100; i++)
            {
                int a = ran.Next(25);
                int b = ran.Next(25);

                // C#의 튜플 문법으로 두 변수의 값을 교환 (tmp 필요X)
                (numbers[a], numbers[b]) = (numbers[b], numbers[a]);
            }

            // 2차원 배열로 변환
            int idx = 0;

            for (int i=0; i< board.GetLength(0); i++)
            {
                for (int j=0; j< board.GetLength(1); j++)
                {
                    board[i, j] = numbers[idx++];
                }
            }

            int inputNum = 0;

            // 게임 시작
            while (5 > bingoCnt)
            {
                Console.Clear();

                // 빙고판 출력
                Console.WriteLine("현재 빙고판");

                for (int i=0; i< board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        if (marked[i, j])
                        {
                            Console.Write(" X ");
                        }
                        else
                        {
                            Console.Write($"{board[i, j], 2} ");
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine($"현재 빙고 수 : {bingoCnt}");
                Console.WriteLine();
                Console.Write("숫자 입력 (1 ~ 25) : ");
                inputNum = int.Parse(Console.ReadLine());

                bool isFind = false;
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        if (inputNum == board[i, j])
                        {
                            marked[i, j] = true;    // 숫자를 X로 변경
                            isFind = true;
                            break;
                        }
                    }

                    if (true == isFind)
                    {
                        break;
                    }
                }

                // 빙고 개수 체크
                bingoCnt = 0;

                // 가로 체크
                for (int i=0; i< board.GetLength(0); i++)
                {
                    bool rowBingo = true;
                    for (int j=0; j<board.GetLength(1); j++)
                    {
                        if (false == marked[i, j])
                        {
                            rowBingo = false;
                        }
                    }
                    if (true == rowBingo)
                    {
                        bingoCnt++;
                    }
                }

                // 세로 체크
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    bool colBingo = true;
                    for (int i=0; i< board.GetLength(0); i++)
                    {
                        if (false == marked[i, j])
                        {
                            colBingo = false;
                        }
                    }
                    if (true == colBingo)
                    {
                        bingoCnt++;
                    }
                }

                // 대각선 체크 (왼쪽 위 -> 오른쪽 아래)
                bool diag1Bingo = true;
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    if (false == marked[i, i])
                    {
                        diag1Bingo = false;
                    }
                }
                if (true == diag1Bingo)
                {
                    bingoCnt++;
                }

                // 대각선 체크 (오른쪽 위 -> 왼쪽 아래)
                bool diag2Bingo = true;
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    if (false == marked[i, 4 - i])
                    {
                        diag2Bingo = false;
                    }
                }
                if (true == diag2Bingo)
                {
                    bingoCnt++;
                }
            }

            Console.WriteLine("");
            Console.WriteLine("빙고 5개 완성! 게임종료");
        }
    }
}
