using System.Diagnostics;

namespace ProjectCreator
{
    public class NewHTML : ProjectCreator
    {
        private readonly Logger log;

        public NewHTML()
        {
            log = new Logger("NewHTML");
        }

        public void Initialize()
        {
            try
            {
                CreateDirectory();
                CreateIndex();
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

        private static string GenerateIndex()
        {
            return
            "<!DOCTYPE html>" + Environment.NewLine +
            "   <html lang=\"en\">" + Environment.NewLine +
            "   <head>" + Environment.NewLine +
            "    <meta charset=\"UTF-8\">" + Environment.NewLine +
            "    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">" + Environment.NewLine +
            "    <title>Document</title>" + Environment.NewLine +
            "</head>" + Environment.NewLine +
            "<body>" + Environment.NewLine +
            Environment.NewLine +
            "</body>" + Environment.NewLine +
            "</html>";
        }

        public void CreateIndex()
        {
            log.Info("Generating index.html file");
            string filepath = Path.Combine(".", ProjectName, "index.html");
            try
            {
                File.AppendAllText(filepath, GenerateIndex());
            }
            catch (IOException e)
            {
                log.Error($"{e}");
                throw;
            }
        }
    }
}