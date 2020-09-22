using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace ProjectCleaner.Classes
{
    class Cleaner
    {
        private string PathToFolder;
        private ICollection<string> rootFolderContents;
        private ICollection<string> innerFolderContents;
        private IEnumerable<string> RootFoldersPatterns;
        private IEnumerable<string> InnerFoldersPaterns;
        private bool ClearVS;
        private bool ClearRelease;
        private DirectoryInfo projectsInfo;

        public Cleaner()
        {
            rootFolderContents = new List<string>();
            innerFolderContents = new List<string>();
        }
        public void SetParameters(string pathToFolder, bool clearVS, bool clearRelease)
        {
            if (clearVS)
            {
                var PrName = Process.GetProcessesByName("devenv");
                if (PrName.Length > 0)
                {
                    throw new Exception("You want to clear \".vs\" folder. Close Visual Studio first!");
                }
            }
            PathToFolder = pathToFolder;
            ClearVS = clearVS;
            ClearRelease = clearRelease;
            Work();
        }

        private void FindRepos(DirectoryInfo dirInfo)
        {
            var directories = dirInfo.GetDirectories();

            var reposName = default(string);

            if (directories.Length != 0)
            {
                var repos = directories.FirstOrDefault(x => x.Name.Equals("repos"));

                if (repos == null)
                {
                    foreach (var dir in directories)
                    {
                        FindRepos(dir);
                    }
                }
                else
                {
                    reposName = repos.FullName;
                    projectsInfo = new DirectoryInfo(reposName);
                }
            }
        }

        private void FindSource(DirectoryInfo dirInfo)
        {
            var directories = dirInfo.GetDirectories();

            if (directories.Any(x => x.Name.Equals("repos")))
            {
                FindRepos(dirInfo);
                return;
            }

            var sourceName = default(string);

            if (directories.Length != 0)
            {
                var source = directories.FirstOrDefault(x => x.Name.Equals("source"));

                if (source == null)
                {
                    foreach (var dir in directories)
                    {
                        FindSource(dir);
                    }
                }
                else
                {
                    sourceName = source.FullName;
                    FindRepos(source);
                }
            }
        }

        private void DeleteJunkFiles()
        {
            var directories = projectsInfo.GetDirectories();

            var path = default(string);

            var dirInfo = default(DirectoryInfo);

            foreach (var dir in directories)
            {
                foreach (var rootPatt in RootFoldersPatterns)
                {
                    path = string.Concat(dir.FullName, rootPatt);
                    if (Directory.Exists(path))
                    {
                        dirInfo = new DirectoryInfo(path);
                        dirInfo.Delete(true);
                    }
                }

                foreach (var innerPatt in InnerFoldersPaterns)
                {
                    path = string.Concat(dir.FullName, innerPatt);
                    if (Directory.Exists(path))
                    {
                        dirInfo = new DirectoryInfo(path);
                        dirInfo.Delete(true);
                    }
                }
            }
            GC.Collect();
        }

        private void Work()
        {
            if (!Directory.Exists(PathToFolder))
            {
                throw new Exception("Current folder does not exist!");
            }

            var dirInfo = new DirectoryInfo(PathToFolder);

            FillPatterns();

            FindSource(dirInfo);

            if (projectsInfo == null)
            {
                projectsInfo = new DirectoryInfo(PathToFolder);
            }

            DeleteJunkFiles();
        }

        private void FillPatterns()
        {
            FillRootPatt();
            FillInnerPatt();
        }

        private void FillRootPatt()
        {
            if (ClearVS)
            {
                RootFoldersPatterns = new string[] { "\\x32\\Debug", "\\x64\\Debug", "\\.vs" };
            }
            else if (ClearRelease)
            {
                RootFoldersPatterns = new string[] { "\\x32", "\\x64", "\\bin" };
            }
            else if (ClearVS && ClearRelease)
            {
                RootFoldersPatterns = new string[] { "\\x32", "\\x64", "\\.vs", "\\bin" };
            }
            else
            {
                RootFoldersPatterns = new string[] { "\\x32\\Debug", "\\x64\\Debug", "\\bin\\Debug" };
            }
        }

        private void FillInnerPatt()
        {
            if (ClearRelease)
            {
                InnerFoldersPaterns = new string[] { "\\bin", "\\obj", "\\x32", "\\x64" };
            }
            else
            {
                InnerFoldersPaterns = new string[] { "\\bin\\Debug", "\\obj", "\\x32\\Debug", "\\x64\\Debug" };
            }
        }
    }
}
