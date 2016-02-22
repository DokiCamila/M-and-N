using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemaCorporativo.UTIL.nameAndLastName
{
    class nameAndLastName
    {
        public static string FormataNome(string nome)
        {
            int primeiro, segundo, terceiro;
            string completo;
            string nome1, nome2, nome3, nome4;

            primeiro = nome.IndexOf(" ");

            nome1 = nome.Substring(0, primeiro + 1);
            nome2 = nome.Substring(primeiro + 1, (nome.Length - (primeiro + 1)));

            completo = nome1.Trim() + " " + nome2;

            segundo = nome2.IndexOf(" ");

            if (segundo > 0)
            {
                nome3 = nome2.Substring(segundo + 1, (nome2.Length - (segundo + 1)));
                nome2 = nome2.Substring(0, segundo);

                if (nome2.Length <= 3)
                {
                    terceiro = nome3.IndexOf(" ");

                    if (terceiro > 0)
                    {
                        // achei um espaço
                        nome4 = nome3.Substring(0, terceiro + 1);
                        completo = nome1.Trim() + " " + nome2 + " " + nome4.Trim();
                    }
                    else
                    {
                        // n achei, terminou o nome
                        completo = nome1.Trim() + " " + nome2 + " " + nome3;
                    }
                }
                else
                {
                    completo = nome1.Trim() + " " + nome2.Substring(0, segundo);
                }
            }

            return completo;
        }
    }
}
