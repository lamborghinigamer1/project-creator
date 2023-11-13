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
                    var selector = new Selector(userOS);
                    log.Info("Successfully created project");
                    Console.WriteLine("Successfully created project");
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