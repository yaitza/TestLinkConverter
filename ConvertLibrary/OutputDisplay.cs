using System.Drawing;
using System.Runtime.CompilerServices;

namespace ConvertLibrary
{
    public static class OutputDisplay
    {
        public delegate void ShowMessageHandler(string msg, Color color);

        public static ShowMessageHandler ShowMethod { get; set; }

        public static void ShowMessage(string msg, Color color)
        {
            if (ShowMethod != null)
            {
                ShowMethod.Invoke(msg, color);
            }
        }
    }
}