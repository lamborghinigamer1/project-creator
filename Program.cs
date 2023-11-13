namespace ProjectCreator
{
    class Program
    {
        static void Main()
        {
            OperatingSystem userOS = Environment.OSVersion;

            var log = new Logger("Program");
            log.Info("Starting Project creator");

            try
            {
                if (userOS != null)
                {
                    log.Debug($"Operating System Information: {userOS}");
                    var newc = new NewC()
                    {
                        ProjectName = "Juan",
                        OS = userOS
                    };
                    newc.Initialize();
                }
                else
                {
                    log.Error("Could not determine operating system");
                }
            }
            catch (Exception e)
            {
                log.Error($"{e}");
            }
        }
    }
}