/* 
 * APS SDK
 *
 * The APS Platform contains an expanding collection of web service components that can be used with Autodesk cloud-based products or your own technologies. Take advantage of Autodesk’s expertise in design and engineering.
 *
 * Model Derivative API
 *
 * Model Derivative Service Documentation
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

namespace Autodesk.ModelDerivative.Model
{
    /// <summary>
    /// JobSvfOutputFormatAdvanced
    /// </summary>
    [DataContract]
    public partial class JobSvf2OutputFormatAdvancedNWD : JobSvf2OutputFormatAdvanced
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobSvfOutputFormatAdvancedNWD" /> class.
        /// </summary>
        public JobSvf2OutputFormatAdvancedNWD()
        {
        }

/// <summary>
        /// Specifies whether hidden objects must be extracted or not. true: Extract hidden objects from the input file. false: (Default) Do not extract hidden objects from the input file. 
        /// </summary>
        /// <value>Specifies whether hidden objects must be extracted or not. true: Extract hidden objects from the input file. false: (Default) Do not extract hidden objects from the input file. </value>
        [DataMember(Name = "hiddenObjects", EmitDefaultValue = false)]
        public bool? HiddenObjects { get; set; }

        /// <summary>
        /// Specifies whether basic material properties must be extracted or not. true: Extract properties for basic materials. false: (Default) Do not extract properties for basic materials. 
        /// </summary>
        /// <value>Specifies whether basic material properties must be extracted or not. true: Extract properties for basic materials. false: (Default) Do not extract properties for basic materials. </value>
        [DataMember(Name = "basicMaterialProperties", EmitDefaultValue = false)]
        public bool? BasicMaterialProperties { get; set; }

        /// <summary>
        /// Specifies how to handle Autodesk material properties. true: Extract properties for Autodesk materials. false: (Default) Do not extract properties for Autodesk materials.
        /// </summary>
        /// <value>Specifies how to handle Autodesk material properties. true: Extract properties for Autodesk materials. false: (Default) Do not extract properties for Autodesk materials.</value>
        [DataMember(Name = "autodeskMaterialProperties", EmitDefaultValue = false)]
        public bool? AutodeskMaterialProperties { get; set; }

        /// <summary>
        /// Specifies whether timeliner properties must be extracted or not. true: Extract timeliner properties. false: (Default) Do not extract timeliner properties.
        /// </summary>
        /// <value>Specifies whether timeliner properties must be extracted or not. true: Extract timeliner properties. false: (Default) Do not extract timeliner properties.</value>
        [DataMember(Name = "timelinerProperties", EmitDefaultValue = false)]
        public bool? TimelinerProperties { get; set; }
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
