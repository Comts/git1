using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telephone
{
    public enum eMainCommand
    {
        Add = 1,
        Delete,
        search,
        Show,
        Exit
    }
    class TelephoneBook
    {
        private Number mNum;
        private BookController mBookController;
        public TelephoneBook()
        {
            mNum = new Number();
            mBookController = new BookController();
        }
        public void MainLoop()
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("==========전화번호부==========");
                Console.WriteLine("1. 추가");
                Console.WriteLine("2. 삭제");
                Console.WriteLine("3. 검색");
                Console.WriteLine("4. 보기");
                Console.WriteLine("5. 나가기");
                Console.Write("커맨드를 입력하세요 >>> ");
                int Command = 0;
                if (!int.TryParse(Console.ReadLine(), out Command))
                {
                    Console.WriteLine("번호를 입력하세요.");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
                if (Command > 5 || Command < 1)
                {
                    Console.WriteLine("1~5사이의 숫자를 입력해주세요>>>");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
                switch ((eMainCommand)Command)
                {
                    case eMainCommand.Add:
                        Console.Write("전화번호 입력해주세요>>>");
                        mNum.PhoneNumber = int.Parse(Console.ReadLine());
                        Console.Write("이름을 입력해주세요>>>");
                        mNum.Name = Console.ReadLine();
                        Console.Write("생일을 입력해주세요>>>");
                        mNum.Birthday = int.Parse(Console.ReadLine());
                        mBookController.AddNum(mNum);
                        break;
                    case eMainCommand.Delete:
                        break;
                    case eMainCommand.search:
                        break;
                    case eMainCommand.Show:
                        mBookController.ShowAll();
                        break;
                    case eMainCommand.Exit:
                        return;
                    default:
                        Console.WriteLine("잘못된 커맨드 입니다.");
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
