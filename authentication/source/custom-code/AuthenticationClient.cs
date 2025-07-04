using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Authentication.Http;
using Autodesk.Authentication.Model;
using Autodesk.Authentication.Client;
using System.Net.Http;
using System.Net.Cache;
using System.Text.Json.Nodes;
using Autodesk.SDKManager;

namespace Autodesk.Authentication
{

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    /// 

    public class AuthenticationClient
    {
        /// <summary>
        /// Gets the token API instance.
        /// </summary>
        public ITokenApi tokenApi { get; }
        /// <summary>
        /// Gets the users API instance.
        /// </summary>
        public IUsersApi usersApi { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationClient"/> class.
        /// </summary>
        /// <param name="sdkManager">The SDK manager instance.</param>
        public AuthenticationClient(SDKManager.SDKManager sdkManager = default)
        {
            if (sdkManager == null)
            {
                sdkManager = SdkManagerBuilder.Create().Build();
            }
            this.tokenApi = new TokenApi(sdkManager);
            this.usersApi = new UsersApi(sdkManager);
        }


        #region UsersApi
        /// <summary>
        /// Get User Info
        /// </summary>
        /// <remarks>
        ///Retrieves information about the authenticated user.
        /// </remarks>
        /// <exception cref="AuthenticationApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">
        /// The 3-legged access token of the currently logged in user.
        /// </param>
        /// <param name="throwOnError">
        /// Indicates whether to throw an exception on error.
        /// </param>
        /// <returns>Task of ApiResponse&lt;UserInfo&gt;</returns>

        public async System.Threading.Tasks.Task<UserInfo> GetUserInfoAsync(string authorization, bool throwOnError = true)
        {
            var response = await this.usersApi.GetUserInfoAsync(authorization, throwOnError);
            return response.Content;
        }

        #endregion UsersApi

        #region tokenapi

        /// <summary>
        /// Acquire Two Legged Token
        /// </summary>
        /// <remarks>
        ///Returns a 2-legged access token.
        ///This operation has a rate limit of 500 calls per minute.
        /// </remarks>
        /// <exception cref="AuthenticationApiException">Thrown when fails to make API call</exception>
        /// <param name="clientId">
        ///The Client ID of the calling application, as registered with APS.
        /// </param>
        /// <param name="clientSecret">
        ///The Client secret of the calling application, as registered with APS.
        /// </param>       
        /// <param name="scopes">
        ///A list of requested scopes. See the [Developer's Guide documentation on scopes](/en/docs/oauth/v2/developers_guide/scopes/) for a list of valid values you can provide.
        /// </param>
        /// <param name="throwOnError">
        /// Indicates whether to throw an exception on error.
        /// </param>
        /// <returns>Task of &lt;TwoLeggedToken&gt;</returns>
        public async System.Threading.Tasks.Task<TwoLeggedToken> GetTwoLeggedTokenAsync(string clientId, string clientSecret, List<Scopes> scopes, bool throwOnError = true)
        {
            var clientIdSecret = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
            var response = await this.tokenApi.FetchTokenAsync(authorization: clientIdSecret, scopes: scopes, grantType: GrantType.ClientCredentials, throwOnError: throwOnError);
            return await LocalMarshalling.DeserializeAsync<TwoLeggedToken>(response.Content);
        }

        /// <summary>
        /// Authorize User
        /// </summary>
        /// <remarks>
        ///Returns a browser URL to redirect an end user in order to acquire the user’s consent to authorize the application to access resources on their behalf.
        ///
        ///Invoking this operation is the first step in authenticating users and retrieving an authorization code grant. The authorization code that is generated remains valid for 5 minutes, while the ID token stays valid for 60 minutes. Any access tokens you obtain are valid for 60 minutes, and refresh tokens remain valid for 15 days.
        ///
        ///This operation has a rate limit of 500 calls per minute.
        ///
        ///**Note:** This operation is intended for use with client-side applications only. It is not suitable for server-side applications.
        /// </remarks>
        /// <param name="clientId">
        ///The Client ID of the calling application, as registered with APS.
        /// </param>
        /// <param name="responseType">
        ///The type of response you want to receive. Possible values are: 
        ///
        ///-  `code` - Authorization code grant.
        ///-  `id_token` - OpenID Connect ID token.
        /// </param>
        /// <param name="redirectUri">
        ///The URI that APS redirects users to after they grant or deny access permission to the application. Must match the Callback URL for the application as registered with APS.
        ///
        ///Must be specified as a URL-safe string. It can include query parameters or any other valid URL construct.
        /// </param>
        /// <param name="nonce">
        ///A random string that is sent with the request. APS passes back the same string to you so that you can verify whether you received the same string that you sent. This check mitigates token replay attacks (optional)
        /// </param>
        /// <param name="state">
        ///A URL-encoded random string. The authorization flow will pass the same string back to the Callback URL using the `state` query string parameter. This process helps ensure that the callback you receive is a response to what you originally requested. It prevents malicious actors from forging requests.
        ///
        ///The string can only contain alphanumeric characters, commas, periods, underscores, and hyphens.             (optional)
        /// </param>
        /// <param name="scopes">
        ///A URL-encoded space-delimited list of requested scopes. See the [Developer's Guide documentation on scopes](/en/docs/oauth/v2/developers_guide/scopes/) for a list of valid values you can provide.
        /// </param>
        /// <param name="responseMode">
        ///Specifies how the authorization response should be returned. Valid values are:
        ///
        ///- `fragment` - Encode the response parameters in the fragment of the redirect URI. A fragment in a URI is the optional part of the URI that appears after a `#` symbol, which refers to a specific section within a resource. For example, `section` in `https://www.mysite.org/myresource#section`.
        ///- `form_post` - Embed the authorization response parameter in an HTML form.
        ///- `query` -  Embed the authorization response as a query string parameter of the redirect URI. 
        ///
        ///If `id_token` is stated as `response_type`,  only `form_post` is allowed as `response_mode`.' (optional)
        /// </param>
        /// <param name="prompt">
        ///Specifies how to prompt users for authentication. Possible values are: 
        ///
        ///- `login` : Always prompt the user for authentication, regardless of the state of the login session. 
        ///
        ///**Note:** If you do not specify this parameter, the system will not prompt the user for authentication as long as a login session is active. If a login session is not active, the system will prompt the user for authentication. (optional)
        /// </param>
        /// <param name="authoptions">
        ///A JSON object containing options that specify how to display the sign-in page. Refer the [Developer's Guide documentation on AuthOptions](/en/docs/oauth/v2/developers_guide/authoptions/) for supported values. (optional)
        /// </param>
        /// <param name="codeChallenge">
        ///A URL-encoded string derived from the code verifier sent in the authorization request with the Proof Key for Code Exchange (PKCE) grant flow. (optional)
        /// </param>
        /// <param name="codeChallengeMethod">
        ///The method used to derive the code challenge for the PKCE grant flow. Possible value is:
        ///
        ///- `S256`- Hashes the code verifier using the SHA-256 algorithm and then applies Base64 URL encoding. (optional)
        /// </param>
        /// <returns>string</returns>
        public string Authorize(string clientId, ResponseType responseType, string redirectUri, List<Scopes> scopes, string nonce = default(string), string state = default(string), string responseMode = default(string), string prompt = default(string), string authoptions = default(string), string codeChallenge = default(string), string codeChallengeMethod = default(string))
        {
            return this.tokenApi.Authorize(clientId, responseType, redirectUri, nonce, state, scopes, responseMode, prompt, authoptions, codeChallenge, codeChallengeMethod);
        }

        /// <summary>
        /// Acquire Three Legged Token
        /// </summary>
        /// <remarks>
        /// Returns a 3-legged access token.
        ///This operation has a rate limit of 500 calls per minute.
        /// </remarks>
        /// <exception cref="AuthenticationApiException">Thrown when fails to make API call</exception>       
        /// <param name="clientId">
        ///The Client ID of the calling application, as registered with APS.
        /// </param>
        /// <param name="clientSecret">
        ///The Client secret of the calling application, as registered with APS.
        ///**Note** The clientSecret is required only for Traditional Web Apps and Server-to-Server Apps. It is not required for Desktop, Mobile, and Single-Page Apps. (optional)
        /// </param>       
        /// <param name="code">
        ///The authorization code that was passed to your application when the user granted access permission to your application. It was passed as the `code` query parameter to the redirect URI when you called [Authorize User](/en/docs/oauth/v2/reference/http/authorize-GET/).   
        /// </param>
        /// <param name="redirectUri">
        ///The URI that APS redirects users to after they grant or deny access permission to the application. Must match the Callback URL for the application registered with APS.   
        /// </param>
        /// <param name="codeVerifier">
        ///A random URL-encoded string between 43 characters and 128 characters. In a PKCE grant flow, the authentication server uses this string to verify the code challenge that was passed when you called [Authorize User](/en/docs/oauth/v2/reference/http/authorize-GET/).    Required if [`code_challenge` was specified when you called [Authorize User](/en/docs/oauth/v2/reference/http/authorize-GET/).  (optional)
        /// </param> 
        /// <param name="throwOnError">
        /// Indicates whether to throw an exception on error.
        /// </param>
        /// <returns>Task of &lt;ThreeLeggedToken&gt;</returns>
        public async System.Threading.Tasks.Task<ThreeLeggedToken> GetThreeLeggedTokenAsync(string clientId, string code, string redirectUri, string clientSecret = default(string), string codeVerifier = default(string), bool throwOnError = true)
        {
            if (!string.IsNullOrEmpty(clientSecret))
            { // for private clients
                var clientIdSecret = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
                var response = await this.tokenApi.FetchTokenAsync(authorization: clientIdSecret, code: code, grantType: GrantType.AuthorizationCode, redirectUri: redirectUri, codeVerifier: codeVerifier, throwOnError: throwOnError);
                return await LocalMarshalling.DeserializeAsync<ThreeLeggedToken>(response.Content);

            }
            else
            {   // for public clients
                var response = await this.tokenApi.FetchTokenAsync(clientId: clientId, code: code, grantType: GrantType.AuthorizationCode, redirectUri: redirectUri, codeVerifier: codeVerifier, throwOnError: throwOnError);
                return await LocalMarshalling.DeserializeAsync<ThreeLeggedToken>(response.Content);

            }
        }

        /// <summary>
        /// Acquire Refresh Token
        /// </summary>
        /// <remarks>
        ///Returns a new 3-legged access token using the refresh token provided in the request.
        ///This operation has a rate limit of 500 calls per minute.
        /// </remarks>
        /// <exception cref="AuthenticationApiException">Thrown when fails to make API call</exception>       
        /// <param name="clientId">
        ///The Client ID of the calling application, as registered with APS.
        /// </param>
        /// <param name="clientSecret">
        ///The Client secret of the calling application, as registered with APS.
        ///**Note** The clientSecret is required only for Traditional Web Apps and Server-to-Server Apps. It is not required for Desktop, Mobile, and Single-Page Apps. (optional)
        /// </param>             
        /// <param name="refreshToken">
        ///The refresh token used to acquire a new access token and a refresh token.     
        /// </param> 
        /// <param name="scopes">
        ///A URL-encoded space-delimited list of requested scopes. See the [Developer's Guide documentation on scopes](/en/docs/oauth/v2/developers_guide/scopes/) for a list of valid values you can provide.
        ///If specified, scopes have to be primarily same with or a subset of the scopes used to generate the refresh_token.(optional)
        /// </param>   
        /// <param name="throwOnError">
        /// Indicates whether to throw an exception on error.
        /// </param>
        /// <returns>Task of  &lt;ThreeLeggedToken&gt;</returns>
        public async System.Threading.Tasks.Task<ThreeLeggedToken> RefreshTokenAsync(string refreshToken, string clientId, string clientSecret = default(string), List<Scopes> scopes = null, bool throwOnError = true)
        {

            if (!string.IsNullOrEmpty(clientSecret))
            {   // for private clients
                var clientIdSecret = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
                var response = await this.tokenApi.FetchTokenAsync(authorization: clientIdSecret, scopes: scopes, grantType: GrantType.RefreshToken, refreshToken: refreshToken, throwOnError: throwOnError);
                return await LocalMarshalling.DeserializeAsync<ThreeLeggedToken>(response.Content);
            }
            else
            {   // for public clients
                var response = await this.tokenApi.FetchTokenAsync(clientId: clientId, scopes: scopes, grantType: GrantType.RefreshToken, refreshToken: refreshToken, throwOnError: throwOnError);
                return await LocalMarshalling.DeserializeAsync<ThreeLeggedToken>(response.Content);

            }
        }




        /// <summary>
        /// Get JWKS
        /// </summary>
        /// <remarks>
        ///Returns a set of public keys in the JSON Web Key Set (JWKS) format.
        ///
        ///Public keys returned by this operation can be used to validate the asymmetric JWT signature of an access token without making network calls. It can be used to validate both two-legged access tokens and three-legged access tokens. 
        ///
        ///See the Developer's Guide topic on [Asymmetric Signing](/en/docs/oauth/v2/developers_guide/asymmetric-encryption/) for more information. 
        /// </remarks>
        /// <exception cref="AuthenticationApiException">Thrown when fails to make API call</exception>
        /// <param name="throwOnError">
        /// Indicates whether to throw an exception on error.
        /// </param>
        /// <returns>Task of &lt;Jwks&gt;></returns>

        public async System.Threading.Tasks.Task<Jwks> GetKeysAsync(bool throwOnError = true)
        {
            var response = await this.tokenApi.GetKeysAsync(throwOnError);
            return response.Content;
        }

        /// <summary>
        /// Get OIDC Specification
        /// </summary>
        /// <remarks>
        ///Returns an OpenID Connect Discovery Specification compliant JSON document. It contains a list of the OpenID/OAuth endpoints, supported scopes, claims, public keys used to sign the tokens, and other details.
        /// </remarks>
        /// <exception cref="AuthenticationApiException">Thrown when fails to make API call</exception>
        /// <param name="throwOnError">
        /// Indicates whether to throw an exception on error.
        /// </param>
        /// <returns>Task of &lt;OidcSpec&gt;></returns>
        public async System.Threading.Tasks.Task<OidcSpec> GetOidcSpecAsync(bool throwOnError = true)
        {
            var response = await this.tokenApi.GetOidcSpecAsync(throwOnError);
            return response.Content;
        }


        /// <summary>
        /// Introspect Token
        /// </summary>
        /// <remarks>
        ///Returns metadata about the specified access token or reference token.
        ///
        ///An application can only introspect its own tokens.
        ///
        ///This operation has a rate limit of 500 calls per minute.
        /// </remarks>
        /// <exception cref="AuthenticationApiException">Thrown when fails to make API call</exception>
        /// <param name="clientSecret">
        ///The Client secret of the calling application, as registered with APS.
        ///**Note** The clientSecret is required only for Traditional Web Apps and Server-to-Server Apps. It is not required for Desktop, Mobile, and Single-Page Apps. (optional)
        /// </param>  
        /// <param name="token">
        ///The token to be introspected. (optional)
        /// </param>
        /// <param name="clientId">
        ///The Client ID of the calling application, as registered with APS.
        /// </param>
        /// <param name="throwOnError">
        /// Indicates whether to throw an exception on error.
        /// </param>
        /// <returns>Task of &lt;IntrospectToken&gt;></returns>
        public async System.Threading.Tasks.Task<IntrospectToken> IntrospectTokenAsync(string token, string clientId, string clientSecret = default(string), bool throwOnError = true)
        {
            if (!string.IsNullOrEmpty(clientSecret))
            { // for private clients
                var clientIdSecret = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
                var response = await this.tokenApi.IntrospectTokenAsync(token: token, authorization: clientIdSecret, throwOnError: throwOnError);
                return response.Content;
            }
            else
            { // for public clients
                var response = await this.tokenApi.IntrospectTokenAsync(token: token, clientId: clientId, throwOnError: throwOnError);
                return response.Content;
            }
        }


        /// <summary>
        /// Logout
        /// </summary>
        /// <remarks>
        ///Signs out the currently authenticated user from the APS authorization server. Thereafter, this operation redirects the user to the `post_logout_redirect_uri`, or to the Autodesk Sign-in page when no `post_logout_redirect_uri` is provided.
        ///
        ///This operation has a rate limit of 500 calls per minute.
        /// </remarks>
        /// <exception cref="AuthenticationApiException">Thrown when fails to make API call</exception>
        /// <param name="postLogoutRedirectUri">
        ///The URI to redirect your users to once logout is performed. If you do not specify this parameter your users are redirected to the Autodesk Sign-in page. 
        ///
        ///**Note:**  You must provide a redirect URI that is pre-registered with APS. This precaution is taken to prevent unauthorized applications from hijacking the logout process. (optional)
        /// </param>

        /// <returns>string</returns>
        public string Logout(string postLogoutRedirectUri = default(string))
        {
            return this.tokenApi.Logout(postLogoutRedirectUri);
        }


        /// <summary>
        /// Revoke Token
        /// </summary>
        /// <remarks>
        ///Revokes an active access token or refresh token.
        ///
        ///An application can only revoke its own tokens.
        ///
        ///This operation has a rate limit of 100 calls per minute.
        /// </remarks>
        /// <exception cref="AuthenticationApiException">Thrown when fails to make API call</exception>
        /// <param name="token">
        ///The token to be revoked.  
        /// </param>
        /// <param name="tokenTypeHint">
        /// The tokentypehint. Could be refresh token or access token.
        /// </param>
        /// <param name="clientSecret">
        ///The Client secret of the calling application, as registered with APS.
        ///**Note** The clientSecret is required only for Traditional Web Apps and Server-to-Server Apps. It is not required for Desktop, Mobile, and Single-Page Apps. (optional)
        /// </param>
        /// <param name="clientId">
        ///The Client ID of the calling application, as registered with APS.
        /// </param>
        /// <param name="throwOnError">
        /// Indicates whether to throw an exception on error.
        /// </param>
        /// <returns>Task of HttpResponseMessage</returns>

        public async System.Threading.Tasks.Task<HttpResponseMessage> RevokeAsync(string token, string clientId, string clientSecret = default(string), TokenTypeHint tokenTypeHint = default(TokenTypeHint), bool throwOnError = true)
        {
            if (!string.IsNullOrEmpty(clientSecret))
            { // for private clients 
                var clientIdSecret = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
                return await this.tokenApi.RevokeAsync(token: token, tokenTypeHint: tokenTypeHint, authorization: clientIdSecret, throwOnError: throwOnError);
            }
            else
            { //  for public clients
                return await this.tokenApi.RevokeAsync(token: token, tokenTypeHint: tokenTypeHint, clientId: clientId, throwOnError: throwOnError);

            }

            #endregion tokenapi
        }
    }
}