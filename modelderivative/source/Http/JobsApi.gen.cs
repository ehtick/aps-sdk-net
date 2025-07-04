/* 
 * APS SDK
 *
 * The APS Platform contains an expanding collection of web service components that can be used with Autodesk cloud-based products or your own technologies. Take advantage of Autodesk’s expertise in design and engineering.
 *
 * Model Derivative
 *
 * Use the Model Derivative API to translate designs from one CAD format to another. You can also use this API to extract metadata from a model.
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
using Autodesk.Forge.Core;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using Autodesk.ModelDerivative.Model;
using Autodesk.ModelDerivative.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Autodesk.SDKManager;

namespace Autodesk.ModelDerivative.Http
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IJobsApi
    {
        /// <summary>
        /// Specify References
        /// </summary>
        /// <remarks>
        ///Specifies the location of the files referenced by the specified source design.
        ///
        ///When you call [Create Translation Job](/en/docs/model-derivative/v2/reference/http/job-POST), set  `checkReferences` to `true`.   The Model Derivative service will then use the details you specify in this operation to locate and download the referenced files.
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="urn">
        ///The Base64 (URL Safe) encoded design URN.
        /// </param>
        /// <param name="region">
        ///Specifies the data center where the manifest and derivatives of the specified source design are stored. Possible values are:
        ///
        ///- `US` - (Default) Data center for the US region.
        ///- `EMEA` - Data center for the European Union, Middle East, and Africa. 
        ///- `AUS` - (Beta) Data center for the Australia region.
        ///- `CAN` : Data center for the Canada region.
        ///- `DEU` : Data center for the Germany region.
        ///- `IND` : Data center for the India region.
        ///- `JPN` : Data center for the Japan region.
        ///- `GBR`  : Data center for the United Kingdom region.
        ///
        ///**Note**: Beta features are subject to change. Please avoid using them in production environments. (optional)
        /// </param>
        /// <param name="specifyReferencesPayload">
        /// (optional)
        /// </param>
        /// <returns>Task of ApiResponse&lt;SpecifyReferences&gt;</returns>

        System.Threading.Tasks.Task<ApiResponse<SpecifyReferences>> SpecifyReferencesAsync(string urn, Region? region = null, SpecifyReferencesPayload specifyReferencesPayload = default(SpecifyReferencesPayload), string accessToken = null, bool throwOnError = true);
        /// <summary>
        /// Create Translation Job
        /// </summary>
        /// <remarks>
        ///Creates a job to translate the specified source design to another format, generating derivatives of the source design. You can optionaly:
        ///
        ///- Extract selected parts of a design and export the set of geometries in OBJ format.
        ///- Generate different-sized thumbnails.
        ///
        ///When the translation job runs, details about the process and generated derivatives are logged to a JSON file known as a manifest. This manifest is typically created during the first translation of a source design. Subsequently, the system updates the same manifest whenever a translation is performed for that design. 
        ///
        ///If necessary, you can set the `x-ads-force` parameter to `true`. Then, the system will delete the existing manifest and create a new one. However, be aware that doing so will also delete all previously generated derivatives.
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="xAdsForce">
        ///`true`: Forces the system to parse property data all over again. Use this option to retrieve an object tree when previous attempts have failed.
        ///
        ///`false`: (Default) Use previously parsed property data to extract the object tree. (optional)
        /// </param>
        /// <param name="xAdsDerivativeFormat">
        ///Specifies what Object IDs to return, if the design has legacy SVF derivatives generated by the BIM Docs service. Possible values are:  
        ///
        ///- `latest` - (Default) Return SVF2 Object IDs. 
        ///- `fallback` - Return SVF Object IDs.  
        ///
        ///**Note:**  
        ///
        ///1. This parameter applies only to designs with legacy SVF derivatives generated by the BIM 360 Docs service. 
        ///2. The BIM 360 Docs service now generates SVF2 derivatives. SVF2 Object IDs may not be compatible with the SVF Object IDs previously generated by the BIM 360 Docs service. Setting this header to fallback may resolve backward compatibility issues resulting from Object IDs of legacy SVF derivatives being retained on the client side. 
        ///3. If you use this parameter with one derivative (URN), you must use it consistently across the following: 
        ///
        ///   - [Create Translation Job](/en/docs/model-derivative/v2/reference/http/job-POST) (for OBJ output) 
        ///   - [Fetch Object Tree](/en/docs/model-derivative/v2/reference/http/urn-metadata-modelguid-GET)
        ///   - [Fetch All Properties](/en/docs/model-derivative/v2/reference/http/urn-metadata-guid-properties-GET)
        ///   - [Fetch Specific Properties](en/docs/model-derivative/v2/reference/http/metadata/urn-metadata-guid-properties-query-POST) (optional)
        /// </param>
        /// <param name="region">
        ///Specifies the data center where the manifest and derivatives of the specified source design are stored. Possible values are:
        ///
        ///- `US` - (Default) Data center for the US region.
        ///- `EMEA` - Data center for the European Union, Middle East, and Africa. 
        ///- `AUS` - (Beta) Data center for the Australia region.
        ///- `CAN` : Data center for the Canada region.
        ///- `DEU` : Data center for the Germany region.
        ///- `IND` : Data center for the India region.
        ///- `JPN` : Data center for the Japan region.
        ///- `GBR`  : Data center for the United Kingdom region.
        ///
        ///**Note**: Beta features are subject to change. Please avoid using them in production environments. (optional)
        /// </param>
        /// <param name="jobPayload">
        /// (optional)
        /// </param>
        /// <returns>Task of ApiResponse&lt;Job&gt;</returns>

        System.Threading.Tasks.Task<ApiResponse<Job>> StartJobAsync(bool? xAdsForce = default(bool?), XAdsDerivativeFormat? xAdsDerivativeFormat = null, Region? region = null, JobPayload jobPayload = default(JobPayload), string accessToken = null, bool throwOnError = true);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class JobsApi : IJobsApi
    {
        ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="JobsApi"/> class
        /// using SDKManager object
        /// </summary>
        /// <param name="sdkManager">An instance of SDKManager</param>
        /// <returns></returns>
        public JobsApi(SDKManager.SDKManager sdkManager)
        {
            this.Service = sdkManager.ApsClient.Service;
            this.logger = sdkManager.Logger;
        }
        private void SetQueryParameter(string name, object value, Dictionary<string, object> dictionary)
        {
            if (value is Enum)
            {
                var type = value.GetType();
                var memberInfos = type.GetMember(value.ToString());
                var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == type);
                var valueAttributes = enumValueMemberInfo.GetCustomAttributes(typeof(EnumMemberAttribute), false);
                if (valueAttributes.Length > 0)
                {
                    dictionary.Add(name, ((EnumMemberAttribute)valueAttributes[0]).Value);
                }
            }
            else if (value is int)
            {
                if ((int)value > 0)
                {
                    dictionary.Add(name, value);
                }
            }
            else
            {
                if (value != null)
                {
                    dictionary.Add(name, value);
                }
            }
        }
        private void SetHeader(string baseName, object value, HttpRequestMessage req)
        {
            if (value is DateTime)
            {
                if ((DateTime)value != DateTime.MinValue)
                {
                    req.Headers.TryAddWithoutValidation(baseName, LocalMarshalling.ParameterToString(value)); // header parameter
                }
            }
            else
            {
                if (value != null)
                {
                    if (!string.Equals(baseName, "Content-Range"))
                    {
                        req.Headers.TryAddWithoutValidation(baseName, LocalMarshalling.ParameterToString(value)); // header parameter
                    }
                    else
                    {
                        req.Content.Headers.Add(baseName, LocalMarshalling.ParameterToString(value));
                    }
                }
            }

        }

        /// <summary>
        /// Gets or sets the ApsConfiguration object
        /// </summary>
        /// <value>An instance of the ForgeService</value>
        public ForgeService Service { get; set; }

        /// <summary>
        /// Specify References
        /// </summary>
        /// <remarks>
        ///Specifies the location of the files referenced by the specified source design.
        ///
        ///When you call [Create Translation Job](/en/docs/model-derivative/v2/reference/http/job-POST), set  `checkReferences` to `true`.   The Model Derivative service will then use the details you specify in this operation to locate and download the referenced files.
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="urn">
        ///The Base64 (URL Safe) encoded design URN.
        /// </param>
        /// <param name="region">
        ///Specifies the data center where the manifest and derivatives of the specified source design are stored. Possible values are:
        ///
        ///- `US` - (Default) Data center for the US region.
        ///- `EMEA` - Data center for the European Union, Middle East, and Africa. 
        ///- `AUS` - (Beta) Data center for the Australia region.
        ///- `CAN` : Data center for the Canada region.
        ///- `DEU` : Data center for the Germany region.
        ///- `IND` : Data center for the India region.
        ///- `JPN` : Data center for the Japan region.
        ///- `GBR`  : Data center for the United Kingdom region.
        ///
        ///**Note**: Beta features are subject to change. Please avoid using them in production environments. (optional)
        /// </param>
        /// <param name="specifyReferencesPayload">
        /// (optional)
        /// </param>
        /// <returns>Task of ApiResponse&lt;SpecifyReferences&gt;></returns>

        public async System.Threading.Tasks.Task<ApiResponse<SpecifyReferences>> SpecifyReferencesAsync(string urn, Region? region = null, SpecifyReferencesPayload specifyReferencesPayload = default(SpecifyReferencesPayload), string accessToken = null, bool throwOnError = true)
        {
            logger.LogInformation("Entered into SpecifyReferencesAsync ");
            using (var request = new HttpRequestMessage())
            {
                var queryParam = new Dictionary<string, object>();
                request.RequestUri =
                    Marshalling.BuildRequestUri("/modelderivative/v2/designdata/{urn}/references",
                        routeParameters: new Dictionary<string, object> {
                            { "urn", urn},
                        },
                        queryParameters: queryParam
                    );

                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("User-Agent", "APS SDK/MODEL DERIVATIVE/C#/2.0.0");
                if (!string.IsNullOrEmpty(accessToken))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");
                }

                request.Content = Marshalling.Serialize(specifyReferencesPayload); // http body (model) parameter


                SetHeader("region", region, request);

                // tell the underlying pipeline what scope we'd like to use
                // if (scopes == null)
                // {
                // TBD:Naren FORCE-4027 - If accessToken is null, acquire auth token using auth SDK, with defined scope.
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), "data:read data:write data:create ");
                // }
                // else
                // {
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), scopes);
                // }
                // if (scopes == null)
                // {
                // TBD:Naren FORCE-4027 - If accessToken is null, acquire auth token using auth SDK, with defined scope.
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), "data:read data:write data:create ");
                // }
                // else
                // {
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), scopes);
                // }

                request.Method = new HttpMethod("POST");

                // make the HTTP request
                var response = await this.Service.Client.SendAsync(request);

                if (throwOnError)
                {
                    try
                    {
                        await response.EnsureSuccessStatusCodeAsync();
                    }
                    catch (HttpRequestException ex)
                    {
                        throw new ModelDerivativeApiException(ex.Message, response, ex);
                    }
                }
                else if (!response.IsSuccessStatusCode)
                {
                    logger.LogError($"response unsuccess with status code: {response.StatusCode}");
                    return new ApiResponse<SpecifyReferences>(response, default(SpecifyReferences));
                }
                logger.LogInformation($"Exited from SpecifyReferencesAsync with response statusCode: {response.StatusCode}");
                return new ApiResponse<SpecifyReferences>(response, await LocalMarshalling.DeserializeAsync<SpecifyReferences>(response.Content));

            } // using
        }
        /// <summary>
        /// Create Translation Job
        /// </summary>
        /// <remarks>
        ///Creates a job to translate the specified source design to another format, generating derivatives of the source design. You can optionaly:
        ///
        ///- Extract selected parts of a design and export the set of geometries in OBJ format.
        ///- Generate different-sized thumbnails.
        ///
        ///When the translation job runs, details about the process and generated derivatives are logged to a JSON file known as a manifest. This manifest is typically created during the first translation of a source design. Subsequently, the system updates the same manifest whenever a translation is performed for that design. 
        ///
        ///If necessary, you can set the `x-ads-force` parameter to `true`. Then, the system will delete the existing manifest and create a new one. However, be aware that doing so will also delete all previously generated derivatives.
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="xAdsForce">
        ///`true`: Forces the system to parse property data all over again. Use this option to retrieve an object tree when previous attempts have failed.
        ///
        ///`false`: (Default) Use previously parsed property data to extract the object tree. (optional)
        /// </param>
        /// <param name="xAdsDerivativeFormat">
        ///Specifies what Object IDs to return, if the design has legacy SVF derivatives generated by the BIM Docs service. Possible values are:  
        ///
        ///- `latest` - (Default) Return SVF2 Object IDs. 
        ///- `fallback` - Return SVF Object IDs.  
        ///
        ///**Note:**  
        ///
        ///1. This parameter applies only to designs with legacy SVF derivatives generated by the BIM 360 Docs service. 
        ///2. The BIM 360 Docs service now generates SVF2 derivatives. SVF2 Object IDs may not be compatible with the SVF Object IDs previously generated by the BIM 360 Docs service. Setting this header to fallback may resolve backward compatibility issues resulting from Object IDs of legacy SVF derivatives being retained on the client side. 
        ///3. If you use this parameter with one derivative (URN), you must use it consistently across the following: 
        ///
        ///   - [Create Translation Job](/en/docs/model-derivative/v2/reference/http/job-POST) (for OBJ output) 
        ///   - [Fetch Object Tree](/en/docs/model-derivative/v2/reference/http/urn-metadata-modelguid-GET)
        ///   - [Fetch All Properties](/en/docs/model-derivative/v2/reference/http/urn-metadata-guid-properties-GET)
        ///   - [Fetch Specific Properties](en/docs/model-derivative/v2/reference/http/metadata/urn-metadata-guid-properties-query-POST) (optional)
        /// </param>
        /// <param name="region">
        ///Specifies the data center where the manifest and derivatives of the specified source design are stored. Possible values are:
        ///
        ///- `US` - (Default) Data center for the US region.
        ///- `EMEA` - Data center for the European Union, Middle East, and Africa. 
        ///- `AUS` - (Beta) Data center for the Australia region.
        ///- `CAN` : Data center for the Canada region.
        ///- `DEU` : Data center for the Germany region.
        ///- `IND` : Data center for the India region.
        ///- `JPN` : Data center for the Japan region.
        ///- `GBR`  : Data center for the United Kingdom region.
        ///
        ///**Note**: Beta features are subject to change. Please avoid using them in production environments. (optional)
        /// </param>
        /// <param name="jobPayload">
        /// (optional)
        /// </param>
        /// <returns>Task of ApiResponse&lt;Job&gt;></returns>

        public async System.Threading.Tasks.Task<ApiResponse<Job>> StartJobAsync(bool? xAdsForce = default(bool?), XAdsDerivativeFormat? xAdsDerivativeFormat = null, Region? region = null, JobPayload jobPayload = default(JobPayload), string accessToken = null, bool throwOnError = true)
        {
            logger.LogInformation("Entered into StartJobAsync ");
            using (var request = new HttpRequestMessage())
            {
                var queryParam = new Dictionary<string, object>();
                request.RequestUri =
                    Marshalling.BuildRequestUri("/modelderivative/v2/designdata/job",
                        routeParameters: new Dictionary<string, object>
                        {
                        },
                        queryParameters: queryParam
                    );

                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("User-Agent", "APS SDK/MODEL DERIVATIVE/C#/2.0.0");
                if (!string.IsNullOrEmpty(accessToken))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");
                }

                request.Content = Marshalling.Serialize(jobPayload); // http body (model) parameter


                SetHeader("x-ads-force", xAdsForce, request);
                SetHeader("x-ads-derivative-format", xAdsDerivativeFormat, request);
                SetHeader("region", region, request);

                // tell the underlying pipeline what scope we'd like to use
                // if (scopes == null)
                // {
                // TBD:Naren FORCE-4027 - If accessToken is null, acquire auth token using auth SDK, with defined scope.
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), "data:read data:write data:create ");
                // }
                // else
                // {
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), scopes);
                // }
                // if (scopes == null)
                // {
                // TBD:Naren FORCE-4027 - If accessToken is null, acquire auth token using auth SDK, with defined scope.
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), "data:read data:write data:create ");
                // }
                // else
                // {
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), scopes);
                // }

                request.Method = new HttpMethod("POST");

                // make the HTTP request
                var response = await this.Service.Client.SendAsync(request);

                if (throwOnError)
                {
                    try
                    {
                        await response.EnsureSuccessStatusCodeAsync();
                    }
                    catch (HttpRequestException ex)
                    {
                        throw new ModelDerivativeApiException(ex.Message, response, ex);
                    }
                }
                else if (!response.IsSuccessStatusCode)
                {
                    logger.LogError($"response unsuccess with status code: {response.StatusCode}");
                    return new ApiResponse<Job>(response, default(Job));
                }
                logger.LogInformation($"Exited from StartJobAsync with response statusCode: {response.StatusCode}");
                return new ApiResponse<Job>(response, await LocalMarshalling.DeserializeAsync<Job>(response.Content));

            } // using
        }
    }
}
