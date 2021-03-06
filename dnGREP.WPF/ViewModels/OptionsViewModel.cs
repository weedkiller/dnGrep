﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;
using dnGREP.Common;
using Microsoft.Win32;

namespace dnGREP.WPF
{
    public class OptionsViewModel : WorkspaceViewModel, IDataErrorInfo
    {
        public OptionsViewModel()
        {
            loadSetting();
        }              

        #region Private Variables and Properties
        private System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
        private static string SHELL_KEY_NAME = "dnGREP";
        private static string OLD_SHELL_KEY_NAME = "nGREP";
        private static string SHELL_MENU_TEXT = "dnGREP...";
        private GrepSettings settings
        {
            get { return GrepSettings.Instance; }
        }
        #endregion

        #region Properties
        public bool CanSave
        {
            get {
                if (EnableWindowsIntegration != isShellRegistered("Directory") ||
                EnableStartupAcceleration != isStartupRegistered() ||
                EnableCheckForUpdates != settings.Get<bool>(GrepSettings.Key.EnableUpdateChecking) ||
                CheckForUpdatesInterval != settings.Get<int>(GrepSettings.Key.UpdateCheckInterval) ||
                ShowLinesInContext != settings.Get<bool>(GrepSettings.Key.ShowLinesInContext) ||
                ContextLinesBefore != settings.Get<int>(GrepSettings.Key.ContextLinesBefore) ||
                ContextLinesAfter != settings.Get<int>(GrepSettings.Key.ContextLinesAfter) ||
                CustomEditorPath != settings.Get<string>(GrepSettings.Key.CustomEditor) ||
                CustomEditorArgs != settings.Get<string>(GrepSettings.Key.CustomEditorArgs) ||
                ShowFilePathInResults != settings.Get<bool>(GrepSettings.Key.ShowFilePathInResults) ||
                AllowSearchWithEmptyPattern != settings.Get<bool>(GrepSettings.Key.AllowSearchingForFileNamePattern) ||
                AutoExpandSearchTree != settings.Get<bool>(GrepSettings.Key.ExpandResults) ||
                EnableClearType != (settings.Get<TextFormattingMode>(GrepSettings.Key.TextFormatting) == TextFormattingMode.Ideal) ||
                MatchThreshold != settings.Get<double>(GrepSettings.Key.FuzzyMatchThreshold))
                    return true;
                else
                    return false;
            }
        }

        private bool enableWindowsIntegration;
        public bool EnableWindowsIntegration
        {
            get { return enableWindowsIntegration; }
            set
            {
                if (value == enableWindowsIntegration)
                    return;

                enableWindowsIntegration = value;

                base.OnPropertyChanged(() => EnableWindowsIntegration);
            }
        }

        private bool enableStartupAcceleration;
        public bool EnableStartupAcceleration
        {
            get { return enableStartupAcceleration; }
            set
            {
                if (value == enableStartupAcceleration)
                    return;

                enableStartupAcceleration = value;

                base.OnPropertyChanged(() => EnableStartupAcceleration);
            }
        }

        private string windowsIntegrationTooltip;
        public string WindowsIntegrationTooltip
        {
            get { return windowsIntegrationTooltip; }
            set
            {
                if (value == windowsIntegrationTooltip)
                    return;

                windowsIntegrationTooltip = value;

                base.OnPropertyChanged(() => WindowsIntegrationTooltip);
            }
        }

        private string startupAccelerationTooltip;
        public string StartupAccelerationTooltip
        {
            get { return startupAccelerationTooltip; }
            set
            {
                if (value == startupAccelerationTooltip)
                    return;

                startupAccelerationTooltip = value;

                base.OnPropertyChanged(() => StartupAccelerationTooltip);
            }
        }

        private string panelTooltip;
        public string PanelTooltip
        {
            get { return panelTooltip; }
            set
            {
                if (value == panelTooltip)
                    return;

                panelTooltip = value;

                base.OnPropertyChanged(() => PanelTooltip);
            }
        }

