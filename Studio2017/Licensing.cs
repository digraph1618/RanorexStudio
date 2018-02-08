/*
 * Created by Ranorex
 * User: astan
 * Date: 18.01.2018
 * Time: 14:20
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
	/// Description of Licensing.
	/// </summary>
	[TestModule("887A96F6-73ED-4777-86F6-486EBE7ED380", ModuleType.UserCode, 1)]
	public class Licensing : ITestModule
	{
		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		public Licensing()
		{
			// Do not delete - a parameterless constructor is required!

		}

		private static Studio2017Repository repo = Studio2017Repository.Instance;
		UtilityMethods utilityMethods = new UtilityMethods();
		
		/// <summary>
		/// Performs the playback of actions in this module.
		/// </summary>
		/// <remarks>You should not call this method directly, instead pass the module
		/// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
		/// that will in turn invoke this method.</remarks>
		void ITestModule.Run()
		{
            Mouse.DefaultMoveTime = 0;
            Keyboard.DefaultKeyPressTime = 0;
            Mouse.DefaultClickTime = 0;
            Delay.SpeedFactor = 0.0;
			
			
			string keyName = @"Software\SDL\Studio15";
			string registryEntry = "MachineSupport";
			
			
			//Delete registry for First step
			utilityMethods.deleteRegistry(keyName, registryEntry);

			
			//Start Studio
			utilityMethods.startStudio();
			
			
			if (repo.LicenseManagerForm.ButtonActivateButtonInfo.Exists(5000)) {
				utilityMethods.studioActivation("clujhv28");
			}
			
			//First setup
			utilityMethods.firstSetup("@");
			
			
			//Turn off Automatic Updates
			utilityMethods.turnOffAutomaticUpdates();
			
			
			//Start Studio
			utilityMethods.startStudio();
			
			
			//Go to Product Activation
			utilityMethods.goToProductActivation();
			
			
			if (repo.LicenseManagerForm.ButtonDeactivateButtonInfo.Exists(5000)) {
			
				utilityMethods.deactivateButton();
				utilityMethods.studioActivation("clujhv28");
			}
			
			else {
				
				utilityMethods.studioActivation("clujhv28");
			}
			
			utilityMethods.closeStudio();
		}
	}
}
