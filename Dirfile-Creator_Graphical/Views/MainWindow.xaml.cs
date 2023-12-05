// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 05/12/2023     \\

using System.Windows;
using Dirfile_Creator_Graphical.Models;
using Dirfile_lib.API.Extraction.Modes;

namespace Dirfile_Creator_Graphical.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.Model = new MainWindowModel();

            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the model of Main window.
        /// </summary>
        private MainWindowModel Model { get; set; }

        /// <summary>
        /// Changes the path mode to be used in context.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event arguments.</param>
        private void ChangePathMode(object sender, RoutedEventArgs e)
        {
            if (RelativeRadioButton.IsChecked.HasValue)
            {
                if (RelativeRadioButton.IsChecked.Value)
                    this.Model.PathMode = PathMode.Relative;
                else
                    this.Model.PathMode = PathMode.Absolute;
            }
        }

        /// <summary>
        /// Changes the slash mode to be used in context.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event arguments.</param>
        private void ChangeSlashMode(object sender, RoutedEventArgs e)
        {
            if (BackwardRadioButton.IsChecked.HasValue)
            {
                if (BackwardRadioButton.IsChecked.Value)
                    this.Model.SlashMode = SlashMode.Backward;
                else
                    this.Model.SlashMode = SlashMode.Forward;
            }
        }

        /// <summary>
        /// Creates dirfiles from input string.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event arguments.</param>
        private void CreateDirFiles(object sender, RoutedEventArgs e)
        {
            this.Model.CreateDirfiles(InputField.Text);
        }
    }
}