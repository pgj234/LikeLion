using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace study16
{
    class Program
    {
        // 열거형이란?
        // Enumeration enum
        // 숫자 값에 이름을 부여하는 자료형
        // 가독성을 높이고, 의미 있는 값으로 표현 가능
        // 기본적으로 첫 번째 값은 0부터 시작하며 1씩 증가

        // 1. 기본적인 enum 사용법
        enum DayOfWeek
        {
            Sunday,
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday
        }

        // 2. enum 값 변경 (0부터 시작하지 않기)
        enum StatusCode
        {
            Success = 200,
            Badrequest = 400,
            Unaauthorized = 401,
            NotFound
        }

        enum Weapontype
        {
            Sword,
            Bow,
            Staff
        }

        static void Quiz_1(Weapontype weapon)
        {
            if (Weapontype.Sword == weapon)
            {
                Console.WriteLine("검 선택");
            }
            else if (Weapontype.Bow == weapon)
            {
                Console.WriteLine("활 선택");
            }
            else if (Weapontype.Staff == weapon)
            {
                Console.WriteLine("지팡이 선택");
            }
        }

        static void Main(string[] args)
        {
            // 문제
            // 열거형과 함수를 이용해서 풀어주세요.
            // Weapontype.Sword     // 검을 선택했습니다
            // Weapontype.Bow       // 활을 선택했습니다
            // Weapontype.Staff     // 지팡이를 선택했습니다

            // ChooseWeapon(WeaponType.Bow);        // 출력 : 활을 선택했습니다
            Quiz_1(Weapontype.Staff);

            //DayOfWeek today = DayOfWeek.Wednesday;

            //Console.WriteLine(today);
            //Console.WriteLine((int)today);

            //StatusCode sc = StatusCode.NotFound;
            //Console.WriteLine(sc);
            //Console.WriteLine((int)sc);
        }
    }
}
