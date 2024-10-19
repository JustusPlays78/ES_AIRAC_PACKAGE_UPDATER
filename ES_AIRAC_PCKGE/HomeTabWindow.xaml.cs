using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ES_AIRAC_PCKGE.EnumsList;
using ES_AIRAC_PCKGE.Services;
using ES_AIRAC_PCKGE.Services.Backend;
using Microsoft.Win32;

namespace ES_AIRAC_PCKGE
{
    
    public static string AppVersion = "1.0.0.0";
    public static string LogPath = System.IO.Directory.GetCurrentDirectory()+ "\\debuging\\log_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".txt";
    public static string ConfigPath = System.IO.Directory.GetCurrentDirectory() + "\\config.json";
    public static string? SctPath;
    
    private LoggerService _loggerService = new();
    private ConfigService _configService = new();
    private BackendService _backendService = new();
    
    
    private Brush bluecolor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#233244"));
    private Brush whitecolor = Brushes.White;
    
    public HomeTabWindow()
    { 
        _loggerService.OnStart();
        _loggerService.LogMessage(SeverityLevelType.Info, "LoggerService started");

        
        _configService.OnStart();
        _loggerService.LogMessage(SeverityLevelType.Info, "Config setup completed");
        _configService = _configService.GetConfig();
        
        _backendService.OnStart(_configService);
        
        
        InitializeComponent();
        EnableDisableTab(HomeTabGrid,HomeTabButton, true);
        EnableDisableTab(CredentialsTabGrid,CredentialsTabButton, false);
        EnableDisableTab(FeaturesTabGrid, FeaturesTabButton, false);
        _loggerService.LogMessage(SeverityLevelType.Info, "HomeTab open");
    }
        private void CloseApplication(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MinimizeApplication(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            _loggerService.LogMessage(SeverityLevelType.Info, "Application minimized");
        }

        private void MouseDragAndDrop(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void OnTabSwitch(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string tabName = button.Tag as string;
                _loggerService.LogMessage(SeverityLevelType.Info, $"Switched to {tabName} tab");

                switch (tabName)
                {
                    case "Home":
                        MainContent.Content = new HomeGrid();
                        break;
                    case "Credentials":
                        MainContent.Content = new CredentialsGrid();
                        break;
                    case "Features":
                        MainContent.Content = new FeaturesGrid();
                        break;
                }
            }
        }

        private async void SelectSctFolder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Folder Selection."
            };

            if (dialog.ShowDialog() == true)
            {
                string folderPath = System.IO.Path.GetDirectoryName(dialog.FileName);
                await SelectSctFolder(folderPath);
                SctFolderPath.Text = folderPath;
            }
        }

        private async Task SelectSctFolder(string path)
        {
            await _configService.SetSctFolder(path);
            _loggerService.LogMessage(SeverityLevelType.Info, $"SCT folder selected: {path}");
        }

        private void EditFeature(FeatureItem feature)
        {
            // Edit feature logic would be implemented here
            _loggerService.LogMessage(SeverityLevelType.Info, $"Editing feature: {feature.Name}");
        }

        private void DeleteFeature(FeatureItem feature)
        {
            Features.Remove(feature);
            _loggerService.LogMessage(SeverityLevelType.Info, $"Feature deleted: {feature.Name}");
        }

        private void SaveFeature(FeatureItem feature)
        {
            // Save feature logic would be implemented here
            _loggerService.LogMessage(SeverityLevelType.Info, $"Feature saved: {feature.Name}");
        }
    }

    public class FeatureItem
    {
        public string Name { get; set; }
        public string RelPath { get; set; }
        public string Alt { get; set; }
        public string Neu { get; set; }
    }
}