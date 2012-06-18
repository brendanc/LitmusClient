namespace LitmusClient.Litmus
{
    /// <summary>
    /// Class to wrap the Litmus images that comeback as both a thumb and full size image.
    /// </summary>
    public class ResultImage
    {
        public string ImageType { get; set; }
        public string FullImage { get; set; }
        public string ThumbnailImage { get; set; }
    }
}