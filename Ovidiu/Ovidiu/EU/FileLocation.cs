using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ovidiu.EU
{
    class FileLocation
    {
        private static string dataBase;
        private static string system ;
        private static string reportDefinitionPath ;
        private static string directorSalvare;

        public static string DataBase { get => dataBase; set => dataBase = value; }
        public static string System { get => system; set => system = value; }
        public static string ReportDefinitionPath { get => reportDefinitionPath; set => reportDefinitionPath = value; }
        public static string DirectorSalvare { get => directorSalvare; set => directorSalvare = value; }
    }
}
