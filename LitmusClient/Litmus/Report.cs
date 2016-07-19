using System;

namespace LitmusClient.Litmus
{
    public class Report
    {
        public string BugHtml { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Id { get; set; }
        public string ReportName { get; set; }
    }
}