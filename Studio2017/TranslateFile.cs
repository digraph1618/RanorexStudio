/*
 * Created by Ranorex
 * User: astan
 * Date: 29.01.2018
 * Time: 13:55
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
    /// Description of TranslateFile.
    /// </summary>
    [TestModule("6A39A0E4-3F5F-4F23-B5DA-A19481F2E6CF", ModuleType.UserCode, 1)]
    public class TranslateFile : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public TranslateFile()
        {
            // Do not delete - a parameterless constructor is required!
        }

		private static Studio2017Repository repo = Studio2017Repository.Instance;
        UtilityMethods utilityMethods = new UtilityMethods();
        ProjectCreationUtility projectCreationUtility = new ProjectCreationUtility();
        ViewsUtility views = new ViewsUtility();
        EditorUtility editor = new EditorUtility();
        BatchTaskUtility batchTask = new BatchTaskUtility();
        List<string> projectFilesList = new List<string>(new string[] { "SamplePhotoPrinter.doc", "SamplePresentation.pptx", "SampleXML_DITA.xml", "SecondSample.docx", "TryPerfectMatch.doc" });
        
        void ITestModule.Run()
        {
            Mouse.DefaultMoveTime = 0;
            Keyboard.DefaultKeyPressTime = 0;
            Mouse.DefaultClickTime = 0;
            Delay.SpeedFactor = 0.0;
            
            string projectName = utilityMethods.projectNameRandom();
            string projectFilesFolder = @"C:\Users\astan\Documents\Studio 2018\Projects\" + projectName + @"\de-DE\TranslatableFiles\";
            string filesLocation = @"C:\Users\astan\Desktop\TranslatableFiles";
            string tmtbLocation = @"C:\Users\astan\Desktop\Utilities\";
            string fileName = "SamplePhotoPrinter.doc";
            string tmName = "English-German";
            string termbaseName = "Printer";
            string sourceLanguage = "English (United States)";
            string targetLanguage = "German (Germany)";
            string expectedTermSecondSegment = "photo printer";
            string firstSegmentExpectedTranslation = "Erste Schritte";
            string secondSegmentExpectedTranslation = "Geeigneten Aufstellungsort für Ihren Fotodrucker finden";
            string thirdSegmentExpectedTranslation = "Platzieren Sie den Fotodrucker auf einer flachen, sauberen und staubfreien Oberfläche, und stellen Sie ihn an einem trockenen Ort auf, der keinem direkten Sonnenlicht ausgesetzt ist.";
            string ranorexTranslation = "Ranorex translation";
            string customTerm = "RanorexTerm";
            string editedTerm = "RanorexEdited";
            
            
            
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
			
			//Select translation file
			views.selectProject(projectName);
			views.goToFilesView();
			views.includeSubfolders("check");
			views.openFileForTranslation(fileName);	
			
			
			//Get translation of first segment
			string firstTranslation = editor.getSegmentTranslation(1);
			Validate.AreEqual(firstSegmentExpectedTranslation, firstTranslation);
			
			//Apply translation from translation memory
			string secondTranslation = editor.getSegmentTranslation(2);
			Validate.AreEqual(secondSegmentExpectedTranslation, secondTranslation);
			
			//Get terms from Term Recognition
			editor.goToTermRecognition();
			var existingTerms = new List<string>();
			existingTerms = editor.getTerms();
			if (existingTerms.Contains(expectedTermSecondSegment)) {
				Report.Success("Success", "Terms is found for the second segment");
			}
			else {
				Report.Failure("Fail", "Term is not found for the second segment");
			}
			
			//Translate a segment and confirm
			editor.goToHomeView();
			editor.translateSegment(5, ranorexTranslation);
			editor.cofirmTranslation();
			
			string thirdTranslation = editor.getSegmentTranslation(5);
			Validate.AreEqual(ranorexTranslation, thirdTranslation);
			
			
			//Verify TM works delete and then come back to segment to see if the interaction with the TM works
			editor.deleteSegment(5);
			editor.saveFile();
			
			//Move to third segment to change segments and validate
			string fourthTranslation = editor.getSegmentTranslation(3);
			Validate.AreEqual(thirdSegmentExpectedTranslation, fourthTranslation);
			
			//Go back to the deleted segment
			string deletedSegment = editor.getSegmentTranslation(5);
			if (deletedSegment == "\r\n" || deletedSegment == "") {
				editor.applyTranslation();
				deletedSegment = editor.getSegmentTranslation(5);
				Validate.AreEqual(ranorexTranslation, deletedSegment);
			}
			else {
			Validate.AreEqual(ranorexTranslation, deletedSegment);
			}
			
			
			//Add term from termbase
			editor.addTermbase(5);
			string termbaseAddedSegment = editor.getSegmentTranslation(5);
			if (Validate.Equals(termbaseAddedSegment, "USB-Kabel") || Validate.Equals(termbaseAddedSegment, "Fotodrucker")) {
			    Report.Success("Success", "Term was added into target segment");    
			}
			else {
			    Report.Failure("Fail", "Term was not added into target segment");
			}
			
			string englishTerm = editor.addTermInTermbase(customTerm);
            //Get terms from Term Recognition
			var addedTerms = new List<string>();
			addedTerms = editor.getTerms();
			if (addedTerms.Contains(customTerm) && addedTerms.Contains(englishTerm)) {
				Report.Success("Success", "Terms is found for the second segment");
			}
			else {
				Report.Failure("Fail", "Term is not found for the second segment");
			}
			
			//Search and edit term
			if (editor.checkIfTermExists(englishTerm, customTerm)) {
				Report.Success("Term " + englishTerm + " was found in termbase search");
			}
			else {
				Report.Failure("Term" + englishTerm + " was not found in termbase search");
			}
			
			editor.viewTermDetails(customTerm);
			editor.editTerm(editedTerm);
			editor.goToTermRecognition();
			var termsEdited = new List<string>();
			termsEdited = editor.getTerms();
			if (termsEdited.Contains(englishTerm) && termsEdited.Contains(editedTerm)) {
				Report.Success("Success", "Edited term is found");
			}
			else {
				Report.Failure("Fail", "Edited term is not found");
			}
			
			
			//Delete term
			editor.goToTermBaseSearch();
			editor.viewTermDetails(customTerm);
			editor.deleteTermEntry();
			
			//Verify that term is deleted
			if (editor.checkIfTermExists(englishTerm, customTerm)) {
				Report.Failure("Fail", "Term was not deleted");
			}
			else {
				Report.Success("Success", "Term was deleted");
			}
			
			
//			//Finalize project
			batchTask.runBatchTask("Finalize", projectName);
			editor.questionSaveChanges("Yes");
			utilityMethods.pressNextWizard();
			utilityMethods.pressFinishWizard();
			utilityMethods.pressFinishWizard();
			
			//Verify that project was finalised (files are saved as target)
			foreach (string file in projectFilesList) {
				System.DateTime start = System.DateTime.Now;
				while (!System.IO.File.Exists(projectFilesFolder+file) && System.DateTime.Now.Subtract(start).Seconds < 20) {
					Console.WriteLine("Target file " + file + " is not present yet");
				}
				bool fileIsPresent = System.IO.File.Exists(projectFilesFolder+file);
				if (fileIsPresent) {
					Report.Success("Success", "File " + file + " is saved as target");
				}
				else {
					Report.Failure("Fail", "File " + file + " is not saved as target");
				}
        	}
			
			//Close Studio
			utilityMethods.closeStudio();
			editor.questionSaveChanges("No");
        }
        
    }
}
