using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ovidiu.Modules
{
    public static class RegistriiOperatii
    {
        public const int REG_SZ  = 1;
        public const int REG_DWORD= 4;
        public const uint HKEY_CLASSES_ROOT = 0x80000000;
        public const uint HKEY_CURRENT_USER = 0x80000001;
        public const uint HKEY_LOCAL_MACHINE = 0x80000002;
        public const uint HKEY_USERS = 0x80000003;

        public const int ERROR_NONE = 0;
        public const int ERROR_BADDB = 1;
        public const int ERROR_BADKEY = 2;
        public const int ERROR_CANTOPEN = 3;
        public const int ERROR_CANTREAD = 4;
        public const int ERROR_CANTWRITE = 5;
        public const int ERROR_OUTOFMEMORY = 6;
        public const int ERROR_ARENA_TRASHED = 7;
        public const int ERROR_ACCESS_DENIED = 8;
        public const int ERROR_INVALID_PARAMETERS = 87;
        public const int ERROR_NO_MORE_ITEMS = 259;

        public const int KEY_QUERY_VALUE = 0x1;
        public const int KEY_SET_VALUE = 0x2;
        public const int KEY_ALL_ACCESS = 0x3F;

        public const int REG_OPTION_NON_VOLATILE = 0;

        [System.Runtime.InteropServices.DllImport("advapi32.dll")]
        static extern long RegSetValueExString(long hKey, string lpValueName, long Reserved, long dwType, string lpValue, long cbData);
        [System.Runtime.InteropServices.DllImport("advapi32.dll")]
        static extern long RegSetValueExLong(long hKey, string lpValueName, long Reserved, long dwType, long lpValue, long cbData);
        [System.Runtime.InteropServices.DllImport("advapi32.dll")]
        static extern long RegOpenKeyEx(long hKey, string lpSubKey, long ulOptions, long samDesired, long phkResult);
        [System.Runtime.InteropServices.DllImport("advapi32.dll")]
        static extern long RegQueryValueExNULL(long hKey, string lpValueName, long lpReserved, long lpType, long lpData, long lpcbData);
        [System.Runtime.InteropServices.DllImport("advapi32.dll")]
        static extern long RegCloseKey(long hKey);
        [System.Runtime.InteropServices.DllImport("advapi32.dll")]
        static extern long RegQueryValueExString(long hKey, string lpValueName, long lpReserved, long lpType, string lpData, long lpcbData);
        [System.Runtime.InteropServices.DllImport("advapi32.dll")]
        static extern long RegQueryValueExLong(long hKey, string lpValueName, long lpReserved, long lpType, long lpData, long lpcbData);

        public static long SetValueEx(long hKey, string sValueName, long lType, long vValue)
        {
            long lValue;
            string sValue;
            long _SetValueEx=0;
            switch (lType)
            {
                case 1:
                    {
                        sValue = vValue + "\0";
                        _SetValueEx = RegSetValueExString(hKey, sValueName, '0', lType, sValue,sValue.Length);
                        break;
                    }

                case  4:
                    {
                        lValue = vValue;
                        _SetValueEx = RegSetValueExLong(hKey, sValueName, '0', lType, lValue, 4);
                        break;
                    }
            }
            return _SetValueEx;
        }
        static public long QueryValueEx(long lhKey, string szValueName, string vValue)
        {
            long cch=0;
            long lrc;
            long lType=0;
            long lValue=0;
            string sValue;
            long _QueryValueEx = 0;
            lrc = RegQueryValueExNULL(lhKey, szValueName, '0', lType, '0', cch);

            switch (lType)
            {
                case 1:
                    {
                        sValue = cch.ToString();
                        lrc = RegQueryValueExString(lhKey, szValueName,'0', lType, sValue, cch);
                        vValue = sValue.Substring(0, Convert.ToInt32(cch - 1));
                        break;
                    }

                case 4:
                    {
                        lrc = RegQueryValueExLong(lhKey, szValueName,'0', lType, lValue, cch);
                        vValue = lValue.ToString();
                        break;
                    }

                default:
                    { 
                    lrc = -1;
                        break;
                    }

            }
            _QueryValueEx = lrc;
            return _QueryValueEx;
        }

            public static string CitesteValoareREG(long LngHKEYPredefinit, string sKeyName, string sValueName)
        {
            long lRetVal;         // result of the API functions
            long hKey =0 ;         // handle of opened key
            string vValue ="";      // setting of queried value

            lRetVal = RegOpenKeyEx(LngHKEYPredefinit, sKeyName, 0, KEY_QUERY_VALUE, hKey);
            lRetVal = QueryValueEx(hKey, sValueName, vValue);

            
            RegCloseKey(hKey);

            return vValue;
        }
    }
}
