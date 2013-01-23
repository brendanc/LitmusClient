LitmusClient
============

LitmusClient is a c# wrapper for Litmus's Customer API.  It is currently under development.  It uses RestSharp (https://github.com/restsharp/RestSharp) to wrap Litmus's REST API.

You will need your own Litmus account to use this library which you can get here:  http://litmus.com/pricing

**How you can use it:**

_Create a Litmus EmailTest_

```c#
  var client = new LitmusApi(new Account("YourSubDomain", "YourUserName, "YourPassword")); 
  var emailClients = new List<TestingApplication>();
  emailClients.Add(new TestingApplication() { ApplicationCode = "hotmail", ResultType = "email" });
  emailClients.Add(new TestingApplication() { ApplicationCode = "gmailnew", ResultType = "email" });
  emailClients.Add(new TestingApplication() { ApplicationCode = "notes8", ResultType = "email" });
  var subject = string.Format("Test email created by c# wrapper on {0}", DateTime.Now);
  var test = client.CreateEmailTest(emailClients, subject, "<html><body><p>This is a kitten:</p><img src=\"http://placekitten.com/200/300\" alt=\"kitten\"></img></body></html>");

```

_Create a Litmus Page Test_ 

```c#
  var client = new LitmusApi(new Account("YourSubDomain", "YourUserName, "YourPassword"));            
  var pageClients = new List<TestingApplication>();
  pageClients.Add(new TestingApplication() { ApplicationCode = "chrome2", ResultType = "page" });
  pageClients.Add(new TestingApplication() { ApplicationCode = "ie7", ResultType = "page" });
  pageClients.Add(new TestingApplication() { ApplicationCode = "ie6", ResultType = "page" });
  var url = "http://github.com";
  var test = client.CreatePageTest(pageClients, url);

```

_If you just want to use this library in your application:_

- Download it from NuGet:  http://nuget.org/packages/LitmusClient/


**References:**

-  Litmus Docs: http://docs.litmus.com/w/page/18056603/Customer%20API%20documentation
-  Litmus Customer Api liscensing terms:  http://docs.litmus.com/w/page/18056620/Customer%20API%3A%20Getting%20Started#licensing-terms
-  Uses the excellent RestSharp library from https://github.com/restsharp
-  Currently it's awaiting resolution of a small RestSharp issue (https://github.com/restsharp/RestSharp/issues/269).  Until that is resolved use the RestSharp.dll included in the Components directory.
 
