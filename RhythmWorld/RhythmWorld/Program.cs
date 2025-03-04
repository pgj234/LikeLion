using System;
using System.Text;
using WMPLib;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;

namespace RhythmWorld
{
    enum GameState
    {
        Main,
        MusicSelect,
        Playing,
        Clear,
        Gameover
    }

    enum Decision
    {
        Perfect,        // 100점
        Cool,           // 60점
        Good,           // 30점
        Miss,           //  5점
        Fail,           //  0점
        None
    }

    class MusicInfo
    {
        public string musicFileName;
        public decimal bpm;
        public int musicTime;
        public double exMusicStartTime;
        public double exMusicEndTime;
        public ushort difficulty;
        public int highScore;

        public Note notes;
    }

    class Note
    {
        public double offsetSec;
        public string noteFileName;
        public int[] noteArray;             // 한 인덱스당 1비트 (비트는 4비트 까지만)
    }

    class SpaceBar
    {
        public string[] spaceBarNotPushStrArray = new string[]
        {
            "△   △",
            "(◕   ◕)",
            "△━━ △"
        };

        public string[] spaceBarPushStrArray = new string[]
        {
            "▲   ▲",
            "(◕   ◕)",
            "▲━━ ▲"
        };
    }

    class Program
    {
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);

        static ConsoleKeyInfo inputKey;
        static string audioFilePath = "../../../Music/";
        static string noteFilePath = "../../../Notes/";
        static WindowsMediaPlayer wmp = new WindowsMediaPlayer();
        static MusicInfo[] musicInfo = new MusicInfo[]
        {
            new MusicInfo {
                musicFileName = "Rift of the NecroDancer OST - Main Title (Danny Baranowsky).mp3",
                bpm = 118.02m,
                musicTime = 77
            },

            new MusicInfo {
                musicFileName = "Rift of the NecroDancer OST - Om and On (Alex Moukala).mp3",
                bpm = 135.00m,
                musicTime = 203,
                exMusicStartTime = 38.3,
                exMusicEndTime = 58,
                difficulty = 8,
                notes = new Note {
                    offsetSec = 0.143,
                    noteFileName = "Om and On",
                    noteArray = NoteRead($"{noteFilePath}Om and On.txt")
                }
            },

            new MusicInfo {
                musicFileName = "Rift of the NecroDancer OST - Matriarch (Jules Conroy).mp3",
                bpm = 145.01m,
                musicTime = 210,
                exMusicStartTime = 69.14,
                exMusicEndTime = 91,
                difficulty = 5,
                notes = new Note {
                    offsetSec = 0.475,
                    noteFileName = "Matriarch",
                    noteArray = NoteRead($"{noteFilePath}Matriarch.txt")
                }
            }
        };

        static bool musicOn = false;        // 음악 틀어져있는지 체크
        static bool drawFinish = false;     // 화면 그려야하는지 체크
        static decimal bitTime = 0;         // 비트 활성화 시간 체크
        static int exMusicTimeTickCnt = 0;  // 미리듣기 시간 카운트

        // 메모장에서 노트 정보 가져오기
        static int[] NoteRead(string path)
        {
            StreamReader sr = new StreamReader(Path.GetFullPath(path));

            string contentStr = sr.ReadToEnd();
            sr.Close();
            contentStr = contentStr.Replace(" ", string.Empty);
            contentStr = contentStr.Replace("\n", string.Empty);

            string[] tmpStrArray = contentStr.Split(',');
            int[] intArray = new int[tmpStrArray.Length];

            for (int i = 0; i < tmpStrArray.Length; i++)
            {
                intArray[i] = int.Parse(tmpStrArray[i]);
            }

            return intArray;
        }

        // 곡 점수 정보 가져오기
        static void HighScoreLoad(int musicIdx)
        {
            string fullPathStr = Path.GetFullPath($"{audioFilePath}{musicInfo[musicIdx].notes.noteFileName}.txt");
            FileInfo file = new FileInfo(fullPathStr);
            if (false == file.Exists)
            {
                File.WriteAllText(fullPathStr, "0");
            }
            else
            {
                musicInfo[musicIdx].highScore = int.Parse(File.ReadAllText(fullPathStr));
            }
        }

        // 곡 점수 정보 저장
        static void HighScoreSave(int musicIdx, int score)
        {
            string fullPathStr = Path.GetFullPath($"{audioFilePath}{musicInfo[musicIdx].notes.noteFileName}.txt");
            FileInfo file = new FileInfo(fullPathStr);
            if (false == file.Exists)
            {
                File.WriteAllText(fullPathStr, score.ToString());
            }
            else
            {
                File.WriteAllText(fullPathStr, score.ToString());
                musicInfo[musicIdx].highScore = score;
            }
        }

        /// <summary>
        /// 나올 음악 설정 및 재생
        /// </summary>
        /// <param name="musicIdx"></param>
        /// <param name="startTimeVal"></param>
        /// <param name="volVal"></param>
        static void MusicSetting(int musicIdx, double startTimeVal = 0, int volVal = 75)
        {
            if (false == musicOn)
            {
                drawFinish = false;

                bitTime = (60 / musicInfo[musicIdx].bpm) * 10000000;
                musicOn = true;

                wmp.URL = Path.GetFullPath($"{audioFilePath}{musicInfo[musicIdx].musicFileName}");
                wmp.close();

                exMusicTimeTickCnt = Environment.TickCount;

                wmp.controls.currentPosition = startTimeVal;        // 음악 스타트 지점 설정
                wmp.settings.volume = volVal;                       // 음악 볼륨 설정

                wmp.controls.play();
            }
        }

