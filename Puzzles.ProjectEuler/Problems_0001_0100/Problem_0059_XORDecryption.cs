using System;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// Each character on a computer is assigned a unique code and the preferred standard is ASCII 
    /// (American Standard Code for Information Interchange). For example, uppercase A = 65, asterisk (*) = 42, and lowercase k = 107.
    ///
    /// A modern encryption method is to take a text file, convert the bytes to ASCII, then XOR each byte with a given value, 
    /// taken from a secret key. The advantage with the XOR function is that using the same encryption key on the cipher text, 
    /// restores the plain text; for example, 65 XOR 42 = 107, then 107 XOR 42 = 65.
    ///
    /// For unbreakable encryption, the key is the same length as the plain text message, and the key is made up of random bytes. 
    /// The user would keep the encrypted message and the encryption key in different locations, and without both "halves", 
    /// it is impossible to decrypt the message.
    ///
    /// Unfortunately, this method is impractical for most users, so the modified method is to use a password as a key. 
    /// If the password is shorter than the message, which is likely, the key is repeated cyclically throughout the message. 
    /// The balance for this method is using a sufficiently long password key for security, but short enough to be memorable.
    ///
    /// Your task has been made easy, as the encryption key consists of three lower case characters. 
    /// Using cipher1.txt (right click and 'Save Link/Target As...'), a file containing the encrypted ASCII codes, 
    /// and the knowledge that the plain text must contain common English words, decrypt the message 
    /// and find the sum of the ASCII values in the original text.
    /// </summary>
    [TestFixture]
    public class Problem_0059_XORDecryption
    {
        private const string filePath = "Puzzles.ProjectEuler.DataFiles.Problem_0059_cipher1.txt";

        [Test]
        public void ConfirmEncryptDecrypt()
        {
            // 65 XOR 42 = 107, then 107 XOR 42 = 65
            const string letter = "A";
            var letterAsAscii = Convert.ToInt32(letter[0]);
            var encryptedLetter = letterAsAscii ^ 42;
            var decryptedLetterAsAscii = encryptedLetter ^ 42;
            Assert.AreEqual(letterAsAscii, decryptedLetterAsAscii, "Same value");
            var decryptedLetter = (char)decryptedLetterAsAscii;
            Assert.AreEqual(letterAsAscii, decryptedLetter, "Same letter");
        }

        [Test, Explicit]
        public void FindLimitsOfLowerCaseAsciiValues()
        {
            var valuea = Convert.ToInt32('a');
            var valuez = Convert.ToInt32('z');

            // 97 122
            Console.WriteLine("{0} {1}", valuea, valuez);
        }

        /// <summary>
        /// 107359
        /// </summary>
        [Test, Explicit]
        public void FindSumOriginalAscii()
        {
            var content = FileHelper.GetEmbeddedResourceContent(filePath);
            var encryptedLetters = content.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            const int key1 = 103;
            const int key2 = 111;
            const int key3 = 100;

            var total = SumAscii(encryptedLetters, key1, key2, key3);

            Console.WriteLine("Total: {0}", total);
            total.Should().Be(107359);
        }

        //103 111 100: (The Gospel of John, chapter 1) 1 In the beginning the Word already existed. He was with God, and he was God. 2 He was in the beginning with God. 3 He created everything there is. Nothing exists that he didn't make. 4 Life itself was in him, and this life gives light to everyone. 5 The light shines through the darkness, and the darkness can never extinguish it. 6 God sent John the Baptist 7 to tell everyone about the light so that everyone might believe because of his testimony. 8 John himself was not the light; he was only a witness to the light. 9 The one who is the true light, who gives light to everyone, was going to come into the world. 10 But although the world was made through him, the world didn't recognize him when he came. 11 Even in his own land and among his own people, he was not accepted. 12 But to all who believed him and accepted him, he gave the right to become children of God. 13 They are reborn! This is not a physical birth resulting from human passion or plan, this rebirth comes from God.14 So the Word became human and lived here on earth among us. He was full of unfailing love and faithfulness. And we have seen his glory, the glory of the only Son of the Father.

        [Test, Explicit]
        public void DecryptMessageUsingThreeCharacters()
        {
            var content = FileHelper.GetEmbeddedResourceContent(filePath);
            var encryptedLetters = content.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            for (int key1 = 97; key1 <= 122; ++key1)
            {
                for (var key2 = 97; key2 <= 122; ++key2)
                {
                    for (var key3 = 97; key3 <= 122; ++key3)
                    {
                        var message = DecryptMessage(encryptedLetters, key1, key2, key3);
                        var foundWords = HasWords(message);
                        if (foundWords)
                        {
                            Console.WriteLine("{0} {1} {2}: {3}", key1, key2, key3, message);
                        }
                    }
                }
            }
        }

        private static long SumAscii(string[] encryptedLetters, int key1, int key2, int key3)
        {
            long total = 0;

            for (var idx = 0; idx < encryptedLetters.Length; ++idx)
            {
                var ch = encryptedLetters[idx];
                var keyChoice = idx % 3;
                var key = (keyChoice == 0) ? key1 : (keyChoice == 1) ? key2 : key3;

                var chAsAscii = Convert.ToInt32(ch);
                var decryptAsAscii = chAsAscii ^ key;

                total += decryptAsAscii;
            }

            return total;
        }

        private static string DecryptMessage(string[] encryptedLetters, int key1, int key2, int key3)
        {
            var message = new StringBuilder();

            for (var idx = 0; idx < encryptedLetters.Length; ++idx)
            {
                var ch = encryptedLetters[idx];
                var keyChoice = idx % 3;
                var key = (keyChoice == 0) ? key1 : (keyChoice == 1) ? key2 : key3;

                var chAsAscii = Convert.ToInt32(ch);
                var decryptAsAscii = chAsAscii ^ key;
                var decrypt = (char)decryptAsAscii;
                message.Append(decrypt);
            }

            return message.ToString();
        }

        private bool HasWords(string message)
        {
            return message.Contains("the");
        }
    }
}
