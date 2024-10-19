using SWAPIConnectionApp.API;
using SWAPIConnectionApp.Interfaces;
using SWAPIConnectionApp.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAPIConnectionApp.FileAccess
{
    static class FileWriter
    {
        private static void WriteToFile(string fileName, string text)
        {
            File.WriteAllText(fileName, text);
        }

        public static void WriteOutputPersonToFile(Person person)
        {
            var personJson = person.SerializeObjectToJSON();

            FileWriter.AddLog($"Wpisanie danych wyjściowych do pliku: {Const.OutputPersonFileName}");

            WriteToFile(Const.OutputPersonFileName, personJson);
        }

        public static void AddLog(string logText)
        {
            File.AppendAllText(Const.LogsFileName, logText + Environment.NewLine);
        }
    }
}
