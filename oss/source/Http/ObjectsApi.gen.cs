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
using Autodesk.Forge.Core;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using Autodesk.Oss.Model;
using Autodesk.Oss.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Autodesk.SDKManager;

namespace Autodesk.Oss.Http
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IObjectsApi
    {
        /// <summary>
        /// Complete Batch Upload to S3 Signed URLs
        /// </summary>
        /// <remarks>
        ///Requests OSS to start reconstituting the set of objects that were uploaded using signed S3 upload URLs. You must call this operation only after all the objects have been uploaded. 
        ///
        ///You can specify up to 25 objects in this operation. 
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="bucketKey">
        ///The bucket key of the bucket that contains the objects you are operating on.
        /// </param>
        /// <param name="requests">
        /// (optional)
        /// </param>
        /// <returns>Task of ApiResponse&lt;BatchcompleteuploadResponse&gt;</returns>

        System.Threading.Tasks.Task<ApiResponse<BatchcompleteuploadResponse>> BatchCompleteUploadAsync(string bucketKey, BatchcompleteuploadObject requests = default(BatchcompleteuploadObject), string accessToken = null, bool throwOnError = true);
        /// <summary>
        /// Batch Generate Signed S3 Download URLs
        /// </summary>
        /// <remarks>
        ///Creates and returns signed URLs to download a set of objects directly from S3. These signed URLs expire in 2 minutes by default, but you can change this duration if needed.  You must start download the objects before the signed URLs expire. The download itself can take longer.
        ///
        ///Only the application that owns the bucket can call this operation. All other applications that call this operation will receive a "403 Forbidden" error.
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="bucketKey">
        ///The bucket key of the bucket that contains the objects you are operating on.
        /// </param>
        /// <param name="requests">
        ///
        /// </param>
        /// <param name="publicResourceFallback">
        ///Specifies how to return the signed URLs, in case the object was uploaded in chunks, and assembling of chunks is not yet complete.
        ///
        ///- `true` : Return a single signed OSS URL.
        ///- `false` : (Default) Return multiple signed S3 URLs, where each URL points to a chunk. (optional)
        /// </param>
        /// <param name="minutesExpiration">
        ///The time window (in minutes) the signed URL will remain usable. Acceptable values = 1-60 minutes. Default = 2 minutes.
        ///
        ///**Tip:** Use the smallest possible time window to minimize exposure of the signed URL. (optional)
        /// </param>
        /// <returns>Task of ApiResponse&lt;Batchsigneds3downloadResponse&gt;</returns>

        System.Threading.Tasks.Task<ApiResponse<Batchsigneds3downloadResponse>> BatchSignedS3DownloadAsync(string bucketKey, Batchsigneds3downloadObject requests, bool? publicResourceFallback = default(bool?), int? minutesExpiration = default(int?), string accessToken = null, bool throwOnError = true);
        /// <summary>
        /// Batch Generate Signed S3 Upload URLs
        /// </summary>
        /// <remarks>
        ///Creates and returns signed URLs to upload a set of objects directly to S3. These signed URLs expire in 2 minutes by default, but you can change this duration if needed.  You must start uploading the objects before the signed URLs expire. The upload  itself can take longer.
        ///
        ///Only the application that owns the bucket can call this operation. All other applications that call this operation will receive a "403 Forbidden" error.
        ///
        ///If required, you can request an array of signed URLs for each object, which lets you upload the objects in chunks. Once you upload all chunks you must call the [Complete Batch Upload to S3 Signed URL](/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectKey-batchcompleteupload-POST/) operation to indicate completion. This instructs OSS to assemble the chunks and reconstitute the object on OSS. You must call this operation even if you requested a single signed URL for an object.
        ///
        ///If an upload fails after the validity period of a signed URL has elapsed, you can call this operation again to obtain fresh signed URLs. However, you must use the same `uploadKey` that was returned when you originally called this operation. 
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="bucketKey">
        ///The bucket key of the bucket that contains the objects you are operating on.
        /// </param>
        /// <param name="useAcceleration">
        ///`true` : (Default) Generates a faster S3 signed URL using Transfer Acceleration.
        ///
        ///`false`: Generates a standard S3 signed URL. (optional)
        /// </param>
        /// <param name="minutesExpiration">
        ///The time window (in minutes) the signed URL will remain usable. Acceptable values = 1-60 minutes. Default = 2 minutes.
        ///
        ///**Tip:** Use the smallest possible time window to minimize exposure of the signed URL. (optional)
        /// </param>
        /// <param name="requests">
        /// (optional)
        /// </param>
        /// <returns>Task of ApiResponse&lt;Batchsigneds3uploadResponse&gt;</returns>

        System.Threading.Tasks.Task<ApiResponse<Batchsigneds3uploadResponse>> BatchSignedS3UploadAsync(string bucketKey, bool? useAcceleration = default(bool?), int? minutesExpiration = default(int?), Batchsigneds3uploadObject requests = default(Batchsigneds3uploadObject), string accessToken = null, bool throwOnError = true);
        /// <summary>
        /// Complete Upload to S3 Signed URL
        /// </summary>
        /// <remarks>
        ///Requests OSS to assemble and reconstitute the object that was uploaded using signed S3 upload URL. You must call this operation only after all parts/chunks of the object has been uploaded.
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="bucketKey">
        ///The bucket key of the bucket that contains the objects you are operating on.
        /// </param>
        /// <param name="objectKey">
        ///The URL-encoded human friendly name of the object.
        /// </param>
        /// <param name="contentType">
        ///Must be `application/json`.
        /// </param>
        /// <param name="body">
        ///
        /// </param>
        /// <param name="xAdsMetaContentType">
        ///The Content-Type value for the uploaded object to record within OSS. (optional)
        /// </param>
        /// <param name="xAdsMetaContentDisposition">
        ///The Content-Disposition value for the uploaded object to record within OSS. (optional)
        /// </param>
        /// <param name="xAdsMetaContentEncoding">
        ///The Content-Encoding value for the uploaded object to record within OSS. (optional)
        /// </param>
        /// <param name="xAdsMetaCacheControl">
        ///The Cache-Control value for the uploaded object to record within OSS. (optional)
        /// </param>
        /// <param name="xAdsUserDefinedMetadata">
        ///Custom metadata to be stored with the object, which can be retrieved later on download or when retrieving object details. Must be a JSON object that is less than 100 bytes. (optional)
        /// </param>

        /// <returns>Task of HttpResponseMessage</returns>
        System.Threading.Tasks.Task<HttpResponseMessage> CompleteSignedS3UploadAsync(string bucketKey, string objectKey, string contentType, Completes3uploadBody body, string xAdsMetaContentType = default(string), string xAdsMetaContentDisposition = default(string), string xAdsMetaContentEncoding = default(string), string xAdsMetaCacheControl = default(string), string xAdsUserDefinedMetadata = default(string), string accessToken = null, bool throwOnError = true);
        /// <summary>
        /// Copy Object
        /// </summary>
        /// <remarks>
        ///Creates a copy of an object within the bucket.
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="bucketKey">
        ///The bucket key of the bucket that contains the objects you are operating on.
        /// </param>
        /// <param name="objectKey">
        ///The URL-encoded human friendly name of the object.
        /// </param>
        /// <param name="newObjName">
        ///A URL-encoded human friendly name to identify the copied object.
        /// </param>
        /// <returns>Task of ApiResponse&lt;ObjectDetails&gt;</returns>

        System.Threading.Tasks.Task<ApiResponse<ObjectDetails>> CopyToAsync(string bucketKey, string objectKey, string newObjName,  string accessToken = null, bool throwOnError = true);
        /// <summary>
        /// Generate OSS Signed URL
        /// </summary>
        /// <remarks>
        ///Generates a signed URL that can be used to download or upload an object within the specified expiration time. If the object the signed URL points to is deleted or expires before the signed URL expires, the signed URL will no longer be valid.
        ///
        ///In addition to this operation that generates OSS signed URLs, OSS provides operations to generate S3 signed URLs. S3 signed URLs allow direct upload/download from S3 but are restricted to bucket owners. OSS signed URLs also allow upload/download and can be configured for access by other applications, making them suitable for sharing objects across applications.
        ///
        ///Only the application that owns the bucket containing the object can call this operation.
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="bucketKey">
        ///The bucket key of the bucket that contains the objects you are operating on.
        /// </param>
        /// <param name="objectKey">
        ///The URL-encoded human friendly name of the object.
        /// </param>
        /// <param name="access">
        /// (optional)
        /// </param>
        /// <param name="useCdn">
        ///`true` : Returns a Cloudfront URL to the object instead of an Autodesk URL (one that points to a location on https://developer.api.autodesk.com). Applications can interact with the Cloudfront URL exactly like with any classic public resource in OSS. They can use GET requests to download objects or PUT requests to upload objects.
        ///
        ///`false` : (Default) Returns an Autodesk URL (one that points to a location on https://developer.api.autodesk.com) to the object. (optional)
        /// </param>
        /// <param name="createSignedResource">
        /// (optional)
        /// </param>
        /// <returns>Task of ApiResponse&lt;CreateObjectSigned&gt;</returns>

        System.Threading.Tasks.Task<ApiResponse<CreateObjectSigned>> CreateSignedResourceAsync(string bucketKey, string objectKey, Access? access = null, bool? useCdn = default(bool?), CreateSignedResource createSignedResource = default(CreateSignedResource), string accessToken = null, bool throwOnError = true);
        /// <summary>
        /// Delete Object
        /// </summary>
        /// <remarks>
        ///Deletes an object from the bucket.
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="bucketKey">
        ///The bucket key of the bucket that contains the objects you are operating on.
        /// </param>
        /// <param name="objectKey">
        ///The URL-encoded human friendly name of the object.
        /// </param>
        /// <returns>Task of HttpResponseMessage</returns>
        System.Threading.Tasks.Task<HttpResponseMessage> DeleteObjectAsync(string bucketKey, string objectKey, string accessToken = null, bool throwOnError = true);
        /// <summary>
        /// Delete Object Using Signed URL
        /// </summary>
        /// <remarks>
        ///Delete an object using an OSS signed URL to access it.
        ///
        ///Only applications that own the bucket containing the object can call this operation. 
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="hash">
        ///The ID component of the signed URL.
        ///
        ///**Note:** The signed URL returned by [Generate OSS Signed URL](/en/docs/data/v2/reference/http/signedresources-:id-GET/) contains `hash` as a URI parameter.
        /// </param>
        /// <param name="xAdsRegion">
        ///Specifies where the bucket containing the object is stored. Possible values are:
        ///
        ///- `US` : (Default) Data center for the US region.
        ///- `EMEA` : Data center for the European Union, Middle East, and Africa.
        ///- `AUS` : (Beta) Data center for Australia.
        ///- `CAN` : Data center for the Canada region.
        ///- `DEU` : Data center for the Germany region.
        ///- `IND` : Data center for the India region.
        ///- `JPN` : Data center for the Japan region.
        ///- `GBR` : Data center for the United Kingdom region.
        ///
        ///**Note:** Beta features are subject to change. Please do not use in production environments. (optional)
        /// </param>

        /// <returns>Task of HttpResponseMessage</returns>
        System.Threading.Tasks.Task<HttpResponseMessage> DeleteSignedResourceAsync(string hash, Region? xAdsRegion = null, string accessToken = null, bool throwOnError = true);
        /// <summary>
        /// Get Object Details
        /// </summary>
        /// <remarks>
        ///Returns detailed information about the specified object.
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="bucketKey">
        ///The bucket key of the bucket that contains the objects you are operating on.
        /// </param>
        /// <param name="objectKey">
        ///The URL-encoded human friendly name of the object.
        /// </param>
        /// <param name="ifModifiedSince">
        ///A timestamp in the HTTP date format (Mon, DD Month YYYY HH:MM:SS GMT). The requested data is returned only if the object has been modified since the specified timestamp. If not, a 304 (Not Modified) HTTP status is returned. (optional)
        /// </param>
        /// <param name="with">
        /// (optional)
        /// </param>
        /// <returns>Task of ApiResponse&lt;ObjectFullDetails&gt;</returns>

        System.Threading.Tasks.Task<ApiResponse<ObjectFullDetails>> GetObjectDetailsAsync(string bucketKey, string objectKey, DateTime? ifModifiedSince = default(DateTime?), With? with = null, string accessToken = null, bool throwOnError = true);
        /// <summary>
        /// List Objects
        /// </summary>
        /// <remarks>
        ///Returns a list objects in the specified bucket. 
        ///
        ///Only the application that owns the bucket can call this operation. All other applications that call this operation will receive a "403 Forbidden" error.
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="bucketKey">
        ///The bucket key of the bucket that contains the objects you are operating on.
        /// </param>
        /// <param name="limit">
        ///The number of items you want per page.
        ///Acceptable values = 1-100. Default = 10. (optional, default to 10)
        /// </param>
        /// <param name="beginsWith">
        ///Filters the results by the value you specify. Only the objects with their `objectKey` beginning with the specified string are returned. (optional)
        /// </param>
        /// <param name="startAt">
        ///The ID of the last item that was returned in the previous result set.  It enables the system to return subsequent items starting from the next one after the specified ID. (optional)
        /// </param>
        /// <returns>Task of ApiResponse&lt;BucketObjects&gt;</returns>

        System.Threading.Tasks.Task<ApiResponse<BucketObjects>> GetObjectsAsync(string bucketKey, int? limit = default(int?), string beginsWith = default(string), string startAt = default(string), string accessToken = null, bool throwOnError = true);
        /// <summary>
        /// Download Object Using Signed URL
        /// </summary>
        /// <remarks>
        ///Downloads an object using an OSS signed URL.
        ///
        ///**Note:** The signed URL returned by [Generate OSS Signed URL](/en/docs/data/v2/reference/http/signedresources-:id-GET/)  contains the `hash` URI parameter as well. 
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="hash">
        ///The ID component of the signed URL.
        ///
        ///**Note:** The signed URL returned by [Generate OSS Signed URL](/en/docs/data/v2/reference/http/signedresources-:id-GET/) contains `hash` as a URI parameter.
        /// </param>
        /// <param name="range">
        ///The byte range to download, specified in the form `bytes=<START_BYTE>-<END_BYTE>`. (optional)
        /// </param>
        /// <param name="ifNoneMatch">
        ///The last known ETag value of the object. OSS returns the requested data  only if the `If-None-Match` header differs from the ETag value of the object on OSS, which indicates that the object on OSS is newer. If not, it returns a 304 "Not Modified" HTTP status. (optional)
        /// </param>
        /// <param name="ifModifiedSince">
        ///A timestamp in the HTTP date format (Mon, DD Month YYYY HH:MM:SS GMT). The requested data is returned only if the object has been modified since the specified timestamp. If not, a 304 (Not Modified) HTTP status is returned. (optional)
        /// </param>
        /// <param name="acceptEncoding">
        ///The compression format your prefer to receive the data. Possible values are:
        ///
        ///- `gzip` - Use the gzip format
        ///
        ///**Note:** You cannot use `Accept-Encoding:gzip` with a Range header containing an end byte range. OSS will not honor the End byte range if `Accept-Encoding: gzip` header is used. (optional)
        /// </param>
        /// <param name="region">
        ///Specifies where the bucket containing the object is stored. Possible values are:
        ///
        ///- `US` : (Default) Data center for the US region.
        ///- `EMEA` : Data center for the European Union, Middle East, and Africa.
        ///- `AUS` : (Beta) Data center for Australia.
        ///- `CAN` : Data center for the Canada region.
        ///- `DEU` : Data center for the Germany region.
        ///- `IND` : Data center for the India region.
        ///- `JPN` : Data center for the Japan region.
        ///- `GBR` : Data center for the United Kingdom region.
        ///
        ///**Note:** Beta features are subject to change. Please do not use in production environments. (optional)
        /// </param>
        /// <param name="responseContentDisposition">
        ///The value of the Content-Disposition header you want to receive when you download the object using the signed URL. If you do not specify a value, the Content-Disposition header defaults to the value stored with OSS. (optional)
        /// </param>
        /// <param name="responseContentType">
        ///The value of the Content-Type header you want to receive when you download the object using the signed URL. If you do not specify a value, the Content-Type header defaults to the value stored with OSS. (optional)
        /// </param>
        /// <returns>Task of ApiResponse&lt;System.IO.Stream&gt;</returns>

        System.Threading.Tasks.Task<ApiResponse<System.IO.Stream>> GetSignedResourceAsync(string hash, string range = default(string), string ifNoneMatch = default(string), DateTime? ifModifiedSince = default(DateTime?), string acceptEncoding = default(string), Region? region = null, string responseContentDisposition = default(string), string responseContentType = default(string), string accessToken = null, bool throwOnError = true);
        /// <summary>
        /// Generate Signed S3 Download URL
        /// </summary>
        /// <remarks>
        ///Gets a signed URL to download an object directly from S3, bypassing OSS servers. This signed URL expires in 2 minutes by default, but you can change this duration if needed.  You must start the download before the signed URL expires. The download itself can take longer. If the download fails after the validity period of the signed URL has elapsed, you can call this operation again to obtain a fresh signed URL.
        ///
        ///Only applications that have read access to the object can call this operation.   
        ///
        ///You can use range headers with the signed download URL to download the object in chunks. This ability lets you download chunks in parallel, which can result in faster downloads.
        ///
        ///If the object you want to download was uploaded in chunks and is still assembling on OSS, you will receive multiple S3 URLs instead of just one. Each URL will point to a chunk. If you prefer to receive a single URL, set the `public-resource-fallback` query parameter to `true`. This setting will make OSS fallback to returning a single signed OSS URL, if assembling is still in progress. 
        ///
        ///In addition to this operation that generates S3 signed URLs, OSS provides an operation to generate OSS signed URLs. S3 signed URLs allow direct upload/download from S3 but are restricted to bucket owners. OSS signed URLs also allow upload/download and can be configured for access by other applications, making them suitable for sharing objects across applications.
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="bucketKey">
        ///The bucket key of the bucket that contains the objects you are operating on.
        /// </param>
        /// <param name="objectKey">
        ///The URL-encoded human friendly name of the object.
        /// </param>
        /// <param name="ifNoneMatch">
        ///The last known ETag value of the object. OSS returns the signed URL only if the `If-None-Match` header differs from the ETag value of the object on S3. If not, it returns a 304 "Not Modified" HTTP status. (optional)
        /// </param>
        /// <param name="ifModifiedSince">
        ///A timestamp in the HTTP date format (Mon, DD Month YYYY HH:MM:SS GMT). The signed URL is returned only if the object has been modified since the specified timestamp. If not, a 304 (Not Modified) HTTP status is returned. (optional)
        /// </param>
        /// <param name="responseContentType">
        ///The value of the Content-Type header you want to receive when you download the object using the signed URL. If you do not specify a value, the Content-Type header defaults to the value stored with OSS. (optional)
        /// </param>
        /// <param name="responseContentDisposition">
        ///The value of the Content-Disposition header you want to receive when you download the object using the signed URL. If you do not specify a value, the Content-Disposition header defaults to the value stored with OSS. (optional)
        /// </param>
        /// <param name="responseCacheControl">
        ///The value of the Cache-Control header you want to receive when you download the object using the signed URL. If you do not specify a value, the Cache-Control header defaults to the value stored with OSS. (optional)
        /// </param>
        /// <param name="publicResourceFallback">
        ///Specifies how to return the signed URLs, in case the object was uploaded in chunks, and assembling of chunks is not yet complete.
        ///
        ///- `true` : Return a single signed OSS URL.
        ///- `false` : (Default) Return multiple signed S3 URLs, where each URL points to a chunk. (optional)
        /// </param>
        /// <param name="minutesExpiration">
        ///The time window (in minutes) the signed URL will remain usable. Acceptable values = 1-60 minutes. Default = 2 minutes.
        ///
        ///**Tip:** Use the smallest possible time window to minimize exposure of the signed URL. (optional)
        /// </param>
        /// <param name="useCdn">
        ///`true` : Returns a URL that points to a CloudFront edge location.
        ///
        ///`false` : (Default) Returns a URL that points directly to the S3 object. (optional)
        /// </param>
        /// <returns>Task of ApiResponse&lt;Signeds3downloadResponse&gt;</returns>

        System.Threading.Tasks.Task<ApiResponse<Signeds3downloadResponse>> SignedS3DownloadAsync(string bucketKey, string objectKey, string ifNoneMatch = default(string), DateTime? ifModifiedSince = default(DateTime?), string responseContentType = default(string), string responseContentDisposition = default(string), string responseCacheControl = default(string), bool? publicResourceFallback = default(bool?), int? minutesExpiration = default(int?), bool? useCdn = default(bool?), string accessToken = null, bool throwOnError = true);
        /// <summary>
        /// Generate Signed S3 Upload URL
        /// </summary>
        /// <remarks>
        ///Gets a signed URL to upload an object directly to S3, bypassing OSS servers. You can also request an array of signed URLs which lets you upload an object in chunks.
        ///
        ///This signed URL expires in 2 minutes by default, but you can change this duration if needed.  You must start the upload before the signed URL expires. The upload itself can take longer. If the upload fails after the validity period of the signed URL has elapsed, you can call this operation again to obtain a fresh signed URL (or an array of signed URLs as the case may be). However, you must use the same `uploadKey` that was returned when you originally called this operation. 
        ///
        ///Only applications that own the bucket can call this operation.
        ///
        ///**Note:** Once you upload all chunks you must call the [Complete Upload to S3 Signed URL](/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectKey-signeds3upload-POST/) operation to indicate completion. This instructs OSS to assemble the chunks and reconstitute the object on OSS. You must call this operation even when using a single signed URL. 
        ///
        ///In addition to this operation that generates S3 signed URLs, OSS provides an operation to generate OSS signed URLs. S3 signed URLs allow direct upload/download from S3 but are restricted to bucket owners. OSS signed URLs also allow upload/download and can be configured for access by other applications, making them suitable for sharing objects across applications.    
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="bucketKey">
        ///The bucket key of the bucket that contains the objects you are operating on.
        /// </param>
        /// <param name="objectKey">
        ///The URL-encoded human friendly name of the object.
        /// </param>
        /// <param name="parts">
        ///The number of parts you intend to chunk the object for uploading. OSS will return that many signed URLs, one URL for each chunk. If you do not specify a value you'll get only one URL to upload the entire object.              (optional)
        /// </param>
        /// <param name="firstPart">
        ///The index of the first chunk to be uploaded. (optional)
        /// </param>
        /// <param name="uploadKey">
        ///The `uploadKey` of a previously-initiated upload, in order to request more chunk upload URLs for the same upload. If you do not specify a value, OSS will initiate a new upload entirely. (optional)
        /// </param>
        /// <param name="minutesExpiration">
        ///The time window (in minutes) the signed URL will remain usable. Acceptable values = 1-60 minutes. Default = 2 minutes.
        ///
        ///**Tip:** Use the smallest possible time window to minimize exposure of the signed URL. (optional)
        /// </param>
        /// <param name="useAcceleration">
        ///`true` : (Default) Generates a faster S3 signed URL using Transfer Acceleration.
        ///
        ///`false`: Generates a standard S3 signed URL. (optional)
        /// </param>
        /// <returns>Task of ApiResponse&lt;Signeds3uploadResponse&gt;</returns>

        System.Threading.Tasks.Task<ApiResponse<Signeds3uploadResponse>> SignedS3UploadAsync(string bucketKey, string objectKey, int? parts = default(int?), int? firstPart = default(int?), string uploadKey = default(string), int? minutesExpiration = default(int?), bool? useAcceleration = default(bool?), string accessToken = null, bool throwOnError = true);
        /// <summary>
        /// Replace Object Using Signed URL
        /// </summary>
        /// <remarks>
        ///Replaces an object that already exists on OSS, using an OSS signed URL. 
        ///
        ///The signed URL must fulfil the following conditions:
        ///
        ///- The signed URL is valid (it has not expired as yet).
        ///- It was generated with `write` or `readwrite` for the `access` parameter.
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="hash">
        ///The ID component of the signed URL.
        ///
        ///**Note:** The signed URL returned by [Generate OSS Signed URL](/en/docs/data/v2/reference/http/signedresources-:id-GET/) contains `hash` as a URI parameter.
        /// </param>
        /// <param name="contentLength">
        ///The size of the data contained in the request body, in bytes.
        /// </param>
        /// <param name="body">
        ///The object to upload.
        /// </param>
        /// <param name="contentType">
        ///The MIME type of the object to upload; can be any type except 'multipart/form-data'. This can be omitted, but we recommend adding it. (optional)
        /// </param>
        /// <param name="contentDisposition">
        ///The suggested file name to use when this object is downloaded as a file. (optional)
        /// </param>
        /// <param name="xAdsRegion">
        ///Specifies where the bucket containing the object is stored. Possible values are:
        ///
        ///- `US` : (Default) Data center for the US region.
        ///- `EMEA` : Data center for the European Union, Middle East, and Africa.
        ///- `AUS` : (Beta) Data center for Australia.
        ///- `CAN` : Data center for the Canada region.
        ///- `DEU` : Data center for the Germany region.
        ///- `IND` : Data center for the India region.
        ///- `JPN` : Data center for the Japan region.
        ///- `GBR` : Data center for the United Kingdom region.
        ///
        ///**Note:** Beta features are subject to change. Please do not use in production environments. (optional)
        /// </param>
        /// <param name="ifMatch">
        ///The current value of the `sha1` attribute of the object you want to replace. OSS checks the `If-Match` header against the `sha1` attribute of the object in OSS. If they match, OSS allows the object to be overwritten. Otherwise, it means that the object on OSS has been modified since you retrieved the `sha1` and the request fails. (optional)
        /// </param>
        /// <returns>Task of ApiResponse&lt;ObjectDetails&gt;</returns>

        System.Threading.Tasks.Task<ApiResponse<ObjectDetails>> UploadSignedResourceAsync(string hash, int? contentLength, System.IO.Stream body, string contentType = default(string), string contentDisposition = default(string), Region? xAdsRegion = null, string ifMatch = default(string), string accessToken = null, bool throwOnError = true);
        /// <summary>
        /// Upload Object Using Signed URL
        /// </summary>
        /// <remarks>
        ///Performs a resumable upload using an OSS signed URL. Use this operation to upload an object in chunks.
        ///
        ///**Note:** The signed URL returned by [Generate OSS Signed URL](/en/docs/data/v2/reference/http/signedresources-:id-GET/) contains the `hash` as a URI parameter. 
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="hash">
        ///The ID component of the signed URL.
        ///
        ///**Note:** The signed URL returned by [Generate OSS Signed URL](/en/docs/data/v2/reference/http/signedresources-:id-GET/) contains `hash` as a URI parameter.
        /// </param>
        /// <param name="contentRange">
        ///The byte range to upload, specified in the form `bytes <START_BYTE>-<END_BYTE>/<TOTAL_BYTES>`.
        /// </param>
        /// <param name="sessionId">
        ///An ID to uniquely identify the file upload session.
        /// </param>
        /// <param name="body">
        ///The chunk to upload.
        /// </param>
        /// <param name="contentType">
        ///The MIME type of the object to upload; can be any type except 'multipart/form-data'. This can be omitted, but we recommend adding it. (optional)
        /// </param>
        /// <param name="contentDisposition">
        ///The suggested file name to use when this object is downloaded as a file. (optional)
        /// </param>
        /// <param name="xAdsRegion">
        ///Specifies where the bucket containing the object is stored. Possible values are:
        ///
        ///- `US` : (Default) Data center for the US region.
        ///- `EMEA` : Data center for the European Union, Middle East, and Africa.
        ///- `AUS` : (Beta) Data center for Australia.
        ///- `CAN` : Data center for the Canada region.
        ///- `DEU` : Data center for the Germany region.
        ///- `IND` : Data center for the India region.
        ///- `JPN` : Data center for the Japan region.
        ///- `GBR` : Data center for the United Kingdom region.
        ///
        ///**Note:** Beta features are subject to change. Please do not use in production environments. (optional)
        /// </param>
        /// <returns>Task of ApiResponse&lt;ObjectDetails&gt;</returns>

        System.Threading.Tasks.Task<ApiResponse<ObjectDetails>> UploadSignedResourcesChunkAsync(string hash, string contentRange, string sessionId, System.IO.Stream body, string contentType = default(string), string contentDisposition = default(string), Region? xAdsRegion = null, string accessToken = null, bool throwOnError = true);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class ObjectsApi : IObjectsApi
    {
        ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectsApi"/> class
        /// using SDKManager object
        /// </summary>
        /// <param name="sdkManager">An instance of SDKManager</param>
        /// <returns></returns>
        public ObjectsApi(SDKManager.SDKManager sdkManager)
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
                    if (!string.Equals(baseName, "Content-Range")
                        && !string.Equals(baseName, "Content-Type")
			&& !string.Equals(baseName, "Content-Disposition"))
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
        /// Complete Batch Upload to S3 Signed URLs
        /// </summary>
        /// <remarks>
        ///Requests OSS to start reconstituting the set of objects that were uploaded using signed S3 upload URLs. You must call this operation only after all the objects have been uploaded. 
        ///
        ///You can specify up to 25 objects in this operation. 
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="bucketKey">
        ///The bucket key of the bucket that contains the objects you are operating on.
        /// </param>
        /// <param name="requests">
        /// (optional)
        /// </param>
        /// <returns>Task of ApiResponse&lt;BatchcompleteuploadResponse&gt;></returns>

        public async System.Threading.Tasks.Task<ApiResponse<BatchcompleteuploadResponse>> BatchCompleteUploadAsync(string bucketKey, BatchcompleteuploadObject requests = default(BatchcompleteuploadObject), string accessToken = null, bool throwOnError = true)
        {
            logger.LogInformation("Entered into BatchCompleteUploadAsync ");
            using (var request = new HttpRequestMessage())
            {
                var queryParam = new Dictionary<string, object>();
                request.RequestUri =
                    Marshalling.BuildRequestUri($"/oss/v2/buckets/{bucketKey}/objects/batchcompleteupload",
                        routeParameters: new Dictionary<string, object> {
                            { "bucketKey", bucketKey},
                        },
                        queryParameters: queryParam
                    );

                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("User-Agent", "APS SDK/OSS/C#/1.0.0");
                if (!string.IsNullOrEmpty(accessToken))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");
                }

                request.Content = Marshalling.Serialize(requests); // http body (model) parameter



                // tell the underlying pipeline what scope we'd like to use
                // if (scopes == null)
                // {
                // TBD:Naren FORCE-4027 - If accessToken is null, acquire auth token using auth SDK, with defined scope.
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), "data:write data:create ");
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
                        throw new OssApiException(ex.Message, response, ex);
                    }
                }
                else if (!response.IsSuccessStatusCode)
                {
                    logger.LogError($"response unsuccess with status code: {response.StatusCode}");
                    return new ApiResponse<BatchcompleteuploadResponse>(response, default(BatchcompleteuploadResponse));
                }
                logger.LogInformation($"Exited from BatchCompleteUploadAsync with response statusCode: {response.StatusCode}");
                return new ApiResponse<BatchcompleteuploadResponse>(response, await LocalMarshalling.DeserializeAsync<BatchcompleteuploadResponse>(response.Content));

            } // using
        }
        /// <summary>
        /// Batch Generate Signed S3 Download URLs
        /// </summary>
        /// <remarks>
        ///Creates and returns signed URLs to download a set of objects directly from S3. These signed URLs expire in 2 minutes by default, but you can change this duration if needed.  You must start download the objects before the signed URLs expire. The download itself can take longer.
        ///
        ///Only the application that owns the bucket can call this operation. All other applications that call this operation will receive a "403 Forbidden" error.
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="bucketKey">
        ///The bucket key of the bucket that contains the objects you are operating on.
        /// </param>
        /// <param name="requests">
        ///
        /// </param>
        /// <param name="publicResourceFallback">
        ///Specifies how to return the signed URLs, in case the object was uploaded in chunks, and assembling of chunks is not yet complete.
        ///
        ///- `true` : Return a single signed OSS URL.
        ///- `false` : (Default) Return multiple signed S3 URLs, where each URL points to a chunk. (optional)
        /// </param>
        /// <param name="minutesExpiration">
        ///The time window (in minutes) the signed URL will remain usable. Acceptable values = 1-60 minutes. Default = 2 minutes.
        ///
        ///**Tip:** Use the smallest possible time window to minimize exposure of the signed URL. (optional)
        /// </param>
        /// <returns>Task of ApiResponse&lt;Batchsigneds3downloadResponse&gt;></returns>

        public async System.Threading.Tasks.Task<ApiResponse<Batchsigneds3downloadResponse>> BatchSignedS3DownloadAsync(string bucketKey, Batchsigneds3downloadObject requests, bool? publicResourceFallback = default(bool?), int? minutesExpiration = default(int?), string accessToken = null, bool throwOnError = true)
        {
            logger.LogInformation("Entered into BatchSignedS3DownloadAsync ");
            using (var request = new HttpRequestMessage())
            {
                var queryParam = new Dictionary<string, object>();
                SetQueryParameter("public-resource-fallback", publicResourceFallback, queryParam);
                SetQueryParameter("minutesExpiration", minutesExpiration, queryParam);
                request.RequestUri =
                    Marshalling.BuildRequestUri($"/oss/v2/buckets/{bucketKey}/objects/batchsigneds3download",
                        routeParameters: new Dictionary<string, object> {
                            { "bucketKey", bucketKey},
                        },
                        queryParameters: queryParam
                    );

                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("User-Agent", "APS SDK/OSS/C#/1.0.0");
                if (!string.IsNullOrEmpty(accessToken))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");
                }

                request.Content = Marshalling.Serialize(requests); // http body (model) parameter



                // tell the underlying pipeline what scope we'd like to use
                // if (scopes == null)
                // {
                // TBD:Naren FORCE-4027 - If accessToken is null, acquire auth token using auth SDK, with defined scope.
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), "data:read ");
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
                        throw new OssApiException(ex.Message, response, ex);
                    }
                }
                else if (!response.IsSuccessStatusCode)
                {
                    logger.LogError($"response unsuccess with status code: {response.StatusCode}");
                    return new ApiResponse<Batchsigneds3downloadResponse>(response, default(Batchsigneds3downloadResponse));
                }
                logger.LogInformation($"Exited from BatchSignedS3DownloadAsync with response statusCode: {response.StatusCode}");
                return new ApiResponse<Batchsigneds3downloadResponse>(response, await LocalMarshalling.DeserializeAsync<Batchsigneds3downloadResponse>(response.Content));

            } // using
        }
        /// <summary>
        /// Batch Generate Signed S3 Upload URLs
        /// </summary>
        /// <remarks>
        ///Creates and returns signed URLs to upload a set of objects directly to S3. These signed URLs expire in 2 minutes by default, but you can change this duration if needed.  You must start uploading the objects before the signed URLs expire. The upload  itself can take longer.
        ///
        ///Only the application that owns the bucket can call this operation. All other applications that call this operation will receive a "403 Forbidden" error.
        ///
        ///If required, you can request an array of signed URLs for each object, which lets you upload the objects in chunks. Once you upload all chunks you must call the [Complete Batch Upload to S3 Signed URL](/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectKey-batchcompleteupload-POST/) operation to indicate completion. This instructs OSS to assemble the chunks and reconstitute the object on OSS. You must call this operation even if you requested a single signed URL for an object.
        ///
        ///If an upload fails after the validity period of a signed URL has elapsed, you can call this operation again to obtain fresh signed URLs. However, you must use the same `uploadKey` that was returned when you originally called this operation. 
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="bucketKey">
        ///The bucket key of the bucket that contains the objects you are operating on.
        /// </param>
        /// <param name="useAcceleration">
        ///`true` : (Default) Generates a faster S3 signed URL using Transfer Acceleration.
        ///
        ///`false`: Generates a standard S3 signed URL. (optional)
        /// </param>
        /// <param name="minutesExpiration">
        ///The time window (in minutes) the signed URL will remain usable. Acceptable values = 1-60 minutes. Default = 2 minutes.
        ///
        ///**Tip:** Use the smallest possible time window to minimize exposure of the signed URL. (optional)
        /// </param>
        /// <param name="requests">
        /// (optional)
        /// </param>
        /// <returns>Task of ApiResponse&lt;Batchsigneds3uploadResponse&gt;></returns>

        public async System.Threading.Tasks.Task<ApiResponse<Batchsigneds3uploadResponse>> BatchSignedS3UploadAsync(string bucketKey, bool? useAcceleration = default(bool?), int? minutesExpiration = default(int?), Batchsigneds3uploadObject requests = default(Batchsigneds3uploadObject), string accessToken = null, bool throwOnError = true)
        {
            logger.LogInformation("Entered into BatchSignedS3UploadAsync ");
            using (var request = new HttpRequestMessage())
            {
                var queryParam = new Dictionary<string, object>();
                SetQueryParameter("useAcceleration", useAcceleration, queryParam);
                SetQueryParameter("minutesExpiration", minutesExpiration, queryParam);
                request.RequestUri =
                    Marshalling.BuildRequestUri($"/oss/v2/buckets/{bucketKey}/objects/batchsigneds3upload",
                        routeParameters: new Dictionary<string, object> {
                            { "bucketKey", bucketKey},
                        },
                        queryParameters: queryParam
                    );

                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("User-Agent", "APS SDK/OSS/C#/1.0.0");
                if (!string.IsNullOrEmpty(accessToken))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");
                }

                request.Content = Marshalling.Serialize(requests); // http body (model) parameter



                // tell the underlying pipeline what scope we'd like to use
                // if (scopes == null)
                // {
                // TBD:Naren FORCE-4027 - If accessToken is null, acquire auth token using auth SDK, with defined scope.
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), "data:write data:create ");
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
                        throw new OssApiException(ex.Message, response, ex);
                    }
                }
                else if (!response.IsSuccessStatusCode)
                {
                    logger.LogError($"response unsuccess with status code: {response.StatusCode}");
                    return new ApiResponse<Batchsigneds3uploadResponse>(response, default(Batchsigneds3uploadResponse));
                }
                logger.LogInformation($"Exited from BatchSignedS3UploadAsync with response statusCode: {response.StatusCode}");
                return new ApiResponse<Batchsigneds3uploadResponse>(response, await LocalMarshalling.DeserializeAsync<Batchsigneds3uploadResponse>(response.Content));

            } // using
        }
        /// <summary>
        /// Complete Upload to S3 Signed URL
        /// </summary>
        /// <remarks>
        ///Requests OSS to assemble and reconstitute the object that was uploaded using signed S3 upload URL. You must call this operation only after all parts/chunks of the object has been uploaded.
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="bucketKey">
        ///The bucket key of the bucket that contains the objects you are operating on.
        /// </param>
        /// <param name="objectKey">
        ///The URL-encoded human friendly name of the object.
        /// </param>
        /// <param name="contentType">
        ///Must be `application/json`.
        /// </param>
        /// <param name="body">
        ///
        /// </param>
        /// <param name="xAdsMetaContentType">
        ///The Content-Type value for the uploaded object to record within OSS. (optional)
        /// </param>
        /// <param name="xAdsMetaContentDisposition">
        ///The Content-Disposition value for the uploaded object to record within OSS. (optional)
        /// </param>
        /// <param name="xAdsMetaContentEncoding">
        ///The Content-Encoding value for the uploaded object to record within OSS. (optional)
        /// </param>
        /// <param name="xAdsMetaCacheControl">
        ///The Cache-Control value for the uploaded object to record within OSS. (optional)
        /// </param>
        /// <param name="xAdsUserDefinedMetadata">
        ///Custom metadata to be stored with the object, which can be retrieved later on download or when retrieving object details. Must be a JSON object that is less than 100 bytes. (optional)
        /// </param>

        /// <returns>Task of HttpResponseMessage</returns>
        public async System.Threading.Tasks.Task<HttpResponseMessage> CompleteSignedS3UploadAsync(string bucketKey, string objectKey, string contentType, Completes3uploadBody body, string xAdsMetaContentType = default(string), string xAdsMetaContentDisposition = default(string), string xAdsMetaContentEncoding = default(string), string xAdsMetaCacheControl = default(string), string xAdsUserDefinedMetadata = default(string), string accessToken = null, bool throwOnError = true)
        {
            logger.LogInformation("Entered into CompleteSignedS3UploadAsync ");
            using (var request = new HttpRequestMessage())
            {
                var queryParam = new Dictionary<string, object>();
                request.RequestUri =
                    Marshalling.BuildRequestUri($"/oss/v2/buckets/{bucketKey}/objects/{objectKey}/signeds3upload",
                        routeParameters: new Dictionary<string, object> {
                            { "bucketKey", bucketKey},
                            { "objectKey", objectKey},
                        },
                        queryParameters: queryParam
                    );

                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("User-Agent", "APS SDK/OSS/C#/1.0.0");
                if (!string.IsNullOrEmpty(accessToken))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");
                }

                request.Content = Marshalling.Serialize(body); // http body (model) parameter

                //No need to set content-type header here, as it is set by Marshalling.serialize method else it will throw error 'Cannot add value because header 'Content-Type' does not support multiple values.'.
                // SetHeader("Content-Type", contentType, request);
                SetHeader("x-ads-meta-Content-Type", xAdsMetaContentType, request);
                SetHeader("x-ads-meta-Content-Disposition", xAdsMetaContentDisposition, request);
                SetHeader("x-ads-meta-Content-Encoding", xAdsMetaContentEncoding, request);
                SetHeader("x-ads-meta-Cache-Control", xAdsMetaCacheControl, request);
                SetHeader("x-ads-user-defined-metadata", xAdsUserDefinedMetadata, request);

                // tell the underlying pipeline what scope we'd like to use
                // if (scopes == null)
                // {
                // TBD:Naren FORCE-4027 - If accessToken is null, acquire auth token using auth SDK, with defined scope.
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), "data:write data:create ");
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
                        throw new OssApiException(ex.Message, response, ex);
                    }
                }
                else if (!response.IsSuccessStatusCode)
                {
                    logger.LogError($"response unsuccess with status code: {response.StatusCode}");
                    return response;
                }
                logger.LogInformation($"Exited from CompleteSignedS3UploadAsync with response statusCode: {response.StatusCode}");
                return response;

            } // using
        }
        /// <summary>
        /// Copy Object
        /// </summary>
        /// <remarks>
        ///Creates a copy of an object within the bucket.
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="bucketKey">
        ///The bucket key of the bucket that contains the objects you are operating on.
        /// </param>
        /// <param name="objectKey">
        ///The URL-encoded human friendly name of the object.
        /// </param>
        /// <param name="newObjName">
        ///A URL-encoded human friendly name to identify the copied object.
        /// <returns>Task of ApiResponse&lt;ObjectDetails&gt;></returns>
        public async System.Threading.Tasks.Task<ApiResponse<ObjectDetails>> CopyToAsync(string bucketKey, string objectKey, string newObjName, string accessToken = null, bool throwOnError = true)
        {
            logger.LogInformation("Entered into CopyToAsync ");
            using (var request = new HttpRequestMessage())
            {
                var queryParam = new Dictionary<string, object>();
                request.RequestUri =
                    Marshalling.BuildRequestUri($"/oss/v2/buckets/{bucketKey}/objects/{objectKey}/copyto/{newObjName}",
                        routeParameters: new Dictionary<string, object> {
                            { "bucketKey", bucketKey},
                            { "objectKey", objectKey},
                            { "newObjName", newObjName},
                        },
                        queryParameters: queryParam
                    );

                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("User-Agent", "APS SDK/OSS/C#/1.0.0");
                if (!string.IsNullOrEmpty(accessToken))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");
                }

                // tell the underlying pipeline what scope we'd like to use
                // if (scopes == null)
                // {
                // TBD:Naren FORCE-4027 - If accessToken is null, acquire auth token using auth SDK, with defined scope.
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), "data:write data:create ");
                // }
                // else
                // {
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), scopes);
                // }

                request.Method = new HttpMethod("PUT");

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
                        throw new OssApiException(ex.Message, response, ex);
                    }
                }
                else if (!response.IsSuccessStatusCode)
                {
                    logger.LogError($"response unsuccess with status code: {response.StatusCode}");
                    return new ApiResponse<ObjectDetails>(response, default(ObjectDetails));
                }
                logger.LogInformation($"Exited from CopyToAsync with response statusCode: {response.StatusCode}");
                return new ApiResponse<ObjectDetails>(response, await LocalMarshalling.DeserializeAsync<ObjectDetails>(response.Content));

            } // using
        }
        /// <summary>
        /// Generate OSS Signed URL
        /// </summary>
        /// <remarks>
        ///Generates a signed URL that can be used to download or upload an object within the specified expiration time. If the object the signed URL points to is deleted or expires before the signed URL expires, the signed URL will no longer be valid.
        ///
        ///In addition to this operation that generates OSS signed URLs, OSS provides operations to generate S3 signed URLs. S3 signed URLs allow direct upload/download from S3 but are restricted to bucket owners. OSS signed URLs also allow upload/download and can be configured for access by other applications, making them suitable for sharing objects across applications.
        ///
        ///Only the application that owns the bucket containing the object can call this operation.
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="bucketKey">
        ///The bucket key of the bucket that contains the objects you are operating on.
        /// </param>
        /// <param name="objectKey">
        ///The URL-encoded human friendly name of the object.
        /// </param>
        /// <param name="access">
        /// (optional)
        /// </param>
        /// <param name="useCdn">
        ///`true` : Returns a Cloudfront URL to the object instead of an Autodesk URL (one that points to a location on https://developer.api.autodesk.com). Applications can interact with the Cloudfront URL exactly like with any classic public resource in OSS. They can use GET requests to download objects or PUT requests to upload objects.
        ///
        ///`false` : (Default) Returns an Autodesk URL (one that points to a location on https://developer.api.autodesk.com) to the object. (optional)
        /// </param>
        /// <param name="createSignedResource">
        /// (optional)
        /// </param>
        /// <returns>Task of ApiResponse&lt;CreateObjectSigned&gt;></returns>

        public async System.Threading.Tasks.Task<ApiResponse<CreateObjectSigned>> CreateSignedResourceAsync(string bucketKey, string objectKey, Access? access = null, bool? useCdn = default(bool?), CreateSignedResource createSignedResource = default(CreateSignedResource), string accessToken = null, bool throwOnError = true)
        {
            logger.LogInformation("Entered into CreateSignedResourceAsync ");
            using (var request = new HttpRequestMessage())
            {
                var queryParam = new Dictionary<string, object>();
                SetQueryParameter("access", access, queryParam);
                SetQueryParameter("useCdn", useCdn, queryParam);
                request.RequestUri =
                    Marshalling.BuildRequestUri($"/oss/v2/buckets/{bucketKey}/objects/{objectKey}/signed",
                        routeParameters: new Dictionary<string, object> {
                            { "bucketKey", bucketKey},
                            { "objectKey", objectKey},
                        },
                        queryParameters: queryParam
                    );

                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("User-Agent", "APS SDK/OSS/C#/1.0.0");
                if (!string.IsNullOrEmpty(accessToken))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");
                }

                request.Content = Marshalling.Serialize(createSignedResource); // http body (model) parameter



                // tell the underlying pipeline what scope we'd like to use
                // if (scopes == null)
                // {
                // TBD:Naren FORCE-4027 - If accessToken is null, acquire auth token using auth SDK, with defined scope.
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), "data:write ");
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
                        throw new OssApiException(ex.Message, response, ex);
                    }
                }
                else if (!response.IsSuccessStatusCode)
                {
                    logger.LogError($"response unsuccess with status code: {response.StatusCode}");
                    return new ApiResponse<CreateObjectSigned>(response, default(CreateObjectSigned));
                }
                logger.LogInformation($"Exited from CreateSignedResourceAsync with response statusCode: {response.StatusCode}");
                return new ApiResponse<CreateObjectSigned>(response, await LocalMarshalling.DeserializeAsync<CreateObjectSigned>(response.Content));

            } // using
        }
        /// <summary>
        /// Delete Object
        /// </summary>
        /// <remarks>
        ///Deletes an object from the bucket.
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="bucketKey">
        ///The bucket key of the bucket that contains the objects you are operating on.
        /// </param>
        /// <param name="objectKey">
        ///The URL-encoded human friendly name of the object.
        /// </param>


        /// <returns>Task of HttpResponseMessage</returns>
        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteObjectAsync(string bucketKey, string objectKey, string accessToken = null, bool throwOnError = true)
        {
            logger.LogInformation("Entered into DeleteObjectAsync ");
            using (var request = new HttpRequestMessage())
            {
                var queryParam = new Dictionary<string, object>();
                request.RequestUri =
                    Marshalling.BuildRequestUri($"/oss/v2/buckets/{bucketKey}/objects/{objectKey}",
                        routeParameters: new Dictionary<string, object> {
                            { "bucketKey", bucketKey},
                            { "objectKey", objectKey},
                        },
                        queryParameters: queryParam
                    );

                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("User-Agent", "APS SDK/OSS/C#/1.0.0");
                if (!string.IsNullOrEmpty(accessToken))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");
                }

                // tell the underlying pipeline what scope we'd like to use
                // if (scopes == null)
                // {
                // TBD:Naren FORCE-4027 - If accessToken is null, acquire auth token using auth SDK, with defined scope.
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), "data:write ");
                // }
                // else
                // {
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), scopes);
                // }

                request.Method = new HttpMethod("DELETE");

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
                        throw new OssApiException(ex.Message, response, ex);
                    }
                }
                else if (!response.IsSuccessStatusCode)
                {
                    logger.LogError($"response unsuccess with status code: {response.StatusCode}");
                    return response;
                }
                logger.LogInformation($"Exited from DeleteObjectAsync with response statusCode: {response.StatusCode}");
                return response;

            } // using
        }
        /// <summary>
        /// Delete Object Using Signed URL
        /// </summary>
        /// <remarks>
        ///Delete an object using an OSS signed URL to access it.
        ///
        ///Only applications that own the bucket containing the object can call this operation. 
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="hash">
        ///The ID component of the signed URL.
        ///
        ///**Note:** The signed URL returned by [Generate OSS Signed URL](/en/docs/data/v2/reference/http/signedresources-:id-GET/) contains `hash` as a URI parameter.
        /// </param>
        /// <param name="xAdsRegion">
        ///Specifies where the bucket containing the object is stored. Possible values are:
        ///
        ///- `US` : (Default) Data center for the US region.
        ///- `EMEA` : Data center for the European Union, Middle East, and Africa.
        ///- `AUS` : (Beta) Data center for Australia.
        ///- `CAN` : Data center for the Canada region.
        ///- `DEU` : Data center for the Germany region.
        ///- `IND` : Data center for the India region.
        ///- `JPN` : Data center for the Japan region.
        ///- `GBR` : Data center for the United Kingdom region.
        ///
        ///**Note:** Beta features are subject to change. Please do not use in production environments. (optional)
        /// </param>

        /// <returns>Task of HttpResponseMessage</returns>
        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteSignedResourceAsync(string hash, Region? xAdsRegion = null, string accessToken = null, bool throwOnError = true)
        {
            logger.LogInformation("Entered into DeleteSignedResourceAsync ");
            using (var request = new HttpRequestMessage())
            {
                var queryParam = new Dictionary<string, object>();
                request.RequestUri =
                    Marshalling.BuildRequestUri($"/oss/v2/signedresources/{hash}",
                        routeParameters: new Dictionary<string, object> {
                            { "hash", hash},
                        },
                        queryParameters: queryParam
                    );

                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("User-Agent", "APS SDK/OSS/C#/1.0.0");
                if (!string.IsNullOrEmpty(accessToken))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");
                }



                SetHeader("x-ads-region", xAdsRegion, request);

                // tell the underlying pipeline what scope we'd like to use
                // if (scopes == null)
                // {
                // TBD:Naren FORCE-4027 - If accessToken is null, acquire auth token using auth SDK, with defined scope.
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), "data:write ");
                // }
                // else
                // {
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), scopes);
                // }

                request.Method = new HttpMethod("DELETE");

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
                        throw new OssApiException(ex.Message, response, ex);
                    }
                }
                else if (!response.IsSuccessStatusCode)
                {
                    logger.LogError($"response unsuccess with status code: {response.StatusCode}");
                    return response;
                }
                logger.LogInformation($"Exited from DeleteSignedResourceAsync with response statusCode: {response.StatusCode}");
                return response;

            } // using
        }
        /// <summary>
        /// Get Object Details
        /// </summary>
        /// <remarks>
        ///Returns detailed information about the specified object.
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="bucketKey">
        ///The bucket key of the bucket that contains the objects you are operating on.
        /// </param>
        /// <param name="objectKey">
        ///The URL-encoded human friendly name of the object.
        /// </param>
        /// <param name="ifModifiedSince">
        ///A timestamp in the HTTP date format (Mon, DD Month YYYY HH:MM:SS GMT). The requested data is returned only if the object has been modified since the specified timestamp. If not, a 304 (Not Modified) HTTP status is returned. (optional)
        /// </param>

        /// <param name="with">
        /// (optional)
        /// </param>
        /// <returns>Task of ApiResponse&lt;ObjectFullDetails&gt;></returns>

        public async System.Threading.Tasks.Task<ApiResponse<ObjectFullDetails>> GetObjectDetailsAsync(string bucketKey, string objectKey, DateTime? ifModifiedSince = default(DateTime?), With? with = null, string accessToken = null, bool throwOnError = true)
        {
            logger.LogInformation("Entered into GetObjectDetailsAsync ");
            using (var request = new HttpRequestMessage())
            {
                var queryParam = new Dictionary<string, object>();
                SetQueryParameter("with", with, queryParam);
                request.RequestUri =
                    Marshalling.BuildRequestUri($"/oss/v2/buckets/{bucketKey}/objects/{objectKey}/details",
                        routeParameters: new Dictionary<string, object> {
                            { "bucketKey", bucketKey},
                            { "objectKey", objectKey},
                        },
                        queryParameters: queryParam
                    );

                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("User-Agent", "APS SDK/OSS/C#/1.0.0");
                if (!string.IsNullOrEmpty(accessToken))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");
                }



                SetHeader("If-Modified-Since", ifModifiedSince, request);


                // tell the underlying pipeline what scope we'd like to use
                // if (scopes == null)
                // {
                // TBD:Naren FORCE-4027 - If accessToken is null, acquire auth token using auth SDK, with defined scope.
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), "data:read ");
                // }
                // else
                // {
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), scopes);
                // }

                request.Method = new HttpMethod("GET");

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
                        throw new OssApiException(ex.Message, response, ex);
                    }
                }
                else if (!response.IsSuccessStatusCode)
                {
                    logger.LogError($"response unsuccess with status code: {response.StatusCode}");
                    return new ApiResponse<ObjectFullDetails>(response, default(ObjectFullDetails));
                }
                logger.LogInformation($"Exited from GetObjectDetailsAsync with response statusCode: {response.StatusCode}");
                return new ApiResponse<ObjectFullDetails>(response, await LocalMarshalling.DeserializeAsync<ObjectFullDetails>(response.Content));

            } // using
        }
        /// <summary>
        /// List Objects
        /// </summary>
        /// <remarks>
        ///Returns a list objects in the specified bucket. 
        ///
        ///Only the application that owns the bucket can call this operation. All other applications that call this operation will receive a "403 Forbidden" error.
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="bucketKey">
        ///The bucket key of the bucket that contains the objects you are operating on.
        /// </param>
        /// <param name="limit">
        ///The number of items you want per page.
        ///Acceptable values = 1-100. Default = 10. (optional, default to 10)
        /// </param>
        /// <param name="beginsWith">
        ///Filters the results by the value you specify. Only the objects with their `objectKey` beginning with the specified string are returned. (optional)
        /// </param>
        /// <param name="startAt">
        ///The ID of the last item that was returned in the previous result set.  It enables the system to return subsequent items starting from the next one after the specified ID. (optional)
        /// </param>
        /// <returns>Task of ApiResponse&lt;BucketObjects&gt;></returns>

        public async System.Threading.Tasks.Task<ApiResponse<BucketObjects>> GetObjectsAsync(string bucketKey, int? limit = default(int?), string beginsWith = default(string), string startAt = default(string), string accessToken = null, bool throwOnError = true)
        {
            logger.LogInformation("Entered into GetObjectsAsync ");
            using (var request = new HttpRequestMessage())
            {
                var queryParam = new Dictionary<string, object>();
                SetQueryParameter("limit", limit, queryParam);
                SetQueryParameter("beginsWith", beginsWith, queryParam);
                SetQueryParameter("startAt", startAt, queryParam);
                request.RequestUri =
                    Marshalling.BuildRequestUri($"/oss/v2/buckets/{bucketKey}/objects",
                        routeParameters: new Dictionary<string, object> {
                            { "bucketKey", bucketKey},
                        },
                        queryParameters: queryParam
                    );

                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("User-Agent", "APS SDK/OSS/C#/1.0.0");
                if (!string.IsNullOrEmpty(accessToken))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");
                }
                // tell the underlying pipeline what scope we'd like to use
                // if (scopes == null)
                // {
                // TBD:Naren FORCE-4027 - If accessToken is null, acquire auth token using auth SDK, with defined scope.
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), "data:read ");
                // }
                // else
                // {
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), scopes);
                // }

                request.Method = new HttpMethod("GET");

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
                        throw new OssApiException(ex.Message, response, ex);
                    }
                }
                else if (!response.IsSuccessStatusCode)
                {
                    logger.LogError($"response unsuccess with status code: {response.StatusCode}");
                    return new ApiResponse<BucketObjects>(response, default(BucketObjects));
                }
                logger.LogInformation($"Exited from GetObjectsAsync with response statusCode: {response.StatusCode}");
                return new ApiResponse<BucketObjects>(response, await LocalMarshalling.DeserializeAsync<BucketObjects>(response.Content));

            } // using
        }
        /// <summary>
        /// Download Object Using Signed URL
        /// </summary>
        /// <remarks>
        ///Downloads an object using an OSS signed URL.
        ///
        ///**Note:** The signed URL returned by [Generate OSS Signed URL](/en/docs/data/v2/reference/http/signedresources-:id-GET/)  contains the `hash` URI parameter as well. 
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="hash">
        ///The ID component of the signed URL.
        ///
        ///**Note:** The signed URL returned by [Generate OSS Signed URL](/en/docs/data/v2/reference/http/signedresources-:id-GET/) contains `hash` as a URI parameter.
        /// </param>
        /// <param name="range">
        ///The byte range to download, specified in the form `bytes=<START_BYTE>-<END_BYTE>`. (optional)
        /// </param>
        /// <param name="ifNoneMatch">
        ///The last known ETag value of the object. OSS returns the requested data  only if the `If-None-Match` header differs from the ETag value of the object on OSS, which indicates that the object on OSS is newer. If not, it returns a 304 "Not Modified" HTTP status. (optional)
        /// </param>
        /// <param name="ifModifiedSince">
        ///A timestamp in the HTTP date format (Mon, DD Month YYYY HH:MM:SS GMT). The requested data is returned only if the object has been modified since the specified timestamp. If not, a 304 (Not Modified) HTTP status is returned. (optional)
        /// </param>
        /// <param name="acceptEncoding">
        ///The compression format your prefer to receive the data. Possible values are:
        ///
        ///- `gzip` - Use the gzip format
        ///
        ///**Note:** You cannot use `Accept-Encoding:gzip` with a Range header containing an end byte range. OSS will not honor the End byte range if `Accept-Encoding: gzip` header is used. (optional)
        /// </param>
        /// <param name="region">
        ///Specifies where the bucket containing the object is stored. Possible values are:
        ///
        ///- `US` : (Default) Data center for the US region.
        ///- `EMEA` : Data center for the European Union, Middle East, and Africa.
        ///- `AUS` : (Beta) Data center for Australia.
        ///- `CAN` : Data center for the Canada region.
        ///- `DEU` : Data center for the Germany region.
        ///- `IND` : Data center for the India region.
        ///- `JPN` : Data center for the Japan region.
        ///- `GBR` : Data center for the United Kingdom region.
        ///
        ///**Note:** Beta features are subject to change. Please do not use in production environments. (optional)
        /// </param>
        /// <param name="responseContentDisposition">
        ///The value of the Content-Disposition header you want to receive when you download the object using the signed URL. If you do not specify a value, the Content-Disposition header defaults to the value stored with OSS. (optional)
        /// </param>
        /// <param name="responseContentType">
        ///The value of the Content-Type header you want to receive when you download the object using the signed URL. If you do not specify a value, the Content-Type header defaults to the value stored with OSS. (optional)
        /// </param>
        /// <returns>Task of ApiResponse&lt;System.IO.Stream&gt;></returns>

        public async System.Threading.Tasks.Task<ApiResponse<System.IO.Stream>> GetSignedResourceAsync(string hash, string range = default(string), string ifNoneMatch = default(string), DateTime? ifModifiedSince = default(DateTime?), string acceptEncoding = default(string), Region? region = null, string responseContentDisposition = default(string), string responseContentType = default(string), string accessToken = null, bool throwOnError = true)
        {
            logger.LogInformation("Entered into GetSignedResourceAsync ");
            using (var request = new HttpRequestMessage())
            {
                var queryParam = new Dictionary<string, object>();
                SetQueryParameter("region", region, queryParam);
                SetQueryParameter("response-content-disposition", responseContentDisposition, queryParam);
                SetQueryParameter("response-content-type", responseContentType, queryParam);
                request.RequestUri =
                    Marshalling.BuildRequestUri($"/oss/v2/signedresources/{hash}",
                        routeParameters: new Dictionary<string, object> {
                            { "hash", hash},
                        },
                        queryParameters: queryParam
                    );

                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("User-Agent", "APS SDK/OSS/C#/1.0.0");
                if (!string.IsNullOrEmpty(accessToken))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");
                }



                SetHeader("Range", range, request);
                SetHeader("If-None-Match", ifNoneMatch, request);
                SetHeader("If-Modified-Since", ifModifiedSince, request);
                SetHeader("Accept-Encoding", acceptEncoding, request);

                // tell the underlying pipeline what scope we'd like to use
                // if (scopes == null)
                // {
                // TBD:Naren FORCE-4027 - If accessToken is null, acquire auth token using auth SDK, with defined scope.
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), "data:read ");
                // }
                // else
                // {
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), scopes);
                // }

                request.Method = new HttpMethod("GET");

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
                        throw new OssApiException(ex.Message, response, ex);
                    }
                }
                else if (!response.IsSuccessStatusCode)
                {
                    logger.LogError($"response unsuccess with status code: {response.StatusCode}");
                    return new ApiResponse<System.IO.Stream>(response, default(System.IO.Stream));
                }
                logger.LogInformation($"Exited from GetSignedResourceAsync with response statusCode: {response.StatusCode}");
                return new ApiResponse<System.IO.Stream>(response, await LocalMarshalling.DeserializeAsync<System.IO.Stream>(response.Content));

            } // using
        }
        /// <summary>
        /// Generate Signed S3 Download URL
        /// </summary>
        /// <remarks>
        ///Gets a signed URL to download an object directly from S3, bypassing OSS servers. This signed URL expires in 2 minutes by default, but you can change this duration if needed.  You must start the download before the signed URL expires. The download itself can take longer. If the download fails after the validity period of the signed URL has elapsed, you can call this operation again to obtain a fresh signed URL.
        ///
        ///Only applications that have read access to the object can call this operation.   
        ///
        ///You can use range headers with the signed download URL to download the object in chunks. This ability lets you download chunks in parallel, which can result in faster downloads.
        ///
        ///If the object you want to download was uploaded in chunks and is still assembling on OSS, you will receive multiple S3 URLs instead of just one. Each URL will point to a chunk. If you prefer to receive a single URL, set the `public-resource-fallback` query parameter to `true`. This setting will make OSS fallback to returning a single signed OSS URL, if assembling is still in progress. 
        ///
        ///In addition to this operation that generates S3 signed URLs, OSS provides an operation to generate OSS signed URLs. S3 signed URLs allow direct upload/download from S3 but are restricted to bucket owners. OSS signed URLs also allow upload/download and can be configured for access by other applications, making them suitable for sharing objects across applications.
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="bucketKey">
        ///The bucket key of the bucket that contains the objects you are operating on.
        /// </param>
        /// <param name="objectKey">
        ///The URL-encoded human friendly name of the object.
        /// </param>
        /// <param name="ifNoneMatch">
        ///The last known ETag value of the object. OSS returns the signed URL only if the `If-None-Match` header differs from the ETag value of the object on S3. If not, it returns a 304 "Not Modified" HTTP status. (optional)
        /// </param>
        /// <param name="ifModifiedSince">
        ///A timestamp in the HTTP date format (Mon, DD Month YYYY HH:MM:SS GMT). The signed URL is returned only if the object has been modified since the specified timestamp. If not, a 304 (Not Modified) HTTP status is returned. (optional)
        /// </param>
        /// <param name="xAdsAcmScopes">
        ///Optional OSS-compliant scope reference to increase bucket search performance (optional)
        /// </param>
        /// <param name="responseContentType">
        ///The value of the Content-Type header you want to receive when you download the object using the signed URL. If you do not specify a value, the Content-Type header defaults to the value stored with OSS. (optional)
        /// </param>
        /// <param name="responseContentDisposition">
        ///The value of the Content-Disposition header you want to receive when you download the object using the signed URL. If you do not specify a value, the Content-Disposition header defaults to the value stored with OSS. (optional)
        /// </param>
        /// <param name="responseCacheControl">
        ///The value of the Cache-Control header you want to receive when you download the object using the signed URL. If you do not specify a value, the Cache-Control header defaults to the value stored with OSS. (optional)
        /// </param>
        /// <param name="publicResourceFallback">
        ///Specifies how to return the signed URLs, in case the object was uploaded in chunks, and assembling of chunks is not yet complete.
        ///
        ///- `true` : Return a single signed OSS URL.
        ///- `false` : (Default) Return multiple signed S3 URLs, where each URL points to a chunk. (optional)
        /// </param>
        /// <param name="minutesExpiration">
        ///The time window (in minutes) the signed URL will remain usable. Acceptable values = 1-60 minutes. Default = 2 minutes.
        ///
        ///**Tip:** Use the smallest possible time window to minimize exposure of the signed URL. (optional)
        /// </param>
        /// <param name="useCdn">
        ///`true` : Returns a URL that points to a CloudFront edge location.
        ///
        ///`false` : (Default) Returns a URL that points directly to the S3 object. (optional)
        /// </param>
        /// <returns>Task of ApiResponse&lt;Signeds3downloadResponse&gt;></returns>

        public async System.Threading.Tasks.Task<ApiResponse<Signeds3downloadResponse>> SignedS3DownloadAsync(string bucketKey, string objectKey, string ifNoneMatch = default(string), DateTime? ifModifiedSince = default(DateTime?), string responseContentType = default(string), string responseContentDisposition = default(string), string responseCacheControl = default(string), bool? publicResourceFallback = default(bool?), int? minutesExpiration = default(int?), bool? useCdn = default(bool?), string accessToken = null, bool throwOnError = true)
        {
            logger.LogInformation("Entered into SignedS3DownloadAsync ");
            using (var request = new HttpRequestMessage())
            {
                var queryParam = new Dictionary<string, object>();
                SetQueryParameter("response-content-type", responseContentType, queryParam);
                SetQueryParameter("response-content-disposition", responseContentDisposition, queryParam);
                SetQueryParameter("response-cache-control", responseCacheControl, queryParam);
                SetQueryParameter("public-resource-fallback", publicResourceFallback, queryParam);
                SetQueryParameter("minutesExpiration", minutesExpiration, queryParam);
                SetQueryParameter("useCdn", useCdn, queryParam);
                request.RequestUri =
                    Marshalling.BuildRequestUri($"/oss/v2/buckets/{bucketKey}/objects/{objectKey}/signeds3download",
                        routeParameters: new Dictionary<string, object> {
                            { "bucketKey", bucketKey},
                            { "objectKey", objectKey},
                        },
                        queryParameters: queryParam
                    );

                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("User-Agent", "APS SDK/OSS/C#/1.0.0");
                if (!string.IsNullOrEmpty(accessToken))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");
                }



                SetHeader("If-None-Match", ifNoneMatch, request);
                SetHeader("If-Modified-Since", ifModifiedSince, request);

                // tell the underlying pipeline what scope we'd like to use
                // if (scopes == null)
                // {
                // TBD:Naren FORCE-4027 - If accessToken is null, acquire auth token using auth SDK, with defined scope.
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), "data:read ");
                // }
                // else
                // {
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), scopes);
                // }

                request.Method = new HttpMethod("GET");

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
                        throw new OssApiException(ex.Message, response, ex);
                    }
                }
                else if (!response.IsSuccessStatusCode)
                {
                    logger.LogError($"response unsuccess with status code: {response.StatusCode}");
                    return new ApiResponse<Signeds3downloadResponse>(response, default(Signeds3downloadResponse));
                }
                logger.LogInformation($"Exited from SignedS3DownloadAsync with response statusCode: {response.StatusCode}");
                return new ApiResponse<Signeds3downloadResponse>(response, await LocalMarshalling.DeserializeAsync<Signeds3downloadResponse>(response.Content));

            } // using
        }
        /// <summary>
        /// Generate Signed S3 Upload URL
        /// </summary>
        /// <remarks>
        ///Gets a signed URL to upload an object directly to S3, bypassing OSS servers. You can also request an array of signed URLs which lets you upload an object in chunks.
        ///
        ///This signed URL expires in 2 minutes by default, but you can change this duration if needed.  You must start the upload before the signed URL expires. The upload itself can take longer. If the upload fails after the validity period of the signed URL has elapsed, you can call this operation again to obtain a fresh signed URL (or an array of signed URLs as the case may be). However, you must use the same `uploadKey` that was returned when you originally called this operation. 
        ///
        ///Only applications that own the bucket can call this operation.
        ///
        ///**Note:** Once you upload all chunks you must call the [Complete Upload to S3 Signed URL](/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectKey-signeds3upload-POST/) operation to indicate completion. This instructs OSS to assemble the chunks and reconstitute the object on OSS. You must call this operation even when using a single signed URL. 
        ///
        ///In addition to this operation that generates S3 signed URLs, OSS provides an operation to generate OSS signed URLs. S3 signed URLs allow direct upload/download from S3 but are restricted to bucket owners. OSS signed URLs also allow upload/download and can be configured for access by other applications, making them suitable for sharing objects across applications.    
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="bucketKey">
        ///The bucket key of the bucket that contains the objects you are operating on.
        /// </param>
        /// <param name="objectKey">
        ///The URL-encoded human friendly name of the object.
        /// </param>
        /// <param name="parts">
        ///The number of parts you intend to chunk the object for uploading. OSS will return that many signed URLs, one URL for each chunk. If you do not specify a value you'll get only one URL to upload the entire object.              (optional)
        /// </param>
        /// <param name="firstPart">
        ///The index of the first chunk to be uploaded. (optional)
        /// </param>
        /// <param name="uploadKey">
        ///The `uploadKey` of a previously-initiated upload, in order to request more chunk upload URLs for the same upload. If you do not specify a value, OSS will initiate a new upload entirely. (optional)
        /// </param>
        /// <param name="minutesExpiration">
        ///The time window (in minutes) the signed URL will remain usable. Acceptable values = 1-60 minutes. Default = 2 minutes.
        ///
        ///**Tip:** Use the smallest possible time window to minimize exposure of the signed URL. (optional)
        /// </param>
        /// <param name="useAcceleration">
        ///`true` : (Default) Generates a faster S3 signed URL using Transfer Acceleration.
        ///
        ///`false`: Generates a standard S3 signed URL. (optional)
        /// </param>
        /// <returns>Task of ApiResponse&lt;Signeds3uploadResponse&gt;></returns>

        public async System.Threading.Tasks.Task<ApiResponse<Signeds3uploadResponse>> SignedS3UploadAsync(string bucketKey, string objectKey, int? parts = default(int?), int? firstPart = default(int?), string uploadKey = default(string), int? minutesExpiration = default(int?), bool? useAcceleration = default(bool?), string accessToken = null, bool throwOnError = true)
        {
            logger.LogInformation("Entered into SignedS3UploadAsync ");
            using (var request = new HttpRequestMessage())
            {
                var queryParam = new Dictionary<string, object>();
                SetQueryParameter("parts", parts, queryParam);
                SetQueryParameter("firstPart", firstPart, queryParam);
                SetQueryParameter("uploadKey", uploadKey, queryParam);
                SetQueryParameter("minutesExpiration", minutesExpiration, queryParam);
                SetQueryParameter("useAcceleration", useAcceleration, queryParam);
                request.RequestUri =
                    Marshalling.BuildRequestUri($"/oss/v2/buckets/{bucketKey}/objects/{objectKey}/signeds3upload",
                        routeParameters: new Dictionary<string, object> {
                            { "bucketKey", bucketKey},
                            { "objectKey", objectKey},
                        },
                        queryParameters: queryParam
                    );

                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("User-Agent", "APS SDK/OSS/C#/1.0.0");
                if (!string.IsNullOrEmpty(accessToken))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");
                }



                // tell the underlying pipeline what scope we'd like to use
                // if (scopes == null)
                // {
                // TBD:Naren FORCE-4027 - If accessToken is null, acquire auth token using auth SDK, with defined scope.
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), "data:write data:create ");
                // }
                // else
                // {
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), scopes);
                // }

                request.Method = new HttpMethod("GET");

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
                        throw new OssApiException(ex.Message, response, ex);
                    }
                }
                else if (!response.IsSuccessStatusCode)
                {
                    logger.LogError($"response unsuccess with status code: {response.StatusCode}");
                    return new ApiResponse<Signeds3uploadResponse>(response, default(Signeds3uploadResponse));
                }
                logger.LogInformation($"Exited from SignedS3UploadAsync with response statusCode: {response.StatusCode}");
                return new ApiResponse<Signeds3uploadResponse>(response, await LocalMarshalling.DeserializeAsync<Signeds3uploadResponse>(response.Content));

            } // using
        }
        /// <summary>
        /// Replace Object Using Signed URL
        /// </summary>
        /// <remarks>
        ///Replaces an object that already exists on OSS, using an OSS signed URL. 
        ///
        ///The signed URL must fulfil the following conditions:
        ///
        ///- The signed URL is valid (it has not expired as yet).
        ///- It was generated with `write` or `readwrite` for the `access` parameter.
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="hash">
        ///The ID component of the signed URL.
        ///
        ///**Note:** The signed URL returned by [Generate OSS Signed URL](/en/docs/data/v2/reference/http/signedresources-:id-GET/) contains `hash` as a URI parameter.
        /// </param>
        /// <param name="contentLength">
        ///The size of the data contained in the request body, in bytes.
        /// </param>
        /// <param name="body">
        ///The object to upload.
        /// </param>
        /// <param name="contentType">
        ///The MIME type of the object to upload; can be any type except 'multipart/form-data'. This can be omitted, but we recommend adding it. (optional)
        /// </param>
        /// <param name="contentDisposition">
        ///The suggested file name to use when this object is downloaded as a file. (optional)
        /// </param>
        /// <param name="xAdsRegion">
        ///Specifies where the bucket containing the object is stored. Possible values are:
        ///
        ///- `US` : (Default) Data center for the US region.
        ///- `EMEA` : Data center for the European Union, Middle East, and Africa.
        ///- `AUS` : (Beta) Data center for Australia.
        ///- `CAN` : Data center for the Canada region.
        ///- `DEU` : Data center for the Germany region.
        ///- `IND` : Data center for the India region.
        ///- `JPN` : Data center for the Japan region.
        ///- `GBR` : Data center for the United Kingdom region.
        ///
        ///**Note:** Beta features are subject to change. Please do not use in production environments. (optional)
        /// </param>
        /// <param name="ifMatch">
        ///The current value of the `sha1` attribute of the object you want to replace. OSS checks the `If-Match` header against the `sha1` attribute of the object in OSS. If they match, OSS allows the object to be overwritten. Otherwise, it means that the object on OSS has been modified since you retrieved the `sha1` and the request fails. (optional)
        /// </param>
        /// <returns>Task of ApiResponse&lt;ObjectDetails&gt;></returns>

        public async System.Threading.Tasks.Task<ApiResponse<ObjectDetails>> UploadSignedResourceAsync(string hash, int? contentLength, System.IO.Stream body, string contentType = default(string), string contentDisposition = default(string), Region? xAdsRegion = null, string ifMatch = default(string), string accessToken = null, bool throwOnError = true)
        {
            logger.LogInformation("Entered into UploadSignedResourceAsync ");
            using (var request = new HttpRequestMessage())
            {
                var queryParam = new Dictionary<string, object>();
                request.RequestUri =
                    Marshalling.BuildRequestUri($"/oss/v2/signedresources/{hash}",
                        routeParameters: new Dictionary<string, object> {
                            { "hash", hash},
                        },
                        queryParameters: queryParam
                    );

                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("User-Agent", "APS SDK/OSS/C#/1.0.0");
                if (!string.IsNullOrEmpty(accessToken))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");
                }


                request.Content = new StreamContent(body);

                SetHeader("Content-Type", contentType, request);
                SetHeader("Content-Length", contentLength, request);
                SetHeader("Content-Disposition", contentDisposition, request);
                SetHeader("x-ads-region", xAdsRegion, request);
                SetHeader("If-Match", ifMatch, request);

                // tell the underlying pipeline what scope we'd like to use
                // if (scopes == null)
                // {
                // TBD:Naren FORCE-4027 - If accessToken is null, acquire auth token using auth SDK, with defined scope.
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), "data:write ");
                // }
                // else
                // {
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), scopes);
                // }

                request.Method = new HttpMethod("PUT");

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
                        throw new OssApiException(ex.Message, response, ex);
                    }
                }
                else if (!response.IsSuccessStatusCode)
                {
                    logger.LogError($"response unsuccess with status code: {response.StatusCode}");
                    return new ApiResponse<ObjectDetails>(response, default(ObjectDetails));
                }
                logger.LogInformation($"Exited from UploadSignedResourceAsync with response statusCode: {response.StatusCode}");
                return new ApiResponse<ObjectDetails>(response, await LocalMarshalling.DeserializeAsync<ObjectDetails>(response.Content));

            } // using
        }
        /// <summary>
        /// Upload Object Using Signed URL
        /// </summary>
        /// <remarks>
        ///Performs a resumable upload using an OSS signed URL. Use this operation to upload an object in chunks.
        ///
        ///**Note:** The signed URL returned by [Generate OSS Signed URL](/en/docs/data/v2/reference/http/signedresources-:id-GET/) contains the `hash` as a URI parameter. 
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown when fails to make API call</exception>
        /// <param name="hash">
        ///The ID component of the signed URL.
        ///
        ///**Note:** The signed URL returned by [Generate OSS Signed URL](/en/docs/data/v2/reference/http/signedresources-:id-GET/) contains `hash` as a URI parameter.
        /// </param>
        /// <param name="contentRange">
        ///The byte range to upload, specified in the form `bytes <START_BYTE>-<END_BYTE>/<TOTAL_BYTES>`.
        /// </param>
        /// <param name="sessionId">
        ///An ID to uniquely identify the file upload session.
        /// </param>
        /// <param name="body">
        ///The chunk to upload.
        /// </param>
        /// <param name="contentType">
        ///The MIME type of the object to upload; can be any type except 'multipart/form-data'. This can be omitted, but we recommend adding it. (optional)
        /// </param>
        /// <param name="contentDisposition">
        ///The suggested file name to use when this object is downloaded as a file. (optional)
        /// </param>
        /// <param name="xAdsRegion">
        ///Specifies where the bucket containing the object is stored. Possible values are:
        ///
        ///- `US` : (Default) Data center for the US region.
        ///- `EMEA` : Data center for the European Union, Middle East, and Africa.
        ///- `AUS` : (Beta) Data center for Australia.
        ///- `CAN` : Data center for the Canada region.
        ///- `DEU` : Data center for the Germany region.
        ///- `IND` : Data center for the India region.
        ///- `JPN` : Data center for the Japan region.
        ///- `GBR` : Data center for the United Kingdom region.
        ///
        ///**Note:** Beta features are subject to change. Please do not use in production environments. (optional)
        /// </param>
        /// <returns>Task of ApiResponse&lt;ObjectDetails&gt;></returns>

        public async System.Threading.Tasks.Task<ApiResponse<ObjectDetails>> UploadSignedResourcesChunkAsync(string hash, string contentRange, string sessionId, System.IO.Stream body, string contentType = default(string), string contentDisposition = default(string), Region? xAdsRegion = null, string accessToken = null, bool throwOnError = true)
        {
            logger.LogInformation("Entered into UploadSignedResourcesChunkAsync ");
            using (var request = new HttpRequestMessage())
            {
                var queryParam = new Dictionary<string, object>();
                request.RequestUri =
                    Marshalling.BuildRequestUri($"/oss/v2/signedresources/{hash}/resumable",
                        routeParameters: new Dictionary<string, object> {
                            { "hash", hash},
                        },
                        queryParameters: queryParam
                    );

                request.Headers.TryAddWithoutValidation("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("User-Agent", "APS SDK/OSS/C#/1.0.0");
                if (!string.IsNullOrEmpty(accessToken))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");
                }


                request.Content = new StreamContent(body);

                SetHeader("Content-Type", contentType, request);
                SetHeader("Content-Range", contentRange, request);
                SetHeader("Content-Disposition", contentDisposition, request);
                SetHeader("x-ads-region", xAdsRegion, request);
                SetHeader("Session-Id", sessionId, request);

                // tell the underlying pipeline what scope we'd like to use
                // if (scopes == null)
                // {
                // TBD:Naren FORCE-4027 - If accessToken is null, acquire auth token using auth SDK, with defined scope.
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), "data:write ");
                // }
                // else
                // {
                // request.Properties.Add(ForgeApsConfiguration.ScopeKey.ToString(), scopes);
                // }

                request.Method = new HttpMethod("PUT");

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
                        throw new OssApiException(ex.Message, response, ex);
                    }
                }
                else if (!response.IsSuccessStatusCode)
                {
                    logger.LogError($"response unsuccess with status code: {response.StatusCode}");
                    return new ApiResponse<ObjectDetails>(response, default(ObjectDetails));
                }
                logger.LogInformation($"Exited from UploadSignedResourcesChunkAsync with response statusCode: {response.StatusCode}");
                return (response.Content?.Headers.ContentType?.MediaType == null)
                    ? new ApiResponse<ObjectDetails>(response, default(ObjectDetails))
                    : new ApiResponse<ObjectDetails>(response, await LocalMarshalling.DeserializeAsync<ObjectDetails>(response.Content));

            } // using
        }
    }
}
