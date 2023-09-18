using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    static void Main()
    {
        if (!IsRunningAsAdmin())
        {
            RelaunchAsAdmin();
            return;
        }

        Console.Title = "Administrator: Vizion Cleaner";

        ConsoleColor originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Cyan;

        AnimateStep("[1/4] Connection to server");
        AnimateStep("[2/4] Authentification");
        AnimateStep("[3/4] Authentification reussite");
        AnimateStep("[4/4] Lancement de Vizion cleaner");

        Console.ForegroundColor = originalColor;

        int choice;

        do
        {
            Console.Clear();
            choice = ShowMenu();

            switch (choice)
            {
                case 1:
                    Console.WriteLine("\nMenu du nettoyage d'ordinateur sélectionné.");
                    Cleaning.PerformComputerCleanup();
                    break;
                case 2:
                    Console.WriteLine("\nRejoindre le Discord sélectionné.");
                    OpenDiscordLink();
                    break;
                case 3:
                    Console.WriteLine("\nQuitter Vizion Cleaner sélectionné.");
                    Environment.Exit(0);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nOption invalide.");
                    Console.ResetColor();
                    break;
            }
        } while (choice != 3);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Tâche terminée. Appuyez sur une touche pour quitter.");
        Console.ResetColor();
        Console.ReadKey();
    }

    static void AnimateStep(string stepText)
    {
        Console.Write($"{stepText}....");
        for (int i = 0; i < 3; i++)
        {
            Thread.Sleep(200);
            Console.Write("\b \b");
            Thread.Sleep(300);
            Console.Write(".");
        }
        Console.WriteLine();
        SimulateProgress();
    }

    static void SimulateProgress()
    {
        Thread.Sleep(2000);
    }

    static int ShowMenu()
    {
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
        Console.WriteLine("\nVizion Cleaner - Menu de choix :");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("[1]");
        Console.ResetColor();

        Console.WriteLine(" Menu du nettoyage d'ordinateur");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("[2]");
        Console.ResetColor();

        Console.WriteLine(" Rejoindre le Discord");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("[3]");
        Console.ResetColor();

        Console.WriteLine(" Quitter Vizion Cleaner\n");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Choisissez une option : ");
        Console.ResetColor();

        char choice = Console.ReadKey().KeyChar;

        int choiceInt;
        if (int.TryParse(choice.ToString(), out choiceInt))
        {
            return choiceInt;
        }
        else
        {
            return 0;
        }
    }

    static bool IsRunningAsAdmin()
    {
        return new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent())
            .IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
    }

    static void RelaunchAsAdmin()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = Process.GetCurrentProcess().MainModule.FileName,
            Verb = "runas"
        };

        try
        {
            Process.Start(startInfo);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Erreur lors du redémarrage avec des privilèges d'administrateur : " + ex.Message);
            Console.ResetColor();
        }

        Environment.Exit(0);
    }

    static void OpenDiscordLink()
    {
        try
        {
            Process.Start("https://discord.gg/D9mXWxJyfC");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Erreur lors de l'ouverture du lien Discord : " + ex.Message);
            Console.ResetColor();
        }
    }
}
