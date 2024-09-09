namespace CB.Domain.Common.Helpers;

using FluentFTP;
using FluentFTP.Helpers;
using Microsoft.Extensions.Configuration;
using FtpConfig = Configs.FtpConfig;

public static class FtpHelper {
    private static readonly FluentFTP.FtpConfig configClient = new() {
        EncryptionMode = FtpEncryptionMode.Explicit,
        ValidateAnyCertificate = true
    };




    public static async Task UploadImageAsync(string filename, byte[]? data, IConfiguration configuration, string key = "Ftp") {
        await UploadFile(filename, data, configuration.GetSection(key).Get<FtpConfig>()!);
    }

    public static async Task UploadFile(string filename, byte[]? data, IConfiguration configuration, string key = "Ftp") {
        await UploadFile(filename, data, configuration.GetSection(key).Get<FtpConfig>()!);
    }

    public static async Task<byte[]> DownloadBytes(string filename, IConfiguration configuration, string key = "Ftp") {
        return await DownloadBytes(filename, configuration.GetSection(key).Get<FtpConfig>()!);
    }

    public static async Task<FtpStatus> DownloadFile(string localPath, string filename, IConfiguration configuration, string key = "Ftp") {
        return await DownloadFile(localPath, filename, configuration.GetSection(key).Get<FtpConfig>()!);
    }

    public static async Task DeleteFileAsync(string filename, IConfiguration configuration, string key = "Ftp") {
        await DeleteFile(filename, configuration.GetSection(key).Get<FtpConfig>()!);
    }

    private static async Task UploadFile(string filename, byte[]? data, FtpConfig config) {
        if (data == null) return;
        using var client = new AsyncFtpClient(config.Host, config.Username, config.Password, config.Port, config: configClient);
        await client.AutoConnect();

        // Dir found
        var dir = filename.GetFtpDirectoryName() ?? string.Empty;
        var existed = await client.DirectoryExists(dir);
        if (!existed) await client.CreateDirectory(dir);

        using Stream stream = new MemoryStream(data);
        await client.UploadStream(stream, filename);
    }

    private static async Task<byte[]> DownloadBytes(string path, FtpConfig config) {
        using var client = new AsyncFtpClient(config.Host, config.Username, config.Password, config: configClient);
        await client.AutoConnect();
        var existed = await client.FileExists(path);
        return !existed ? [] : await client.DownloadBytes(path, 0);
    }

    private static async Task<FtpStatus> DownloadFile(string localPath, string path, FtpConfig config) {
        using var client = new AsyncFtpClient(config.Host, config.Username, config.Password, config: configClient);
        await client.AutoConnect();
        var existed = await client.FileExists(path);
        return !existed ? FtpStatus.Failed : await client.DownloadFile(localPath, path, FtpLocalExists.Overwrite);
    }

    private static async Task DeleteFile(string filename, FtpConfig config) {
        using var client = new AsyncFtpClient(config.Host, config.Username, config.Password, config: configClient);
        await client.AutoConnect();
        var existed = await client.FileExists(filename);
        if (!existed) return;

        await client.DeleteFile(filename);
    }
}
