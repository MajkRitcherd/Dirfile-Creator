// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 11/12/2023     \\

using System.ComponentModel;

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
        /// Gets or sets the Dirfile model.
        /// </summary>
        public DirfileModel DirfileModel { get; set; } = new DirfileModel();

        /// <summary>
        /// Gets or sets a value indicating whether or not the input field is empty.
        /// </summary>
        public bool IsEmptyInputField { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the relative input field is empty.
        /// </summary>
        public bool IsEmptyRelativeInputField { get; set; }

        /// <summary>
        /// Gets or sets the relative path.
        /// </summary>
        public string RelativePath { get; set; } = string.Empty;

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
        /// <param name="isEmpty">True, if set the property to true, otherwise false.</param>
        public void SetIsEmpty(string propertyName, bool isEmpty)
        {
            var prop = this.GetType().GetProperty(propertyName);

            if (prop != null)
            {
                prop.SetValue(this, isEmpty);
                this.OnPropertyChanged(propertyName);
            }
        }
    }
}