namespace CybersecurityChatbot
{
    public class Chatbot
    {
        private string? userName;
        private readonly ResponseSystem responseSystem;
        private readonly InputValidator inputValidator;

        public Chatbot()
        {
            responseSystem = new ResponseSystem();
            inputValidator = new InputValidator();
        }

        public void Start()
        {
            DisplayWelcomeMessage();
            GetUserName();

            bool continueChat = true;
            while (continueChat)
            {
                string userInput = GetUserInput();

                if (inputValidator.IsExitCommand(userInput))
                {
                    continueChat = false;
                    DisplayGoodbye();
                }
                else if (!inputValidator.IsValidInput(userInput))
                {
                    DisplayInvalidInputMessage();
                }
                else
                {
                    string response = responseSystem.GetResponse(userInput, userName);
                    TypewriterEffect(response);
                }
            }
        }

        private void DisplayWelcomeMessage()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║         Welcome to the Cybersecurity Awareness Bot      ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Thread.Sleep(1000);
        }

        private void GetUserName()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Please enter your name: ");
            Console.ResetColor();

            userName = Console.ReadLine()?.Trim();

            while (string.IsNullOrWhiteSpace(userName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Name cannot be empty. Please enter your name: ");
                Console.ResetColor();
                userName = Console.ReadLine()?.Trim();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nNice to meet you, {userName}! I'm here to help you learn about cybersecurity.\n");
            Console.ResetColor();
            Thread.Sleep(1000);
        }

        private string GetUserInput()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"{userName} > ");
            Console.ResetColor();
            return Console.ReadLine()?.Trim() ?? string.Empty;
        }

        private void DisplayInvalidInputMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nI didn't quite understand that. Could you rephrase?");
            Console.WriteLine("You can ask me about:");
            Console.WriteLine("• Password safety");
            Console.WriteLine("• Phishing attacks");
            Console.WriteLine("• Safe browsing");
            Console.WriteLine("• General cybersecurity tips");
            Console.ResetColor();
        }

        private void DisplayGoodbye()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║    Thank you for chatting, {userName}! Stay safe online!   ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
            Console.ResetColor();
        }

        private void TypewriterEffect(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(30); // Typing effect delay
            }
            Console.WriteLine("\n");
            Console.ResetColor();
        }
    }
}