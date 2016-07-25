namespace LitmusClient.Entities
{
    /// <summary>
    /// Class to wrap the Litmus images that comeback as both a thumb and full size image.
    /// </summary>
    public class ResultImage
    {
        /// <summary>
        /// Litmus captures screenshots based on the various states that pages and emails can appear in the specified client.
        /// For example, some clients will block images - this is indicated by the supports_content_blocking attribute of a testing application.
        /// The various values you would see for this are:
        /// </summary>
        public string ImageType { get; set; }

        /// <summary>
        /// A url path to the full image for this result image. Is supplied without the leading "http://" allowing you to specify http:// or https:// depending on your security requirements.
        /// </summary>
        public string FullImage { get; set; }

        /// <summary>
        /// A url path to the thumbnail image for this result image. Is supplied without the leading "http://" allowing you to specify http:// or https:// depending on your security requirements. Thumbnail images are always 119x84 pixels.
        /// </summary>
        public string ThumbnailImage { get; set; }
    }
}