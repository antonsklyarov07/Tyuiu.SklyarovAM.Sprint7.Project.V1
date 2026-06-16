using System;
using System.Windows.Forms;

namespace Tyuiu.SklyarovAM.Sprint7.V1
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm_SAM());
        }
    }
}