﻿using CB.Domain.Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CB.Api.Controllers;
[ApiController, AllowAnonymous, Route("images")]
public class ImageController(IServiceProvider serviceProvider) : BaseController(serviceProvider) {
    private readonly string[] imageExtention = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff" };
    private readonly IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();

    [HttpGet, Route("{path}")]
    public async Task<IActionResult> View(string path) {
        var datas = await FtpHelper.DownloadBytes($"images/{path}", configuration);

        string extension = Path.GetExtension(path).ToLowerInvariant();
        if (Array.IndexOf(imageExtention, extension) < 0) {
            return BadRequest("Invalid image file extension.");
        }

        // Check MIME type
        new FileExtensionContentTypeProvider().TryGetContentType(path, out var mimeType);
        return File(datas, mimeType ?? "application/octet-stream"); // Change the MIME type as needed
    }

}
