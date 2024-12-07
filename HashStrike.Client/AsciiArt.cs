using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashStrike.Client
{
    public static class AsciiArt
    {
        public static void PaintArt()
        {
            string[] asciiArt = new string[]
        {
            "         *       _               _           _        _ _           ",
            "     000***0000t| |d1c30145e17b6| |9d4f418d9| |e6a579(_) |678b0833dfbef8e00cb69d451f53 4f  7b  d   4           ",
            "    00*******007| |__   __ _ ___| |__    ___| |_ _ __ _| | _____91f3 9dc7 5a 6f 7 3 4   1  c  ",
            "   00000***00000| '_ \\8/ _` / __| '_ \\7c/ __| __| '__| | |/ / _ \\82735b009d56f1515c82b c7df 16 4 e  2  4   0    f",
            "   00*000*000000| |1| | (_| bcaf5c67654fc14e3d2c2cac06c574d6e1c3afea6fb132dd 7b9 5a 20 62 6  a   b   4   f    1",
            "    ***000000002|_|0|_|\\__,_|___/_|k|_|1|___/\\__|_|th|_|_|\\_\\___|ab36e945b0f5a57eeafdc7 5a 6  f  73 4  1   c    ",
            "     *0000000005fb739ebc860c6d6b3b91f398131f059189c0e93f82959d55d90dda1 06f 3e  d    6  "
        };

            foreach (string line in asciiArt)
            {
                foreach (char c in line)
                {
                    if (char.IsDigit(c) || char.IsLetter(c))
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta; // Фиолетовый для букв и цифр
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan; // Голубой для остальных символов
                    }
                    Console.Write(c);
                }
                Console.WriteLine(); // Переход на новую строку
            }
        }
    }
}
