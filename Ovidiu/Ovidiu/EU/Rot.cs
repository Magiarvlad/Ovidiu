using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ovidiu.EU
{
    public class Rot
    {
        private static byte nrZecLei;
        private static byte nrZecValuta;
        private static byte nrZecCalcule;
        private static byte nrZecTaxare;

        public static byte NrZecLei { get => nrZecLei; set => nrZecLei = value; }
        public static byte NrZecValuta { get => nrZecValuta; set => nrZecValuta = value; }
        public static byte NrZecCalcule { get => nrZecCalcule; set => nrZecCalcule = value; }
        public static byte NrZecTaxare { get => nrZecTaxare; set => nrZecTaxare = value; }
    }
}
