using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HelpDeskApp.Shared;

namespace HelpDeskApp.Server.Controllers
{
    [Route("Storage")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly ILogger<StorageController> _logger;
        private readonly IFileStorage _storage;    
        private const int MaxFilenameLength = 50;
        private static readonly Regex filenameRegex = new Regex("[^a-zA-Z0-9._]");
        public StorageController(
            ILogger<StorageController> logger,
            IFileStorage storage
            )
        {
            _logger = logger;
            _storage = storage;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetBlobNames()
        {
            return await _storage.GetNames();
        }

        [HttpGet("{blobName}")]
        public async Task<IActionResult> GetBlobName(string blobName)
        {
            if( blobName == null ){
                return NotFound();
            }
            var stream = await _storage.Load(blobName);
            // This usage of File() always triggers the browser to perform a file download.
            // We always use "application/octet-stream" as the content type because we don't record
            // any information about content type from the user when they upload a file.
            return File(stream, "application/octet-stream", blobName);
        }

        [HttpPost("[action]")]
        public async Task Save(IList<IFormFile> UploadFiles)
        {
            try
            {
                foreach ( var file in UploadFiles)
                {
                    // IFormFile.FileName is untrustworthy user input, and we're
                    // using it for both blob names and for display on the page,
                    // so we aggressively sanitize. In a real app, we'd probably
                    // do something more complex and robust for handling filenames.
                    var name = SanitizeFilename(file.FileName);

                    if (String.IsNullOrWhiteSpace(name))
                    {
                        throw new ArgumentException();
                    }

                    using (Stream stream = file.OpenReadStream())
                    {
                        await _storage.Save(stream, name);
                    }
                }
            }
            catch (Exception e)
            {
                Response.Clear();
                Response.StatusCode = 204;
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "File failed to upload";
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = e.Message;   
            }
        }   

        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteBlob(string name)
        {
            // IFormFile.FileName is untrustworthy user input, and we're
            // using it for both blob names and for display on the page,
            // so we aggressively sanitize. In a real app, we'd probably
            // do something more complex and robust for handling filenames.
            name = SanitizeFilename(name);

            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException();
            }

            await _storage.Delete(name);

            return new OkResult();
        }   

        private static string SanitizeFilename(string filename)
        {
            var sanitizedFilename = filenameRegex.Replace(filename, "").TrimEnd('.');

            if (sanitizedFilename.Length > MaxFilenameLength)
            {
                sanitizedFilename = sanitizedFilename.Substring(0, MaxFilenameLength);
            }

            return sanitizedFilename;
        }
    }
}
