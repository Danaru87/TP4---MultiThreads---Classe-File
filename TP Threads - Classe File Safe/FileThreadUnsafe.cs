using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TP_Threads___Classe_File_Safe
{
    class FileThreadUnsafe<T>
    {
        readonly object blocmoi = new object();
        private T[] tab;
        private int tete, queue;

        public FileThreadUnsafe(int taille)
        {
            tab = new T[taille];
            Init();
        }

        private int Suivant(int i)
        {
            lock(blocmoi)
            {
                return (i + 1) % tab.Length;
            }
        }
        private void Init()
        {
            lock(blocmoi)
            {
                tete = queue = -1;
            }
        }

        public void Enfiler(T element)
        {
            lock(blocmoi)
            {
                if (Pleine())
                    throw new ExceptionFilePleine();
                else if (Vide())
                    tab[queue = tete = 0] = element;
                else
                    tab[queue = Suivant(queue)] = element;
            }
        }

        public void Defiler()
        {
            lock(blocmoi)
            {
                if (Vide())
                {
                    throw new ExceptionFileVide();
                }
                else if (tete == queue)
                {
                    Thread.Sleep(1000);
                    Init();
                }
                else
                {
                    tete = Suivant(tete);
                }
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
            lock(blocmoi)
            {
                if (Vide())
                    return 0;
                else if (tete <= queue)
                    return queue - tete + 1;
                else
                    return tab.Length + queue - tete + 1;
            }
        }

        public T Premier()
        {
            lock(blocmoi)
            {
                if (Vide())
                {
                    throw new ExceptionFileVide();
                }
                else
                {
                    return tab[tete];
                }
            }
        }

        public class ExceptionFileVide : Exception { }
        public class ExceptionFilePleine: Exception { }
    }
}
