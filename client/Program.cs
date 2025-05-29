using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
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
