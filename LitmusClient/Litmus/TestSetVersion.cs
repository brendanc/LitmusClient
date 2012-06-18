using System.Collections.Generic;

namespace LitmusClient.Litmus
{
     /// <summary>
     /// Class corresponding to a single version within a Litmus test
     /// </summary>
    public class TestSetVersion
    {
        public int Version { get; set; }
        public string UrlOrGuid { get; set; }
        public bool Received { get; set; }
        public List<Result> Results { get; set; }
    }
}