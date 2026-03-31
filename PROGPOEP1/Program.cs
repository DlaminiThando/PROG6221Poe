namespace CybersecurityChatbot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Cybersecurity Awareness Bot";

            // Play voice greeting
            VoiceGreeting.PlayWelcome();

            // Display ASCII art header
            AsciiArt.DisplayLogo();

            // Start chatbot interaction
            Chatbot chatbot = new Chatbot();
            chatbot.Start();
        }
    }
}