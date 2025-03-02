using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace study19
{
    class Person
    {
        public string Name;
        public int Age;

        //기본생성자 
        //클래스가 객체로 생성될때 자동으로 실행되는 특별한 메서드
        //클래스와 같은이름을 가지며, 반환형이 없다 (void도 사용하지않음)
        //객체를 만들때 필요한 초기값을 설정할 때 많이 사용한다.
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
            Console.WriteLine("매개변수 있는 생성자가 생성됨");
        }

        public void ShowInfo()
        {
            Console.WriteLine($"이름 : {Name}, 나이 : {Age}");
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            //클래스
            Person p1 = new Person("박광진", 3); //객체 생성  instance 
            p1.ShowInfo();

            Console.WriteLine();

            Person p2 = new Person("철수", 32);
            p2.ShowInfo();
        }
    }
}
