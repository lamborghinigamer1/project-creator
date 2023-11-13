namespace ProjectCreator
{
    public class Logger
    {
        private readonly string classname;

        public Logger(string classname)
        {
            this.classname = classname;
        }

        private static void LogToFile(string message)
        {
            string filepath = "./projectcreator.log";
            try
            {
                File.AppendAllText(filepath, message + Environment.NewLine);
            }
            catch (IOException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static string LogWithTime()
        {
            DateTime currentTime = DateTime.Now;
            return currentTime.ToString();
        }

        public void Log(string level, string message)
        {
            string finalmessage = $"[{LogWithTime()} {classname} - {level}]: {message}";

            // Console.WriteLine(finalmessage);

            LogToFile(finalmessage);

        }

        public void Error(string message)
        {
            Log("ERROR", message);
        }

        public void Debug(string message)
        {
            Log("DEBUG", message);
        }

        public void Info(string message)
        {
            Log("INFO", message);
        }
        public void Warning(string message)
        {
            Log("WARNING", message);
        }
    }
}