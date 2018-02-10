/*
 * Created by Ranorex
 * User: astan
 * Date: 09.02.2018
 * Time: 10:33
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;
using System.Linq;
using System.IO;
using System.IO.Compression;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace Studio2017
{
    /// <summary>
    /// Description of CopyStudioBuild.
    /// </summary>
    [TestModule("BC48316B-4BF1-4786-AAFE-76F828C6EA3E", ModuleType.UserCode, 1)]
    public class InstallStudio : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public InstallStudio()
        {
            // Do not delete - a parameterless constructor is required!
        }
		private static Studio2017Repository repo = Studio2017Repository.Instance;
        
		string localBuildFolder = @"C:\Automation\Builds\";
		string buildsServer = @"\\sheffdevproj1.global.sdl.corp\BuildDrop\";
		string studioFolderPattern = "TranslationStudio.Master_15*";
		string studioPattern = "SDLTradosStudio*";
        
        void ITestModule.Run()
        {
            Mouse.DefaultMoveTime = 0;
            Keyboard.DefaultKeyPressTime = 0;
            Mouse.DefaultClickTime = 0;
            Delay.SpeedFactor = 0.0;
            
            var studioDirectories = System.IO.Directory.GetDirectories(@"\\sheffdevproj1.global.sdl.corp\BuildDrop", "TranslationStudio.Master_15");
            
            var directory = new System.IO.DirectoryInfo(buildsServer).GetDirectories(studioFolderPattern).OrderBy(folder => folder.LastWriteTime).ToList();
            int lastBuild = directory.Count;
            string buildFolder = directory[lastBuild-1].FullName;
            
            var studioExecutable = new System.IO.DirectoryInfo(@buildFolder).GetFiles(studioPattern);
            
            var sourceFolder = @buildFolder + @"\" + studioExecutable[0];
            var localDestinationFolder = @localBuildFolder+studioExecutable[0];
            
            var localStudioExecutable = new System.IO.DirectoryInfo(localBuildFolder).GetFiles(studioPattern);
            
            File.Copy(sourceFolder, localDestinationFolder, true);
            
            Host.Local.RunApplication(localBuildFolder + "SDLTradosStudio2018_14666.exe");
            
            repo.StudioInstallation.Accept.Click();
            
            repo.StudioInstallation.BackPanel.CheckBoxAcceptInfo.WaitForExists(60000);
            repo.StudioInstallation.BackPanel.CheckBoxAcceptInfo.WaitForAttributeEqual(5000, "enabled", true);
            repo.StudioInstallation.BackPanel.CheckBoxAccept.Click();
            
            repo.StudioInstallation.BackPanel.ButtonNextInfo.WaitForAttributeEqual(5000, "enabled", true);
            repo.StudioInstallation.BackPanel.ButtonNext.Click();
			
            
            repo.StudioInstallation.SetupCompletedInfo.WaitForExists(12000000);
            
            repo.StudioInstallation.ButtonOK.Click();
            
        }
    }
}
