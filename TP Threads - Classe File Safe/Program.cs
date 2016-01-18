using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Threads___Classe_File_Safe
{
    class Program
    {
        static void Main(string[] args)
        {
            FileThreadUnsafe<int> f = new FileThreadUnsafe<int>(10);

            for (int i = 1; i <= 5; i ++)
            {
                f.Enfiler(i);
            }
            Console.WriteLine("Nombre d'élements: "+ f.NbElements());

            while(!f.Vide())
            {
                Console.WriteLine(f.Premier());
                f.Defiler();
            }
        }
    }
}
