using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace iExplore_Full_Screen
{
    class Program
    {
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public static void Main()
        {
            IntPtr h = Process.GetCurrentProcess().MainWindowHandle;
            ShowWindow(h, 0);
            Launch();
            Console.ReadLine();
        }

        private static void Launch()
        {
            string Sucursal = Environment.GetEnvironmentVariable("OFICINA");
            string Computer = Environment.GetEnvironmentVariable("COMPUTERNAME");

            string Path = Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            if (Path == null)
                Path = Environment.GetEnvironmentVariable("ProgramFiles");
                               
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = Path + "\\Google\\Chrome\\Application\\chrome.exe";
            psi.Arguments = "-kiosk --incognito http://ui-totem-sacatuturno-prod.prodopenshift.bancogalicia.com.ar/?sucursal=" + Sucursal + "&terminal=" + Computer;
           
            Process p = new Process();
            p.StartInfo = psi;
            p.EnableRaisingEvents = true;
            p.Exited += LaunchAgain; //C# 2.0 syntax - alternative: p.Exited += new EventHandler(LaunchAgain);

            p.Start();
        }

        private static void LaunchAgain(object o, EventArgs e)
        {
            Console.WriteLine("Iniciando BGTotem");
            Launch();
        }
    }
}