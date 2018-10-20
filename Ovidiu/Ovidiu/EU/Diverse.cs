using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ovidiu.EU
{
    class Diverse
    {
        private static bool updateCurs;
        private static bool verificaUpdate;
        private static bool verificaNet;

        public static bool UpdateCurs { get => updateCurs; set => updateCurs = value; }
        public static bool VerificaUpdate { get => verificaUpdate; set => verificaUpdate = value; }
        public static bool VerificaNet { get => verificaNet; set => verificaNet = value; }
    }
}
