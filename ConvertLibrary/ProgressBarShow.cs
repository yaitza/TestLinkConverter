using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertLibrary
{
    public static class ProgressBarShow
    {
        public delegate void SetProgressHandler(int ipos);

        public static SetProgressHandler SetProgressValue { get; set; }

        public static void ShowProgressValue(int ipos)
        {
            if (SetProgressValue != null)
            {
                SetProgressValue.Invoke(ipos);
            }
        }

    }
}
