using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Reflection.Emit;

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
        Fail            //  0점
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
            "△  △",
            "(◕人◕)",
            "△ㅡ△"
        };

        public string[] spaceBarPushStrArray = new string[]
        {
            "▲  ▲",
            "(◕人◕)",
            "▲ㅡ▲"
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
                musicTime = 205,
                exMusicStartTime = 38.3,
                exMusicEndTime = 58,
                difficulty = 4,
                notes = new Note {
                    offsetSec = 0.143,
                    noteFileName = "Om and On",
                    noteArray = NoteRead($"{noteFilePath}Om and On.txt")
                }
            },

            new MusicInfo {
                musicFileName = "Rift of the NecroDancer OST - Matriarch (Jules Conroy).mp3",
                bpm = 145.01m,
                musicTime = 212,
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
            StreamReader sr = new StreamReader(path);

            string contentStr = null;

            contentStr = sr.ReadToEnd();
            sr.Close();
            contentStr = contentStr.Replace(" ", string.Empty);
            contentStr = contentStr.Replace("\n", string.Empty);

            string[] tmpStrArray = contentStr.Split(',');
            int[] intArray = new int[tmpStrArray.Length];

            for (int i=0; i<tmpStrArray.Length; i++)
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
        static void MusicSetting(int musicIdx, double startTimeVal = 0, int volVal = 70)
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

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            GameState gameState = new GameState();
            gameState = GameState.Main;

            Console.SetWindowSize(61, 30);  // 콘솔 창 크기 설정 (가로 61, 세로 30)
            Console.SetBufferSize(61, 30);  // 버퍼 크기도 동일하게 설정 (스크롤 방지)
            Console.CursorVisible = false;

            SpaceBar spaceBar = new SpaceBar();

            Random rand = new Random();

            // 곡 별로 점수 가져오기
            for (int i=1; i<musicInfo.Length; i++)
            {
                HighScoreLoad(i);
            }

            bool[] leftNoteDecisionArray = new bool[30];    // 왼쪽 노트 판정 배열

            bool[] rightNoteDecisionArray = new bool[30];   // 오른쪽 노트 판정 배열

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
                                            Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
                                            break;

                                        case 9:
                                            Console.WriteLine("┃▷▷▷▷▷▷             리듬 월드             ◁◁◁◁◁◁┃");
                                            break;

                                        case 12:
                                            switch (bitCnt)
                                            {
                                                case 0:         // 강
                                                    Console.WriteLine("┃▷▷▷▷▷▷▷▷▷▷    1.게임 시작    ◁◁◁◁◁◁◁◁◁◁┃");
                                                    break;

                                                case 1:         // 약
                                                case 3:
                                                    Console.WriteLine("┃▷▷                    1.게임 시작                    ◁◁┃");
                                                    break;

                                                case 2:         // 중간
                                                    Console.WriteLine("┃▷▷▷▷                1.게임 시작                ◁◁◁◁┃");
                                                    break;
                                            }
                                            break;

                                        case 14:
                                            switch (bitCnt)
                                            {
                                                case 0:         // 강
                                                    Console.WriteLine("┃▷▷▷▷▷▷▷▷▷▷    2.게임 종료    ◁◁◁◁◁◁◁◁◁◁┃");
                                                    break;

                                                case 1:         // 약
                                                case 3:
                                                    Console.WriteLine("┃▷▷                    2.게임 종료                    ◁◁┃");
                                                    break;

                                                case 2:         // 중간
                                                    Console.WriteLine("┃▷▷▷▷                2.게임 종료                ◁◁◁◁┃");
                                                    break;
                                            }
                                            break;

                                        case 26:
                                            Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
                                            break;

                                        case 27:
                                            Console.WriteLine("");
                                            break;

                                        case 28:
                                            Console.WriteLine("                 음악 : Rift of the NecroDancer");
                                            break;

                                        default:
                                            Console.WriteLine("┃▶▶                                                   ◀◀┃");
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
                                            break;

                                        case 9:
                                            Console.WriteLine("┃▷                       리듬 월드                       ◁┃");
                                            break;

                                        case 12:
                                            Console.WriteLine("┃▷                      1.게임 시작                      ◁┃");
                                            break;

                                        case 14:
                                            Console.WriteLine("┃▷                      2.게임 종료                      ◁┃");
                                            break;

                                        case 26:
                                            Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
                                            break;

                                        case 27:
                                            Console.WriteLine("");
                                            break;

                                        case 28:
                                            Console.WriteLine("                 음악 : Rift of the NecroDancer");
                                            break;

                                        default:
                                            Console.WriteLine("┃▶                                                       ◀┃");
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
                                            Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
                                            break;

                                        case 2:
                                            Console.WriteLine("┃          곡 선택 : 숫자키            시작 : 엔터          ┃");
                                            break;

                                        case 12:
                                            if (1 == choiceMusicIdx)
                                            {
                                                Console.WriteLine($"┃      ◀◀◀◀◀  1. {musicInfo[1].notes.noteFileName} (난이도:{musicInfo[1].difficulty})  ▶▶▶▶▶      ┃");
                                            }
                                            else
                                            {
                                                Console.WriteLine($"┃                  1. {musicInfo[1].notes.noteFileName} (난이도:{musicInfo[1].difficulty})                  ┃");
                                            }
                                            break;

                                        case 14:
                                            if (2 == choiceMusicIdx)
                                            {
                                                Console.WriteLine($"┃      ◀◀◀◀◀  2. {musicInfo[2].notes.noteFileName} (난이도:{musicInfo[2].difficulty})  ▶▶▶▶▶      ┃");
                                            }
                                            else
                                            {
                                                Console.WriteLine($"┃                  2. {musicInfo[2].notes.noteFileName} (난이도:{musicInfo[2].difficulty})                  ┃");
                                            }
                                            break;

                                        case 15:
                                            Console.WriteLine("┃                  [2는 현재 듣기만 가능]                   ┃");
                                            break;

                                        case 26:
                                            Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
                                            break;

                                        case 27:
                                            Console.WriteLine("");
                                            break;

                                        case 28:
                                            Console.WriteLine("           음악 : Rift of the NecroDancer (Esc 뒤로)");
                                            break;

                                        default:
                                            Console.WriteLine("┃                                                           ┃");
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
                                            break;

                                        case 2:
                                            Console.WriteLine("┃          곡 선택 : 숫자키            시작 : 엔터          ┃");
                                            break;

                                        case 12:
                                            if (1 == choiceMusicIdx)
                                            {
                                                Console.WriteLine($"┃            ◀◀  1. {musicInfo[1].notes.noteFileName} (난이도:{musicInfo[1].difficulty})  ▶▶            ┃");
                                            }
                                            else
                                            {
                                                Console.WriteLine($"┃                  1. {musicInfo[1].notes.noteFileName} (난이도:{musicInfo[1].difficulty})                  ┃");
                                            }
                                            break;

                                        case 14:
                                            if (2 == choiceMusicIdx)
                                            {
                                                Console.WriteLine($"┃            ◀◀  2. {musicInfo[2].notes.noteFileName} (난이도:{musicInfo[2].difficulty})  ▶▶            ┃");
                                            }
                                            else
                                            {
                                                Console.WriteLine($"┃                  2. {musicInfo[2].notes.noteFileName} (난이도:{musicInfo[2].difficulty})                  ┃");
                                            }
                                            break;

                                        case 15:
                                            Console.WriteLine("┃                  [2는 현재 듣기만 가능]                   ┃");
                                            break;

                                        case 26:
                                            Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
                                            break;

                                        case 27:
                                            Console.WriteLine("");
                                            break;

                                        case 28:
                                            Console.WriteLine("           음악 : Rift of the NecroDancer (Esc 뒤로)");
                                            break;

                                        default:
                                            Console.WriteLine("┃                                                           ┃");
                                            break;
                                    }
                                }
                            }
                        }
                        break;

                    case GameState.Playing:
                        // 노트 가운데 넘어가면 Fail

                        // 4분의 1박자씩 노트 등장
                        if (bitTimeCnt + bitTime * 0.25m < DateTime.UtcNow.Ticks)
                        {
                            bitTimeCnt = DateTime.UtcNow.Ticks;

                            switch (rand.Next(0, 2))
                            {
                                case 0:         // 왼쪽에서 나오기
                                    break;

                                case 1:         // 오른쪽에서 나오기
                                    break;
                            }
                            //musicInfo[choiceMusicIdx].notes.noteArray[bitIdxCnt];
                            bitIdxCnt++;
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

                            Console.SetCursorPosition(0, 1);
                            Console.Write($"               {musicInfo[choiceMusicIdx].notes.noteFileName}                  점수 : {score}");

                            // 스페이스 바 액션 상태
                            if (true == spaceBarActionIng)       // 스페이스 바 액션 중인 상태
                            {
                                Console.SetCursorPosition(0, 13);
                                Console.Write(" ────────────────────────── ━━━━━ ───────────────────────────");
                                Console.SetCursorPosition(0, 17);
                                Console.Write("                                                             ");
                                Console.SetCursorPosition(0, 18);
                                Console.Write("                                                             ");

                                for (int i = 0; i < spaceBar.spaceBarPushStrArray.Length; i++)
                                {
                                    Console.SetCursorPosition(28, i + 14);
                                    Console.Write(spaceBar.spaceBarPushStrArray[i]);
                                }
                            }
                            else        // 스페이스 바 안눌린 상태
                            {
                                Console.SetCursorPosition(0, 13);
                                Console.Write(" ──────────────────────────       ───────────────────────────");
                                Console.SetCursorPosition(0, 14);
                                Console.Write("                                                             ");
                                Console.SetCursorPosition(0, 15);
                                Console.Write("                            ━━━━━                            ");
                                for (int i = 0; i < spaceBar.spaceBarNotPushStrArray.Length; i++)
                                {
                                    Console.SetCursorPosition(28, i + 16);
                                    Console.Write(spaceBar.spaceBarNotPushStrArray[i]);
                                }
                            }

                            Console.SetCursorPosition(0, 23);
                            Console.Write("                           SPACE BAR                         ");
                            Console.SetCursorPosition(0, 28);
                            Console.Write("           음악 : Rift of the NecroDancer (Esc 중단)");

                            if (true == isMusicStop)
                            {
                                Console.SetCursorPosition(0, 26);
                                Console.Write(" ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                            }
                            else if (true == visualBit)
                            {
                                Console.SetCursorPosition(0, 25);
                                Console.Write(" ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃ ┃");
                                Console.SetCursorPosition(0, 26);
                                Console.Write(" ┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━┻━");
                            }
                            else
                            {
                                Console.SetCursorPosition(0, 26);
                                Console.Write(" ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                            }
                        }

                        if (true == isMusicStop)
                        {
                            Console.SetCursorPosition(0, 5);
                            Console.Write("                          ●●●●●");
                            Console.SetCursorPosition(0, 6);
                            Console.Write("                                  ●");
                            Console.SetCursorPosition(0, 7);
                            Console.Write("                          ●●●●●");
                            Console.SetCursorPosition(0, 8);
                            Console.Write("                                  ●");
                            Console.SetCursorPosition(0, 9);
                            Console.Write("                          ●●●●●");
                            Thread.Sleep(750);

                            Console.SetCursorPosition(0, 8);
                            Console.Write("                          ●                       ");
                            Thread.Sleep(750);

                            for (int i = 5; i < 10; i++)
                            {
                                Console.SetCursorPosition(0, i);
                                Console.Write("                              ●               ");
                            }
                            Thread.Sleep(750);

                            Console.SetCursorPosition(0, 5);
                            Console.Write("                                       ");
                            Console.SetCursorPosition(0, 6);
                            Console.Write("                                       ");
                            Console.SetCursorPosition(0, 7);
                            Console.Write("                                       ");
                            Console.SetCursorPosition(0, 8);
                            Console.Write("                                       ");
                            Console.SetCursorPosition(0, 9);
                            Console.Write("                                       ");

                            MusicSetting(choiceMusicIdx, musicInfo[choiceMusicIdx].notes.offsetSec);

                            isMusicStop = false;
                            spaceBarActionIng = false;
                            spaceBarActionCnt = 0;
                            bitCnt = 0;
                            bitTimeCnt = DateTime.UtcNow.Ticks;
                        }

                        // 스페이스 바 전용 키 체크
                        isSpacePress = (GetAsyncKeyState(0x20) & 0x8000) != 0;      // 실시간 스페이스바 눌림 정보 가져오기
                        if (!spaceBarPressed && isSpacePress)
                        {
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
                        break;

                    case GameState.Clear:
                        Console.Clear();
                        wmp.controls.stop();

                        HighScoreSave(choiceMusicIdx, score);

                        Console.SetCursorPosition(28, 4);
                        Console.WriteLine("C L E A R");
                        Console.SetCursorPosition(28, 12);
                        Console.WriteLine(musicInfo[choiceMusicIdx].notes.noteFileName);
                        Console.SetCursorPosition(28, 15);
                        Console.WriteLine($"점수 : {score}");
                        Console.SetCursorPosition(24, 23);
                        Console.WriteLine("Enter를 눌러 계속");

                        Console.ReadLine();

                        score = 0;

                        musicOn = false;
                        gameState = GameState.MusicSelect;
                        break;

                    case GameState.Gameover:
                        Console.Clear();
                        wmp.controls.stop();

                        Console.SetCursorPosition(30, 4);
                        Console.WriteLine("중 단");
                        Console.SetCursorPosition(28, 12);
                        Console.WriteLine(musicInfo[choiceMusicIdx].notes.noteFileName);
                        Console.SetCursorPosition(28, 15);
                        Console.WriteLine($"점수 : {score}");
                        Console.SetCursorPosition(24, 23);
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
