using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ovidiu.Modules
{
    public class Verifica_Exista_Fisier
    {

        public static bool Verifica_Fisier(string path)
        {
            if (File.Exists(path))
                return true;
            else
                return false;
        }
    }
}
