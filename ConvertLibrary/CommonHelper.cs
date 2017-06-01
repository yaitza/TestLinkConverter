using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferLibrary
{
    public static class CommonHelper
    {
        /// <summary>
        /// 杀掉Excel进程
        /// </summary>
        public static void KillExcelProcess()
        {
            Process[] procs = Process.GetProcessesByName("Excel");
            foreach (Process pro in procs)
            {
                pro.Kill();
            }
        }

        /// <summary>
        /// 删除HTML标签以及删除字符串换行符
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <returns>处理后字符串</returns>
        public static string DelTags(string sourceStr)
        {
            string newStr = CommonHelper.DelHtmlTags(sourceStr);
            return CommonHelper.DelLinsTags(newStr);
        }


        /// <summary>
        /// 删除HTML标签
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <returns>处理后字符串</returns>
        private static string DelHtmlTags(string sourceStr)
        {
            string newStr = System.Text.RegularExpressions.Regex.Replace(sourceStr, "<[^>]+>", "");
            return newStr;
        }

        /// <summary>
        /// 删除字符串中换行符
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <returns>处理后字符串</returns>
        private static string DelLinsTags(string sourceStr)
        {
            string newStr = sourceStr.Replace("\n", "").Replace("\r", "").Replace("\t","");
            return newStr;
        }
    }
}
