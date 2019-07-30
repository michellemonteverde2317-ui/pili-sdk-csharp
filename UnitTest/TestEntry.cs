using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class TestEntry
    {
        // Replace with your keys here
        public string ACCESS_KEY = "";
        public string SECRET_KEY = "";

        // Replace with your hub name
        public string HUB_NAME = "";

        //配置文件的所在路径
        private string setupFile = System.Environment.CurrentDirectory + "\\..\\..\\Qiniu_setup.ini";
        //private string setupFile = @"e:\\Qiniu_setup.ini";
        public TestEntry()
        {
            if (System.IO.File.Exists(this.setupFile))
            {
                //获取配置文件中的信息
                Dictionary<string, string> dic = new Dictionary<string, string>();
                string text = System.IO.File.ReadAllText(this.setupFile);                
                foreach(string row in text.Split('\n'))
                {
                    if (string.Empty.Equals(row.Trim())) continue;
                    if (row.Trim().StartsWith("//")) continue;
                    if (row.IndexOf("=") < 0) continue;
                    string key = row.Substring(0,row.IndexOf("=")).Trim();
                    string val = row.Substring(row.IndexOf("=")+1).Trim();
                    dic.Add(key, val);
                }
                //向当前对象的相关属性赋值
                foreach(KeyValuePair<string, string> kvp in dic)
                {
                    if (kvp.Key.Equals("ACCESS_KEY", StringComparison.CurrentCultureIgnoreCase))
                        this.ACCESS_KEY = kvp.Value;
                    if (kvp.Key.Equals("SECRET_KEY", StringComparison.CurrentCultureIgnoreCase))
                        this.SECRET_KEY = kvp.Value;
                    if (kvp.Key.Equals("HUB_NAME", StringComparison.CurrentCultureIgnoreCase))
                        this.HUB_NAME = kvp.Value;
                }
            }
        }
    }
}
