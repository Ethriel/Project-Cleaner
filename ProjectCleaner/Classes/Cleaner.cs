using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace ProjectCleaner.Classes
{
    class Cleaner
    {
        string PathToFolder;
        List<DirectoryInfo> Directories;
        List<string> RootFolderContents;
        List<string> InnerFolderContents;
        List<string> PreparedInnerFolders;
        string[] RootFoldersPatterns;
        string[] InnerFoldersPaterns;
        bool ClearVS;
        bool ClearRelease;

        public Cleaner()
        {
            Directories = new List<DirectoryInfo>();
            RootFolderContents = new List<string>();
            InnerFolderContents = new List<string>();
            PreparedInnerFolders = new List<string>();
        }
        public void SetParameters(string pathToFolder, bool clearVS, bool clearRelease)
        {
            if(clearVS)
            {
                Process[] PrName = Process.GetProcessesByName("devenv");
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

        void Work()
        {
            if(!Directory.Exists(PathToFolder))
            {
                throw new Exception("Current folder does not exist!");
            }
            DirectoryInfo DirInfo = new DirectoryInfo(PathToFolder);
            Directories = DirInfo.GetDirectories().ToList();
            FillPatterns();
            FillFolders();
            CleanProjectRootFolder();
            CleanProjectsFolders();
        }
        void CleanProjectRootFolder()
        {
            for (int i = 0; i < RootFolderContents.Count; i++)
            {
                if (Directory.Exists(RootFolderContents[i]))
                    new DirectoryInfo(RootFolderContents[i]).Delete(true);
            }
            GC.Collect();
        }

        void CleanProjectsFolders()
        {
            for (int i = 0; i < InnerFolderContents.Count; i++)
            {
                if (Directory.Exists(InnerFolderContents[i]))
                    new DirectoryInfo(InnerFolderContents[i]).Delete(true);
            }
            GC.Collect();
        }
        private void FillRootFolder()
        {
            for (int i = 0; i < Directories.Count; i++)
            {
                for (int j = 0; j < RootFoldersPatterns.Length; j++)
                {
                    RootFolderContents.Add(Directories[i].FullName + RootFoldersPatterns[j]);
                }
            }
        }

        private void FillInnerFolders()
        {
            for (int i = 0; i < PreparedInnerFolders.Count; i++)
            {
                for (int j = 0; j < InnerFoldersPaterns.Length; j++)
                {
                    InnerFolderContents.Add(PreparedInnerFolders[i] + InnerFoldersPaterns[j]);
                }
            }
        }

        private void FillFolders()
        {
           FillRootFolder();
           FillInnerFolders();
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
            else if(ClearVS && ClearRelease)
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
            PrepareInnerFolders();
            if (ClearRelease)
            {
                InnerFoldersPaterns = new string[] { "\\bin", "\\obj", "\\x32", "\\x64" };
            }
            else
            {
                InnerFoldersPaterns = new string[] { "\\bin\\Debug", "\\obj", "\\x32\\Debug", "\\x64\\Debug" };
            }
        }

        private void FillPatterns()
        {
            FillRootPatt();
            FillInnerPatt();
        }

        private void PrepareInnerFolders()
        {
            for (int i = 0; i < Directories.Count; i++)
            {
                PreparedInnerFolders.Add(Directories[i].FullName + "\\" + Directories[i].Name);
                PreparedInnerFolders.Add(Directories[i].FullName + "\\" + Directories[i].Name);
                PreparedInnerFolders.Add(Directories[i].FullName + "\\" + Directories[i].Name);
                PreparedInnerFolders.Add(Directories[i].FullName + "\\" + Directories[i].Name);
                PreparedInnerFolders.Add(Directories[i].FullName + "\\" + Directories[i].Name);
            }
        }
    }
}
