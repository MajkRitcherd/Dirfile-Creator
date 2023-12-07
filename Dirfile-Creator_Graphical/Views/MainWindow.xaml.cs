// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 05/12/2023     \\

using System;
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
            DataContext = this.Model;

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
            if (AbsoluteRadioButton.IsChecked.HasValue)
            {
                if (AbsoluteRadioButton.IsChecked.Value)
                    this.Model.PathMode = PathMode.Absolute;
                else
                    this.Model.PathMode = PathMode.Relative;
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
            if (!this.InputsNonEmpty())
            {
                MessageBox.Show("Input fields can't be empty!");
                return;
            }

            try
            {
                this.Model.RelativePath = RelativePathInput.Text;

                this.Model.CreateDirfiles(InputField.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Checks, whether or not the input fields (TextBoxes) are empty.
        /// </summary>
        /// <returns>True, if inputs are not empty, otherwise false.</returns>
        private bool InputsNonEmpty()
        {
            var isNonEmptyInputField = !string.IsNullOrEmpty(this.InputField.Text);

            if (!isNonEmptyInputField)
            {
                this.Model.SetIsEmpty(MainWindowModel.IsEmptyProperties.IsEmptyInputField.ToString(), true);
            }

            if (this.Model.PathMode == PathMode.Relative)
            {
                if (string.IsNullOrEmpty(this.RelativePathInput.Text))
                    this.Model.SetIsEmpty(MainWindowModel.IsEmptyProperties.IsEmptyRelativeInputField.ToString(), true);

                return !string.IsNullOrEmpty(this.Model.RelativePath) && isNonEmptyInputField;
            }

            return isNonEmptyInputField;
        }

        /// <summary>
        /// Handle text changed event in input field.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="args">Event arguments.</param>
        private void InputFieldTextChanged(object sender, RoutedEventArgs args)
        {
            if (this.Model.IsEmptyInputField && !string.IsNullOrEmpty(this.InputField.Text))
                this.Model.SetIsEmpty(MainWindowModel.IsEmptyProperties.IsEmptyInputField.ToString(), false);
        }

        /// <summary>
        /// Handle text changed event in relative input field.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="args">Event args.</param>
        private void RelativeInputFieldTextChanged(object sender, RoutedEventArgs args)
        {
            if (this.Model.IsEmptyRelativeInputField && !string.IsNullOrEmpty(this.RelativePathInput.Text))
                this.Model.SetIsEmpty(MainWindowModel.IsEmptyProperties.IsEmptyRelativeInputField.ToString(), false);
        }
    }
}