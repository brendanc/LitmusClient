namespace LitmusClient.Entities
{
    public static class ImageType
    {
        /// <summary>
        /// The window, or "chrome" view of the test subject when rendered in the specified testing application.
        /// For some email clients (those without preview panes), the "window" view will render how the email appears in the inbox view.
        /// </summary>
        public static readonly string Window = "window";

        /// <summary>
        /// The full page, "chrome"-less view of the test subject when rendered in the specified testing application.
        /// </summary>
        public static readonly string Full = "full";

        /// <summary>
        /// the window, or "chrome" view with all images and external content enabled.
        /// For emails this would be if you were on the safe senders list of the user receiving your email. For some email clients (those without preview panes), the "window" view will render how the email appears in the inbox view.
        /// </summary>
        public static readonly string WindowOn = "window_on";

        /// <summary>
        /// The window, or "chrome" view with all images and external content disabled.
        /// For emails this would be if you were treated as an unknown sender on clients that support this feature (e.g. Apple Mail and Gmail to name two such clients).
        /// For some email clients (those without preview panes), the "window" view will render how the email appears in the inbox view.
        /// </summary>
        public static readonly string WindowOff = "window_off";

        /// <summary>
        /// The full page view of the email with all images and external content enabled.
        /// </summary>
        public static readonly string FullOn = "full_on";

        /// <summary>
        /// The full page view of the email with all images and external content disabled.
        /// </summary>
        public static readonly string FullOff = "full_off";
    }
}