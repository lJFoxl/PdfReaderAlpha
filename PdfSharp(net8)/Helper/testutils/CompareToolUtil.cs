using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace PdfSharp_net8_.Helper.testutils
{
    public class CompareToolUtil
    {
        private static int tempFileCounter = 0;
        private const int stdOutputIndex = 0;
        private const int ErrOutputIndex = 1;
        private const string splitRegex = "((\".+?\"|[^'\\s]|'.+?')+)\\s*";
        
        public static string CreateTempCopy(string file, string tempFilePrefix, string tempFilePostfix) {
            string copiedFile = null;
            try {
                copiedFile = Path.Combine(Path.GetTempPath(), 
                    tempFilePrefix + Guid.NewGuid() + Interlocked.Increment(ref tempFileCounter) + tempFilePostfix);
                Copy(file, copiedFile);
            } catch (IOException e) {
                RemoveFiles(new object[] {copiedFile});
                throw e;
            }
            return copiedFile;
        }
        
        public static void Copy(string fileToCopy, string copiedFile) {
            File.Copy(fileToCopy, copiedFile, true);
        }
        
        public static string CreateTempDirectory(string tempFilePrefix) {
            string temporaryDirectory = null;
            try {
                temporaryDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + tempFilePrefix + 
                                                                      Interlocked.Increment(ref tempFileCounter));
                Directory.CreateDirectory(temporaryDirectory);
            } catch (IOException e) {
                RemoveFiles(new object[] {temporaryDirectory});
                throw e;    
            }
            return temporaryDirectory;
        }
        
        public static bool RemoveFiles(object[] paths) {
            bool allFilesAreRemoved = true;
            foreach (string path in paths) {
                try {
                    if (null != path) {
                        File.Delete(path);
                    }
                } catch (Exception e) {
                    if (Directory.Exists(path)) {
                        try {   
                            Directory.Delete(path);
                        }
                        catch (Exception exc) {
                            allFilesAreRemoved = false;
                        }
                    }
                    else {
                        allFilesAreRemoved = false;
                    }
                }
            }
            return allFilesAreRemoved;
        }
          internal static void SetProcessStartInfo(Process proc, string exec, string @params, string workingDir) {
            string[] processArguments = PrepareProcessArguments(exec, @params);
            proc.StartInfo = new ProcessStartInfo(processArguments[0], processArguments[1]);
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WorkingDirectory = workingDir;
        }
        
        internal static string[] PrepareProcessArguments(string exec, string @params) {
            bool isExcitingFile;
            try
            {   
                if (exec.EndsWith("compare"))
                {
                    @params = "compare " + @params;
                    exec = exec.Replace("compare", "");
                }

                isExcitingFile = new FileInfo(exec).Exists;
            }
            catch (Exception)
            {
                isExcitingFile = false;
            }

            return isExcitingFile
                ? new string[] {exec, @params.Replace("'", "\"")}
                : SplitIntoProcessArguments(exec, @params);
        }
        
        internal static string[] SplitIntoProcessArguments(string command, string @params) {
            Regex regex = new Regex(splitRegex);
            MatchCollection matches = regex.Matches(command);
            string processCommand = "";
            string processArguments = "";
            if (matches.Count > 0)
            {
                processCommand = matches[0].Value.Trim();
                for (int i = 1; i < matches.Count; i++)
                {
                    Match match = matches[i];
                    processArguments += match.Value;
                }

                processArguments = processArguments + " " + @params;
                processArguments = processArguments.Replace("'", "\"").Trim();
            }

            return new string[] {processCommand, processArguments};
        }
        
        internal static string GetProcessOutput(Process p) {
            StringBuilder[] builders = GetProcessOutputBuilders(p);

            return builders[stdOutputIndex].ToString() 
                   + '\n' 
                   + builders[ErrOutputIndex].ToString();
        }
        
        internal static StringBuilder[] GetProcessOutputBuilders(Process p) {
            StringBuilder bri = new StringBuilder();
            StringBuilder bre = new StringBuilder();
            do {
                bri.Append(p.StandardOutput.ReadToEnd());
                bre.Append(p.StandardError.ReadToEnd());
            } while (!p.HasExited);
                
            Console.Out.WriteLine(bre.ToString());

            StringBuilder[] resultOutputArray = new StringBuilder[] { bri, bre };
            return resultOutputArray;
        }
    }
}