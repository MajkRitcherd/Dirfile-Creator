// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 05/12/2023     \\

using System;
using System.ComponentModel;
using Dirfile_lib.API.Context;
using Dirfile_lib.API.Extraction.Modes;

namespace Dirfile_Creator_Graphical.Models
{
    /// <summary>
    /// Model for MainWindow.
    /// </summary>
    internal class MainWindowModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowModel"/> class.
        /// </summary>
        public MainWindowModel()
        {
        }

        /// <summary>
        /// Event handler.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        /// <summary>
        /// Enum contains properties that can be set (specifically properties with IsEmpty).
        /// </summary>
        public enum IsEmptyProperties
        {
            /// <summary>
            /// IsEmptyInputField property.
            /// </summary>
            IsEmptyInputField,

            /// <summary>
            /// IsEmptyRelativeInputField.
            /// </summary>
            IsEmptyRelativeInputField
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not the input field is empty.
        /// </summary>
        public bool IsEmptyInputField { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the relative input field is empty.
        /// </summary>
        public bool IsEmptyRelativeInputField { get; set; }

        /// <summary>
        /// Gets or sets the path mode to be used in context.
        /// </summary>
        public PathMode PathMode { get; set; }

        /// <summary>
        /// Gets or sets the relative path.
        /// </summary>
        public string RelativePath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the slash mode to be used in context.
        /// </summary>
        public SlashMode SlashMode { get; set; }

        /// <summary>
        /// Gets or sets the input string.
        /// </summary>
        private string InputString { get; set; } = string.Empty;

        /// <summary>
        /// Creates dirfiles from input string.
        /// </summary>
        /// <param name="inputString">Input string.</param>
        public void CreateDirfiles(string inputString)
        {
            this.InputString = inputString;

            switch (this.PathMode)
            {
                case PathMode.Absolute:
                    this.CreateAbsolute();
                    break;

                case PathMode.Relative:
                    this.CreateRelative();
                    break;

                default:
                    throw new Exception($"Path mode: '{this.PathMode}' is not defined in Dirfile lib.");
            }
        }

        /// <summary>
        /// Invokes  event that property was changed.
        /// </summary>
        /// <param name="propertyName">Name of changed property.</param>
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets IsEmpty property by passed property name.
        /// </summary>
        /// <param name="propertyName">Name of property.</param>
        public void SetIsEmpty(string propertyName)
        {
            var prop = this.GetType().GetProperty(propertyName);

            if (prop != null)
            {
                prop.SetValue(this, true);
                this.OnPropertyChanged(propertyName);
            }
        }

        /// <summary>
        /// Creates dirfiles using absolute dirfile context.
        /// </summary>
        /// <param name="inputString">Input string.</param>
        private void CreateAbsolute()
        {
            using var dirfileContext = new DirfileContext(this.SlashMode);
            dirfileContext.Create(this.InputString);
        }

        /// <summary>
        /// Creates dirfiles using relative dirfile context.
        /// </summary>
        /// <param name="inputString">Input string.</param>
        private void CreateRelative()
        {
            using var dirfileContext = new DirfileContext(this.RelativePath, this.SlashMode);
            dirfileContext.Create(this.InputString);
        }
    }
}