/*
 * Created by Ranorex
 * User: astan
 * Date: 19.01.2018
 * Time: 15:55
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
    /// Description of CreateProject.
    /// </summary>
    [TestModule("1F59DAA3-2A41-4297-AD17-BE0F7757BD47", ModuleType.UserCode, 1)]
    public class CreateProject : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public CreateProject()
        {
            // Do not delete - a parameterless constructor is required!
        }

        private static Studio2017Repository repo = Studio2017Repository.Instance;
        UtilityMethods utilityMethods = new UtilityMethods();
        ProjectCreationUtility projectCreationUtility = new ProjectCreationUtility();
        

        void ITestModule.Run()
        {
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
            string filesLocation = @"C:\Users\astan\Desktop\TranslatableFiles";
            string tmtbLocation = @"C:\Users\astan\Desktop\Utilities\";
            string tmName = "English-German";
            string termbaseName = "Printer";
            string projectName = utilityMethods.projectNameRandom();
            string sourceLanguage = "English (United States)";
            string targetLanguage = "German (Germany)";

            //Start Studio
            utilityMethods.startStudio();
            
            
            //Activate if necessary
			if (repo.LicenseManagerForm.ButtonActivateButtonInfo.Exists(5000)) {
				
				utilityMethods.studioActivation("clujhv28");
			}
            
			//First setup
			if (repo.WizardWinForm.SDLTradosStudioSetupInfo.Exists(5000)){
				
				utilityMethods.firstSetup("@");
			}
			
			
			//Turn off Automatic Updates
			utilityMethods.turnOffAutomaticUpdates();
			
			//Start Studio
			utilityMethods.startStudio();
			
			//Create project
			projectCreationUtility.goToNewProjectWizard();
			projectCreationUtility.createProject(projectName, filesLocation, tmtbLocation, tmName, termbaseName, sourceLanguage, targetLanguage);
        }
    }
}
