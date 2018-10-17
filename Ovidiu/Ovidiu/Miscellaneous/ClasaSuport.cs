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
            bool result = false; // true If is running
            string procName = Process.GetCurrentProcess().ProcessName;
            Process[] processes = Process.GetProcessesByName(procName);
            if (processes.Length > 1)  
                result = true;
            return result;
        }

        public static void StartProgramByFileName(string fileName, bool asAdministrator = false)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = fileName;
            if (asAdministrator = true)
                proc.StartInfo.Verb = "runas";
            proc.Start();
        }
    }
}
