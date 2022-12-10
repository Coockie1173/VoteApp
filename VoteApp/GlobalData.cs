using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteApp
{
    public interface IGetDeviceInfo { string GetDeviceID(); }

    internal class GlobalData
    {
        public static string MyID = "";
        public static string URI = "http://127.0.0.1:8000";
    }
}
