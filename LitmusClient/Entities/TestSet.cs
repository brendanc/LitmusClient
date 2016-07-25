using System;
using System.Collections.Generic;

namespace LitmusClient.Entities
{
    /// <summary>
    /// Object corresponding to a Litmus test.  TestSets can be for email or web pages.  TestSets can contain multiple versions.
    /// </summary>
    public class TestSet
    {
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Service { get; set; }
        public string State { get; set; }
        public bool PublicSharing { get; set; }
        public string UrlOrGuid { get; set; }
        public int Id { get; set; }

        public List<TestSetVersion> TestSetVersions { get; set; }
    }
}