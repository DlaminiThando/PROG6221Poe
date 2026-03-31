using System;
using System.IO;
using System.Media;

namespace CybersecurityChatbot
{
    public static class VoiceGreeting
    {
        public static void PlayWelcome()
        {
            try
            {
                // Try multiple possible locations for the WAV file
                string audioPath = FindAudioFile("greeting.wav");

                if (!string.IsNullOrEmpty(audioPath) && File.Exists(audioPath))
                {
                    Console.WriteLine($"Found voice file at: {audioPath}"); // Debug info (optional)

                    using (SoundPlayer player = new SoundPlayer(audioPath))
                    {
                        player.Load(); // Load the sound first
                        player.PlaySync(); // Play and wait for completion
                    }

                    Console.WriteLine("Voice greeting played successfully!");
                }
                else
                {
                    // File not found - show visual/text greeting instead
                    DisplayTextGreeting();
                }
            }
            catch (InvalidOperationException)
            {
                // SoundPlayer not supported in this environment
                DisplayTextGreeting();
            }
            catch (Exception ex)
            {
                // Any other error
                Console.WriteLine($"[Note: Voice greeting unavailable - {ex.Message}]");
                DisplayTextGreeting();
            }
        }

        private static string FindAudioFile(string filename)
        {
            // Get the current working directory
            string currentDir = Environment.CurrentDirectory;

            // Get the base directory (where the .exe is)
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            // List all possible paths to check
            string[] possiblePaths = new string[]
            {
                // Current directory
                filename,
                Path.Combine(currentDir, filename),
                
                // Base directory (bin/Debug/net8.0)
                Path.Combine(baseDir, filename),
                
                // Project root (one level up from bin/Debug)
                Path.Combine(baseDir, "..", "..", "..", filename),
                Path.Combine(baseDir, "..", "..", "..", "Resources", filename),
                
                // Resources folder
                Path.Combine(baseDir, "Resources", filename),
                Path.Combine(currentDir, "Resources", filename),
                
                // Audio folder
                Path.Combine(baseDir, "Audio", filename),
                Path.Combine(currentDir, "Audio", filename),
                
                // Full path option - you can add your specific path here
                // @"C:\Users\tc\OneDrive\Desktop\PROGPOEP1\PROGPOEP1\greeting.wav"
            };

            // Check each path
            foreach (string path in possiblePaths)
            {
                try
                {
                    if (File.Exists(path))
                    {
                        return Path.GetFullPath(path); // Return full path
                    }
                }
                catch
                {
                    // Skip invalid paths
                    continue;
                }
            }

            return null;
        }

        private static void DisplayTextGreeting()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║  🎤 VOICE GREETING:                                       ║");
            Console.WriteLine("║                                                            ║");
            Console.WriteLine("║  \"Hello! Welcome to the Cybersecurity Awareness Bot.      ║");
            Console.WriteLine("║   I'm here to help you stay safe online.\"                  ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            // Small delay to simulate greeting
            System.Threading.Thread.Sleep(2000);
        }

        // Optional: Add this method to help debug where your file should go
        public static void ShowFileLocationHelp()
        {
            Console.WriteLine("\n=== FILE LOCATION HELP ===");
            Console.WriteLine($"Your program is running from:");
            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            Console.WriteLine($"\nPlace your 'greeting.wav' file in one of these locations:");
            Console.WriteLine($"1. {AppDomain.CurrentDomain.BaseDirectory}");
            Console.WriteLine($"2. {Environment.CurrentDirectory}");
            Console.WriteLine($"3. The project folder: {Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."))}");
            Console.WriteLine("===========================\n");
        }
    }
}