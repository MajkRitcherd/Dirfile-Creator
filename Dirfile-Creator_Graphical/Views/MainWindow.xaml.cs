// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 12/12/2023     \\

using System;
using System.Windows;
using Dirfile_Creator_Graphical.Models;
using Dirfile_Creator_Graphical.UIHelpers;
using Dirfile_lib.API.Extraction.Modes;
using Dirfile_lib.Core.Constants;
using Ookii.Dialogs.Wpf;

namespace Dirfile_Creator_Graphical.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Back slash constant.
        /// </summary>
        private const char BSlash = '\\';

        /// <summary>
        /// Forward slash constant.
        /// </summary>
        private const char FSlash = '/';

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
        /// Adds Dirfile's create operation to Input field.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event args.</param>
        private void ButtonClick_CreateDirfile(object sender, RoutedEventArgs e)
        {
            this.InputField.AddText($" {DirFile.Operations.Next} ");
        }

        /// <summary>
        /// Adds Dirfile's end of text operations to Input field.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Events args.</param>
        private void ButtonClick_EndOfText(object sender, RoutedEventArgs e)
        {
            this.InputField.AddText(DirFile.Operations.EndOfText);
        }

        /// <summary>
        /// Adds Dirfile's redirect to previous directory and create Dirfile operation to Input field.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Events args.</param>
        private void ButtonClick_RedirectBackAndCreateDirfile(object sender, RoutedEventArgs e)
        {
            this.InputField.AddText($" {DirFile.Operations.Prev} ");
        }

        /// <summary>
        /// Adds Dirfile's create operation in the last dirfile to Input field.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event args.</param>
        private void ButtonClick_RedirectToLastAndCreateDirfile(object sender, RoutedEventArgs e)
        {
            const string changeOperation = DirFile.Operations.Change;

            var useBSlash = this.Model.DirfileModel.SlashMode == SlashMode.Backward;
            var textToAdd = useBSlash ? changeOperation : changeOperation.Replace(BSlash, FSlash);

            this.InputField.AddText(textToAdd);
        }

        /// <summary>
        /// Adds Dirfile's start of text operations to Input field.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Events args.</param>
        private void ButtonClick_StartOfText(object sender, RoutedEventArgs e)
        {
            this.InputField.AddText(DirFile.Operations.StartOfText);
        }

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
                    this.Model.DirfileModel.PathMode = PathMode.Absolute;
                else
                    this.Model.DirfileModel.PathMode = PathMode.Relative;
            }
        }

        /// <summary>
        /// Changes slashes in the Input field and relative path input field.
        /// </summary>
        private void ChangeSlashes()
        {
            var isFSlash = this.Model.DirfileModel.SlashMode == SlashMode.Forward;
            var oldSlash = isFSlash ? BSlash : FSlash;
            var newSlash = isFSlash ? FSlash : BSlash;

            // Replace slashes in the Input field, if there's any text
            if (this.InputField != null && !string.IsNullOrEmpty(this.InputField.Text))
                this.InputField.Text = this.InputField.Text.Replace(oldSlash, newSlash);

            // Replace slashes in the relative path input field, if there's any text
            if (this.RelativePathInput != null && !string.IsNullOrEmpty(this.RelativePathInput.Text))
                this.RelativePathInput.Text = this.RelativePathInput.Text.Replace(oldSlash, newSlash);
        }

        /// <summary>
        /// Changes the slash mode to be used in context. <br />
        /// Also it changes used slashes in the Input field and relative input path, if there's any text.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event arguments.</param>
        private void ChangeSlashMode(object sender, RoutedEventArgs e)
        {
            if (BackwardRadioButton.IsChecked.HasValue)
            {
                if (BackwardRadioButton.IsChecked.Value)
                    this.Model.DirfileModel.SlashMode = SlashMode.Backward;
                else
                    this.Model.DirfileModel.SlashMode = SlashMode.Forward;
            }

            this.ChangeSlashes();
        }

        /// <summary>
        /// Creates dirfiles from input string.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event arguments.</param>
        private void CreateDirFiles(object sender, RoutedEventArgs e)
        {
            if (!this.InputsNonEmpty(out string msg))
            {
                MessageBox.Show("When creating dirfiles, an error occurred:\n" + msg);
                return;
            }

            try
            {
                this.Model.RelativePath = RelativePathInput.Text;

                this.Model.DirfileModel.CreateDirfiles(InputField.Text, RelativePathInput.Text);

                MessageBox.Show("Dirfiles successfully created.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An internal error occured:\nMessage: \"{ex.Message}\"");
            }
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
        /// Checks, whether or not the input fields (TextBoxes) are empty.
        /// </summary>
        /// <returns>True, if inputs are not empty, otherwise false.</returns>
        private bool InputsNonEmpty(out string msg)
        {
            msg = string.Empty;
            var isNonEmptyInputField = !string.IsNullOrEmpty(this.InputField.Text);

            // In case of empty Input field
            if (!isNonEmptyInputField)
            {
                this.Model.SetIsEmpty(MainWindowModel.IsEmptyProperties.IsEmptyInputField.ToString(), true);
                msg = "Input field cannot be empty!";
            }

            // In case of empty relative path input field.
            if (this.Model.DirfileModel.PathMode == PathMode.Relative)
            {
                if (string.IsNullOrEmpty(this.RelativePathInput.Text))
                {
                    this.Model.SetIsEmpty(MainWindowModel.IsEmptyProperties.IsEmptyRelativeInputField.ToString(), true);

                    if (isNonEmptyInputField)
                        msg = "Relative path input cannot be empty!";
                    else
                        msg = "Input field and relative path input cannot be empty!";
                }

                return !this.Model.IsEmptyRelativeInputField && isNonEmptyInputField;
            }

            return isNonEmptyInputField;
        }

        /// <summary>
        /// Opens folder browser dialog and sets path to Relative path input.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Mouse button event args.</param>
        private void MouseDoubleClick_ChooseRelativePath(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var folderBrowserDialog = new VistaFolderBrowserDialog();
            bool? result = folderBrowserDialog.ShowDialog();

            // If user clicks OK button
            if (result == true)
            {
                var relativePath = folderBrowserDialog.SelectedPath;

                if (this.Model.DirfileModel.SlashMode == SlashMode.Forward)
                    relativePath = relativePath.Replace(BSlash, FSlash);

                this.RelativePathInput.Text = relativePath;
            }
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