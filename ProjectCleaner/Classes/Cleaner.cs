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
        public void SetParameters(string pathToFolder)
        {
            Process[] PrName = Process.GetProcessesByName("devenv");
            if(PrName.Length > 0)
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
            List<DirectoryInfo> Directories = DirInfo.GetDirectories().ToList();
            CleanProjectRootFolder(Directories);
            CleanProjectsFolders(Directories);
        }
        void CleanProjectRootFolder(List<DirectoryInfo> Dirs)
        {
            string PathToVs = "";
            string PathToDebug = "";
            string PathToX32 = "";
            string PathToX64 = "";
            for (int i = 0; i < Dirs.Count; i++)
            {
                PathToVs = Dirs[i].FullName + "\\" + ".vs";
                PathToDebug = Dirs[i].FullName + "\\" + "Debug";
                PathToX32 = Dirs[i].FullName + "\\" + "x32";
                PathToX64 = Dirs[i].FullName + "\\" + "x64";

                if (Directory.Exists(PathToVs))
                {
                    new DirectoryInfo(PathToVs).Delete(true);
                }

                if(Directory.Exists(PathToDebug))
                {
                    new DirectoryInfo(PathToDebug).Delete(true);
                }

                if (Directory.Exists(PathToX32))
                {
                    new DirectoryInfo(PathToX32).Delete(true);
                }

                if (Directory.Exists(PathToX64))
                {
                    new DirectoryInfo(PathToX64).Delete(true);
                }
            }
            GC.Collect();
        }

        void CleanProjectsFolders(List<DirectoryInfo> Dirs)
        {
            string PathToInnerDebug = "";
            string PathToInnderBin = "";
            string PathToInnerObj = "";
            string PathToInnerX32 = "";
            string PathToInnerX64 = "";
            for (int i = 0; i < Dirs.Count; i++)
            {
                PathToInnerDebug = Dirs[i].FullName + "\\" + Dirs[i].Name + "\\" + "Debug";
                PathToInnderBin = Dirs[i].FullName + "\\" + Dirs[i].Name + "\\" + "bin";
                PathToInnerObj = Dirs[i].FullName + "\\" + Dirs[i].Name + "\\" + "obj";
                PathToInnerX32 = Dirs[i].FullName + "\\" + Dirs[i].Name + "\\" + "x32";
                PathToInnerX64 = Dirs[i].FullName + "\\" + Dirs[i].Name + "\\" + "x64";
                if (Directory.Exists(PathToInnerDebug))
                {
                    new DirectoryInfo(PathToInnerDebug).Delete(true);
                }

                if(Directory.Exists(PathToInnderBin))
                {
                    new DirectoryInfo(PathToInnderBin).Delete(true);
                }

                if (Directory.Exists(PathToInnerObj))
                {
                    new DirectoryInfo(PathToInnerObj).Delete(true);
                }

                if (Directory.Exists(PathToInnerX32))
                {
                    new DirectoryInfo(PathToInnerX32).Delete(true);
                }

                if (Directory.Exists(PathToInnerX64))
                {
                    new DirectoryInfo(PathToInnerX64).Delete(true);
                }
            }
            GC.Collect();
        }
    }
}
