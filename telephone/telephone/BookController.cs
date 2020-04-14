using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telephone
{

    public enum eSerchCommand
    {
        PhoneNumber = 1,
        Name,
        Birthday,
        Exit
    }
    class BookController
    {
        private List<string> mPhoneNumber;
        private List<int> mBirthday;
        private List<string> mName;
        private int Index;
        public BookController()
        {
            mPhoneNumber = new List<string>();
            mBirthday = new List<int>();
            mName = new List<string>();


        }
        
        public void SerchNum()
        {

            while (true)
            {
                if (mPhoneNumber.Count == 0)
                {
                    Console.WriteLine("<비어있음>");
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("==========검색==========");
                    Console.WriteLine("1. 전화번호검색");
                    Console.WriteLine("2. 이름검색");
                    Console.WriteLine("3. 생일검색");
                    Console.WriteLine("4. 돌아가기");
                    Console.Write("커맨드를 입력하세요 >>> ");
                    int Command = 0;
                    if (!int.TryParse(Console.ReadLine(), out Command))
                    {
                        Console.WriteLine("번호를 입력하세요.");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }
                    if (Command > 4 || Command < 1)
                    {
                        Console.WriteLine("1~4사이의 숫자를 입력해주세요>>>");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }
                    switch ((eSerchCommand)Command)
                    {
                        case eSerchCommand.PhoneNumber:
                            Console.WriteLine("전화번호입력>>");
                            Index = mPhoneNumber.IndexOf(Console.ReadLine());
                            
                            break;
                        case eSerchCommand.Name:
                            Console.WriteLine("이름입력>>");
                            Index = mName.IndexOf(Console.ReadLine());
                            break;
                        case eSerchCommand.Birthday:
                            Console.WriteLine("생일입력>>");
                            Index = mBirthday.IndexOf(int.Parse(Console.ReadLine()));
                            break;
                        case eSerchCommand.Exit:
                            return;
                        default:
                            Console.WriteLine("잘못된 커맨드 입니다.");
                            break;
                    }
                    if (Index >= 0 && Index <= mPhoneNumber.Count)
                    {
                        Console.WriteLine(Index + 1);
                        Console.Write("Phone: " + mPhoneNumber[Index]);
                        Console.Write("\tName: " + mName[Index]);
                        Console.Write("\tBirth: " + mBirthday[Index]);
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("검색하지 못 하였습니다.");

                    }
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        private void PhoneSerch()
        {

        }
        public void AddNum()
        {

            Console.Write("전화번호 입력해주세요>>>");
            mPhoneNumber.Add(Console.ReadLine());
            Console.Write("이름을 입력해주세요>>>");
            mName.Add(Console.ReadLine());
            Console.Write("생일을 입력해주세요>>>");
            mBirthday.Add(int.Parse(Console.ReadLine()));
            Console.WriteLine("전화번호추가완료");
        }

        public void DeleteNum(int id) 
        {
            id--;
            if (id >= 0 && id < mPhoneNumber.Count)
            {
                mPhoneNumber.RemoveAt(id);
                mName.RemoveAt(id);
                mBirthday.RemoveAt(id);
            }
            Console.WriteLine(id+1+"번째 삭제완료");
        }

        public bool ShowAll()
        {
            if (mPhoneNumber.Count == 0)
            {
                Console.WriteLine("<비어있음>");
                return false;
            }
            for (int i =0; i< mPhoneNumber.Count;i++)
            {
                Console.WriteLine(i+1);
                Console.Write("Phone: " + mPhoneNumber[i]);
                Console.Write("\tName: " + mName[i]);
                Console.Write("\tBirth: " + mBirthday[i]);
                Console.WriteLine();
            }
            return true;

        }
    }
}
