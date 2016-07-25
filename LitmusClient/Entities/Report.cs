using System;

namespace LitmusClient.Entities
{
    public class Report
    {
        /// <summary>
        /// A URL escaped snippet of HTML that you need to insert into your email in order to track statistics to this list.
        /// The HTML is escaped for the purpose of enclosing in a XML document, you should unencode it before using (e.g. turning &lt; back into <)
        /// </summary>
        public string BugHtml { get; set; }

        /// <summary>
        /// The time that this report was first created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// A globally unique identifier for this report
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// A name given to this report by the end-user that is used to identify it.
        /// </summary>
        public string Name { get; set; }
    }
}