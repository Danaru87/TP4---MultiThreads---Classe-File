using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TP_Threads___Classe_File_Safe
{
    class Program
    {
        static Thread th1;
        static Thread th2;
        static FileThreadUnsafe<int> f;
        static void Main(string[] args)
        {
            f = new FileThreadUnsafe<int>(10);

            for (int i = 0; i < 100; i++)
            {
                try { f.Enfiler(i); }
                catch (Exception e) { Console.WriteLine("La file est pleine"); break; }
            }
            Console.WriteLine("Nombre d'élements: "+ f.NbElements());

            th1 = new Thread(TH1Func);
            th2 = new Thread(TH2Func);
            th1.Start();
            th2.Start();
            th1.Join();
            th2.Join();
        }

        public static void TH1Func()
        {
            Console.WriteLine(f.NbElements());
            while(!f.Vide())
            {
                try
                {
                    f.Defiler();
                }
                catch(Exception e)
                {
                    Console.WriteLine("Aucune action effectuée: " + e.Message);
                }
            }
        }

        public static void TH2Func()
        {
            Console.WriteLine(f.NbElements());
            while(!f.Vide())
            {
                try
                {
                    Console.WriteLine(f.Premier());
                    f.Defiler();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
            }
        }
    }
}
