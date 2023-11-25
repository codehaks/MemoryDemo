using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;



Console.WriteLine("Press any key to start...");

Console.WriteLine("Proccessor Id : {0}", Process.GetCurrentProcess().Id);

Console.ReadLine();
//await CopyFilesAsync();
await StreamCopyFilesAsync();
Console.ReadLine();
Console.WriteLine("Done");



static async Task StreamCopyFilesAsync()
{
    byte[] buffer = new byte[16 * 1024];
    var files = Directory.GetFiles(@"G:\Projects\Data\Fake-Faces");

    var c = 0;
    foreach (var file in files)
    {
        Console.WriteLine(file);
        long totalBytes = new FileInfo(file).Length;

        using var output = File.Create(@"G:\Projects\Data\Fake-Faces2\" + c + ".jpg");

        Stream input = new FileStream(file, FileMode.Open);
        long totalReadBytes = 0;
        int readBytes;

        while ((readBytes = input.Read(buffer, 0, buffer.Length)) > 0)
        {
            await output.WriteAsync(buffer, 0, readBytes);
            totalReadBytes += readBytes;
        }

        c++;



    }
}

static async Task CopyFilesAsync()
{
    var files = System.IO.Directory.GetFiles(@"E:\Projects\Data\faces");
    var c = 0;
    foreach (var file in files)
    {
        Console.WriteLine(file);
        var data = await File.ReadAllBytesAsync(file);
        await File.WriteAllBytesAsync(@"E:\Projects\Data\faces2\" + c + ".jpg", data);
        c++;
    }
}
