using System;
using System.Collections.Generic;
using System.Linq;

namespace CybersecurityChatbot
{
    public class ResponseSystem
    {
        private readonly Dictionary<string, string[]> responses;
        private readonly string[] greetingKeywords;
        private readonly string[] thankKeywords;
        private readonly string[] exitKeywords;

        public ResponseSystem()
        {
            // Keywords to identify different intent types
            greetingKeywords = new[] { "hello", "hi", "hey", "greetings", "howdy", "good morning", "good afternoon", "good evening" };
            thankKeywords = new[] { "thank", "thanks", "appreciate", "thank you" };
            exitKeywords = new[] { "bye", "goodbye", "exit", "quit", "see you", "cya", "see ya" };

            // Cybersecurity responses
            responses = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
            {
                ["phishing"] = new[]
                {
                    "Phishing is when cybercriminals try to trick you into revealing sensitive information like passwords or credit card details. They often use fake emails or websites that look legitimate. Always check the sender's email address carefully and never click suspicious links!",
                    "To avoid phishing attacks: 1) Verify email senders before responding 2) Hover over links to see the real URL 3) Never enter personal info on unsecured sites 4) When in doubt, contact the company directly through their official website.",
                    "Phishing attempts often create urgency ('Your account will be closed!'). Stay calm and verify through official channels. Remember, legitimate companies won't ask for your password via email.",
                    "Be cautious of emails with spelling mistakes, generic greetings like 'Dear Customer', or requests for personal information. These are common phishing red flags!"
                },

                ["password"] = new[]
                {
                    "Strong passwords are your first line of defense! Use at least 12 characters with a mix of uppercase, lowercase, numbers, and symbols. Avoid using personal information like birthdates or pet names.",
                    "Consider using a password manager to generate and store complex passwords. And always enable two-factor authentication (2FA) whenever possible - it adds an extra layer of security!",
                    "Never reuse passwords across different sites. If one site gets hacked, criminals will try those same credentials on banking, email, and social media sites.",
                    "A good trick: Use a passphrase instead of a password. For example, 'Purple-Elephant-Dances-2024!' is both strong and memorable."
                },

                ["safe browsing"] = new[]
                {
                    "Safe browsing tips: Look for HTTPS (padlock icon) in the address bar, keep your browser updated, avoid downloading from untrusted sites, and be cautious with pop-ups.",
                    "Use ad-blockers and consider privacy-focused browsers like Firefox or Brave. Be especially careful on public Wi-Fi - use a VPN if possible!"
                },

                ["browsing"] = new[]
                {
                    "For secure browsing: 1) Check for HTTPS 2) Don't save passwords on shared computers 3) Clear your cache regularly 4) Use private/incognito mode when needed.",
                    "Watch out for fake websites that mimic real ones. Check the URL carefully - 'amazon.com' is safe, but 'amazon-login.com' might be dangerous!",
                    "Keep your browser and extensions updated. Outdated software can have security vulnerabilities that hackers exploit."
                },

                ["how are you"] = new[]
                {
                    "I'm functioning perfectly, thanks for asking! Ready to help you stay safe online!",
                    "I'm doing great! Just like your firewall should be - always on and vigilant!",
                    "Better than ever! Thanks for checking in. What cybersecurity topic shall we explore today?"
                },

                ["purpose"] = new[]
                {
                    "My purpose is to educate you about cybersecurity best practices. I can teach you about password safety, phishing prevention, safe browsing, and general online security tips!",
                    "I'm your cybersecurity awareness companion! Think of me as your personal security guard in the digital world.",
                    "I exist to help you navigate the online world safely. Ask me anything about staying secure on the internet!"
                },

                ["help"] = new[]
                {
                    "I can help you with:\n🔒 Password safety\n🎣 Phishing awareness\n🌐 Safe browsing tips\n🛡️ General cybersecurity advice\n\nJust ask me about any of these topics!",
                    "Here's what I know about:\n• Creating strong passwords\n• Recognizing phishing emails\n• Safe browsing habits\n• Protecting your personal info online\n\nWhat would you like to learn about?"
                },

                ["what can you do"] = new[]
                {
                    "I'm a cybersecurity awareness bot! Ask me about:\n• Creating strong passwords\n• Recognizing phishing emails\n• Safe browsing habits\n• Protecting your personal info online",
                    "I can answer your questions about online safety! Try asking me about passwords, phishing, or safe browsing."
                },

                ["cybersecurity"] = new[]
                {
                    "Cybersecurity is all about protecting your digital life! This includes using strong passwords, recognizing phishing attempts, keeping software updated, and being careful what you share online.",
                    "Think of cybersecurity as digital hygiene - just like washing your hands, you need good habits to stay safe online!"
                },

                ["virus"] = new[]
                {
                    "Viruses are malicious programs that can damage your computer or steal data. Protect yourself by: 1) Using antivirus software 2) Keeping everything updated 3) Not downloading suspicious attachments 4) Being careful with email attachments.",
                    "Good antivirus software, regular updates, and common sense are your best defense against viruses and malware!"
                },

                ["hacker"] = new[]
                {
                    "Hackers are people who try to break into computer systems. White hat hackers help find security flaws, while black hat hackers have malicious intent. Always use strong passwords and keep your software updated to protect against hackers!",
                    "To protect yourself from hackers: Use 2FA, strong passwords, keep software updated, and be careful what you click on!"
                }
            };
        }

