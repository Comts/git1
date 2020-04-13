using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telephone
{
    
    class Number
    {
        public string Name;
        public int PhoneNumber;
        public int Birthday;
    }

    class BookController
    {
        private List<Number> mNumber;
        public BookController()
        {
            mNumber = new List<Number>();

        }

        public void AddNum(Number num)
        {
            mNumber.Add(num);
            Console.WriteLine("추가");
        }

        public void DeleteNum(int id) 
        {
            if (id >= 0 && id < mNumber.Count)
            {
                mNumber.RemoveAt(id);
            }
        }
        public void ShowAll()
        {
            for(int i =0; i<mNumber.Count;i++)
            {
                if (mNumber[i] == null)
                {
                    Console.WriteLine("<비어있음>");
                }
                else
                {
                    Console.WriteLine("Phone: " + mNumber[i].PhoneNumber);
                    Console.WriteLine("Name: " + mNumber[i].Name);
                    Console.WriteLine("Birth: " + mNumber[i].Birthday);
                    Console.WriteLine();
                }
            }

        }
    }
}
