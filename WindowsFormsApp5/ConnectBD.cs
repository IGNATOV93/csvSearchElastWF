using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace WindowsFormsApp5
{
     class ConnectionString
    { 
        public static string ConnStr
        {
            get { return ConfigurationManager.ConnectionStrings["WindowsFormsApp5.Properties.Settings.connectString"].ConnectionString; }
        }
    }
}
