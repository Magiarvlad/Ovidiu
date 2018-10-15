using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ovidiu.Miscellaneous
{
    public static class ClasaSuport
    {

        public static bool ProgramIsAlreadyRunning()
        {
            string procName = Process.GetCurrentProcess().ProcessName;
            Process[] processes = Process.GetProcessesByName(procName);

            if (processes.Length > 1)
                return true;
            else
                return false;
        }










    }

    
}
