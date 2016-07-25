using System.Collections.Generic;

namespace LitmusClient.Entities
{
    /// <summary>
    /// Class corresponding to a single version within a Litmus test
    /// </summary>
    public class TestSetVersion
    {
        public bool? InlineCss { get; set; }
        public int Version { get; set; }
        public string UrlOrGuid { get; set; }
        public bool Received { get; set; }
        public List<Result> Results { get; set; }
    }
}