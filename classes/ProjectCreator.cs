using System.Diagnostics;

namespace ProjectCreator
{
    public abstract class ProjectCreator
    {
        private readonly Logger log;

        public required string ProjectName
        {
            get; set;
        }

        public required OperatingSystem OS
        {
            get; set;
        }

        public ProjectCreator()
        {
            log = new Logger("ProjectCreator");
        }

        public void InitializeGit()
        {
            log.Info("Using bash to initialize git");
            var gitinit = new ProcessStartInfo
            {
                FileName = "git",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = Path.Combine(".", ProjectName),
                Arguments = "init"
            };

            Process? process = new()
            {
                StartInfo = gitinit
            };

            if (process != null)
            {
                process.Start();
                process.WaitForExit();
            }
        }

        public void StartCode()
        {
            string command = $"code {ProjectName}";

            Process? process = null;

            switch (OS.Platform)
            {
                case PlatformID.Unix:
                    log.Info("Starting vscode using bash");
                    var startcodeUnix = new ProcessStartInfo
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
                        StartInfo = startcodeUnix
                    };
                    break;
                case PlatformID.Win32NT:
                    log.Info("Starting vscode using cmd");
                    var startcodeWin = new ProcessStartInfo
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
                        StartInfo = startcodeWin
                    };
                    break;
            }

            if (process != null)
            {
                process.Start();
                process.WaitForExit();
            }
        }

        public abstract void CreateDirectory();
    }
}