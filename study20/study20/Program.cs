using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace study20
{
    // Game 클래스
    class Game
    {
        public static int minerals;
        public static int gas;
        public static int population;

        public static void ShowInfo()
        {
            Console.WriteLine($"미네랄 {minerals}   가스 {gas}   인구수 {population}");
        }
    }

    class Program
    {
        // 마린 클래스
        // 이름, 미네랄 = 50
        // 기본 생성자, 인자있는 생성자
        class Marine
        {
            public string Name;
            public int Minerals;

            public Marine()
            {
                Name = "Marine";
                Minerals = 50;
            }

            public Marine(string _name, int _minerals)
            {
                Name = _name;
                Minerals = _minerals;
            }

            public void ShowInfo()
            {
                Console.WriteLine($"이름 : {Name}, 미네랄 : {Minerals}");
            }
        }

        // SCV 클래스
        // 이름, 미네랄 = 50
        // 기본 생성자, 인자있는 생성자
        class SCV
        {
            public string Name;
            public int Minerals;

            public SCV()
            {
                Name = "SCV";
                Minerals = 50;
            }

            public SCV(string _name, int _minerals)
            {
                Name = _name;
                Minerals = _minerals;
            }

            public void ShowInfo()
            {
                Console.WriteLine($"이름 : {Name}, 미네랄 : {Minerals}");
            }
        }

        // 배럭 클래스
        // 이름, 미네랄 = 150
        // 기본 생성자, 인자있는 생성자
        class Barracks
        {
            public string Name;
            public int Minerals;

            public Barracks()
            {
                Name = "Barracks";
                Minerals = 150;
            }

            // this 키워드를 이용해보자
            // this 자기 자신(class)을 가르킨다
            public Barracks(string Name, int Minerals)
            {
                this.Name = Name;
                this.Minerals = Minerals;
            }

            public void ShowInfo()
            {
                Console.WriteLine($"이름 : {Name}, 미네랄 : {Minerals}");
            }
        }

        // 미네랄 클래스
        // Minerals 1500
        // 7개 시작
        class MineralsField
        {
            public string Name;
            public int Minerals;

            public MineralsField()
            {
                Name = "Minerals Field";
                Minerals = 1500;
            }

            public MineralsField(string _name, int _minerals)
            {
                Name = _name;
                Minerals = _minerals;
            }

            public void ShowInfo()
            {
                Console.WriteLine($"이름 : {Name}, 미네랄 : {Minerals}");
            }
        }

        static void Main(string[] args)
        {
            Game.minerals = 50;
            Game.gas = 0;
            Game.population = 4;
            Game.ShowInfo();

            Console.WriteLine();

            Marine marine = new Marine("황제 테란", 100);
            SCV scv = new SCV("열받은 SCV", 70);
            Barracks barracks = new Barracks();
            MineralsField[] mineralsField = new MineralsField[7];       // 클래스의 배열

            marine.ShowInfo();

            Console.WriteLine();

            scv.ShowInfo();

            Console.WriteLine();

            barracks.ShowInfo();

            Console.WriteLine();

            for (int i=0; i<mineralsField.Length; i++)
            {
                mineralsField[i] = new MineralsField();     // 각 배열에 new 객체화
                mineralsField[i].ShowInfo();
            }
        }
    }
}
