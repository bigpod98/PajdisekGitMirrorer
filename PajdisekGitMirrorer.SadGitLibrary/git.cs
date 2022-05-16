namespace PajdisekGitMirrorer.SadGitLibrary
{
    public class git
    {
        public static ReportObject cloneallbranches(string sourceRepo, string name)
        {
            ReportObject report = new ReportObject();
            try
            {
                System.IO.Directory.SetCurrentDirectory("/");
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "git";
                p.StartInfo.Arguments = $"clone --mirror {sourceRepo}";
                p.Start();
                p.WaitForExit();

                if (p.ExitCode == 0)
                {
                    report.Command = $"{p.StartInfo.FileName} {p.StartInfo.Arguments}";
                    report.ExitCode = p.ExitCode;
                    report.Success = true;
                    report.NameOfRepo = name;
                    report.CommandOutput = p.StandardOutput.ReadToEnd();
                }
                else
                {
                    report.Command = $"{p.StartInfo.FileName} {p.StartInfo.Arguments}";
                    report.ExitCode = p.ExitCode;
                    report.Success = false;
                    report.NameOfRepo = name;
                    report.CommandOutput = p.StandardOutput.ReadToEnd();
                }
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Console.WriteLine("install git you idiot");
                report.NameOfRepo = name;
                report.Success = false;
                throw;
            }

            return report;
        }

        public static ReportObject pushallbranches(string targetRepo, string name)
        {
            ReportObject report = new ReportObject();

            try
            {
                System.IO.Directory.SetCurrentDirectory($"/{name}.git");
                Console.WriteLine(targetRepo);
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "git";
                p.StartInfo.Arguments = $"push --mirror {targetRepo}";
                p.Start();
                p.WaitForExit();

                if (p.ExitCode == 0)
                {
                    report.Command = $"{p.StartInfo.FileName} {p.StartInfo.Arguments}";
                    report.ExitCode = p.ExitCode;
                    report.Success = true;
                    report.NameOfRepo = name;
                    report.CommandOutput = p.StandardOutput.ReadToEnd();
                }
                else
                {
                    report.Command = $"{p.StartInfo.FileName} {p.StartInfo.Arguments}";
                    report.ExitCode = p.ExitCode;
                    report.Success = false;
                    report.NameOfRepo = name;
                    report.CommandOutput = p.StandardOutput.ReadToEnd();
                }

            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Console.WriteLine("install git please");
                report.NameOfRepo = name;
                report.Success = false;
                throw;
            }

            return report;
        }
    }
}
