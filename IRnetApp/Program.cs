using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32.SafeHandles;
using FreeImageAPI;
using System.Diagnostics;

namespace neIR
{
    static class Program
    {
        /*[DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode, SetLastError = true)]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        static extern bool SetDllDirectory(string lpPathName);*/

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*int wsize = IntPtr.Size;
            string libdir = (wsize == 4) ? "x86" : "x64";*/
            string appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            string fiDll="FreeImage.dll";
            if (!File.Exists(appPath +"/"+ fiDll)) throw new System.DllNotFoundException(fiDll+" is missing");

            string oldPath = System.Environment.GetEnvironmentVariable("PATH");
            if (!oldPath.Contains(appPath))
                System.Environment.SetEnvironmentVariable("PATH", appPath + ";" + oldPath);
            //SetDllDirectory( appPath );
            Debug.WriteLine("Application path: " + appPath);
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());

            System.Environment.SetEnvironmentVariable("PATH", oldPath);
        }
    }
}
