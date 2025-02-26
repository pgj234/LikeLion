using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 콘솔좌표배우기
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = 80;     // 사이즈

            // 콘솔 창 크기 설정
            Console.SetWindowSize(size, size);

            // 콘솔 버퍼 크기도 설정 (스크롤 없이 고정된 창 유지)
            Console.SetBufferSize(size, size);

            Console.CursorVisible = false;

            Console.Clear();

            Random ran = new Random();

            ushort gameState = 0;               // 0:메뉴 선택 중, 1:게임 중, 2:게임 오버

            int ball_x = 0;                     // 공 x좌표
            int ball_y = 0;                     // 공 y좌표
            int ballSmashCnt = 0;               // 내가 볼을 쳐낸 횟수
            int playerDefenceNum = 0;           // 플레이어 막을 곳 번호

            string inputStr = "";               // 사용자 입력 string

            bool firstStart = true;             // 처음 아래 벽 닿아도 게임오버 방지
            bool isBallHorizontal_ReflectionRight = true;   // 공이 벽 튕겨나갈 방향 true가 오른쪽
            bool isBallVertical_ReflectionDown = true;      // 공이 벽 튕겨나갈 방향 true가 아래쪽

            while (true)
            {
                Thread.Sleep(10);

                Console.Clear();

                for (int y = 0; y < size; y++)                // 화면 그리기
                {
                    Console.SetCursorPosition(0, y);

                    switch (gameState)
                    {
                        case 0:     // 메뉴 선택 중
                            switch (y)
                            {
                                case 0:
                                case 1:
                                    break;

                                case 2:         // 위 테두리
                                    Console.Write("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
                                    break;

                                case 33:        // 가운데
                                    Console.Write("┃                                이 지    핑 퐁                                ┃");
                                    break;

                                case 45:        // 메뉴 1번
                                    Console.Write("┃                                1. 게임 시작                                  ┃");
                                    break;

                                case 47:        // 메뉴 2번
                                    Console.Write("┃                                2. 종료                                       ┃");
                                    break;

                                case 79:        // 아래 테두리
                                    Console.Write("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");

                                    // 게임 시작 메뉴 선택
                                    inputStr = Console.ReadLine();
                                    if (0 == string.Compare("1", inputStr))
                                    {
                                        ball_x = ran.Next(1, size - 1);     // 0이랑 오른쪽 끝엔 벽 좌표
                                        ball_y = 4;                                  // 0은 공백, 1은 점수 UI, 2랑 아래쪽 끝엔 벽 좌표, 3은 상대 골키퍼

                                        isBallHorizontal_ReflectionRight = true;               // 처음은 오른쪽으로
                                        isBallVertical_ReflectionDown = true;                  // 처음은 아래쪽으로

                                        inputStr = "";
                                        gameState = 1;
                                        ballSmashCnt = 0;
                                        firstStart = true;
                                    }
                                    else if (0 == string.Compare("2", inputStr))
                                    {
                                        Environment.Exit(1);
                                    }
                                    break;

                                default:        // 좌우 벽
                                    Console.Write("┃                                                                              ┃");
                                    break;
                            }

                                break;

                        case 1:     // 게임 중
                            switch (y)
                            {
                                case 0:
                                    break;

                                case 1:
                                    Console.Write($"                                 쳐낸 횟수 : {ballSmashCnt}");
                                    break;

                                case 2:         // 위 테두리
                                    Console.Write("┏━━━━━━━━━━━━━━━━━━━━━━━━━┳┳━━━━━━━━━━━━━━━━━━━━━━━━┳┳━━━━━━━━━━━━━━━━━━━━━━━━━┓");
                                    break;

                                case 3:     // 상대 골키퍼 위치 리프레시
                                    Console.Write("┃                                                                              ┃");

                                    Console.SetCursorPosition(ball_x, y);
                                    Console.Write("━");

                                    break;

                                case 78:        // 내 방어선
                                    if (0 != string.Compare("", inputStr))      // 방어선 선택 되어있으면 방어선 표시
                                    {
                                        switch (inputStr)
                                        {
                                            case "1":       // 왼쪽
                                                Console.Write("┃━━━━━━━━━━━━━━━━━━━━━━━━━━                                                    ┃");
                                                break;

                                            case "2":       // 중앙
                                                Console.Write("┃                          ━━━━━━━━━━━━━━━━━━━━━━━━━━                          ┃");
                                                break;

                                            case "3":       // 오른쪽
                                                Console.Write("┃                                                   ━━━━━━━━━━━━━━━━━━━━━━━━━━━┃");
                                                break;
                                        }
                                    }
                                    break;

                                case 79:        // 아래 테두리
                                    Console.Write("┗━━━━━━━━━━━━━━━━━━━━━━━━━┻┻━━━━━━━━━━━━━━━━━━━━━━━━┻┻━━━━━━━━━━━━━━━━━━━━━━━━━┛");

                                    // 공 위치 갱신
                                    Console.SetCursorPosition(ball_x, ball_y);
                                    Console.Write("●");

                                    // 오른쪽, 왼쪽
                                    if (true == isBallHorizontal_ReflectionRight)      // 오른쪽 벽 검사
                                    {
                                        if (ball_x > size - 2)     // 오른쪽 벽에 닿은 상태이므로 다음 왼쪽으로 방향 바꾸고 공 왼쪽으로 보내기
                                        {
                                            isBallHorizontal_ReflectionRight = false;

                                            ball_x -= 1;
                                        }
                                        else        // 오른쪽 벽에 안닿았으니 그대로 진행
                                        {
                                            ball_x += 1;
                                        }
                                    }
                                    else        // 왼쪽 벽 검사
                                    {
                                        if (ball_x < 2)     // 왼쪽 벽에 닿은 상태이므로 다음 오른쪽으로 방향 바꾸고 공 오른쪽으로 보내기
                                        {
                                            isBallHorizontal_ReflectionRight = true;

                                            ball_x += 1;
                                        }
                                        else        // 왼쪽 벽에 안닿았으니 그대로 진행
                                        {
                                            ball_x -= 1;
                                        }
                                    }

                                    // 위, 아래
                                    if (true == isBallVertical_ReflectionDown)      // 아래 벽 검사
                                    {
                                        ball_y += 1;

                                        if (ball_y > size - 3)      // 아래 벽에 닿아서 게임오버
                                        {
                                            gameState = 2;
                                        }
                                        else if (ball_y == size - 3)       // 플레이어 선에 닿은 상태이므로 선 닿았는지 체크         다음 위쪽으로 방향 바꾸고 공 위쪽으로 보내기
                                        {
                                            if (true == firstStart)
                                            {
                                                isBallVertical_ReflectionDown = false;

                                                firstStart = false;
                                            }
                                            else
                                            {
                                                switch (int.Parse(inputStr))
                                                {
                                                    case 1:       // 왼쪽
                                                        if (27 > ball_x)    // 막음
                                                        {
                                                            isBallVertical_ReflectionDown = false;

                                                            ball_y -= 1;

                                                            ballSmashCnt += 1;
                                                        }
                                                        break;

                                                    case 2:       // 중앙
                                                        if (26 < ball_x && ball_x < 53)    // 막음
                                                        {
                                                            isBallVertical_ReflectionDown = false;

                                                            ball_y -= 1;

                                                            ballSmashCnt += 1;
                                                        }
                                                        break;

                                                    case 3:       // 오른쪽
                                                        if (52 < ball_x)    // 막음
                                                        {
                                                            isBallVertical_ReflectionDown = false;

                                                            ball_y -= 1;

                                                            ballSmashCnt += 1;
                                                        }
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                    else        // 위 적 골키퍼에 닿음 검사
                                    {
                                        if (ball_y < 5)       // 위 적 골키퍼에 닿은 상태이므로 다음 랜덤으로 방향 바꾸고 공 아래쪽으로 보내기 + x 값 랜덤으로 약간 수정 (플레이어 선택까지)
                                        {
                                            isBallVertical_ReflectionDown = true;

                                            ball_x += ran.Next(-7, 8);

                                            if (ball_x > size - 2)      // 오른쪽 벽 좌표 이상이므로 X랜덤으로 다시 설정
                                            {
                                                ball_x = size - 2;
                                            }
                                            else if (ball_x < 2)        // 왼쪽 벽 좌표 이하이므로 왼쪽 최대 값으로 설정
                                            {
                                                ball_x = 2;
                                            }

                                            if (0 > ball_x)     // 왼쪽으로 팅구기
                                            {
                                                isBallHorizontal_ReflectionRight = false;
                                            }
                                            else        // 오른쪽으로 팅구기
                                            {
                                                isBallHorizontal_ReflectionRight = true;
                                            }

                                            Console.SetCursorPosition(0, 40);
                                            Console.Write("┃                               방어선 번호 선택                               ┃");
                                            for (int i=44; i<size -1; i++)
                                            {
                                                if (60 == i)
                                                {
                                                    Console.SetCursorPosition(0, i);
                                                    Console.Write("┃            1            ┃┃            2           ┃┃            3            ┃");
                                                }
                                                else
                                                {
                                                    Console.SetCursorPosition(0, i);
                                                    Console.Write("┃                         ┃┃                        ┃┃                         ┃");
                                                }
                                            }

                                            Console.SetCursorPosition(39, 42);
                                            inputStr = Console.ReadLine();
                                            switch (inputStr)
                                            {
                                                case "1":
                                                case "2":
                                                case "3":
                                                    playerDefenceNum = int.Parse(inputStr);
                                                    break;

                                                default:
                                                    continue;
                                            }
                                        }
                                        else        // 아래 벽에 안닿았으니 그대로 진행
                                        {
                                            ball_y -= 1;
                                        }
                                    }

                                        break;

                                default:        // 좌우 벽
                                    Console.Write("┃                                                                              ┃");
                                    break;
                            }

                            break;

                        case 2:         // 게임 오버
                            Console.SetCursorPosition(34, 40);
                            Console.Write($"쳐낸 횟수 : {ballSmashCnt}");
                            Console.SetCursorPosition(24, 42);
                            Console.Write("메인메뉴로 가려면 엔터를 누르세요");
                            Console.ReadLine();
                            gameState = 0;
                            break;
                    }
                }
            }
        }
    }
}
