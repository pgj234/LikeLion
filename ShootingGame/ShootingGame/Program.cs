using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ShootingGame
{
    // 미사일 클래스
    public class Bullet
    {
        public int x;
        public int y;
        public bool isFire;
    }

    // 플레이어 클래스
    public class Player
    {
        [DllImport("msvcrt.dll")]
        static extern int _getch();  //c언어 함수 가져옴

        public int player_X;    // 플레이어 x좌표
        public int player_Y;    // 플레이어 y좌표
        public Bullet[] playerBullet = new Bullet[20];
        public Bullet[] playerBullet2 = new Bullet[20];
        public Bullet[] playerBullet3 = new Bullet[20];
        public Item item = new Item();
        public int itemCnt = 0;
        public int score = 0;

        public Player()
        {
            // 플레이어 좌표 위치 초기화
            player_X = 0;
            player_Y = 12;

            for (int i=0; i<20; i++)
            {
                playerBullet[i] = new Bullet();
                playerBullet[i].x = 0;
                playerBullet[i].y = 0;
                playerBullet[i].isFire = false;

                playerBullet2[i] = new Bullet();
                playerBullet2[i].x = 0;
                playerBullet2[i].y = 0;
                playerBullet2[i].isFire = false;

                playerBullet3[i] = new Bullet();
                playerBullet3[i].x = 0;
                playerBullet3[i].y = 0;
                playerBullet3[i].isFire = false;
            }
        }

        public void GameMain()
        {
            // 키를 입력하는 부분
            KeyControl();

            // 플레이어를 그려준다
            PlayerDraw();

            // UI 점수
            UIscore();

            if (item.itemLife)
            {
                item.ItemMove();
                item.ItemDraw();
                CrashItem();
            }
        }

        public void KeyControl()
        {
            int pressKey = 0;   // 정수형 변수선언 키 값 받기

            if (Console.KeyAvailable)   // 키가 눌렸을 때 true
            {
                pressKey = _getch();

                if (pressKey == 0 || pressKey == 224) // 화살표 키 또는 특수 키 감지
                {
                    pressKey = _getch(); // 실제 키 값 읽기
                }

                switch (pressKey)
                {
                    case 72:    // 위쪽 방향 아스키 코드
                        player_Y--;
                        if (player_Y < 1)
                        {
                            player_Y = 1;
                        }
                        break;

                    case 75:    // 왼쪽 화살표 키
                        player_X--;
                        if (player_X < 1)
                        {
                            player_X = 1;
                        }
                        break;

                    case 77:    // 오른쪽 화살표 키
                        player_X++;
                        if (player_X > 75)
                        {
                            player_X = 75;
                        }
                        break;

                    case 80:    // 아래 화살표 키
                        player_Y++;
                        if (player_Y > 21)
                        {
                            player_Y = 21;
                        }
                        break;

                    case 32:    // 스페이스 바
                                // 총알 발사
                        for (int i = 0; i < 20; i++)
                        {
                            // 미사일이 false 발사 가능
                            if (false == playerBullet[i].isFire && 1 != itemCnt)
                            {
                                playerBullet[i].isFire = true;

                                // 플레이어 앞에서 미사일 쏘기 + 5
                                playerBullet[i].x = player_X + 5;
                                playerBullet[i].y = player_Y + 1;

                                // 한 발씩 쏘기
                                break;
                            }
                        }

                        for (int i = 0; i < 20; i++)
                        {
                            // 미사일이 false 발사 가능
                            if (false == playerBullet2[i].isFire && 0 < itemCnt)
                            {
                                playerBullet2[i].isFire = true;

                                // 플레이어 앞에서 미사일 쏘기 + 5
                                playerBullet2[i].x = player_X + 5;
                                playerBullet2[i].y = player_Y;

                                // 한 발씩 쏘기
                                break;
                            }
                        }

                        for (int i = 0; i < 20; i++)
                        {
                            // 미사일이 false 발사 가능
                            if (false == playerBullet3[i].isFire)
                            {
                                playerBullet3[i].isFire = true;

                                // 플레이어 앞에서 미사일 쏘기 + 5
                                playerBullet3[i].x = player_X + 5;
                                playerBullet3[i].y = player_Y + 2;

                                // 한 발씩 쏘기
                                break;
                            }
                        }
                        break;
                }
            }
        }

        // 플레이어 그리기
        public void PlayerDraw()
        {
            string[] player = new string[]
            {
                "->",
                ">>>",
                "->"
            };      // 배열 문자열로 그리기

            for (int i=0; i<player.Length; i++)
            {
                // 콘솔 좌표 설정, 플레이어X, 플레이어Y
                Console.SetCursorPosition(player_X, player_Y +i);

                // 문자열 배열 출력
                Console.WriteLine(player[i]);
            }
        }

        // 미사일 그리기
        public void BulletDraw()
        {
            string bullet = "->";       // 미사일 모습

            // 20개
            for (int i=0; i<20; i++)
            {
                // 미사일이 살아있는 상태
                if (true == playerBullet[i].isFire)
                {
                    // 좌표 설정 -> 중간 위치를 위해 보정을 위해 x-1
                    Console.SetCursorPosition(playerBullet[i].x, playerBullet[i].y);
                    // 총알 출력
                    Console.Write(bullet);

                    playerBullet[i].x++;        // 미사일 오른쪽으로 날아가기

                    if (playerBullet[i].x > 78)
                    {
                        playerBullet[i].isFire = false;     // 미사일 false 다시 준비 상태
                    }
                }
            }
        }

        // 미사일 그리기2
        public void BulletDraw2()
        {
            string bullet = "->";       // 미사일 모습

            // 20개
            for (int i = 0; i < 20; i++)
            {
                // 미사일이 살아있는 상태
                if (true == playerBullet2[i].isFire)
                {
                    // 좌표 설정 -> 중간 위치를 위해 보정을 위해 x-1
                    Console.SetCursorPosition(playerBullet2[i].x, playerBullet2[i].y);
                    // 총알 출력
                    Console.Write(bullet);

                    playerBullet2[i].x++;        // 미사일 오른쪽으로 날아가기

                    if (playerBullet2[i].x > 78)
                    {
                        playerBullet2[i].isFire = false;     // 미사일 false 다시 준비 상태
                    }
                }
            }
        }

        // 미사일 그리기3
        public void BulletDraw3()
        {
            string bullet = "->";       // 미사일 모습

            // 20개
            for (int i = 0; i < 20; i++)
            {
                // 미사일이 살아있는 상태
                if (true == playerBullet3[i].isFire)
                {
                    // 좌표 설정 -> 중간 위치를 위해 보정을 위해 x-1
                    Console.SetCursorPosition(playerBullet3[i].x, playerBullet3[i].y);
                    // 총알 출력
                    Console.Write(bullet);

                    playerBullet3[i].x++;        // 미사일 오른쪽으로 날아가기

                    if (playerBullet3[i].x > 78)
                    {
                        playerBullet3[i].isFire = false;     // 미사일 false 다시 준비 상태
                    }
                }
            }
        }

        public void ClashEnemyAndBullet(Enemy enemy)
        {
            // 미사일 20
            for (int i=0; i<20; i++)
            {
                // 살아있는 미사일
                if (true == playerBullet[i].isFire)
                {
                    // 미사일과 적의 y값이 같을 때
                    if (enemy.enemy_Y == playerBullet[i].y)
                    {
                        if (enemy.enemy_X -1 <= playerBullet[i].x && enemy.enemy_X +1 >= playerBullet[i].x)     // 충돌
                        {
                            // 충돌
                            item.itemLife = true;
                            item.itemX = enemy.enemy_X;
                            item.itemY = enemy.enemy_Y;

                            Random rand = new Random();
                            enemy.enemy_X = 75;
                            enemy.enemy_Y = rand.Next(2, 22);

                            playerBullet[i].isFire = false;     // 미사일도 준비 상태로 만들어주기

                            // 점수
                            score += 100;
                        }
                    }
                }
            }

            // 미사일2 20
            for (int i = 0; i < 20; i++)
            {
                // 살아있는 미사일
                if (true == playerBullet2[i].isFire)
                {
                    // 미사일과 적의 y값이 같을 때
                    if (enemy.enemy_Y == playerBullet2[i].y)
                    {
                        if (enemy.enemy_X - 1 <= playerBullet2[i].x && enemy.enemy_X + 1 >= playerBullet2[i].x)     // 충돌
                        {
                            // 충돌
                            item.itemLife = true;
                            item.itemX = enemy.enemy_X;
                            item.itemY = enemy.enemy_Y;

                            Random rand = new Random();
                            enemy.enemy_X = 75;
                            enemy.enemy_Y = rand.Next(2, 22);

                            playerBullet2[i].isFire = false;     // 미사일도 준비 상태로 만들어주기

                            // 점수
                            score += 100;
                        }
                    }
                }
            }

            // 미사일3 20
            for (int i = 0; i < 20; i++)
            {
                // 살아있는 미사일
                if (true == playerBullet3[i].isFire)
                {
                    // 미사일과 적의 y값이 같을 때
                    if (enemy.enemy_Y == playerBullet3[i].y)
                    {
                        if (enemy.enemy_X - 1 <= playerBullet3[i].x && enemy.enemy_X + 1 >= playerBullet3[i].x)     // 충돌
                        {
                            // 충돌
                            item.itemLife = true;
                            item.itemX = enemy.enemy_X;
                            item.itemY = enemy.enemy_Y;

                            Random rand = new Random();
                            enemy.enemy_X = 75;
                            enemy.enemy_Y = rand.Next(2, 22);

                            playerBullet3[i].isFire = false;     // 미사일도 준비 상태로 만들어주기

                            // 점수
                            score += 100;
                        }
                    }
                }
            }
        }



        public void UIscore()
        {
            Console.SetCursorPosition(63, 0);
            Console.Write("┏━━━━━━━━━━━━━━┓");
            Console.SetCursorPosition(63, 1);
            Console.Write("┃              ┃");
            Console.SetCursorPosition(65, 1);
            Console.Write("Score : " + score);
            Console.SetCursorPosition(63, 2);
            Console.Write("┗━━━━━━━━━━━━━━┛");
        }

        // 아이템 충돌이 일어나면 양쪽 미사일 발사
        public void CrashItem()
        {
            if (item.itemY == player_Y +1)
            {
                if (player_X >= item.itemX-2 && player_X <= item.itemX +2)
                {
                    item.itemLife = false;

                    if (itemCnt < 3)
                    {
                        itemCnt++;
                    }

                    // 총알 초기화
                    for (int i=0; i<playerBullet.Length; i++)
                    {
                        playerBullet[i] = new Bullet();
                        playerBullet[i].x = 0;
                        playerBullet[i].y = 0;
                        playerBullet[i].isFire = false;

                        playerBullet2[i] = new Bullet();
                        playerBullet2[i].x = 0;
                        playerBullet2[i].y = 0;
                        playerBullet2[i].isFire = false;

                        playerBullet3[i] = new Bullet();
                        playerBullet3[i].x = 0;
                        playerBullet3[i].y = 0;
                        playerBullet3[i].isFire = false;
                    }
                }
            }
        }
    }

    // 적 클래스
    public class Enemy
    {
        public int enemy_X;     // X좌표
        public int enemy_Y;     // Y좌표

        public Enemy()
        {
            // 적 좌표 초기화
            enemy_X = 75;
            enemy_Y = 12;
        }

        // 적 움직임
        public void EnemyMove()
        {
            Random rand = new Random();     // 랜덤
            enemy_X--;      // 왼쪽으로 움직임

            if (enemy_X < 2)        // 화면 왼쪽 넘어가면 새로 좌표 잡기
            {
                enemy_X = 75;       // 좌표 75
                enemy_Y = rand.Next(2, 22);     // 2~21
            }
        }

        // 적 그리기
        public void EnemyDraw()
        {
            string enemy = "<-0->";     // 문자열로 표현
            Console.SetCursorPosition(enemy_X, enemy_Y);        // 좌표 설정
            Console.Write(enemy);    // 출력
        }
    }

    // 아이템 클래스
    public class Item
    {
        public string itemName;
        public string itemSprite;
        public int itemX = 0;
        public int itemY = 0;
        public bool itemLife = false;

        bool moveAvailable = false;

        public void ItemDraw()
        {
            Console.SetCursorPosition(itemX, itemY);
            itemSprite = "★";
            Console.Write(itemSprite);
        }

        public void ItemMove()
        {
            if (2 > itemX)
            {
                itemLife = false;
            }

            if (false == moveAvailable)
            {
                moveAvailable = true;
            }
            else
            {
                itemX -= 1;
                moveAvailable = false;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(80, 25);
            Console.SetBufferSize(80, 25);

            // 플레이어 생성
            Player player = new Player();
            Enemy enemy = new Enemy();      // 적 생성

            // 유니티처럼 속도 프레임 속도
            int dwTime = Environment.TickCount;     // 1000 = 1초

            while (true)
            {
                // 0.05초 지연
                if (dwTime + 50 < Environment.TickCount)
                {
                    // 현재 시간 세팅
                    dwTime = Environment.TickCount;
                    Console.Clear();

                    player.GameMain();      // 플레이어
                    // 총알
                    if (0 == player.itemCnt)
                    {
                        player.BulletDraw();
                    }
                    else if (1 == player.itemCnt)
                    {
                        player.BulletDraw2();
                        player.BulletDraw3();
                    }
                    else
                    {
                        player.BulletDraw();
                        player.BulletDraw2();
                        player.BulletDraw3();
                    }

                    enemy.EnemyMove();      // 적 이동
                    enemy.EnemyDraw();      // 적 그리기

                    //충돌 처리
                    player.ClashEnemyAndBullet(enemy);
                }
            }
        }
    }
}
