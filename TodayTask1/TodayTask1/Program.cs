using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TodayTask1
{
    class Program
    {
        static void Main(string[] args)
        {
            // 로딩
            // 시작화면
            // 게임 스토리1

            Console.WriteLine("로 딩 중 (잠시만 기다려주세요)");
            Console.Write("☆☆☆☆☆☆☆☆");
            Thread.Sleep(500);
            Console.Clear();

            Console.WriteLine("로 딩 중 (잠시만 기다려주세요)");
            Console.Write("★☆☆☆☆☆☆☆");
            Thread.Sleep(500);
            Console.Clear();

            Console.WriteLine("로 딩 중 (잠시만 기다려주세요)");
            Console.Write("★★☆☆☆☆☆☆");
            Thread.Sleep(500);
            Console.Clear();

            Console.WriteLine("로 딩 중 (잠시만 기다려주세요)");
            Console.Write("★★★☆☆☆☆☆");
            Thread.Sleep(500);
            Console.Clear();

            Console.WriteLine("로 딩 중 (잠시만 기다려주세요)");
            Console.Write("★★★★☆☆☆☆");
            Thread.Sleep(500);
            Console.Clear();

            Console.WriteLine("로 딩 중 (잠시만 기다려주세요)");
            Console.Write("★★★★★☆☆☆");
            Thread.Sleep(500);
            Console.Clear();

            Console.WriteLine("로 딩 중 (잠시만 기다려주세요)");
            Console.Write("★★★★★★☆☆");
            Thread.Sleep(500);
            Console.Clear();

            Console.WriteLine("로 딩 중 (잠시만 기다려주세요)");
            Console.Write("★★★★★★★☆");
            Thread.Sleep(500);
            Console.Clear();

            Console.WriteLine("로 딩 중 (잠시만 기다려주세요)");
            Console.Write("★★★★★★★★");
            Thread.Sleep(500);
            Console.Clear();

            Console.WriteLine("엔터를 누르면 시작됩니다");
            Console.ReadLine();
            Console.Clear();

            Console.WriteLine("아구라시아의 협곡에 오신 것을 환영합니다 (엔터로 계속)");
            Console.ReadLine();
            Console.Clear();

            Console.WriteLine("[나레이션] : 당신의 직업은 귀검사이며, 정글을 돌아 몬스터를 잡으며 성장하고, 팀원과 함께 상대방을 잡아내야합니다 (엔터로 계속)");
            Console.ReadLine();
            Console.Clear();

            Console.WriteLine("[나레이션] : 게임 3분 경과, 당신은 주황버섯을 잡고 있었습니다 \n 하지만, 갑자기 팀원이 빨리와서 안도와주냐고 욕을하기 시작합니다 (엔터로 계속)");
            Console.ReadLine();
            Console.Clear();

            Console.WriteLine("[나레이션] : 당신은 다음과 같이 말합니다 (채팅에 할 말을 입력 후 엔터)");
            string strInput = Console.ReadLine();       // 좀만 기다려보셈
            Console.Clear();

            Console.WriteLine($"나 : {strInput}");
            Thread.Sleep(2500);

            Console.WriteLine($"liiliIIli : 안오면 던짐 ㅅㄱ");
            Thread.Sleep(2500);
            Console.Clear();

            Console.WriteLine("[나레이션] : 당신은 n분만 기다려보라고 말하려고합니다 (n에 들어갈 시간 숫자(정수)를 입력 후 엔터)");
            int numInput = int.Parse(Console.ReadLine());       // n분
            Console.Clear();

            Console.WriteLine($"나 : {numInput}분만 버텨보셈");
            Thread.Sleep(2000);

            Console.WriteLine("[나레이션] : 'liiliIIli'님이 사망하였습니다");
            Thread.Sleep(3000);

            Console.WriteLine($"liiliIIli : 뭐?? {numInput}분? 넌 이판 평생해라");
            Thread.Sleep(2000);

            Console.WriteLine("[나레이션] : 'liiliIIli'님이 게임에서 떠났습니다");
            Thread.Sleep(2000);

            Console.WriteLine("[나레이션] : '울팀나가면나도나감'님이 게임에서 떠났습니다");
            Thread.Sleep(2500);

            Console.WriteLine("열심히하자 : 5:3도 잘하면 이길 수 있음  열심히 해봐여");
            Thread.Sleep(4000);

            Console.WriteLine("트롤한판 : 페이커가 분당 cs 1개 먹는 소리하고앉아있네 나도 나감 ㅅㄱ");
            Thread.Sleep(4500);

            Console.Clear();

            Console.WriteLine("[나레이션] : 현재 당신과 팀원 1명만이 남아있습니다");
            Thread.Sleep(3000);

            Console.WriteLine("[나레이션] : 상대는 5명, 우리는 2명..  어떻게 하시겠습니까?");
            Thread.Sleep(3000);

            Console.WriteLine("1 : 팀원을 버리고 게임을 종료한다");
            Console.WriteLine("2 : 팀원을 버리고 게임을 종료한다");
            Console.WriteLine("(번호 입력 후 엔터)");
            numInput = int.Parse(Console.ReadLine());       // n

            Console.Clear();

            Console.WriteLine($"{numInput}을 선택하여 팀원을 버리고 게임을 종료합니다");
            Thread.Sleep(3000);

            Console.WriteLine("평범한 엔딩 (END)");
        }
    }
}
