using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using ConvertModel;

namespace ConvertLibrary
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
        /// <param name="isShowLines">是否显示换行符</param>
        /// <returns>处理后字符串</returns>
        public static string DelTags(string sourceStr)
        {
            return DelHtmlTags(sourceStr);
        }


        /// <summary>
        /// 删除HTML标签
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <returns>处理后字符串</returns>
        private static string DelHtmlTags(string sourceStr)
        {
            string newStr = Regex.Replace(sourceStr, "<[^>]+>", "");
            newStr = Regex.Replace(newStr, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            newStr = Regex.Replace(newStr, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            newStr = Regex.Replace(newStr, @"-->", "", RegexOptions.IgnoreCase);
            newStr = Regex.Replace(newStr, @"<!--.*", "", RegexOptions.IgnoreCase);
            newStr = Regex.Replace(newStr, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            newStr = Regex.Replace(newStr, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            newStr = Regex.Replace(newStr, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            newStr = Regex.Replace(newStr, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            newStr = Regex.Replace(newStr, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            newStr = Regex.Replace(newStr, @"&ldquo;", "\"", RegexOptions.IgnoreCase);
            newStr = Regex.Replace(newStr, @"&rdquo;", "\"", RegexOptions.IgnoreCase);
            newStr = Regex.Replace(newStr, @"&nbsp;", " ", RegexOptions.IgnoreCase);
            newStr = Regex.Replace(newStr, @"&mdash;", "-", RegexOptions.IgnoreCase);
            return newStr;
        }

        public static string DelExcelTags(string sourceStr)
        {
            string newStr = Regex.Replace(sourceStr, " ", "");
            newStr = Regex.Replace(newStr, @"([\n])+", "", RegexOptions.IgnoreCase);
            return newStr;
        }

        public static string GenerateNoByLineBreak(string sourceStr)
        {
            string strResult = string.Empty;
            string[] strList = sourceStr.Split(new[] { "<li>" }, StringSplitOptions.None);
            int iFlag = 1;
            foreach (string s in strList)
            {
                if (string.IsNullOrWhiteSpace(DelHtmlTags(s)))
                {
                    continue;
                }
                strResult += $"{iFlag}、{DelHtmlTags(s)}{Environment.NewLine}";
                iFlag++;
            }

            return strResult.TrimEnd((char[])"\n\r".ToCharArray());
        }

        /// <summary>
        /// 测试用例优先级类型转换
        /// </summary>
        /// <param name="innerText">优先级</param>
        /// <returns>ImportanceType</returns>
        public static ImportanceType StrToImportanceType(string innerText)
        {
            if (string.IsNullOrEmpty(innerText))
            {
                return ImportanceType.高;
            }
            if (Regex.IsMatch(innerText, @"^[+-]?\d*[.]?\d*$"))
            {
                return (ImportanceType)int.Parse(innerText);
            }
            else
            {
                switch (innerText.ToLower())
                {
                    case "高":
                    case "high":
                        return ImportanceType.高;
                    case "中":
                    case "medium":
                        return ImportanceType.中;
                    case "低":
                    case "low":
                        return ImportanceType.低;
                    default:
                        return ImportanceType.高;
                }
            }
        }

        /// <summary>
        /// 转换为执行方式枚举
        /// </summary>
        /// <param name="innerText">执行方式</param>
        /// <returns>ExecType</returns>
        public static ExecType StrToExecType(string innerText)
        {
            if (string.IsNullOrEmpty(innerText))
            {
                return ExecType.手动;
            }
            if (Regex.IsMatch(innerText, @"^[+-]?\d*[.]?\d*$"))
            {
                return (ExecType)int.Parse(innerText);
            }
            else
            {
                switch (innerText)
                {
                    case "手动":
                        return ExecType.手动;
                    case "自动":
                        return ExecType.自动;
                    default:
                        return ExecType.手动;
                }
            }
        }
    }
}
