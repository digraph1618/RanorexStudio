﻿/*
 * Created by Ranorex
 * User: astan
 * Date: 20.02.2018
 * Time: 14:41
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
    /// Description of Constants.
    /// </summary>
    [TestModule("ED8106A6-ACDF-4156-8E36-7BA2A090B0B5", ModuleType.UserCode, 1)]
    public class Constants
    {
    	    public const string StudioAppPath = @"C:\Program Files (x86)\SDL\SDL Trados Studio\Studio15\SDLTradosStudio.exe";
    		public const string TranslatableFiles = "TranslatableFiles";
    		public const string StudioProjectsFolder = @"C:\Users\astan\Documents\Studio 2018\Projects\";
    		public const string LicenseServer = "clujhv28";
    	    public const string InputFilesLocation = @"C:\Users\astan\Desktop\" + Constants.TranslatableFiles;
            public const string InputTmtbLocation = @"C:\Users\astan\Desktop\Utilities\";
            public const string SamplePhotoPrinter = "SamplePhotoPrinter.doc";
            public const string SamplePresentation = "SamplePresentation.pptx";
            public const string SampleXML = "SampleXML_DITA.xml";
            public const string SampleSecondDoc = "SecondSample.docx";
            public const string SamplePerfectMatch = "TryPerfectMatch.doc";
            public const string EnglishGermanTM = "English-German";
            public const string PrinterTB = "Printer";
            public const string EnglishUS = "English (United States)";
            public const string GermanDE = "German (Germany)";
            public const string ExpectedTermSecondSegment = "photo printer";
            public const string FirstSegmentExpectedTranslation = "Erste Schritte";
            public const string SecondSegmentExpectedTranslation = "Geeigneten Aufstellungsort für Ihren Fotodrucker finden";
            public const string ThirdSegmentExpectedTranslation = "Platzieren Sie den Fotodrucker auf einer flachen, sauberen und staubfreien Oberfläche, und stellen Sie ihn an einem trockenen Ort auf, der keinem direkten Sonnenlicht ausgesetzt ist.";
            public const string RanorexTranslation = "Ranorex translation";
            public const string CustomTerm = "RanorexTerm";
            public const string EditedTerm = "RanorexEdited";
            public const string RegistryPath = @"Software\SDL\Studio15";
			public const string RegistryEntry = "MachineSupport";
			public const string LocalBuildFolder = @"C:\Automation\Builds\";
			public const string BuildsServer = @"\\sheffdevproj1.global.sdl.corp\BuildDrop\";
			public const string StudioFolderPattern = "TranslationStudio.Master_15*";
			public const string StudioExePattern = "SDLTradosStudio*";
			public const string RootOrganization = "Root Organization";
			public const string AutomationProjects = "AutomationProject*";
			public const bool FirstStart = true;
			public const bool NotFirstStart = false;
            
    }
}
