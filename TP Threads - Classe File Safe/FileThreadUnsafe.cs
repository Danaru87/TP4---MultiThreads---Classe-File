using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Threads___Classe_File_Safe
{
    class FileThreadUnsafe<T>
    {
        private T[] tab;
        private int tete, queue;

        public FileThreadUnsafe(int taille)
        {
            tab = new T[taille];
            Init();
        }

        private int Suivant(int i)
        {
            return (i + 1) % tab.Length;
        }

        private void Init()
        {
            tete = queue = -1;
        }

        public void Enfiler(T element)
        {
            if (Pleine())
                throw new ExceptionFilePleine();
            else if (Vide())
                tab[queue = tete = 0] = element;
            else
                tab[queue = Suivant(queue)] = element;
        }

        public void Defiler()
        {
            if(Vide())
            {
                throw new ExceptionFileVide();
            }
            else if(tete == queue)
            {
                tete = queue = -1;
            }
            else
            {
                tete = Suivant(tete);
            }
        }

        public bool Vide()
        {
            return (tete == queue && tete == -1);
        }

        public bool Pleine()
        {
            return tete == Suivant(queue);
        }

        public int NbElements()
        {
            if (Vide())
                return 0;
            else if (tete <= queue)
                return queue - tete + 1;
            else
                return tab.Length + queue - tete + 1;
        }

        public T Premier()
        {
            if(Vide())
            {
                throw new ExceptionFileVide();
            }
               
            else
            {
                System.Threading.Thread.Sleep(5000);
                return tab[tete];
            }
        }

        public class ExceptionFileVide : Exception { }
        public class ExceptionFilePleine: Exception { }
    }
}