        public string GetResponse(string input, string userName)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return $"I didn't catch that, {userName}. Could you please type something?";
            }

            string lowerInput = input.ToLower().Trim();

            // Check for exit commands first
            if (IsExitCommand(lowerInput))
            {
                return GetRandomExitMessage(userName);
            }

            // Check for thanks
            if (IsThankYou(lowerInput))
            {
                return GetRandomThankYou(userName);
            }

            // Check for greetings (but only if it's primarily a greeting)
            if (IsGreeting(lowerInput) && !IsAskingAboutTopic(lowerInput))
            {
                return GetRandomGreeting(userName);
            }

            // Check for questions about the bot's identity
            if (lowerInput.Contains("your name") || lowerInput.Contains("who are you") || lowerInput.Contains("what are you"))
            {
                return $"I'm the Cybersecurity Awareness Bot, {userName}! I'm here to help you learn about online safety. Ask me about passwords, phishing, or safe browsing!";
            }

            // Check for cybersecurity topics - this is the main part!
            foreach (var topic in responses.Keys)
            {
                if (lowerInput.Contains(topic))
                {
                    string[] topicResponses = responses[topic];
                    return topicResponses[new Random().Next(topicResponses.Length)];
                }
            }

            // Check for questions containing "what is" or "tell me about"
            if (lowerInput.Contains("what is") || lowerInput.Contains("tell me about") || lowerInput.Contains("explain"))
            {
                return $"I'd be happy to explain, {userName}! Could you be more specific? You can ask me about:\n• Passwords\n• Phishing\n• Safe browsing\n• Cybersecurity in general";
            }

            // Default response with helpful suggestions
            return $"{userName}, I didn't quite understand that. Could you rephrase or ask me about:\n" +
                   "• Password safety\n" +
                   "• Phishing attacks\n" +
                   "• Safe browsing\n" +
                   "• General cybersecurity tips";
        }

        private bool IsGreeting(string input)
        {
            return greetingKeywords.Any(keyword => input.Contains(keyword));
        }

        private bool IsThankYou(string input)
        {
            return thankKeywords.Any(keyword => input.Contains(keyword));
        }

        private bool IsExitCommand(string input)
        {
            return exitKeywords.Any(keyword => input.Contains(keyword));
        }

        private bool IsAskingAboutTopic(string input)
        {
            // Check if the input is actually asking about a cybersecurity topic
            string[] topicIndicators = { "what is", "tell me about", "explain", "how to", "what are" };
            string[] topics = { "phishing", "password", "browsing", "virus", "hacker", "cybersecurity" };

            return topicIndicators.Any(indicator => input.Contains(indicator)) &&
                   topics.Any(topic => input.Contains(topic));
        }

        private string GetRandomGreeting(string userName)
        {
            string[] greetings = new[]
            {
                $"Hello {userName}! How can I help you with cybersecurity today?",
                $"Hi {userName}! Ready to learn about online safety?",
                $"Hey {userName}! What cybersecurity topic shall we explore?",
                $"Greetings, {userName}! I'm here to help you stay safe online."
            };
            return greetings[new Random().Next(greetings.Length)];
        }

        private string GetRandomThankYou(string userName)
        {
            string[] thanks = new[]
            {
                $"You're welcome, {userName}! Stay safe online!",
                $"Happy to help, {userName}! Remember to stay vigilant online.",
                $"My pleasure, {userName}! Any other cybersecurity questions?",
                $"Glad I could help, {userName}! That's what I'm here for."
            };
            return thanks[new Random().Next(thanks.Length)];
        }

        private string GetRandomExitMessage(string userName)
        {
            string[] exits = new[]
            {
                $"Goodbye, {userName}! Stay safe online!",
                $"See you later, {userName}! Remember to keep your passwords strong!",
                $"Take care, {userName}! Come back if you have more cybersecurity questions.",
                $"Bye, {userName}! Stay vigilant out there!"
            };
            return exits[new Random().Next(exits.Length)];
        }
    }
}