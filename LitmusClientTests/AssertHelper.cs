using LitmusClient.Entities;
using NUnit.Framework;
using System;
using System.Text.RegularExpressions;

namespace LitmusClientTests
{
    public static class AssertHelper
    {
        public static string CleanXml(string xml)
        {
            var cleanSpacingRegex = new Regex(@"\s+", RegexOptions.None);
            return cleanSpacingRegex.Replace(xml, "");
        }

        public static void AssertReport(Report report)
        {
            Assert.That(string.IsNullOrEmpty(report.BugHtml), Is.False);
            Assert.That(report.Id, Is.GreaterThan(0));
            Assert.That(string.IsNullOrEmpty(report.Name), Is.False);
        }

        public static void AssertReportContent(ReportContent report)
        {
            AssertReport(report);
            Assert.That(string.IsNullOrEmpty(report.ClientUsage), Is.False);
            Assert.That(string.IsNullOrEmpty(report.ClientEngagement), Is.False);
            Assert.That(string.IsNullOrEmpty(report.Activity), Is.False);
            if (report.PublicSharing.HasValue &&
                report.PublicSharing.Value)
                Assert.That(string.IsNullOrEmpty(report.SharingUrl), Is.False);
        }

        public static void AssertResult(Result result, bool mustHaveImageUrls)
        {
            AssertIncompleteResult(result);
            Assert.That(string.IsNullOrEmpty(result.ResultType), Is.False);
            AssertTestingApplication(result.TestingApplication);
            foreach (var resultImage in result.ResultImages)
                AssertResultImage(resultImage, mustHaveImageUrls);
        }

        public static void AssertTestingApplication(TestingApplication testingApplication)
        {
            Assert.That(string.IsNullOrEmpty(testingApplication.ApplicationCode), Is.Not.True);
            Assert.That(string.IsNullOrEmpty(testingApplication.ApplicationLongName), Is.Not.True);
            Assert.That(string.IsNullOrEmpty(testingApplication.PlatformName), Is.Not.True);
            Assert.That(string.IsNullOrEmpty(testingApplication.ResultType), Is.Not.True);
            Assert.That(string.IsNullOrEmpty(testingApplication.PlatformLongName), Is.Not.True);
            Assert.That(testingApplication.Status, Is.InRange(0, 2));
        }

        public static void AssertTestSet(TestSet testSet, bool mustHaveImageUrls)
        {
            Assert.That(string.IsNullOrEmpty(testSet.Name), Is.False);
            Assert.That(string.IsNullOrEmpty(testSet.State), Is.False);
            Assert.That(string.IsNullOrEmpty(testSet.Service), Is.False);
            Assert.That(string.IsNullOrEmpty(testSet.UrlOrGuid), Is.False);
            Assert.That(testSet.Id, Is.GreaterThan(0));
            Assert.That(testSet.CreatedAt, Is.GreaterThan(new DateTime(2005, 1, 1)));  //Litmus was founded in 2005 so there shouldn't be any tests created before then :-)
            foreach (var testSetVersion in testSet.TestSetVersions)
                AssertTestSetVersion(testSetVersion, mustHaveImageUrls);
        }

        public static void AssertTestSetVersion(TestSetVersion testSetVersion, bool mustHaveImageUrls)
        {
            AssertIncompleteTestSetVersion(testSetVersion);
            Assert.That(string.IsNullOrEmpty(testSetVersion.UrlOrGuid), Is.False);
            foreach (var result in testSetVersion.Results)
                AssertResult(result, mustHaveImageUrls);
        }

        public static void AssertResultImage(ResultImage resultImage, bool mustHaveImageUrls)
        {
            Assert.That(string.IsNullOrEmpty(resultImage.ImageType), Is.False);
            if (mustHaveImageUrls)
            {
                Assert.That(string.IsNullOrEmpty(resultImage.FullImage), Is.False);
                Assert.That(string.IsNullOrEmpty(resultImage.ThumbnailImage), Is.False);
            }
        }

        public static void AssertIncompleteResult(Result result)
        {
            Assert.That(result.Id, Is.GreaterThan(0));
            Assert.That(string.IsNullOrEmpty(result.TestCode), Is.False);
            Assert.That(string.IsNullOrEmpty(result.State), Is.False);
        }

        public static void AssertIncompleteTestSetVersion(TestSetVersion testSetVersion)
        {
            Assert.That(testSetVersion.Version, Is.GreaterThan(0));
        }
    }
}