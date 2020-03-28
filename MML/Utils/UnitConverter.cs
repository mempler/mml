namespace MML.Utils
{
    /// <summary>
    /// Helper function for Unit Conversation.
    /// </summary>
    public static class UnitConverter
    {
        /// <summary>
        /// Computes Inches to Pixels
        /// </summary>
        /// <returns>Pixels</returns>
        public static double Inch2Pixel(double inch) => inch * 96d;

        /// <summary>
        /// Computes Points to Pixels
        /// </summary>
        /// <returns>Pixels</returns>
        public static double Point2Pixel(double pt) => pt * (96d / 72d);

        /// <summary>
        /// Computes Pica to Pixels
        /// </summary>
        /// <returns>Pixels</returns>
        public static double Pica2Pixel(double pc) => pc * Point2Pixel(12d);
    }
}
