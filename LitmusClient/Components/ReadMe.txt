The LitmusClient library depends on the excellent RestSharp library (https://github.com/restsharp).  

There is a minor issue with RestSharp and the Litmus Api with respect to taking advantage of RestSharp's auto deserialization.
I believe this issue will be resolved ina future release of RestSharp but for now use the RestSharp.dll included in this
folder as it will resolve the issue.  The source behind this dll can be found at: https://github.com/brendanc/RestSharp