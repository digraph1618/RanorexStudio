/*
 * Created by Ranorex
 * User: astan
 * Date: 19.01.2018
 * Time: 16:08
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
    /// Description of ActivateStudio.
    /// </summary>
    [TestModule("79E43D7F-E8C5-42AD-9057-7C27567FA48A", ModuleType.UserCode, 1)]
    public class UtilityMethods
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public UtilityMethods()
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
        
        
        public void studioActivation(string activationServer) {
        
        	repo.LicenseManagerForm.ButtonActivateButton.Click();
			repo.LicenseManagerForm.AlternativeActivationOptions.Click();
			repo.LicenseManagerForm.ButtonLicenseServerButton.Click();
			repo.LicenseManagerForm.Text.TextValue = activationServer;
			repo.LicenseManagerForm.ButtonConnectButton.Click();
			repo.LicenseManagerForm.ButtonOkCancelButton.Click();
        }
        
        public void firstSetup(string email) {
        	var studioFirstSetup = repo.WizardWinForm.SDLTradosStudioSetup;
			repo.WizardWinForm.ButtonNextButton.Click();
			repo.WizardWinForm.EmailAddress.TextValue = email;
			repo.WizardWinForm.ButtonFinishButton.Click();
        }
        
        
        public void turnOffAutomaticUpdates() {
        	repo.StudioWindowForm.ApplicationMenuButton.Click();
			repo.StudioWindowForm.IGOptions.Click();
			repo.SettingsDialogForm.AutomaticUpdates.Click();
			repo.SettingsDialogForm.RadioButtonManuallyCheckForUpdatesOptio.Click();
			repo.SettingsDialogForm.OkButton.Click();
			repo.StudioWindowForm.Close.Click();
        }
        
        public void startStudio() {
        
			Host.Local.RunApplication(@"C:\Program Files (x86)\SDL\SDL Trados Studio\Studio15\SDLTradosStudio.exe");
			}
        
        public void goToProductActivation() {
        
        	repo.StudioWindowForm.HelpRibbon.Click();
			repo.StudioWindowForm.ProductActivation.Click();
       }
        
        public void deactivateButton() {
        	repo.LicenseManagerForm.ButtonDeactivateButton.Click();
        }
        
        public void closeStudio() {
        	repo.StudioWindowForm.Close.Click();
        }
        
        public void pressNextWizard() {
        	repo.WizardWinForm.ButtonNextButtonInfo.WaitForExists(10000);
        	repo.WizardWinForm.ButtonNextButtonInfo.WaitForAttributeEqual(10000, "enabled", true);
        	repo.WizardWinForm.ButtonNextButton.Click();
        }
        
        public void pressFinishWizard() {
        	repo.WizardWinForm.ButtonFinishButtonInfo.WaitForExists(10000);
        	repo.WizardWinForm.ButtonFinishButtonInfo.WaitForAttributeEqual(10000, "enabled", true);
        	repo.WizardWinForm.ButtonFinishButton.Click();
        }
        
        public void deleteRegistry(string registryPath, string entry) {
           using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(registryPath, true))
			{
			if (key == null || key.SubKeyCount == 0) {}
			
			else {
				key.DeleteSubKey(entry);
			  }
			}
        }
        
        public string projectNameRandom() {
        	Random random = new Random();
        	int projectNr = random.Next(1, 10000);
        	string projectName = "AutomationProject" + projectNr.ToString();
        	return projectName;
        }
        
        public void installStudio(string studioExe) {
        	Host.Local.RunApplication(studioExe);
            
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
