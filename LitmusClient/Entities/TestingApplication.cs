namespace LitmusClient.Entities
{
    public class TestingApplication
    {
        /// <summary>
        /// Our testing system uses an algorithm to determine the average time for each client to finish processing, in seconds.
        /// This is the time we use in the Litmus web application to give customers an estimation of how long their test is likely to take based on the clients they have selected.
        /// </summary>
        public int? AverageTimeToProcess { get; set; }

        /// <summary>
        /// Like page clients and the 'popular' parameter, the business parameter is purely used to separate clients visually within the Litmus web application interface, you are free to use this data if you wish.
        /// </summary>
        public bool Business { get; set; }

        /// <summary>
        /// Some email clients block external images from loading immediately, if this parameter is true then the result for this application will have an extra two screenshots returned which show what the email looks like with and without images on.
        /// </summary>
        public bool SupportsContentBlocking { get; set; }

        public bool DesktopClient { get; set; }

        /// <summary>
        /// This parameter will return true if this client is within your user defaults selection. Each user within an account has their own default selection for starting new tests, if a client returns true then it will be tested on if no clients are supplied when starting a new test. Defaults can be changed from within the Litmus application.
        /// </summary>
        public bool Default { get; set; }

        /// <summary>
        /// The Litmus testing infrastructure monitors the status of testing clients and changes this value based on its observations as to whether a client is available or not. The status will be a value 0-2 where: 0 means no problems currently, 1 means there are delays to testing and 2 means a client is unavailable for testing and no results will be returned for this client. You can use the status field to determine which clients you show to end users.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// This parameter is a longer name for the testing application and is normally used for display to the end user.
        /// </summary>
        public string ApplicationLongName { get; set; }

        /// <summary>
        /// This short code is used to identify an application when starting a new test and is passed into the application_codes array within the XML document that gets posted to start a new test.
        /// </summary>
        public string ApplicationCode { get; set; }

        /// <summary>
        /// The short name for the platform that this client runs on. Currently, valid values are: Windows, Mac OS, web-based, Ubuntu, Hosted, Desktop Spam, Mobile devices, Accessibility-based. These values are liable to change in future.
        /// </summary>
        public string PlatformName { get; set; }

        /// <summary>
        /// The longer name for the platform that this client runs on, in some cases these may be the same as the short name where a name was already quite short. Currently, valid values are: Microsoft Windows, Mac OS X, web-based, Ubuntu, Hosted or server-based, Desktop-based Spam Filters, Cell phones and other mobile devices, Accessibility based. These values are liable to change in future.
        /// </summary>
        public string PlatformLongName { get; set; }

        /// <summary>
        /// The type of result this client will return, currently this will either be page or email - this attribute is here for completeness, in practise you will never get a mix of result_type values within the xml you receive back when querying valid clients for a particular service.
        /// </summary>
        public string ResultType { get; set; }
    }
}