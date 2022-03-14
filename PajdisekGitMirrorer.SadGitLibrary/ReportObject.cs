using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PajdisekGitMirrorer.SadGitLibrary
{
    public class ReportObject
    {
        public string Command { get; set; }
        public string NameOfRepo { get; set; }
        public int ExitCode { get; set; }
        public bool Success { get; set; }
        public string? CommandOutput { get; set; }
    }
}
