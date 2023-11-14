namespace ProjectCreator
{
    public class Selector
    {

        private readonly Logger log;

        private readonly OperatingSystem OS;

        public Selector(OperatingSystem OS)
        {
            log = new Logger("Selector");
            this.OS = OS;
            SelectProgrammingLanguage();
        }

        private string GetProjectName()
        {
            log.Info("Asking user for name of project");

            string? projectname = null;

            while (projectname == null || projectname == "")
            {
                Console.WriteLine("Please name your project:");
                projectname = Console.ReadLine();

            }
            return projectname;
        }

        private bool InitializeGitRepo()
        {
            log.Info("Asking user if they want to initialize git");


            string? YesOrNo = null;
            bool answer;

            while (true)
            {
                while (YesOrNo == null)
                {
                    Console.WriteLine("Would you like to initilize a git repo? (y/n) default: y");
                    YesOrNo = Console.ReadLine();
                }

                switch (YesOrNo.ToLower())
                {
                    case "":
                    case "yes":
                    case "y":
                        answer = true;
                        break;
                    case "no":
                    case "n":
                        answer = false;
                        break;
                    default:
                        YesOrNo = null;
                        continue;
                }
                break;
            }
            log.Debug($"User wants to open code: {answer}");
            return answer;
        }

        private bool OpenCode()
        {
            log.Info("Asking user if they want to open vscode");


            string? YesOrNo = null;
            bool answer;

            while (true)
            {
                while (YesOrNo == null)
                {
                    Console.WriteLine("Would you like to open Visual Studio Code? (y/n) default: n");
                    YesOrNo = Console.ReadLine();
                }

                switch (YesOrNo.ToLower())
                {
                    case "yes":
                    case "y":
                        answer = true;
                        break;
                    case "no":
                    case "n":
                    case "":
                        answer = false;
                        break;
                    default:
                        YesOrNo = null;
                        continue;
                }
                break;
            }
            log.Debug($"User wants to open code: {answer}");
            return answer;
        }

        private string ListProgrammingLanguages()
        {
            log.Info("Showing all programming languages to user");
            string[] languages = { "Rust", "C#", "C", "HTML" };
            string[] alias = { "rs", "cs", "c", "html" };
            string result = "";

            for (int i = 0; i < languages.Length; i++)
            {
                result += $"{languages[i]}: {alias[i]}" + Environment.NewLine;
            }
            return result;
        }

        public void SelectProgrammingLanguage()
        {
            log.Info("Asking user what programming language the project should be");

            string? programminglanguage = null;

            while (programminglanguage == null || programminglanguage == "")
            {
                Console.WriteLine("What programming language would you like to use?");
                Console.WriteLine("Type \"list\" if you need a list of available languages");
                programminglanguage = Console.ReadLine();

                if (programminglanguage != null && programminglanguage.ToLower() == "list")
                {
                    Console.WriteLine();
                    Console.WriteLine(ListProgrammingLanguages());
                    programminglanguage = null;
                }
            }

            string projectname = GetProjectName();
            bool opencode = OpenCode();
            bool initializerepo = InitializeGitRepo();

            switch (programminglanguage.ToLower())
            {
                case "rs":
                case "rust":
                    var newrust = new NewRust()
                    {
                        ProjectName = projectname,
                        OS = OS,
                        OpenCode = opencode,
                        InitializeGitRepo = initializerepo,
                    };
                    newrust.Initialize();
                    break;
                case "c":
                    var newc = new NewC()
                    {
                        ProjectName = projectname,
                        OS = OS,
                        OpenCode = opencode,
                        InitializeGitRepo = initializerepo,
                    };
                    newc.Initialize();
                    break;
                case "cs":
                case "csharp":
                case "c#":
                    var newcsharp = new NewCSharp()
                    {
                        ProjectName = projectname,
                        OS = OS,
                        OpenCode = opencode,
                        InitializeGitRepo = initializerepo,
                    };
                    newcsharp.Initialize();
                    break;
                case "html":
                    var newhtml = new NewHTML()
                    {
                        ProjectName = projectname,
                        OS = OS,
                        OpenCode = opencode,
                        InitializeGitRepo = initializerepo,
                    };
                    newhtml.Initialize();
                    break;
                default:
                    Console.WriteLine("Sorry that programming language is not implemented yet");
                    log.Error("Programming language not found");
                    break;
            }
        }
    }
}