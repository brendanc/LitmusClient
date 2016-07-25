using System;
using System.Collections.Generic;

namespace LitmusClient.Entities
{
    /// <summary>
    /// Class corresponding to a single Litmus result - ie for Outlook 2010 or IE7, etc.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// A unique integer identifier assigned to each result by Litmus.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The check_state attribute indicates the compatibility flag as set by a user.
        /// This changes how a result appears in the Litmus web app (red cross or green tick), you are free to set this parameter and use the data yourself via the results/update method.
        /// Either 'ticked', 'crossed' or 'nostate' are allowed as valid values.
        /// A result which returnsnil for check_state is considered to be 'nostate'.
        /// But you cannot set this value as nil yourself, you must set 'nostate' if you want to clear the compatibility state.
        /// </summary>
        public string CheckState { get; set; }

        /// <summary>
        /// If an error occurred during the capture process then this value will be set to a date/time value.
        /// This also indicates that the test is complete and no further testing will occur unless you try to retest this particular result.
        /// </summary>
        public DateTime? ErrorAt { get; set; }

        /// <summary>
        /// If the test completed successfully then this attribute will be set to a date/time value and will indicate when the testing system processed the result.
        /// </summary>
        public DateTime? FinishedAt { get; set; }

        /// <summary>
        /// This date/time value indicates when Litmus begun processing this particular result.
        /// </summary>
        public DateTime? StartedAt { get; set; }

        /// <summary>
        /// Identical to the application_code of the testing application used to process this result.
        /// It's presence in the API is a left over from previous versions to ensure backwards compatibility.
        /// We recommend you use the Result/Testing Application/Application code parameter instead of this value.
        /// </summary>
        public string TestCode { get; set; }

        /// <summary>
        /// Similar to the state attribute on the test version, but with subtle differences in the states returned.
        /// Possible values are:
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Indicates whether this result is a page, email or spam result. You will only get spam results if you carried out an email test and may get a mixture of spam and email result_types within a result array.
        /// </summary>
        public string ResultType { get; set; }

        public TestingApplication TestingApplication { get; set; }

        public List<ResultImage> ResultImages { get; set; }
    }
}