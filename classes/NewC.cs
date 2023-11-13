using System.Diagnostics;

namespace ProjectCreator
{
    public class NewC : ProjectCreator
    {
        private readonly Logger log;

        public NewC()
        {
            log = new Logger("NewC");
        }

        public void Initialize()
        {
            try
            {
                CreateDirectory();
                CreateMainC();
                InitializeGit();
                StartCode();
            }
            catch (Exception)
            {
                log.Error("ProjectName or OS is null or empty");
                throw;
            }
        }

        public override void CreateDirectory()
        {
            string command = $"mkdir \"{ProjectName}\"";

            Process? process = null;

            switch (OS.Platform)
            {
                case PlatformID.Unix:
                    log.Info("Using bash to create directory");
                    var mkdirUnix = new ProcessStartInfo
                    {
                        FileName = "/bin/bash",
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        Arguments = $"-c \"{command}\""
                    };

                    process = new Process()
                    {
                        StartInfo = mkdirUnix
                    };
                    break;
                case PlatformID.Win32NT:
                    log.Info("Using cmd to create directory");
                    var mkdirWindows = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        Arguments = $"/c {command}"
                    };

                    process = new Process()
                    {
                        StartInfo = mkdirWindows
                    };
                    break;
            }

            if (process != null)
            {
                process.Start();
                process.WaitForExit();
            }
        }

        private static string GenerateMainFunction()
        {
            return
                "#include <stdio.h>" + Environment.NewLine + Environment.NewLine +
                    "int main(int argc, char const *argv[])" + Environment.NewLine +
                   "{" + Environment.NewLine +
                   "    printf(\"Hello, World!\");" + Environment.NewLine +
                   "    return 0;" + Environment.NewLine +
                   "}";
        }
        public void CreateMainC()
        {
            log.Info("Generating main.c file");
            string filepath = Path.Combine(".", ProjectName, "main.c");
            try
            {
                File.AppendAllText(filepath, GenerateMainFunction());
            }
            catch (IOException e)
            {
                log.Error($"{e}");
                throw;
            }
        }
    }
}