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
        private BookController mBookController;
        public TelephoneBook()
        {
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
                Console.WriteLine("4. 전체보기");
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
                        mBookController.AddNum();
                        break;
                    case eMainCommand.Delete:
                        if (mBookController.ShowAll())
                        {
                            Console.WriteLine("삭제할 번호의 자리를 입력해주세요>>>");
                            mBookController.DeleteNum(int.Parse(Console.ReadLine()));
                        }
                        break;
                    case eMainCommand.search:
                        mBookController.SerchNum();
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
