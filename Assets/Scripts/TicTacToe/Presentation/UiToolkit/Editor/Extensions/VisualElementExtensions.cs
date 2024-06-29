using UnityEngine.UIElements;

namespace TicTacToe.Presentation.UiToolkit.Editor.Extensions {
    public static class VisualElementExtensions {
        /// <summary>
        /// Sets the style of the visual element, replacing any existing styles with the specified style sheet.
        /// </summary>
        /// <param name="element">The visual element to modify.</param>
        /// <param name="styleSheet">The style sheet to apply to the visual element.</param>
        public static void SetStyle(this VisualElement element, StyleSheet styleSheet) {
            element.styleSheets.Clear(); // Remove all existing style sheets
            element.styleSheets.Add(styleSheet); // Add the new style sheet
        }

        /// <summary>
        /// Adds a style sheet to the visual element without removing existing styles.
        /// </summary>
        /// <param name="element">The visual element to modify.</param>
        /// <param name="styleSheet">The style sheet to add to the visual element's existing styles.</param>
        public static void AddStyle(this VisualElement element, StyleSheet styleSheet) {
            element.styleSheets.Add(styleSheet); // Append the new style sheet to the existing collection
        }
    }
}