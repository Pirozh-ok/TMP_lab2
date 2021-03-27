using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tmp_lab2
{
    public class MyMenu
    {
        public MyMenu(string _path)
        {
            path = _path; 
        }
        const int N = 100;
        private string path; 
        private DateString[] arrayDates = new DateString[N];

        public DateString[] ArrayDates { get => arrayDates; set => arrayDates = value; }

        //функция считывания строк из файла и записывает их в массив
        public void ReadFile()
        {
            string[] strings = File.ReadAllLines(path); // сохраняем все строки файла

            for (int i = 0; i < strings.Length; i++)
            {
                DateString ob = new DateString();
                ReadDate(ref ob, strings[i]);
                ArrayDates[i] = ob;
            }
        }

        //метод чтения данных из строки в структуру. ob - объект куда сохраняем, str - строка считанная из файла
        public void ReadDate(ref DateString ob, string str)
        {
            int i = 0;
            string ch = "";

            while (i < str.Length)
            {
                if (str[i] != ' ')
                    ch += str[i];
                else
                {
                    i = i + 1;
                    ob.Level = Convert.ToInt32(ch);
                    ch = "";
                    break;
                }
                i++;
            }

            while (i < str.Length)
            {
                if (str[i] != ' ')
                    ch += str[i];
                else
                {
                    i = i + 1;
                    ob.NameUzel = ch;
                    ch = "";
                    break;
                }
                i++;
            }

            while (i < str.Length)
            {
                if (str[i] != ' ')
                    ch += str[i];
                else
                {
                    i = i + 1;
                    ob.Status = Convert.ToInt32(ch);
                    ch = "";
                    break;
                }
                i++;
            }

            while (i < str.Length)
            {
                ch += str[i];
                i++;
            }
            ob.NameMethod = ch;
        }

    }
}
