/* 
 * APS SDK
 *
 * The APS Platform contains an expanding collection of web service components that can be used with Autodesk cloud-based products or your own technologies. Take advantage of Autodesk’s expertise in design and engineering.
 *
 * oss
 *
 * The Object Storage Service (OSS) allows your application to download and upload raw files (such as PDF, XLS, DWG, or RVT) that are managed by the Data service.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Net.Http;

namespace Autodesk.Oss
{
  /// <summary>
  /// An object that is returned when an API call fails.
  /// </summary>
  public abstract class ServiceApiException : HttpRequestException
  {
    public HttpResponseMessage HttpResponseMessage {get; set;}

    public ServiceApiException(string message) : base(message) {}
    public ServiceApiException(string message, HttpResponseMessage httpResponseMessage, Exception exception) : base(message, exception)
    {
      this.HttpResponseMessage = httpResponseMessage;
    }
  }

 /// <summary>
  /// An object that is returned when an API call to the Oss service fails.
  /// </summary>
  public class OssApiException : ServiceApiException
  {
    public OssApiException(string message) : base(message) {}
    public OssApiException(string message, HttpResponseMessage httpResponseMessage, Exception exception) : base(message, httpResponseMessage, exception) {}
  }
}
