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
            Console.WriteLine("\nVizion Cleaner - Menu du nettoyage d'ordinateur :");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[1]");
            Console.ResetColor();

            Console.WriteLine(" Vider la corbeille");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[2]");
            Console.ResetColor();

            Console.WriteLine(" Vider le dossier dans C:\\Windows\\Temp");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[3]");
            Console.ResetColor();

            Console.WriteLine(" Vider le dossier dans C:\\Windows\\Prefetch");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[4]");
            Console.ResetColor();

            Console.WriteLine(" Supprimer les fichiers dans C:\\Windows\\SoftwareDistribution\\Download");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[5]");
            Console.ResetColor();

            Console.WriteLine(" Ouvrir l'outil de nettoyage de disque");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[6]");
            Console.ResetColor();

            Console.WriteLine(" Défragmenter le disque C");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[7]");
            Console.ResetColor();

            Console.WriteLine(" Nettoyer et supprimer les fichiers temporaires de Windows (A utiliser en dernier)");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[8]");
            Console.ResetColor();

            Console.WriteLine(" Nettoyer le cache DNS");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[9]");
            Console.ResetColor();

            Console.WriteLine(" Retour au menu principal\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Choisissez une option : ");
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
                    Console.WriteLine("\nOption invalide.");
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
            Console.WriteLine("\nLa corbeille a été vidée avec succès.");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nErreur lors de la suppression de la corbeille : " + ex.Message);
            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Appuyez sur une touche pour continuer.");
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
            Console.WriteLine("\nLe dossier Temp a été vidé avec succès.");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nErreur lors de la suppression du dossier Temp : {ex.Message}");
            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Appuyez sur une touche pour continuer.");
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
            Console.WriteLine("\nLe dossier Prefetch a été vidé avec succès.");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nErreur lors de la suppression du dossier Prefetch : {ex.Message}");
            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Appuyez sur une touche pour continuer.");
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
            Console.WriteLine("\nLe dossier SoftwareDistribution\\Download a été vidé avec succès.");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nErreur lors de la suppression du dossier SoftwareDistribution\\Download : {ex.Message}");
            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Appuyez sur une touche pour continuer.");
        Console.ResetColor();
        Console.ReadKey();
    }

    static void OpenDC()
    {
        try
        {
            Process.Start("cleanmgr", "/sagerun:65535");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nOuverture de l'outil de nettoyage de disque en cours...");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nErreur lors de l'ouverture de l'outil de nettoyage de disque : {ex.Message}");
            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Appuyez sur une touche pour continuer.");
        Console.ResetColor();
        Console.ReadKey();
    }

    static void RunChkdsk()
    {
        try
        {
            Process.Start("cmd.exe", "/c chkdsk C: /F /R");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nExécution de chkdsk C: /F /R en cours...");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nErreur lors de l'exécution de chkdsk C: /F /R : {ex.Message}");
            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Appuyez sur une touche pour continuer.");
        Console.ResetColor();
        Console.ReadKey();
    }

    static void DefragmentDrive(string driveLetter)
    {
        try
        {
            Process.Start("cmd.exe", $"/c defrag {driveLetter}: /U /V");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nExécution de la défragmentation de {driveLetter} en cours...");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nErreur lors de l'exécution de la défragmentation de {driveLetter} : {ex.Message}");
            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Appuyez sur une touche pour continuer.");
        Console.ResetColor();
        Console.ReadKey();
    }

    static void FlushDnsCache()
    {
        try
        {
            Process.Start("cmd.exe", "/c ipconfig /flushdns");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nNettoyage du cache DNS en cours...");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nErreur lors du nettoyage du cache DNS : {ex.Message}");
            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Appuyez sur une touche pour continuer.");
        Console.ResetColor();
        Console.ReadKey();
    }
}