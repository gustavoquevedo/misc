using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginationTester
{
    class PaginationConsoleTester
    {
        static void Main(string[] args)
        {
            var pager = new Pager(70, null, 5);
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            do
            {
                Console.WriteLine("\r\n" + pager.ToString());
                Console.WriteLine("Press N (next), P (previous) or X (exit)");
                key =  Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.N:
                        pager.MoveNext();
                        break;
                    case ConsoleKey.P:
                        pager.MovePrevious();
                        break;

                }
            } while (key.Key != ConsoleKey.X) ;

        }
        
    }

    public class Pager
    {
        private const int PagePositions = 10;
        public Pager(int totalItems, int? page, int pageSize = 10)
        {
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;            

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;

            SetStartAndEnd();
        }

        private void SetStartAndEnd(int positions = PagePositions)
        {
            var startPage = CurrentPage - (int)Math.Floor(PagePositions / 2M);
            var endPage = CurrentPage + (int)Math.Ceiling(PagePositions / 2M) - 1;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > TotalPages)
            {
                endPage = TotalPages;
                if (endPage > PagePositions)
                {
                    startPage = endPage - (PagePositions - 1);
                }
            }

            StartPage = startPage;
            EndPage = endPage;
        }

        public void MovePrevious()
        {
            CurrentPage = Math.Max(CurrentPage - 1, StartPage);
            SetStartAndEnd();
        }

        public void MoveNext()
        {
            CurrentPage = Math.Min(CurrentPage + 1, EndPage);
            SetStartAndEnd();
        }

        public void MoveFirst()
        {
            CurrentPage = StartPage;
            SetStartAndEnd();
        }

        public void MoveLast()
        {
            CurrentPage = EndPage;
            SetStartAndEnd();
        }

        public void MoveToPosition(int position)
        {
            if(position >= StartPage && position <= EndPage)
            {
                CurrentPage = Math.Max(CurrentPage - 1, StartPage);
                SetStartAndEnd();
            }
        }

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }

        public override string ToString()
        {
            return $"TotalItems: {TotalItems}\r\nCurrentPage: {CurrentPage}\r\nPageSize: {PageSize}\r\n"
                + $"TotalPages: {TotalPages}\r\nStartPage: {StartPage}\r\nEndPage: {EndPage}\r\n\r\n"
                ;
        }
    }
}
