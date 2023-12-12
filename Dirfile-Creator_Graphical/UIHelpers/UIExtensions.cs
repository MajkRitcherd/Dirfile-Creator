// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
// ||                                                    || \\
// ||    <Author>       Majk Ritcherd       </Author>    || \\
// ||                                                    || \\
// ||~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|| \\
//                              Last change: 11/12/2023     \\

using System.Windows.Controls;

namespace Dirfile_Creator_Graphical.UIHelpers
{
    /// <summary>
    /// UI extension methods.
    /// </summary>
    public static class UIExtensions
    {
        /// <summary>
        /// Adds text to the end of text box and focuses to the end of it.
        /// </summary>
        /// <param name="textBox">Text box.</param>
        /// <param name="text">Text to add to the end of text box's text.</param>
        public static void AddText(this TextBox textBox, string text)
        {
            textBox.Text += text;

            textBox.FocusToEndOfTextbox();
        }

        /// <summary>
        /// Adds character to the end of a text box's text and focuses to the end of it.
        /// </summary>
        /// <param name="textBox">Text box.</param>
        /// <param name="character">Character to add to the end of text box's text.</param>
        public static void AddText(this TextBox textBox, char character)
        {
            textBox.Text += character;

            textBox.FocusToEndOfTextbox();
        }

        /// <summary>
        /// Focuses to the end of a text box's text.
        /// </summary>
        /// <param name="textBox">Text box.</param>
        public static void FocusToEndOfTextbox(this TextBox textBox)
        {
            textBox.CaretIndex = textBox.Text.Length;
            textBox.Focus();
        }
    }
}