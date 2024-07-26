/* 
 * APS SDK
 *
 * The Forge Platform contains an expanding collection of web service components that can be used with Autodesk cloud-based products or your own technologies. Take advantage of Autodesk’s expertise in design and engineering.
 *
 * Authentication
 *
 * OAuth2 token management APIs.
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

namespace Autodesk.Authentication.Model
{
    /// <summary>
    /// Represents the payload returned in response to a client credentials grant request.
    /// </summary>
    [DataContract]
    public partial class TwoLeggedToken
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TwoLeggedToken" /> class.
        /// </summary>


        public TwoLeggedToken()
        {

        }

        private int? _expiresIn;

        private long? _expiresAt;
        /// <summary>
        ///The access token.
        /// </summary>
        /// <value>
        ///The access token.
        /// </value>
        [DataMember(Name = "access_token", EmitDefaultValue = false)]
        public string AccessToken { get; set; }

        /// <summary>
        ///Will always be Bearer.
        /// </summary>
        /// <value>
        ///Will always be Bearer.
        /// </value>
        [DataMember(Name = "token_type", EmitDefaultValue = false)]
        public string TokenType { get; set; }

        /// <summary>
        ///Access token expiration time (in seconds).
        /// </summary>
        /// <value>
        ///Access token expiration time (in seconds).
        /// </value>
        [DataMember(Name = "expires_in", EmitDefaultValue = false)]
        public int? ExpiresIn
        {
            get { return _expiresIn; }
            set
            {
                _expiresIn = value;
                _expiresAt = DateTimeOffset.Now.ToUnixTimeSeconds() + _expiresIn;

            }
        }

        /// <summary>
        ///Access token expiration time, in Unix seconds.
        /// </summary>
        /// <value>
        ///Access token expiration time, in Unix seconds.
        /// </value>
        [DataMember(Name = "expires_at", EmitDefaultValue = false)]
        public long? ExpiresAt { get { return _expiresAt; } }


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
