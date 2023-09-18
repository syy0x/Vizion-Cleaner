using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

public static class Cleaning
{
    public static void PerformComputerCleanup()
    {
        while (true)
        {
            Console.Clear(); // Effacer le terminal

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" _          _        _           _                  _          _            _          ");
            Console.WriteLine("/\\ \\    _ / /\\      /\\ \\       /\\ \\                /\\ \\       /\\ \\         /\\ \\     _  ");
            Console.WriteLine("\\ \\ \\  /_/ / /      \\ \\ \\     /  \\ \\               \\ \\ \\     /  \\ \\       /  \\ \\   /\\_\\");
            Console.WriteLine("\\ \\ \\  /_/ / /      \\ \\ \\     /  \\ \\               \\ \\ \\     /  \\ \\       /  \\ \\   /\\_\\");
            Console.WriteLine(" \\ \\ \\ \\___\\/       /\\ \\_\\ __/ /\\ \\ \\              /\\ \\_\\   / /\\ \\ \\     / /\\ \\ \\_/ / /");
            Console.WriteLine(" / / /  \\ \\ \\      / /\\/_//___/ /\\ \\ \\            / /\\/_/  / / /\\ \\ \\   / / /\\ \\___/ / ");
            Console.WriteLine(" \\ \\ \\   \\_\\ \\    / / /   \\___\\/ / / /           / / /    / / /  \\ \\_\\ / / /  \\/____/  ");
            Console.WriteLine("  \\ \\ \\  / / /   / / /          / / /           / / /    / / /   / / // / /    / / /   ");
            Console.WriteLine("   \\ \\ \\/ / /   / / /          / / /    _      / / /    / / /   / / // / /    / / /    ");
            Console.WriteLine("    \\ \\ \\/ /___/ / /__         \\ \\ \\__/\\_\\ ___/ / /__  / / /___/ / // / /    / / /     ");
            Console.WriteLine("     \\ \\  //\\__\\/_/___\\         \\ \\___\\/ //\\__\\/_/___\\/ / /____\\/ // / /    / / /      ");
            Console.WriteLine("      \\_\\/ \\/_________/          \\/___/_/ \\/_________/\\/_________/ \\/_/     \\/_/       ");
            Console.WriteLine("                                                                                       ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("                         Free and Open Source cleaning software!");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nVizion Cleaner - Computer cleaning menu :");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[1]");
            Console.ResetColor();

            Console.WriteLine(" Empty trash");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[2]");
            Console.ResetColor();

            Console.WriteLine(" Empty folder in C:\Windows\Temp");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[3]");
            Console.ResetColor();

            Console.WriteLine(" Empty folder in C:\Windows\Prefetch");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[4]");
            Console.ResetColor();

            Console.WriteLine(" Delete files in C:\Windows\SoftwareDistribution\Download");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[5]");
            Console.ResetColor();

            Console.WriteLine(" Open the disk cleaning tool");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[6]");
            Console.ResetColor();

            Console.WriteLine(" Defragment disk C");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[7]");
            Console.ResetColor();

            Console.WriteLine(" Clean and delete Windows temporary files (Use last)");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[8]");
            Console.ResetColor();

            Console.WriteLine(" Cleaning the DNS cache");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[9]");
            Console.ResetColor();

            Console.WriteLine(" Back to main menu\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Choose an option : ");
            Console.ResetColor();
            char choice = Console.ReadKey().KeyChar;

            switch (choice)
            {
                case '1':
                    CleanCorbeille();
                    break;
                case '2':
                    CleanTemp();
                    break;
                case '3':
                    CleanPrefetch(@"C:\Windows\Prefetch");
                    break;
                case '4':
                    CleanSDD();
                    break;
                case '5':
                    OpenDC();
                    break;
                case '6':
                    DefragmentDrive("C:");
                    break;
                case '7':
                    RunChkdsk();
                    break;
                case '8':
                    FlushDnsCache();
                    break;
                case '9':
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid option.");
                    Console.ResetColor();
                    break;
            }
        }
    }

    static void CleanCorbeille()
    {
        try
        {
            var psi = new ProcessStartInfo("cmd.exe")
            {
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = new Process { StartInfo = psi };
            process.Start();

            using (var sw = process.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    sw.WriteLine("rd /s /q C:\\$Recycle.Bin");
                }
            }

            process.WaitForExit();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nThe trash can has been successfully emptied.");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nError when deleting the Recycle Bin : " + ex.Message);
            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Press a key to continue.");
        Console.ResetColor();
        Console.ReadKey();
    }

    static void CleanTemp()
    {
        string tempFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Temp");

        try
        {
            DirectoryInfo tempDir = new DirectoryInfo(tempFolderPath);

            foreach (FileInfo file in tempDir.GetFiles())
            {
                file.Delete();
            }

            foreach (DirectoryInfo subDir in tempDir.GetDirectories())
            {
                subDir.Delete(true);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nTemp file successfully emptied.");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nError deleting Temp folder : {ex.Message}");
            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Press a key to continue.");
        Console.ResetColor();
        Console.ReadKey();
    }

    static void CleanPrefetch(string folderPath)
    {
        try
        {
            DirectoryInfo prefetchDir = new DirectoryInfo(folderPath);

            foreach (FileInfo file in prefetchDir.GetFiles())
            {
                file.Delete();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nThe Prefetch file has been successfully emptied.");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nError deleting Prefetch folder : {ex.Message}");
            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Press a key to continue.");
        Console.ResetColor();
        Console.ReadKey();
    }

    static void CleanSDD()
    {
        string downloadFolder = @"C:\Windows\SoftwareDistribution\Download";

        try
        {
            DirectoryInfo downloadDir = new DirectoryInfo(downloadFolder);

            foreach (FileInfo file in downloadDir.GetFiles())
            {
                file.Delete();
            }

            foreach (DirectoryInfo subDir in downloadDir.GetDirectories())
            {
                subDir.Delete(true);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nThe SoftwareDistribution\Download folder has been successfully emptied.");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nError deleting SoftwareDistribution\Download folder : {ex.Message}");
            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Press a key to continue.");
        Console.ResetColor();
        Console.ReadKey();
    }

    static void OpenDC()
    {
        try
        {
            Process.Start("cleanmgr", "/sagerun:65535");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nOpening the current disk cleaning tool...");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nError opening disk cleaning tool : {ex.Message}");
            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Press a key to continue.");
        Console.ResetColor();
        Console.ReadKey();
    }

    static void RunChkdsk()
    {
        try
        {
            Process.Start("cmd.exe", "/c chkdsk C: /F /R");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nExecution of chkdsk C: /F /R in progress...");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nError running chkdsk C: /F /R : {ex.Message}");
            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Press a key to continue.");
        Console.ResetColor();
        Console.ReadKey();
    }

    static void DefragmentDrive(string driveLetter)
    {
        try
        {
            Process.Start("cmd.exe", $"/c defrag {driveLetter}: /U /V");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nRunning {driveLetter} defragmentation...");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nError executing {driveLetter} defragmentation. : {ex.Message}");
            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Press a key to continue.");
        Console.ResetColor();
        Console.ReadKey();
    }

    static void FlushDnsCache()
    {
        try
        {
            Process.Start("cmd.exe", "/c ipconfig /flushdns");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nDNS cache cleanup in progress...");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nDNS cache cleanup error : {ex.Message}");
            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Press a key to continue.");
        Console.ResetColor();
        Console.ReadKey();
    }
}
