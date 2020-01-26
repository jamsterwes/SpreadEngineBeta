using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using Microsoft.Win32;
using SpreadRuntime;
using SpreadRuntime.Bootstrap;

namespace SpreadLauncher
{
    public partial class MainWindow : Window
    {
        private void OnAssemblyChanged(string newAssembly)
        {
            if (newAssembly != "")
            {
                AssemblyLabel.Content = "Loaded Assembly: " + System.IO.Path.GetFileName(newAssembly);
                PlayButton.IsEnabled = true;
            }
            else
            {
                AssemblyLabel.Content = "No Assembly Loaded!";
                PlayButton.IsEnabled = false;
            }
        }

        private string CurrentAssembly
        {
            get => Properties.Settings.Default.LastAssembly;
            set
            {
                Properties.Settings.Default.LastAssembly = value;
                OnAssemblyChanged(value);
                Properties.Settings.Default.Save();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            OnAssemblyChanged(CurrentAssembly);
        }

        private void LoadAssembly_Click(object sender, RoutedEventArgs e)
        {
            var diag = new OpenFileDialog();
            diag.Filter = "Spread Applications (*.dll)|*.dll";
            if (diag.ShowDialog() == true)
            {
                if (AssemblyLoader.IsAssemblyValid(diag.FileName)) CurrentAssembly = diag.FileName;
                else MessageBox.Show("This assembly is not a valid SpreadEngine assembly");
            }
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            // Hide launcher while application is running
            Hide();

            // Run application
            SpreadApplication app = AssemblyLoader.LoadAssembly(CurrentAssembly);
            ApplicationRunner.RunApplication(app);

            // Shutdown after application is done
            Application.Current.Shutdown();
        }
    }
}
