/*
 * Created by Ranorex
 * User: astan
 * Date: 20.01.2018
 * Time: 22:04
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

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace Studio2017
{
    /// <summary>
    /// Description of ProjectCreation.
    /// </summary>
    [TestModule("E3FFFCDD-5644-41D5-96F8-4D05D19565A0", ModuleType.UserCode, 1)]
    public class ProjectCreationUtility
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public ProjectCreationUtility()
        {
            // Do not delete - a parameterless constructor is required!
        }

        /// <summary>
        /// Performs the playback of actions in this module.
        /// </summary>
        /// <remarks>You should not call this method directly, instead pass the module
        /// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
        /// that will in turn invoke this method.</remarks>
		
        private static Studio2017Repository repo = Studio2017Repository.Instance;
        
        public void goToNewProjectWizard() {
        
        	repo.StudioWindowForm.ApplicationMenuButton.Click();
        	repo.StudioWindowForm.IGNew.Click();
        	repo.StudioWindowForm.IGNewProject.Click();
        }
        
        public void saveTargetAs(string pathToSaveFile) {
        	repo.StudioWindowForm.ApplicationMenuButton.Click();
        	repo.StudioWindowForm.IGSaveTargetAs.Click();
        	repo.SaveTargetAs.PathToSaveFileAsTarget.TextValue = pathToSaveFile;
        	repo.SaveTargetAs.ButtonSave.Click();
        }
        
        
        public void createProject(string projectName, bool publishGS, string GSServer, string serverLocation, string filesFolderPath, string tmtbLocation, string tmName, string termbaseName, string sourceLanguage, string targetLanguage) {
        	pressNext();
        	addProjectName(projectName);
        	publishOnGroupShare(publishGS, GSServer, serverLocation);
        	pressNext();
        	
        	//Add source and target languages
        	addSourceLanguage(sourceLanguage);
        	addTargetLanguage(targetLanguage);
        	pressNext();
        	
        	//Handle Planning and Assignments page
        	if (publishGS == true) {
        	pressNext();
        	}
        	
        	//Add files
        	addFilesFolder(filesFolderPath);
        	pressNext();
        	
        	//Add TM
        	addTM(tmtbLocation, tmName);
        	pressNext();
        	
        	//Add termbases
        	addTermbase(tmtbLocation, termbaseName);
        	pressNext();
        	
        	//PerfectMatch page
        	pressNext();
        	
        	//Batch task page
        	pressNext();
        	
        	//Batch processing settings
        	pressNext();
        	
        	//Project Summary
        	pressFinish();
        	
        	//Close Project
        	pressFinish();
        }
        
        
        public void pressNext() {
        	repo.ProjectWizard.NextButtonInfo.WaitForExists(10000);
        	repo.ProjectWizard.NextButtonInfo.WaitForAttributeEqual(10000, "enabled", true);
        	repo.ProjectWizard.NextButton.Click();
        }
        
        public void pressFinish() {
        	repo.ProjectWizard.FinishProjectInfo.WaitForExists(10000);
        	repo.ProjectWizard.FinishProjectInfo.WaitForAttributeEqual(20000, "enabled", true);
        	repo.ProjectWizard.FinishProject.Click();
        }
        
        public void addProjectTemplate(){
            //to be developed
        }
        
        public void addProjectName(string projectName) {
        	repo.ProjectWizard.ProjectName.TextValue = projectName;
        }
        
        public void addSourceLanguage(string sourceLanguage) {
            repo.ProjectWizard.SourceLanguages.Click();
        	repo.SDLTradosStudio.LanguagesInfo.Path = "//contextmenu[@processname='SDLTradosStudio']//container[@automationid='grid']//container[@automationid='ItemsPresenter']//text[@caption='" + sourceLanguage + "']";
        	repo.SDLTradosStudio.Languages.Click();
        }
        
        public void addTargetLanguage(string targetLanguage) {
            repo.ProjectWizard.RemoveAllLanguages.Click();
        	repo.ProjectWizard.TargetLanguagesInfo.Path = "//form[@automationid='Window_1']//container[@automationid='DockPanel_1']//container[@controlname='_groupBoxTargets']//element[@controlname='_flagsListBoxAvailableHost']//list[@automationid='ListBox']//container[@automationid='ItemsPresenter']//text[@caption='" + targetLanguage + "']"; 
        	repo.ProjectWizard.TargetLanguages.Click();
        	repo.ProjectWizard.AddButtonTargetLanguages.Click();
        }
        
        public void addFilesFolder(string folderPath) {
            repo.ProjectWizard.AddFolder.Click();
        	repo.AddFolder.AddFolderName.TextValue = @"C:\Users\astan\Desktop\TranslatableFiles";
        	repo.AddFolder.SelectFolder.Click();
        	repo.JobProgressDialog.AddingFilesProgressInfo.WaitForAttributeEqual(10000, "visible", false);
        	repo.ProjectWizard.NextButtonInfo.WaitForAttributeEqual(2000, "enabled", true);   
        }
        
        public void addTM(string tmtbLocation, string tmName) {
            repo.ProjectWizard.Use.Click();
        	repo.SDLTradosStudio.FileBasedTMTB.Click();
        	repo.OpenFileBasedTMTB.AddTMTBName.TextValue = tmtbLocation + tmName;
        	repo.OpenFileBasedTMTB.OpenTMTB.Click();
        	repo.ProjectWizard.DisplayedTMsInfo.Path = "//form[@automationid='Window_1']//container[@automationid='DockPanel_1']//container[@controlname='SettingsUIControl']//container[@controlname='_groupBox']//container[@controlname='_translationMemoriesControl']//text[@automationid='[Editor] Edit Area' and @uiautomationvaluevalue~'" + tmName + "']";
        	repo.ProjectWizard.DisplayedTMsInfo.WaitForExists(10000);
        }
        
        public void addServerBasedTM(string tmName) {
        
        //to be completed
        
        }
        
        public void addTermbase(string tmtbLocation, string termbaseName) {
            repo.ProjectWizard.Use.Click();
        	repo.SDLTradosStudio.FileBasedTMTB.Click();
        	repo.OpenFileBasedTMTB.AddTMTBName.TextValue = tmtbLocation + termbaseName;
        	repo.OpenFileBasedTMTB.OpenTMTB.Click();
        	repo.ProjectWizard.DisplayedTermbasesInfo.Path = "//form[@automationid='Window_1']//container[@automationid='DockPanel_1']//container[@controlname='ProjectTermbasesWizardPageControl']//container[@controlname='_termbasesGrid']//text[@automationid='[Editor] Edit Area' and @uiautomationvaluevalue~'" + termbaseName + "']";
        	repo.ProjectWizard.DisplayedTermbasesInfo.WaitForExists(10000);
        }
        
        public void publishOnGroupShare(bool publishOnGS, string gsServer, string serverLocation) {
        	repo.ProjectWizard.PublishGroupShareInfo.WaitForExists(5000);
        	if (publishOnGS) {
        		if (repo.ProjectWizard.PublishGroupShare.Element.GetAttributeValue("checked").Equals(true)) {
        			addServerAndLocation(gsServer, serverLocation);
        		}
        		else {
        			repo.ProjectWizard.PublishGroupShare.Click();
					addServerAndLocation(gsServer, serverLocation);
        		}
        	}
        	else {
        		if (repo.ProjectWizard.PublishGroupShare.Element.GetAttributeValue("checked").Equals(true)) {
        			repo.ProjectWizard.PublishGroupShare.Click();
        		}
        		else {
        		//Publish on GS server checkbox is disabled
        		}
        	}
        }
        
        public void addServerAndLocation(string gsServer, string serverLocation) {
        	repo.ServerList.ServerSelectionInfo.Path = "//list[@controlid='1000']//listitem[@text~'" + gsServer + "']";
        	repo.BrowserDialog.ServerLocationInfo.Path = "//form[@controlname='BrowserDialog']//treeitem[@automationid='0']//text[@automationid='0']//text[@uiautomationvaluevalue='" + serverLocation + "']";
            repo.ProjectWizard.ServerCombobox.Click();
            repo.ServerList.ServerSelection.Click();
        	repo.ProjectWizard.BrowseServerLocation.Click();
        	repo.BrowserDialog.ServerLocation.Click();
        	repo.BrowserDialog.OkButton.Click();
        }
    }
}
