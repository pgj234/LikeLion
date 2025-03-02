using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace study18
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime now;
            TimeSpan duration;

            now = DateTime.Now;     // 현재 날짜 및 시간
            Console.WriteLine($"Current Date and Time : {now}");

            duration = new TimeSpan(1, 30, 0);      // 1시간 30분
            Console.WriteLine($"Duration : {duration}");
        }
    }
}