        private bool isAdministrator;
        public bool IsAdministrator
        {
            get { return isAdministrator; }
            set
            {
                if (value == isAdministrator)
                    return;

                isAdministrator = value;

                base.OnPropertyChanged(() => IsAdministrator);
            }
        }

        private bool enableCheckForUpdates;
        public bool EnableCheckForUpdates
        {
            get { return enableCheckForUpdates; }
            set
            {
                if (value == enableCheckForUpdates)
                    return;

                enableCheckForUpdates = value;

                base.OnPropertyChanged(() => EnableCheckForUpdates);
            }
        }

        private int checkForUpdatesInterval;
        public int CheckForUpdatesInterval
        {
            get { return checkForUpdatesInterval; }
            set
            {
                if (value == checkForUpdatesInterval)
                    return;

                checkForUpdatesInterval = value;

                base.OnPropertyChanged(() => CheckForUpdatesInterval);
            }
        }

        private string customEditorPath;
        public string CustomEditorPath
        {
            get { return customEditorPath; }
            set
            {
                if (value == customEditorPath)
                    return;

                customEditorPath = value;

                base.OnPropertyChanged(() => CustomEditorPath);
            }
        }

        private string customEditorArgs;
        public string CustomEditorArgs
        {
            get { return customEditorArgs; }
            set
            {
                if (value == customEditorArgs)
                    return;

                customEditorArgs = value;

                base.OnPropertyChanged(() => CustomEditorArgs);
            }
        }

        private bool showFilePathInResults;
        public bool ShowFilePathInResults
        {
            get { return showFilePathInResults; }
            set
            {
                if (value == showFilePathInResults)
                    return;

                showFilePathInResults = value;

                base.OnPropertyChanged(() => ShowFilePathInResults);
            }
        }

        private bool showLinesInContext;
        public bool ShowLinesInContext
        {
            get { return showLinesInContext; }
            set
            {
                if (value == showLinesInContext)
                    return;

                showLinesInContext = value;

                base.OnPropertyChanged(() => ShowLinesInContext);
            }
        }

        private int contextLinesBefore;
        public int ContextLinesBefore
        {
            get { return contextLinesBefore; }
            set
            {
                if (value == contextLinesBefore)
                    return;

                contextLinesBefore = value;

                base.OnPropertyChanged(() => ContextLinesBefore);
            }
        }

        private int contextLinesAfter;
        public int ContextLinesAfter
        {
            get { return contextLinesAfter; }
            set
            {
                if (value == contextLinesAfter)
                    return;

                contextLinesAfter = value;

                base.OnPropertyChanged(() => ContextLinesAfter);
            }
        }

        private bool allowSearchWithEmptyPattern;
        public bool AllowSearchWithEmptyPattern
        {
            get { return allowSearchWithEmptyPattern; }
            set
            {
                if (value == allowSearchWithEmptyPattern)
                    return;

                allowSearchWithEmptyPattern = value;

                base.OnPropertyChanged(() => AllowSearchWithEmptyPattern);
            }
        }

        private bool autoExpandSearchTree;
        public bool AutoExpandSearchTree
        {
            get { return autoExpandSearchTree; }
            set
            {
                if (value == autoExpandSearchTree)
                    return;

                autoExpandSearchTree = value;

                base.OnPropertyChanged(() => AutoExpandSearchTree);
            }
        }

        private bool enableClearType;
        public bool EnableClearType
        {
            get { return enableClearType; }
            set
            {
                if (value == enableClearType)
                    return;

                enableClearType = value;

                base.OnPropertyChanged(() => EnableClearType);
            }
        }

        private double matchThreshold;
        public double MatchThreshold
        {
            get { return matchThreshold; }
            set
            {
                if (value == matchThreshold)
                    return;

                matchThreshold = value;

                base.OnPropertyChanged(() => MatchThreshold);
            }
        }
        
