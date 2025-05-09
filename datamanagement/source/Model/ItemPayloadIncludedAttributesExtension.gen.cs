/* 
 * APS SDK
 *
 * The Autodesk Platform Services (formerly Forge Platform) contain an expanding collection of web service components that can be used with Autodesk cloud-based products or your own technologies. Take advantage of Autodesk’s expertise in design and engineering.
 *
 * Data Management
 *
 * The Data Management API provides a unified and consistent way to access data across BIM 360 Team, Fusion Team (formerly known as A360 Team), BIM 360 Docs, A360 Personal, and the Object Storage Service.  With this API, you can accomplish a number of workflows, including accessing a Fusion model in Fusion Team and getting an ordered structure of items, IDs, and properties for generating a bill of materials in a 3rd-party process. Or, you might want to superimpose a Fusion model and a building model to use in the Viewer.
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

namespace Autodesk.DataManagement.Model
{
    /// <summary>
    /// A container of additional properties that extends the default properties of this resource.
    /// </summary>
    [DataContract]
    public partial class ItemPayloadIncludedAttributesExtension 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemPayloadIncludedAttributesExtension" /> class.
        /// </summary>
        public ItemPayloadIncludedAttributesExtension()
        {
        }
        
        /// <summary>
        ///The type of the extension.
///
///For BIM 360 Docs files, use `versions:autodesk.bim360:File`.
///
///For A360 composite design files, use `versions:autodesk.a360:CompositeDesign`.
///
///For A360 Personal, Fusion Team, or BIM 360 Team files, use `versions:autodesk.core:File`.
        /// </summary>
        /// <value>
        ///The type of the extension.
///
///For BIM 360 Docs files, use `versions:autodesk.bim360:File`.
///
///For A360 composite design files, use `versions:autodesk.a360:CompositeDesign`.
///
///For A360 Personal, Fusion Team, or BIM 360 Team files, use `versions:autodesk.core:File`.
        /// </value>
        [DataMember(Name="type", EmitDefaultValue=false)]
        public string Type { get; set; }

        /// <summary>
        ///The version of the extension type (`included[i].attributes.extension.type`). The current version is `1.0`.
        /// </summary>
        /// <value>
        ///The version of the extension type (`included[i].attributes.extension.type`). The current version is `1.0`.
        /// </value>
        [DataMember(Name="version", EmitDefaultValue=false)]
        public string VarVersion { get; set; }

        /// <summary>
        ///The container of the additional properties.
///
///The additional properties must follow the schema specified by `extensions.type` and `extensions.version`. Properties that don't follow the schema will be ignored.
        /// </summary>
        /// <value>
        ///The container of the additional properties.
///
///The additional properties must follow the schema specified by `extensions.type` and `extensions.version`. Properties that don't follow the schema will be ignored.
        /// </value>
        [DataMember(Name="data", EmitDefaultValue=false)]
        public Dictionary<string, Object> Data { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }

}