        // 왼쪽 쳐내는 연출 오브젝트 담기는 배열 (Dir배열 인덱스로 넣어짐 -> 인덱스 3초과하면 사라지게) [-1이면 비어있는 것]
        static short[] leftGoodSmashArray = { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        static short[] leftCoolSmashArray = { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        static short[] leftPerfectSmashArray = { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };

        // 왼쪽 쳐내기 연출 좌표 담기는 배열
        static ushort[,] leftGoodSmashDirArray = { { 26, 10 }, { 18, 7 }, { 10, 4 }, { 2, 0 } };
        static ushort[,] leftCoolSmashDirArray = { { 27, 10 }, { 20, 7 }, { 13, 4 }, { 6, 0 } };
        static ushort[,] leftPerfectSmashDirArray = { { 29, 10 }, { 28, 7 }, { 27, 4 }, { 26, 0 } };

        // 오른쪽 쳐내기 연출 오브젝트 담기는 배열 (Dir배열 인덱스로 넣어짐 -> 인덱스 3초과하면 사라지게) [-1이면 비어있는 것]
        static short[] rightGoodSmashArray = { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        static short[] rightCoolSmashArray = { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        static short[] rightPerfectSmashArray = { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };

        // 오른쪽 쳐내기 연출 좌표 담기는 배열
        static ushort[,] rightGoodSmashDirArray = { { 33, 10 }, { 41, 7 }, { 49, 4 }, { 57, 0 } };
        static ushort[,] rightCoolSmashDirArray = { { 32, 10 }, { 39, 7 }, { 46, 4 }, { 53, 0 } };
        static ushort[,] rightPerfectSmashDirArray = { { 30, 10 }, { 31, 7 }, { 32, 4 }, { 33, 0 } };

        // 노트 쳐내기 연출
        static void SmashDirection()
        {
            // 이전에 그려진거 우선 싹 지우기
            Console.SetCursorPosition(0, 0);
            Console.Write("                                                            ");
            Console.SetCursorPosition(0, 4);
            Console.Write("                                                            ");
            Console.SetCursorPosition(0, 7);
            Console.Write("                                                            ");
            Console.SetCursorPosition(0, 10);
            Console.Write("                                                            ");

            // 연출노트 조정부터
            for (int i=0; i<leftGoodSmashArray.Length; i++)
            {
                // 왼쪽
                // 연출노트 활성화 된 것 찾기
                if (-1 < leftGoodSmashArray[i])
                {
                    // 끝까지 간 연출노트는 삭제
                    if (3 == leftGoodSmashArray[i])
                    {
                        leftGoodSmashArray[i] = -1;
                    }
                    else
                    {
                        // 그리기
                        Console.SetCursorPosition(leftGoodSmashDirArray[leftGoodSmashArray[i], 0], leftGoodSmashDirArray[leftGoodSmashArray[i], 1]);
                        Console.Write("●");

                        leftGoodSmashArray[i]++;
                    }
                }

                // 연출노트 활성화 된 것 찾기
                if (-1 < leftCoolSmashArray[i])
                {
                    // 끝까지 간 연출노트는 삭제
                    if (3 == leftCoolSmashArray[i])
                    {
                        leftCoolSmashArray[i] = -1;
                    }
                    else
                    {
                        // 그리기
                        Console.SetCursorPosition(leftCoolSmashDirArray[leftCoolSmashArray[i], 0], leftCoolSmashDirArray[leftCoolSmashArray[i], 1]);
                        Console.Write("●");

                        leftCoolSmashArray[i]++;
                    }
                }

                // 연출노트 활성화 된 것 찾기
                if (-1 < leftPerfectSmashArray[i])
                {
                    // 끝까지 간 연출노트는 삭제
                    if (3 == leftPerfectSmashArray[i])
                    {
                        leftPerfectSmashArray[i] = -1;
                    }
                    else
                    {
                        // 그리기
                        Console.SetCursorPosition(leftPerfectSmashDirArray[leftPerfectSmashArray[i], 0], leftPerfectSmashDirArray[leftPerfectSmashArray[i], 1]);
                        Console.Write("●");

                        leftPerfectSmashArray[i]++;
                    }
                }


                // 오른쪽
                // 연출노트 활성화 된 것 찾기
                if (-1 < rightGoodSmashArray[i])
                {
                    // 끝까지 간 연출노트는 삭제
                    if (3 == rightGoodSmashArray[i])
                    {
                        rightGoodSmashArray[i] = -1;
                    }
                    else
                    {
                        // 그리기
                        Console.SetCursorPosition(rightGoodSmashDirArray[rightGoodSmashArray[i], 0], rightGoodSmashDirArray[rightGoodSmashArray[i], 1]);
                        Console.Write("●");

                        rightGoodSmashArray[i]++;
                    }
                }

                // 연출노트 활성화 된 것 찾기
                if (-1 < rightCoolSmashArray[i])
                {
                    // 끝까지 간 연출노트는 삭제
                    if (3 == rightCoolSmashArray[i])
                    {
                        rightCoolSmashArray[i] = -1;
                    }
                    else
                    {
                        // 그리기
                        Console.SetCursorPosition(rightCoolSmashDirArray[rightCoolSmashArray[i], 0], rightCoolSmashDirArray[rightCoolSmashArray[i], 1]);
                        Console.Write("●");

                        rightCoolSmashArray[i]++;
                    }
                }

                // 연출노트 활성화 된 것 찾기
                if (-1 < rightPerfectSmashArray[i])
                {
                    // 끝까지 간 연출노트는 삭제
                    if (3 == rightPerfectSmashArray[i])
                    {
                        rightPerfectSmashArray[i] = -1;
                    }
                    else
                    {
                        // 그리기
                        Console.SetCursorPosition(rightPerfectSmashDirArray[rightPerfectSmashArray[i], 0], rightPerfectSmashDirArray[rightPerfectSmashArray[i], 1]);
                        Console.Write("●");

                        rightPerfectSmashArray[i]++;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            GameState gameState = new GameState();
            gameState = GameState.Main;

            Decision noteDecision = new Decision();

            Console.SetWindowSize(60, 30);  // 콘솔 창 크기 설정 (가로 60, 세로 30)
            Console.SetBufferSize(60, 30);  // 버퍼 크기도 동일하게 설정 (스크롤 방지)
            Console.CursorVisible = false;

            SpaceBar spaceBar = new SpaceBar();

            Random rand = new Random();

            // 곡 별로 점수 가져오기
            for (int i = 1; i < musicInfo.Length; i++)
            {
                HighScoreLoad(i);
            }

            int curMusicTimeTickCnt = 0;
            int curMusicTimeUiNum = 0;

            bool leftDecision = true;       // 왼쪽 판정이 우선인지

            bool[] leftNoteDecisionArray = new bool[30];    // 왼쪽 노트 판정 배열
            bool[] rightNoteDecisionArray = new bool[30];   // 오른쪽 노트 판정 배열
            int noteIdx = 0;                                // 노트 인덱스

            bool visualBit = false;     // 배경 비트

            int musicTimeCnt = Environment.TickCount;
            long tCnt = DateTime.UtcNow.Ticks;
            long bitTimeCnt = 0;                    // 4분의 1박자 카운트마다 시간 갱신
            int bitIdxCnt = 0;                     // 비트 인덱스 카운트

            bool spaceBarActionIng = false;          // 스페이스바 액션중
            int spaceBarActionCnt = 0;
            bool spaceBarPressed = false;           // 스페이스바 이전 상태
            bool isSpacePress = false;              // 현재 스페이스바 눌림 정보 가져오기

            bool isMusicStop = true;                // 게임 시작시 처음 준비 시간
            ushort bitCnt = 3;                       // 0:강, 1:약, 2:중, 3:약
            uint visualBitDelayCnt = 0;

            int choiceMusicIdx = 0;
            int score = 0;
            
            while (true)
            {
                switch (gameState)
                {
                    case GameState.Main:
                        MusicSetting(choiceMusicIdx);       // 나올 음악 설정

                        // 메뉴 선택 키 입력
                        if (Console.KeyAvailable)
                        {
                            inputKey = Console.ReadKey(true);
                            switch (inputKey.Key)
                            {
                                case ConsoleKey.D1:
                                    gameState = GameState.MusicSelect;
                                    choiceMusicIdx = 1;
                                    musicOn = false;
                                    break;

                                case ConsoleKey.D2:
                                    Environment.Exit(0);
                                    break;
                            }
                        }

                        // 비트 타이밍
                        if (tCnt + bitTime <= DateTime.UtcNow.Ticks)
                        {
                            tCnt = DateTime.UtcNow.Ticks;
                            visualBit = true;

                            drawFinish = false;

                            if (3 > bitCnt)
                            {
                                bitCnt++;
                            }
                            else
                            {
                                bitCnt = 0;
                            }
                        }

                        // 비트 끝내기 타이밍
                        if (true == visualBit)
                        {
                            visualBitDelayCnt++;

                            if (11000 < visualBitDelayCnt)
                            {
                                visualBitDelayCnt = 0;
                                visualBit = false;

                                drawFinish = false;
                            }
                        }

                        // 화면 그리기 전이면 그려주기
                        if (false == drawFinish)
                        {
                            drawFinish = true;
                            Console.Clear();

                            for (int i = 0; i < 29; i++)
                            {
                                if (true == visualBit)
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
                                            break;

                                        case 8:
                                            Console.WriteLine("┃▷▷▷▷▷▷            리듬  월드            ◁◁◁◁◁◁┃");
                                            break;

                                        case 12:
                                            switch (bitCnt)
                                            {
                                                case 0:         // 강
                                                    Console.WriteLine("┃▷▷▷▷▷▷▷▷▷     1. 게임 시작     ◁◁◁◁◁◁◁◁◁┃");
                                                    break;

                                                case 1:         // 약
                                                case 3:
                                                    Console.WriteLine("┃▷▷                   1. 게임 시작                   ◁◁┃");
                                                    break;

                                                case 2:         // 중간
                                                    Console.WriteLine("┃▷▷▷▷               1. 게임 시작               ◁◁◁◁┃");
                                                    break;
                                            }
                                            break;

                                        case 14:
                                            switch (bitCnt)
                                            {
                                                case 0:         // 강
                                                    Console.WriteLine("┃▷▷▷▷▷▷▷▷▷     2. 게임 종료     ◁◁◁◁◁◁◁◁◁┃");
                                                    break;

                                                case 1:         // 약
                                                case 3:
                                                    Console.WriteLine("┃▷▷                   2. 게임 종료                   ◁◁┃");
                                                    break;

                                                case 2:         // 중간
                                                    Console.WriteLine("┃▷▷▷▷               2. 게임 종료               ◁◁◁◁┃");
                                                    break;
                                            }
                                            break;

                                        case 23:
                                            Console.WriteLine("┃▶▶                 메뉴선택 : 숫자키                ◀◀┃");
                                            break;

                                        case 26:
                                            Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
                                            break;

                                        case 27:
                                            Console.WriteLine();
                                            break;

                                        case 28:
                                            Console.WriteLine("                음악 : Rift of the NecroDancer");
                                            break;

                                        default:
                                            Console.WriteLine("┃▶▶                                                  ◀◀┃");
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
                                            break;

                                        case 8:
                                            Console.WriteLine("┃▷                      리듬  월드                      ◁┃");
                                            break;

                                        case 12:
                                            Console.WriteLine("┃▷                     1. 게임 시작                     ◁┃");
                                            break;

                                        case 14:
                                            Console.WriteLine("┃▷                     2. 게임 종료                     ◁┃");
                                            break;

                                        case 23:
                                            Console.WriteLine("┃▶                   메뉴선택 : 숫자키                  ◀┃");
                                            break;

                                        case 26:
                                            Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
                                            break;

                                        case 27:
                                            Console.WriteLine();
                                            break;

                                        case 28:
                                            Console.WriteLine("                음악 : Rift of the NecroDancer");
                                            break;

                                        default:
                                            Console.WriteLine("┃▶                                                      ◀┃");
                                            break;
                                    }
                                }
                            }
                        }

                        // 노래 끝 다시 재생하기
                        if (musicTimeCnt + (musicInfo[0].musicTime * 1000) < Environment.TickCount)
                        {
                            musicTimeCnt = Environment.TickCount;
                            musicOn = false;
                            bitCnt = 3;
                        }

                        break;

                    case GameState.MusicSelect:               // 음악 선택 화면
                        MusicSetting(choiceMusicIdx, musicInfo[choiceMusicIdx].exMusicStartTime);       // 미리듣기 음악 설정

                        // 미리듣기 재생시간 제한을 위한 카운트
                        if (exMusicTimeTickCnt + (musicInfo[choiceMusicIdx].exMusicEndTime - musicInfo[choiceMusicIdx].exMusicStartTime) * 1000 < Environment.TickCount)
                        {
                            musicOn = false;
                        }

                        // 메뉴 선택 키 입력
                        if (Console.KeyAvailable)
                        {
                            inputKey = Console.ReadKey(true);
                            switch (inputKey.Key)
                            {
                                case ConsoleKey.Escape:
                                    choiceMusicIdx = 0;
                                    musicOn = false;
                                    bitCnt = 3;
                                    gameState = GameState.Main;
                                    break;

                                case ConsoleKey.Enter:
                                    gameState = GameState.Playing;
                                    break;

                                case ConsoleKey.D1:         // 1번곡 미리듣기
                                    if (1 != choiceMusicIdx)
                                    {
                                        choiceMusicIdx = 1;
                                        musicOn = false;
                                        MusicSetting(choiceMusicIdx, musicInfo[choiceMusicIdx].exMusicStartTime);
                                    }
                                    break;

                                case ConsoleKey.D2:         // 2번곡 미리듣기
                                    if (2 != choiceMusicIdx)
                                    {
                                        choiceMusicIdx = 2;
                                        musicOn = false;
                                        MusicSetting(choiceMusicIdx, musicInfo[choiceMusicIdx].exMusicStartTime);
                                    }
                                    break;
                            }
                        }

                        if (GameState.Playing == gameState)
                        {
                            wmp.controls.stop();
                            musicOn = false;
                            isMusicStop = true;
                            drawFinish = false;
                            spaceBarPressed = false;
                            noteDecision = Decision.None;
                            curMusicTimeUiNum = 0;
                            curMusicTimeTickCnt = 0;

                            for (int i = 0; i < leftNoteDecisionArray.Length; i++)
                            {
                                leftNoteDecisionArray[i] = false;
                                rightNoteDecisionArray[i] = false;
                            }

                            continue;
                        }

                        // 비트 타이밍
                        if (tCnt + bitTime <= DateTime.UtcNow.Ticks)
                        {
                            tCnt = DateTime.UtcNow.Ticks;
                            visualBit = true;

                            drawFinish = false;
                        }

                        // 비트 끝내기 타이밍
                        if (true == visualBit)
                        {
                            visualBitDelayCnt++;

                            if (12000 < visualBitDelayCnt)
                            {
                                visualBitDelayCnt = 0;
                                visualBit = false;

                                drawFinish = false;
                            }
                        }

                        if (false == drawFinish)
                        {
                            drawFinish = true;
                            Console.Clear();

                            for (int i = 0; i < 29; i++)
                            {
                                if (true == visualBit)
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
                                            break;

                                        case 2:
                                            Console.WriteLine("┃          곡 선택 : 숫자키           시작 : 엔터          ┃");
                                            break;

                                        case 11:
                                            Console.SetCursorPosition(0, 11);
                                            Console.Write($"┃                       최고 점수 : {musicInfo[1].highScore}");
                                            Console.SetCursorPosition(59, 11);
                                            Console.Write("┃\n");
                                            break;

                                        case 12:
                                            if (1 == choiceMusicIdx)
                                            {
                                                Console.WriteLine($"┃      ◀◀◀◀◀  1. {musicInfo[1].notes.noteFileName} (난이도:{musicInfo[1].difficulty})  ▶▶▶▶▶     ┃");
                                            }
                                            else
                                            {
                                                Console.WriteLine($"┃                  1. {musicInfo[1].notes.noteFileName} (난이도:{musicInfo[1].difficulty})                 ┃");
                                            }
                                            break;

                                        case 14:
                                            Console.SetCursorPosition(0, 14);
                                            Console.Write($"┃                       최고 점수 : {musicInfo[2].highScore}");
                                            Console.SetCursorPosition(59, 14);
                                            Console.Write("┃\n");
                                            break;

                                        case 15:
                                            if (2 == choiceMusicIdx)
                                            {
                                                Console.WriteLine($"┃      ◀◀◀◀◀  2. {musicInfo[2].notes.noteFileName} (난이도:{musicInfo[2].difficulty})  ▶▶▶▶▶     ┃");
                                            }
                                            else
                                            {
                                                Console.WriteLine($"┃                  2. {musicInfo[2].notes.noteFileName} (난이도:{musicInfo[2].difficulty})                 ┃");
                                            }
                                            break;

                                        case 17:
                                            Console.WriteLine("┃                        [2는 미완성]                      ┃");
                                            break;

                                        case 26:
                                            Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
                                            break;

                                        case 27:
                                            Console.WriteLine("");
                                            break;

                                        case 28:
                                            Console.WriteLine("           음악 : Rift of the NecroDancer (Esc 뒤로)");
                                            break;

                                        default:
                                            Console.WriteLine("┃                                                          ┃");
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
                                            break;

                                        case 2:
                                            Console.WriteLine("┃          곡 선택 : 숫자키           시작 : 엔터          ┃");
                                            break;

                                        case 11:
                                            Console.SetCursorPosition(0, 11);
                                            Console.Write($"┃                       최고 점수 : {musicInfo[1].highScore}");
                                            Console.SetCursorPosition(59, 11);
                                            Console.Write("┃\n");
                                            break;

                                        case 12:
                                            if (1 == choiceMusicIdx)
                                            {
                                                Console.WriteLine($"┃            ◀◀  1. {musicInfo[1].notes.noteFileName} (난이도:{musicInfo[1].difficulty})  ▶▶           ┃");
                                            }
                                            else
                                            {
                                                Console.WriteLine($"┃                  1. {musicInfo[1].notes.noteFileName} (난이도:{musicInfo[1].difficulty})                 ┃");
                                            }
                                            break;

                                        case 14:
                                            Console.SetCursorPosition(0, 14);
                                            Console.Write($"┃                       최고 점수 : {musicInfo[2].highScore}");
                                            Console.SetCursorPosition(59, 14);
                                            Console.Write("┃\n");
                                            break;

                                        case 15:
                                            if (2 == choiceMusicIdx)
                                            {
                                                Console.WriteLine($"┃            ◀◀  2. {musicInfo[2].notes.noteFileName} (난이도:{musicInfo[2].difficulty})  ▶▶           ┃");
                                            }
                                            else
                                            {
                                                Console.WriteLine($"┃                  2. {musicInfo[2].notes.noteFileName} (난이도:{musicInfo[2].difficulty})                 ┃");
                                            }
                                            break;

                                        case 17:
                                            Console.WriteLine("┃                        [2는 미완성]                      ┃");
                                            break;

                                        case 26:
                                            Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
                                            break;

                                        case 27:
                                            Console.WriteLine("");
                                            break;

                                        case 28:
                                            Console.WriteLine("           음악 : Rift of the NecroDancer (Esc 뒤로)");
                                            break;

                                        default:
                                            Console.WriteLine("┃                                                          ┃");
                                            break;
                                    }
                                }
                            }
                        }
                        break;

                    case GameState.Playing:
                        // 4분의 1박자씩 노트 등장
                        if (bitTimeCnt + (bitTime * 0.25m) <= DateTime.UtcNow.Ticks)
                        {
                            bitTimeCnt = DateTime.UtcNow.Ticks;

                            // 노트 쳐내기 연출 재생
                            SmashDirection();

                            for (int i= leftNoteDecisionArray.Length - 1; i > -1; i--)
                            {
                                // (왼쪽->오른쪽)
                                // 노트 가운데 넘어가면 Fail
                                if (true == leftNoteDecisionArray[29])
                                {
                                    noteDecision = Decision.Fail;
                                    leftNoteDecisionArray[29] = false;
                                }

                                if (0 < i)
                                {
                                    leftNoteDecisionArray[i] = leftNoteDecisionArray[i - 1];
                                }
                                else       // 노트 처음 부분 [0]
                                {
                                    leftNoteDecisionArray[i] = false;
                                }

                                // (오른쪽 -> 왼쪽)
                                // 노트 가운데 넘어가면 Fail
                                if (true == rightNoteDecisionArray[29])
                                {
                                    noteDecision = Decision.Fail;
                                    rightNoteDecisionArray[29] = false;
                                }

                                if (0 < i)
                                {
                                    rightNoteDecisionArray[i] = rightNoteDecisionArray[i - 1];
                                }
                                else       // 노트 처음 부분 [0]
                                {
                                    rightNoteDecisionArray[i] = false;
                                }
                            }

                            switch (noteDecision)
                            {
                                case Decision.Perfect:
                                    Console.SetCursorPosition(0, 20);
                                    Console.Write("                           Perfect");
                                    break;

                                case Decision.Cool:
                                    Console.SetCursorPosition(0, 20);
                                    Console.Write("                            Cool");
                                    break;

                                case Decision.Good:
                                    Console.SetCursorPosition(0, 20);
                                    Console.Write("                            Good");
                                    break;

                                case Decision.Miss:
                                    Console.SetCursorPosition(0, 20);
                                    Console.Write("                            Miss");
                                    break;

                                case Decision.Fail:
                                    Console.SetCursorPosition(0, 20);
                                    Console.Write("                            Fail");
                                    break;
                            }

                            // 나올 노트가 남았다면
                            if (noteIdx < musicInfo[choiceMusicIdx].notes.noteArray.Length)
                            {
                                // 노트 생성될 차례
                                if (bitIdxCnt == musicInfo[choiceMusicIdx].notes.noteArray[noteIdx] - 1)
                                {
                                    noteIdx++;

                                    switch (rand.Next(0, 2))
                                    {
                                        case 0:         // 왼쪽에 노트 생성
                                            leftNoteDecisionArray[0] = true;
                                            break;

                                        case 1:         // 오른쪽에 노트 생성
                                            rightNoteDecisionArray[0] = true;
                                            break;
                                    }
                                }
                            }

                            bitIdxCnt++;

                            Console.SetCursorPosition(0, 12);
                            Console.Write("                                                             ");
                            for (int i = 0; i < leftNoteDecisionArray.Length; i++)      // 왼쪽 노트 그리기
                            {
                                Console.SetCursorPosition(i, 12);

                                if (false == leftNoteDecisionArray[i])      // 노트 없음
                                {
                                    Console.Write(" ");
                                }
                                else        // 노트 있음
                                {
                                    Console.Write("◎");
                                }
                            }

                            for (int i = 0; i < rightNoteDecisionArray.Length; i++)      // 오른쪽 노트 그리기
                            {
                                Console.SetCursorPosition(59 - i, 12);
                                if (false == rightNoteDecisionArray[i])      // 노트 없음
                                {
                                    Console.Write(" ");
                                }
                                else        // 노트 있음
                                {
                                    Console.Write("◎");
                                }
                            }
                        }

                        // 비트 타이밍
                        if (tCnt + bitTime <= DateTime.UtcNow.Ticks)
                        {
                            tCnt = DateTime.UtcNow.Ticks;
                            visualBit = true;

                            drawFinish = false;
                        }

                        // 비트 끝내기 타이밍
                        if (true == visualBit)
                        {
                            visualBitDelayCnt++;

                            if (12000 < visualBitDelayCnt)
                            {
                                visualBitDelayCnt = 0;
                                visualBit = false;

                                drawFinish = false;
                            }
                        }

                        if (false == drawFinish)
                        {
                            drawFinish = true;
                            Console.Clear();

                            // 현재 시간이 최대 시간이면 최대 시간 안넘게 설정
                            if (curMusicTimeUiNum > musicInfo[choiceMusicIdx].musicTime)
                            {
                                curMusicTimeUiNum = musicInfo[choiceMusicIdx].musicTime;
                            }
                            Console.SetCursorPosition(0, 1);
                            Console.Write($"         {musicInfo[choiceMusicIdx].notes.noteFileName}         {curMusicTimeUiNum}/{musicInfo[choiceMusicIdx].musicTime}         점수 : {score}");

                            // 스페이스 바 액션 상태
                            if (true == spaceBarActionIng)       // 스페이스 바 액션 중인 상태
                            {
                                Console.SetCursorPosition(0, 13);
                                Console.Write(" ───────────────────────── ━━━━━━ ──────────────────────────");
                                Console.SetCursorPosition(0, 17);
                                Console.Write("                                                            ");
                                Console.SetCursorPosition(0, 18);
                                Console.Write("                                                            ");

                                for (int i = 0; i < spaceBar.spaceBarPushStrArray.Length; i++)
                                {
                                    Console.SetCursorPosition(27, i + 14);
                                    Console.Write(spaceBar.spaceBarPushStrArray[i]);
                                }
                            }
                            else        // 스페이스 바 안눌린 상태
                            {
                                Console.SetCursorPosition(0, 13);
                                Console.Write(" ─────────────────────────        ──────────────────────────");
                                Console.SetCursorPosition(0, 14);
                                Console.Write("                                                            ");
                                Console.SetCursorPosition(0, 15);
                                Console.Write("                           ━━━━━━                           ");
                                for (int i = 0; i < spaceBar.spaceBarNotPushStrArray.Length; i++)
                                {
                                    Console.SetCursorPosition(27, i + 16);
                                    Console.Write(spaceBar.spaceBarNotPushStrArray[i]);
                                }
                            }

                            Console.SetCursorPosition(0, 23);
                            Console.Write("                          SPACE BAR                         ");
                            Console.SetCursorPosition(0, 28);
                            Console.Write("           음악 : Rift of the NecroDancer (Esc 중단)");

                            if (true == isMusicStop)
                            {
                                Console.SetCursorPosition(0, 26);
                                Console.Write(" ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                            }
                            else if (true == visualBit)
                            {
                                Console.SetCursorPosition(0, 25);
                                Console.Write(" ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃");
                                Console.SetCursorPosition(0, 26);
                                Console.Write(" ┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻");
                            }
                            else
                            {
                                Console.SetCursorPosition(0, 26);
                                Console.Write(" ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                            }
                        }

                        if (true == isMusicStop)
                        {
                            Console.SetCursorPosition(0, 5);
                            Console.Write("                         ●●●●●");
                            Console.SetCursorPosition(0, 6);
                            Console.Write("                                 ●");
                            Console.SetCursorPosition(0, 7);
                            Console.Write("                         ●●●●●");
                            Console.SetCursorPosition(0, 8);
                            Console.Write("                                 ●");
                            Console.SetCursorPosition(0, 9);
                            Console.Write("                         ●●●●●");
                            Thread.Sleep(750);

                            Console.SetCursorPosition(0, 8);
                            Console.Write("                         ●                       ");
                            Thread.Sleep(750);

                            for (int i = 5; i < 10; i++)
                            {
                                Console.SetCursorPosition(0, i);
                                Console.Write("                             ●               ");
                            }
                            Thread.Sleep(750);

                            Console.SetCursorPosition(0, 5);
                            Console.Write("                                      ");
                            Console.SetCursorPosition(0, 6);
                            Console.Write("                                      ");
                            Console.SetCursorPosition(0, 7);
                            Console.Write("                                      ");
                            Console.SetCursorPosition(0, 8);
                            Console.Write("                                      ");
                            Console.SetCursorPosition(0, 9);
                            Console.Write("                                      ");

                            MusicSetting(choiceMusicIdx, musicInfo[choiceMusicIdx].notes.offsetSec);

                            isMusicStop = false;
                            spaceBarActionIng = false;
                            spaceBarActionCnt = 0;
                            noteIdx = 0;
                            bitCnt = 0;
                            bitIdxCnt = 0;
                            curMusicTimeTickCnt = Environment.TickCount;
                            musicTimeCnt = Environment.TickCount;
                            bitTimeCnt = DateTime.UtcNow.Ticks;
                        }

                        // 스페이스 바 전용 키 체크
                        isSpacePress = (GetAsyncKeyState(0x20) & 0x8000) != 0;      // 실시간 스페이스바 눌림 정보 가져오기
                        if (!spaceBarPressed && isSpacePress)
                        {
                            for (int i=leftNoteDecisionArray.Length-1; i>-1; i--)
                            {
                                // 왼쪽 노트 우선
                                if (true == leftNoteDecisionArray[i])
                                {
                                    leftDecision = true;
                                    break;
                                }

                                // 오른쪽 노트 우선
                                if (true == rightNoteDecisionArray[i])
                                {
                                    leftDecision = false;
                                    break;
                                }
                            }

                            // 왼쪽 노트 판정
                            if (true == leftDecision)
                            {
                                if (true == leftNoteDecisionArray[29])
                                {
                                    leftNoteDecisionArray[29] = false;

                                    Console.SetCursorPosition(29, 12);
                                    Console.Write(" ");
                                    score += 100;

                                    noteDecision = Decision.Perfect;

                                    for (int i = 0; i < leftPerfectSmashArray.Length; i++)
                                    {
                                        if (-1 == leftPerfectSmashArray[i])        // 비어있는 곳 찾기
                                        {
                                            leftPerfectSmashArray[i] = 0;
                                            break;
                                        }
                                    }
                                }
                                else if (false == leftNoteDecisionArray[29] && true == leftNoteDecisionArray[28])
                                {
                                    leftNoteDecisionArray[28] = false;

                                    Console.SetCursorPosition(29, 12);
                                    Console.Write(" ");
                                    score += 100;

                                    noteDecision = Decision.Perfect;

                                    for (int i = 0; i < leftPerfectSmashArray.Length; i++)
                                    {
                                        if (-1 == leftPerfectSmashArray[i])        // 비어있는 곳 찾기
                                        {
                                            leftPerfectSmashArray[i] = 0;
                                            break;
                                        }
                                    }
                                }
                                else if (false == leftNoteDecisionArray[29] && false == leftNoteDecisionArray[28] && true == leftNoteDecisionArray[27])
                                {
                                    leftNoteDecisionArray[27] = false;
                                    Console.SetCursorPosition(29, 12);
                                    Console.Write(" ");
                                    score += 60;

                                    noteDecision = Decision.Cool;

                                    for (int i = 0; i < leftCoolSmashArray.Length; i++)
                                    {
                                        if (-1 == leftCoolSmashArray[i])        // 비어있는 곳 찾기
                                        {
                                            leftCoolSmashArray[i] = 0;
                                            break;
                                        }
                                    }
                                }
                                else if (false == leftNoteDecisionArray[29] && false == leftNoteDecisionArray[28] && false == leftNoteDecisionArray[27] && true == leftNoteDecisionArray[26])
                                {
                                    leftNoteDecisionArray[26] = false;
                                    Console.SetCursorPosition(29, 12);
                                    Console.Write(" ");
                                    score += 30;

                                    noteDecision = Decision.Good;

                                    for (int i = 0; i < leftGoodSmashArray.Length; i++)
                                    {
                                        if (-1 == leftGoodSmashArray[i])        // 비어있는 곳 찾기
                                        {
                                            leftGoodSmashArray[i] = 0;
                                            break;
                                        }
                                    }
                                }
                                else if (false == leftNoteDecisionArray[29] && false == leftNoteDecisionArray[28] && false == leftNoteDecisionArray[27] && false == leftNoteDecisionArray[26] &&
                                    true == leftNoteDecisionArray[25])
                                {
                                    leftNoteDecisionArray[25] = false;

                                    Console.SetCursorPosition(29, 12);
                                    Console.Write(" ");
                                    score += 5;

                                    noteDecision = Decision.Miss;
                                }
                            }
                            else    // 오른쪽 노트 판정
                            {
                                if (true == rightNoteDecisionArray[29])
                                {
                                    rightNoteDecisionArray[29] = false;

                                    Console.SetCursorPosition(29, 12);
                                    Console.Write(" ");
                                    score += 100;

                                    noteDecision = Decision.Perfect;

                                    for (int i = 0; i < rightPerfectSmashArray.Length; i++)
                                    {
                                        if (-1 == rightPerfectSmashArray[i])        // 비어있는 곳 찾기
                                        {
                                            rightPerfectSmashArray[i] = 0;
                                            break;
                                        }
                                    }
                                }
                                else if (false == rightNoteDecisionArray[29] && true == rightNoteDecisionArray[28])
                                {
                                    rightNoteDecisionArray[28] = false;

                                    Console.SetCursorPosition(29, 12);
                                    Console.Write(" ");
                                    score += 100;

                                    noteDecision = Decision.Perfect;

                                    for (int i = 0; i < rightPerfectSmashArray.Length; i++)
                                    {
                                        if (-1 == rightPerfectSmashArray[i])        // 비어있는 곳 찾기
                                        {
                                            rightPerfectSmashArray[i] = 0;
                                            break;
                                        }
                                    }
                                }
                                else if (false == rightNoteDecisionArray[29] && false == rightNoteDecisionArray[28] && true == rightNoteDecisionArray[27])
                                {
                                    rightNoteDecisionArray[27] = false;
                                    Console.SetCursorPosition(29, 12);
                                    Console.Write(" ");
                                    score += 60;

                                    noteDecision = Decision.Cool;

                                    for (int i = 0; i < rightCoolSmashArray.Length; i++)
                                    {
                                        if (-1 == rightCoolSmashArray[i])        // 비어있는 곳 찾기
                                        {
                                            rightCoolSmashArray[i] = 0;
                                            break;
                                        }
                                    }
                                }
                                else if (false == rightNoteDecisionArray[29] && false == rightNoteDecisionArray[28] && false == rightNoteDecisionArray[27] && true == rightNoteDecisionArray[26])
                                {
                                    rightNoteDecisionArray[26] = false;
                                    Console.SetCursorPosition(29, 12);
                                    Console.Write(" ");
                                    score += 30;

                                    noteDecision = Decision.Good;

                                    for (int i = 0; i < rightGoodSmashArray.Length; i++)
                                    {
                                        if (-1 == rightGoodSmashArray[i])        // 비어있는 곳 찾기
                                        {
                                            rightGoodSmashArray[i] = 0;
                                            break;
                                        }
                                    }
                                }
                                else if (false == rightNoteDecisionArray[29] && false == rightNoteDecisionArray[28] && false == rightNoteDecisionArray[27] && false == rightNoteDecisionArray[26] &&
                                    true == rightNoteDecisionArray[25])
                                {
                                    rightNoteDecisionArray[25] = false;

                                    Console.SetCursorPosition(29, 12);
                                    Console.Write(" ");
                                    score += 5;

                                    noteDecision = Decision.Miss;
                                }
                            }

                            spaceBarActionIng = true;
                            spaceBarActionCnt = Environment.TickCount;
                            drawFinish = false;
                        }
                        spaceBarPressed = isSpacePress;

                        if (Console.KeyAvailable)
                        {
                            inputKey = Console.ReadKey(true);

                            switch (inputKey.Key)
                            {
                                case ConsoleKey.Escape:
                                    gameState = GameState.Gameover;
                                    break;
                            }
                        }

                        if (true == spaceBarActionIng)
                        {
                            if (spaceBarActionCnt + 70 < Environment.TickCount)        // 스페이스 바 액션 끝
                            {
                                spaceBarActionIng = false;
                                spaceBarActionCnt = Environment.TickCount;
                                drawFinish = false;
                            }
                        }

                        // 타임 UI
                        if (curMusicTimeTickCnt + 1000 < Environment.TickCount)
                        {
                            curMusicTimeTickCnt = Environment.TickCount;
                            curMusicTimeUiNum++;
                        }

                        // 음악 끝나는 시간 + 여유 오프셋까지 계산해서 클리어 보내기
                        if (musicTimeCnt + (musicInfo[choiceMusicIdx].musicTime * 1000) + 500 < Environment.TickCount)
                        {
                            gameState = GameState.Clear;
                        }
                        break;

                    case GameState.Clear:
                        Console.Clear();
                        wmp.controls.stop();

                        HighScoreSave(choiceMusicIdx, score);

                        Console.SetCursorPosition(27, 4);
                        Console.WriteLine("C L E A R");
                        Console.SetCursorPosition(27, 12);
                        Console.WriteLine(musicInfo[choiceMusicIdx].notes.noteFileName);
                        Console.SetCursorPosition(27, 15);
                        Console.WriteLine($"점수 : {score}");
                        Console.SetCursorPosition(23, 23);
                        Console.WriteLine("Enter를 눌러 계속");

                        Console.ReadLine();

                        score = 0;

                        musicOn = false;
                        gameState = GameState.MusicSelect;
                        break;

                    case GameState.Gameover:
                        Console.Clear();
                        wmp.controls.stop();

                        Console.SetCursorPosition(29, 4);
                        Console.WriteLine("중 단");
                        Console.SetCursorPosition(27, 12);
                        Console.WriteLine(musicInfo[choiceMusicIdx].notes.noteFileName);
                        Console.SetCursorPosition(27, 15);
                        Console.WriteLine($"점수 : {score}");
                        Console.SetCursorPosition(23, 23);
                        Console.WriteLine("Enter를 눌러 계속");

                        Console.ReadLine();

                        score = 0;

                        musicOn = false;
                        gameState = GameState.MusicSelect;
                        break;
                }
            }
        }
    }
}
