using System.Diagnostics;

namespace ProjectCreator
{
    public class NewCSharp : ProjectCreator
    {
        private readonly Logger log;

        public NewCSharp()
        {
            log = new Logger("NewCSharp");
        }

        public void Initialize()
        {
            try
            {
                CreateDirectory();
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
            ProjectName = ProjectName.Replace(' ', '-');
            string command = $"dotnet new console -n \"{ProjectName}\"";

            Process? process = null;

            switch (OS.Platform)
            {
                case PlatformID.Unix:
                    log.Info("Using bash to create new project and directory");
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
                    log.Info("Using cmd to create new project and directory");
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
    }
}