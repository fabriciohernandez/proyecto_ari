using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARIProject.controllers
{
    static class Vigenere
    {

        public static string VigenereEncode(string message, string key)
        {
            char[] auxKey;
            char[,] matrix;
            char [] charMessage = message.ToCharArray();
            char[] tempKey = key.ToCharArray();
            auxKey = new char[charMessage.Length];
            int cont = 0;

            for (int i = 0; i < charMessage.Length; i++)
            {
                auxKey[i] = tempKey[cont];
                cont++;
                if (cont == tempKey.Length)
                    cont = 0;
            }

            matrix = GenMatrixNum();
            return Encode(charMessage, auxKey, matrix);
        }

        private static string Encode(char[] charMessage, char[] auxKey, char[,] matrix)
        {
            char[] encoded = new char[charMessage.Length];
            int i;
            int j;
            for (int cont = 0; cont < charMessage.Length; cont++)
            {
                i = charMessage[cont] - 48;
                j = auxKey[cont] - 48;
                encoded[cont] = matrix[i, j];

            }

            return new string(encoded);
        }
        public static String Decode(string message, string key)
        {
            char[] charMessage = message.ToCharArray();
            char[]  auxKey = new char[charMessage.Length];
            char[] tempKey = key.ToCharArray();
            auxKey = new char[charMessage.Length];
            int counter = 0;

            for (int i = 0; i < charMessage.Length; i++)
            {
                auxKey[i] = tempKey[counter];
                counter++;
                if (counter == tempKey.Length)
                    counter = 0;
            }

            char[] decoded = new char[charMessage.Length];
            for (int cont = 0; cont < charMessage.Length; cont++)
            {
                int aux = (charMessage[cont] - auxKey[cont]);
                if (aux < 0)
                {
                    aux = aux + 10 * 1;
                }
                decoded[cont] = (char)(aux + 48);

            }
            return new string(decoded) ;

        }
        private static char[,] GenMatrixNum()
        {
            int contador;
            char[] abcTemp = GenArrayNums();
            char[] abc = new char[abcTemp.Length * 2];

            for (int c = 0; c < 10; c++)
            {
                abc[c] = abcTemp[c];
                abc[c + 10] = abcTemp[c];
            }
            char[,] matriz = new char[10, 10];
            for (int i = 0; i < 10; i++)
            {
                contador = 0;
                for (int j = 0; j < 10; j++)
                {
                    matriz[i, j] = abc[contador + i];
                    contador++;
                }
            }
            return matriz;
        }

        private static char[] GenArrayNums()
        {
            char[] abc = new char[10];

            for (int i = 48; i <= 57; i++)
            {
                abc[i - 48] = (char)i;
            }
            return abc;
        }
    }
}
