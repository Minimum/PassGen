using System;
using System.Security.Cryptography;

namespace PassGen
{
    public class PasswordGenerator
    {
        private const String UppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const String LowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
        private const String Numbers = "0123456789";
        private const String SpecialCharacters = "`~!@#$%^&*()-_=+[{]}\\|;:\'\",<.>/?*";
        private const String Whitespace = " ";

        public bool UseUppercaseLetters { get; set; }
        public bool UseLowercaseLetters { get; set; }
        public bool UseNumbers { get; set; }
        public bool UseSpecialCharacters { get; set; }
        public bool UseWhitespace { get; set; }

        public PasswordGenerator()
        {
            UseUppercaseLetters = true;
            UseLowercaseLetters = true;
            UseNumbers = true;
            UseSpecialCharacters = false;
            UseWhitespace = false;
        }

        public String Generate(int length)
        {
            String potentialCharacters = "";
            String password = "";

            // Compile list of potential characters
            if (UseUppercaseLetters)
                potentialCharacters += UppercaseLetters;

            if (UseLowercaseLetters)
                potentialCharacters += LowercaseLetters;

            if (UseNumbers)
                potentialCharacters += Numbers;

            if (UseSpecialCharacters)
                potentialCharacters += SpecialCharacters;

            if (UseWhitespace)
                potentialCharacters += Whitespace;

            // Ensure we have valid characters
            int potentialCharacterCount = potentialCharacters.Length;

            if (potentialCharacterCount > 0)
            {
                using (RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider())
                {
                    // Generate a random byte for each of the password's characters
                    // Note: DO NOT use beyond 255 potential characters
                    byte[] randomBytes = new byte[length];

                    rand.GetBytes(randomBytes);

                    // Create the password
                    for (int x = 0; x < length; x++)
                    {
                        int character = randomBytes[x] % potentialCharacterCount;

                        password += potentialCharacters[character];
                    }
                }
            }

            return password;
        }
    }
}