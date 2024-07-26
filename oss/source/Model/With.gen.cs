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
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Autodesk.Oss.Model
{
    /// <summary>
    /// **Not applicable for Head operation**
///The optional information you can request for. To request more than one of the following, specify this parameter multiple times in the request.  Possible values: 
///
///- `createdDate` 
///- `lastAccessedDate` 
///- `lastModifiedDate` 
///- `userDefinedMetadata`
    /// </summary>
    ///<value>**Not applicable for Head operation**
///The optional information you can request for. To request more than one of the following, specify this parameter multiple times in the request.  Possible values: 
///
///- `createdDate` 
///- `lastAccessedDate` 
///- `lastModifiedDate` 
///- `userDefinedMetadata`</value>
    
    [JsonConverter(typeof(StringEnumConverter))]
    
    public enum With
    {
        
        /// <summary>
        /// Enum CreatedDate for value: createdDate
        /// </summary>
        [EnumMember(Value = "createdDate")]
        CreatedDate,
        
        /// <summary>
        /// Enum LastAccessedDate for value: lastAccessedDate
        /// </summary>
        [EnumMember(Value = "lastAccessedDate")]
        LastAccessedDate,
        
        /// <summary>
        /// Enum LastModifiedDate for value: lastModifiedDate
        /// </summary>
        [EnumMember(Value = "lastModifiedDate")]
        LastModifiedDate,
        
        /// <summary>
        /// Enum UserDefinedMetadata for value: userDefinedMetadata
        /// </summary>
        [EnumMember(Value = "userDefinedMetadata")]
        UserDefinedMetadata
    }

}