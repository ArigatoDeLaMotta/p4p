using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            CTest test = new CTest();

            //test.CreateUser();
            //test.CreateOferta();
            //test.CUser_Load();
            /*
            for (int i = 0; i < 200; i++)
            {
                test.CreateOferta();
            }
            */

            test.OfertaSearch();

        }
    }
}
