using System;
using System.Collections.Generic;

namespace LitmusClient.Litmus
{
    /// <summary>
    /// Class corresponding to a single Litmus result - ie for Outlook 2010 or IE7, etc.
    /// </summary>
    public class Result
    {
        public int Id { get; set; }
        public string CheckState { get; set; }

        public DateTime? ErrorAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public DateTime? StartedAt { get; set; }

        public string TestCode { get; set; }
        public string State { get; set; }
        public string ResultType { get; set; }
        public TestingApplication TestingApplication { get; set; }
        public List<ResultImage> ResultImages { get; set; }
    }
}