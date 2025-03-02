using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace inventory
{
    enum ItemName
    {
        포션,
        검,
        방패
    }

    struct Item
    {
        public string Name;
        public int Cnt;
    }

    class Program
    {
        // 최대 아이템 개수 (배열 크기)
        const int Max_ITEMS = 10;

        static Item[] item = new Item[Max_ITEMS];

        // 아이템 배열 (이름 저장)
        //static string[] itemNames = new string[Max_ITEMS];
        //static int[] itemCntArray = new int[Max_ITEMS];

        // 아이템 추가 함수
        static void AddItem(ItemName name, int cnt)
        {
            for (int i = 0; i < Max_ITEMS; i++)
            {
                if (item[i].Name == name.ToString())       // 이미 있는 아이템이면 개수 증가
                {
                    item[i].Cnt += cnt;
                    return;
                }
            }

            // 빈 슬롯에 새로운 아이템 추가
            for (int i = 0; i < Max_ITEMS; i++)
            {
                if (null == item[i].Name)
                {
                    item[i].Name = name.ToString();
                    item[i].Cnt = cnt;
                    return;
                }
            }
            Console.WriteLine("인벤토리가 가득 찼습니다");
        }

        // 아이템 제거 함수
        static void RemoveItem(ItemName name, int cnt)
        {
            for (int i=0; i<Max_ITEMS; i++)
            {
                if (name.ToString() == item[i].Name)       // 이름하고 같은지
                {
                    if (item[i].Cnt >= cnt) //개수가 충분하면 차감
                    {
                        item[i].Cnt -= cnt;
                        if (item[i].Cnt == 0) //개수가 0이면 삭제
                        {
                            item[i].Name = null;
                        }
                        return;
                    }
                    else
                    {
                        Console.WriteLine("아이템 개수가 부족합니다!");
                        Console.WriteLine();
                        return;
                    }
                }
            }
            Console.WriteLine("아이템을 찾을 수 없습니다");
        }

        // 인벤토리 출력 함수
        static void ShowInventory()
        {
            Console.WriteLine("현재 인벤토리 : ");
            bool isEmpty = true;

            for (int i=0; i<Max_ITEMS; i++)
            {
                if (null != item[i].Name)
                {
                    Console.WriteLine($"{item[i].Name} (x{item[i].Cnt})");
                    isEmpty = false;
                }
            }

            if (true == isEmpty)
            {
                Console.WriteLine("인벤토리가 비어있습니다");
            }
        }

        static void Main(string[] args)
        {
            // 테스트 : 아이템 추가
            AddItem(ItemName.포션, 5);
            AddItem(ItemName.검, 1);
            AddItem(ItemName.포션, 3);

            ShowInventory();
            Console.WriteLine();

            // 아이템 사용
            Console.WriteLine("포션 2개 사용");
            RemoveItem(ItemName.포션, 2);
            ShowInventory();
            Console.WriteLine();

            // 테스트 : 없는 아이템 제거
            Console.WriteLine("방패 1개 제거 시도");
            RemoveItem(ItemName.방패, 1);
            ShowInventory();
            Console.WriteLine();

            // 테스트 : 모든 포션 제거
            Console.WriteLine("포션 6개 사용");
            RemoveItem(ItemName.포션, 6);
            ShowInventory();
            Console.WriteLine();
        }
    }
}
