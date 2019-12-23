using GoogleAnalyticsTracker.Simple;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core.Interface;
using System.Net.NetworkInformation;
using System.Xml;

namespace ConvertLibrary
{
    public static class GoogleAnalyticsTracker
    {
        public static async Task Tracker(string pageTitle, string pageUrl)
        {
            SimpleTrackerEnvironment ste = new SimpleTrackerEnvironment(Environment.OSVersion.ToString(), Environment.OSVersion.VersionString, "");
            using (SimpleTracker tracker = new SimpleTracker("UA-97814311-2", ste))
            {
                await tracker.TrackPageViewAsync(pageTitle, pageUrl, new Dictionary<int, string>());
            }

        }
    }

    public class AnalyticsSession : IAnalyticsSession
    {
        [Obsolete]
        public string GenerateCacheBuster()
        {
            string uuid = ConfigurationSettings.AppSettings["uuid"];
            if (string.IsNullOrWhiteSpace(uuid))
            {
                SaveConfig("uuid", uuid);
            }
            return uuid;
        }

        [Obsolete]
        public string GenerateSessionId()
        {
            string uuid = ConfigurationSettings.AppSettings["uuid"];
            if (string.IsNullOrWhiteSpace(uuid))
            {
                SaveConfig("uuid", uuid);
            }
            return uuid;
        }

        private void SaveConfig(string key, string value)
        {
            XmlDocument doc = new XmlDocument();
            //获得配置文件的全路径
            string strFileName = AppDomain.CurrentDomain.BaseDirectory.ToString() + "TestLinkConverter.exe.config";
            doc.Load(strFileName);
            //找出名称为“add”的所有元素
            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性
                XmlAttribute att = nodes[i].Attributes["key"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素
                if (att.Value == key)
                {
                    //对目标元素中的第二个属性赋值
                    att = nodes[i].Attributes["value"];
                    att.Value = value;
                    break;
                }
            }
            //保存上面的修改
            doc.Save(strFileName);
        }
    }
}
