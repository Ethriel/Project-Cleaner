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
        public Cleaner()
        {
            Directories = new List<DirectoryInfo>();
            RootFolderContents = new List<string>();
            InnerFolderContents = new List<string>();
        }
        public void SetParameters(string pathToFolder)
        {
            Process[] PrName = Process.GetProcessesByName("devenv");
            if (PrName.Length > 0)
            {
                throw new Exception("Close Visual Studio first!");
            }
            PathToFolder = pathToFolder;
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
                RootFolderContents.Add(Directories[i].FullName + "\\" + "Debug");
                RootFolderContents.Add(Directories[i].FullName + "\\" + "x32");
                RootFolderContents.Add(Directories[i].FullName + "\\" + "x64");
            }
        }

        private void FillInnerFolders()
        {
            for (int i = 0; i < Directories.Count; i++)
            {
                InnerFolderContents.Add(Directories[i].FullName + "\\" + Directories[i].Name + "\\" + "Debug");
                InnerFolderContents.Add(Directories[i].FullName + "\\" + Directories[i].Name + "\\" + "bin");
                InnerFolderContents.Add(Directories[i].FullName + "\\" + Directories[i].Name + "\\" + "obj");
                InnerFolderContents.Add(Directories[i].FullName + "\\" + Directories[i].Name + "\\" + "x32");
                InnerFolderContents.Add(Directories[i].FullName + "\\" + Directories[i].Name + "\\" + "x64");
            }
        }

        private void FillFolders()
        {
           FillRootFolder();
           FillInnerFolders();
        }
    }
}
