using System;

namespace CybersecurityChatbot
{
    public class InputValidator
    {
        private readonly string[] exitCommands = { "exit", "quit", "bye", "goodbye" };

        public bool IsValidInput(string input)
        {
            return !string.IsNullOrWhiteSpace(input) && input.Length >= 2;
        }

        public bool IsExitCommand(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            string lowerInput = input.ToLower().Trim();
            foreach (string cmd in exitCommands)
            {
                if (lowerInput.Equals(cmd) || lowerInput.Contains(cmd))
                    return true;
            }
            return false;
        }
    }
}