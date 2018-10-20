using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ovidiu.EU
{
    public class EuCulori
    {
        private static long evenRowStyle_BackColor;
        private static long oddRowStyle_BackColor;
        private static long highlightRowStyle_BackColor;
        private static long longHighlightRowStyle_ForeColor;
        private static long meniu_Color;

        public static long EvenRowStyle_BackColor { get => evenRowStyle_BackColor; set => evenRowStyle_BackColor = value; }
        public static long OddRowStyle_BackColor { get => oddRowStyle_BackColor; set => oddRowStyle_BackColor = value; }
        public static long HighlightRowStyle_BackColor { get => highlightRowStyle_BackColor; set => highlightRowStyle_BackColor = value; }
        public static long HighlightRowStyle_ForeColor { get => longHighlightRowStyle_ForeColor; set => longHighlightRowStyle_ForeColor = value; }
        public static long Meniu_Color { get => meniu_Color; set => meniu_Color = value; }
    }
}