        #endregion

        #region Presentation Properties

        RelayCommand _saveCommand;
        /// <summary>
        /// Returns a command that saves the form
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(
                        param => this.Save(),
                        param => this.CanSave
                        );
                }
                return _saveCommand;
            }
        }

        RelayCommand _browseCommand;
        /// <summary>
        /// Returns a command that opens file browse dialog.
        /// </summary>
        public ICommand BrowseCommand
        {
            get
            {
                if (_browseCommand == null)
                {
                    _browseCommand = new RelayCommand(
                        param => this.browse()
                        );
                }
                return _browseCommand;
            }
        }

        RelayCommand _clearSearchesCommand;
        /// <summary>
        /// Returns a command that clears old searches.
        /// </summary>
        public ICommand ClearSearchesCommand
        {
            get
            {
                if (_clearSearchesCommand == null)
                {
                    _clearSearchesCommand = new RelayCommand(
                        param => this.clearSearches()
                        );
                }
                return _clearSearchesCommand;
            }
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Saves the customer to the repository.  This method is invoked by the SaveCommand.
        /// </summary>
        public void Save()
        {
            saveSettings();
            base.CloseCommand.Execute(null);
        }

        #endregion // Public Methods

        #region Private Methods 

        public void browse()
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CustomEditorPath = openFileDialog.FileName;
            }
        }

        public void clearSearches()
        {
            settings.Set<List<string>>(GrepSettings.Key.FastFileMatchBookmarks, new List<string>());
            settings.Set<List<string>>(GrepSettings.Key.FastFileNotMatchBookmarks, new List<string>());
            settings.Set<List<string>>(GrepSettings.Key.FastPathBookmarks, new List<string>());
            settings.Set<List<string>>(GrepSettings.Key.FastReplaceBookmarks, new List<string>());
            settings.Set<List<string>>(GrepSettings.Key.FastSearchBookmarks, new List<string>());
        }

        private void loadSetting()
        {
            checkIfAdmin();
            if (!IsAdministrator)
            {
                WindowsIntegrationTooltip = "To set shell integration run dnGREP with elevated priveleges.";
                StartupAccelerationTooltip = "To enable startup acceleration run dnGREP with elevated priveleges.";
                PanelTooltip = "To change shell integration and startup acceleration options run dnGREP with elevated priveleges.";
            }
            else
            {
                WindowsIntegrationTooltip = "Shell integration enables running an application from shell context menu.";
                StartupAccelerationTooltip = "Startup acceleration loads application libraries on machine start to improve application startup time.";
                PanelTooltip = "Shell integration enables running an application from shell context menu.";
            }
            EnableWindowsIntegration = isShellRegistered("Directory");
            EnableStartupAcceleration = isStartupRegistered();
            EnableCheckForUpdates = settings.Get<bool>(GrepSettings.Key.EnableUpdateChecking);
            CheckForUpdatesInterval = settings.Get<int>(GrepSettings.Key.UpdateCheckInterval);
            CustomEditorPath = settings.Get<string>(GrepSettings.Key.CustomEditor);
            CustomEditorArgs = settings.Get<string>(GrepSettings.Key.CustomEditorArgs);
            ShowFilePathInResults = settings.Get<bool>(GrepSettings.Key.ShowFilePathInResults);
            AllowSearchWithEmptyPattern = settings.Get<bool>(GrepSettings.Key.AllowSearchingForFileNamePattern);
            AutoExpandSearchTree = settings.Get<bool>(GrepSettings.Key.ExpandResults);
            EnableClearType = settings.Get<TextFormattingMode>(GrepSettings.Key.TextFormatting) == TextFormattingMode.Ideal;
            MatchThreshold = settings.Get<double>(GrepSettings.Key.FuzzyMatchThreshold);
            ShowLinesInContext = settings.Get<bool>(GrepSettings.Key.ShowLinesInContext);
            ContextLinesBefore = settings.Get<int>(GrepSettings.Key.ContextLinesBefore);
            ContextLinesAfter = settings.Get<int>(GrepSettings.Key.ContextLinesAfter);
        }

        private void saveSettings()
        {
            if (EnableWindowsIntegration)
            {
                shellRegister("Directory");
                shellRegister("Drive");
                shellRegister("*");
                shellRegister("here");
            }
            else
            {
                shellUnregister("Directory");
                shellUnregister("Drive");
                shellUnregister("*");
                shellUnregister("here");
            }

            if (EnableStartupAcceleration)
            {
                startupRegister();
            }
            else
            {
                startupUnregister();
            }

            settings.Set<bool>(GrepSettings.Key.EnableUpdateChecking, EnableCheckForUpdates);
            settings.Set<int>(GrepSettings.Key.UpdateCheckInterval, CheckForUpdatesInterval);
            settings.Set<string>(GrepSettings.Key.CustomEditor, CustomEditorPath);
            settings.Set<string>(GrepSettings.Key.CustomEditorArgs, CustomEditorArgs);
            settings.Set<bool>(GrepSettings.Key.ShowFilePathInResults, ShowFilePathInResults);
            settings.Set<bool>(GrepSettings.Key.AllowSearchingForFileNamePattern, AllowSearchWithEmptyPattern);
            settings.Set<bool>(GrepSettings.Key.ExpandResults, AutoExpandSearchTree);
            settings.Set<TextFormattingMode>(GrepSettings.Key.TextFormatting, EnableClearType ? TextFormattingMode.Ideal : TextFormattingMode.Display);
            settings.Set<double>(GrepSettings.Key.FuzzyMatchThreshold, MatchThreshold);
            settings.Set<bool>(GrepSettings.Key.ShowLinesInContext, ShowLinesInContext);
            settings.Set<int>(GrepSettings.Key.ContextLinesBefore, ContextLinesBefore);
            settings.Set<int>(GrepSettings.Key.ContextLinesAfter, ContextLinesAfter);
            settings.Save();
        }

        private bool isShellRegistered(string location)
        {
            if (!isAdministrator)
                return false;

            if (location == "here")
            {
                string regPath = string.Format(@"SOFTWARE\Classes\Directory\Background\shell\{0}",
                                           SHELL_KEY_NAME);
                try
                {
                    return Registry.LocalMachine.OpenSubKey(regPath) != null;
                }
                catch (UnauthorizedAccessException)
                {
                    isAdministrator = false;
                    return false;
                }
            }
            else
            {
                string regPath = string.Format(@"{0}\shell\{1}",
                                           location, SHELL_KEY_NAME);
                try
                {
                    return Registry.ClassesRoot.OpenSubKey(regPath) != null;
                }
                catch (UnauthorizedAccessException)
                {
                    isAdministrator = false;
                    return false;
                }
            }
        }

        private void shellRegister(string location)
        {
            if (!isAdministrator)
                return;

            if (!isShellRegistered(location))
            {

                if (location == "here")
                {
                    string regPath = string.Format(@"SOFTWARE\Classes\Directory\Background\shell\{0}", SHELL_KEY_NAME);

                    // add context menu to the registry
                    using (RegistryKey key =
                           Registry.LocalMachine.CreateSubKey(regPath))
                    {
                        key.SetValue(null, SHELL_MENU_TEXT);
                        key.SetValue("Icon", Assembly.GetAssembly(typeof(OptionsView)).Location);
                    }

                    // add command that is invoked to the registry
                    string menuCommand = string.Format("\"{0}\" \"%V\"",
                                           Assembly.GetAssembly(typeof(OptionsView)).Location);
                    using (RegistryKey key = Registry.LocalMachine.CreateSubKey(
                        string.Format(@"{0}\command", regPath)))
                    {
                        key.SetValue(null, menuCommand);
                    }
                }
                else
                {
                    string regPath = string.Format(@"{0}\shell\{1}", location, SHELL_KEY_NAME);

                    // add context menu to the registry
                    using (RegistryKey key =
                           Registry.ClassesRoot.CreateSubKey(regPath))
                    {
                        key.SetValue(null, SHELL_MENU_TEXT);
                        key.SetValue("Icon", Assembly.GetAssembly(typeof(OptionsView)).Location);
                    }

                    // add command that is invoked to the registry
                    string menuCommand = string.Format("\"{0}\" \"%1\"",
                                           Assembly.GetAssembly(typeof(OptionsView)).Location);
                    using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(
                        string.Format(@"{0}\command", regPath)))
                    {
                        key.SetValue(null, menuCommand);
                    }
                }
            }
        }

        private void shellUnregister(string location)
        {
            if (!isAdministrator)
                return;

            if (isShellRegistered(location))
            {
                if (location == "here")
                {
                    string regPath = string.Format(@"SOFTWARE\Classes\Directory\Background\shell\{0}", SHELL_KEY_NAME);
                    Registry.LocalMachine.DeleteSubKeyTree(regPath);
                }
                else
                {
                    string regPath = string.Format(@"{0}\shell\{1}", location, SHELL_KEY_NAME);
                    Registry.ClassesRoot.DeleteSubKeyTree(regPath);
                }
            }
        }

        private void oldShellUnregister()
        {
            if (!isAdministrator)
                return;

            string regPath = string.Format(@"Directory\shell\{0}", OLD_SHELL_KEY_NAME);
            if (Registry.ClassesRoot.OpenSubKey(regPath) != null)
            {
                Registry.ClassesRoot.DeleteSubKeyTree(regPath);
            }
        }

        private bool isStartupRegistered()
        {
            if (!isAdministrator)
                return false;

            string regPath = @"Software\Microsoft\Windows\CurrentVersion\Run";
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(regPath))
                {
                    return key.GetValue(SHELL_KEY_NAME) != null;
                }
            }
            catch (UnauthorizedAccessException)
            {
                isAdministrator = false;
                return false;
            }
        }

        private void startupRegister()
        {
            if (!isAdministrator)
                return;

            if (!isStartupRegistered())
            {
                string regPath = @"Software\Microsoft\Windows\CurrentVersion\Run";

                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(regPath, true))
                {
                    key.SetValue(SHELL_KEY_NAME, string.Format("\"{0}\" /warmUp", Assembly.GetAssembly(typeof(OptionsView)).Location), RegistryValueKind.ExpandString);
                }
            }
        }

        private void startupUnregister()
        {
            if (!isAdministrator)
                return;

            if (isStartupRegistered())
            {
                string regPath = @"Software\Microsoft\Windows\CurrentVersion\Run";

                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(regPath, true))
                {
                    key.DeleteValue(SHELL_KEY_NAME);
                }
            }
        }

        private void checkIfAdmin()
        {
            try
            {
                WindowsIdentity wi = WindowsIdentity.GetCurrent();
                WindowsPrincipal wp = new WindowsPrincipal(wi);

                if (wp.IsInRole("Administrators"))
                {
                    IsAdministrator = true;
                }
                else
                {
                    IsAdministrator = false;
                }
            }
            catch
            {
                IsAdministrator = false;
            }
        }

        #endregion

        #region IDataErrorInfo Members

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error = null;

                // Do validation
                if (IsProperty(() => MatchThreshold, propertyName))
                {
                    if (MatchThreshold < 0 || MatchThreshold > 1.0)
                        error = "Error: Match threshold should be a number between 0 and 1.0";
                }
                // Dirty the commands registered with CommandManager,
                // such as our Save command, so that they are queried
                // to see if they can execute now.
                CommandManager.InvalidateRequerySuggested();

                return error;
            }
        }


        public string Error
        {
            get { return null; }
        }

        #endregion
    }
}
