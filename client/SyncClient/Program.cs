using System;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Current Directory: " + Directory.GetCurrentDirectory());

        var syncer = new SyncManager();

        Console.WriteLine("[1] Upload files\n[2] Download file");
        var option = Console.ReadLine();

        if (option == "1")
            await syncer.UploadAllAsync();
        else if (option == "2")
        {
            Console.Write("Enter filename to download: ");
            var name = Console.ReadLine();
            await syncer.DownloadLatestAsync(name);
        }
    }
}
