using RestSharp.Serializers;
using System;

namespace LitmusClient
{
    /// <summary>
    /// The objects you POST to Litmus are very different from the objects you get back.
    /// For this reason we override the RestSharp XmlSerializer for POST requests only.
    /// </summary>
    public class LitmusPostSerializer : ISerializer
    {
        public string Serialize(object obj)
        {
            return obj.ToString();
        }

        public string RootElement { get; set; }

        public string Namespace { get; set; }

        public string DateFormat { get; set; }

        public string ContentType
        {
            get
            {
                return "application/xml";
            }
            set { throw new NotImplementedException(); }
        }
    }
}