using System.IO;
using System.Net.Http;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class SyncManager
{
    private string serverUrl;
    private string syncFolder;
    private string deviceId;
    private readonly HttpClient client = new();

    public SyncManager()
    {
        dynamic config = JsonConvert.DeserializeObject(File.ReadAllText("Config.json"));
        serverUrl = config.server_url;
        syncFolder = config.sync_folder;
        deviceId = config.device_id;
    }

    public async Task UploadAllAsync()
    {
        foreach (var file in Directory.GetFiles(syncFolder))
        {
            var fileName = Path.GetFileName(file);
            using var content = new MultipartFormDataContent();
            content.Add(new StreamContent(File.OpenRead(file)), "file", fileName);
            content.Add(new StringContent(deviceId), "device_id");

            var response = await client.PostAsync($"{serverUrl}/upload", content);
            Console.WriteLine($"{fileName}: {await response.Content.ReadAsStringAsync()}");
        }
    }

    public async Task DownloadLatestAsync(string filename)
    {
        var response = await client.GetAsync($"{serverUrl}/download?filename={filename}");
        if (response.IsSuccessStatusCode)
        {
            var savePath = Path.Combine(syncFolder, filename);
            using var fs = new FileStream(savePath, FileMode.Create);
            await response.Content.CopyToAsync(fs);
            Console.WriteLine($"Downloaded {filename} to {savePath}");
        }
        else
        {
            Console.WriteLine($"Failed to download {filename}");
        }
    }
}
