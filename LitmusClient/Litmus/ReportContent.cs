namespace LitmusClient.Litmus
{
    public class ReportContent
        : Report
    {
        public bool PublicSharing { get; set; }
        public string SharingUrl { get; set; }
        public string ClientUsage { get; set; }
        public string ClientEngagement { get; set; }
        public string Activity { get; set; }
    }
}