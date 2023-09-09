using System.Security.Cryptography;
using System.Text;

public static class EncryptData
{
    public static byte[] EncryptBytes(string _objectName,byte[] _dataToEncrypt)
    {
        string selectedPassword;

        Console.WriteLine($"Choose a password (remember it) to encrypt the file {_objectName}");
        Console.Write("Password: ");

        selectedPassword = Console.ReadLine();

        byte[] selectedKey = ConvertStringToKey(selectedPassword);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Mode = CipherMode.CFB; // CFB encryption
            aesAlg.Key = selectedKey;

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (ICryptoTransform encryptor = aesAlg.CreateEncryptor())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(_dataToEncrypt, 0, _dataToEncrypt.Length);
                        csEncrypt.FlushFinalBlock();
                        return msEncrypt.ToArray();
                    }
                }
            }
        }
    }

    public static byte[] DecryptBytes(string _objectName, byte[] _dataToDecrypt)
    {
        string selectedPassword;

        Console.WriteLine($"Write the password to decrypt the file {_objectName}");
        Console.Write("Password: ");

        selectedPassword = Console.ReadLine();

        byte[] selectedKey = ConvertStringToKey(selectedPassword);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Mode = CipherMode.CFB; // CFB to decrypt (same as encrypt)
            aesAlg.Key = selectedKey;

            using (MemoryStream msDecrypt = new MemoryStream(_dataToDecrypt))
            {
                using (ICryptoTransform decryptor = aesAlg.CreateDecryptor())
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (MemoryStream msPlain = new MemoryStream())
                        {
                            csDecrypt.CopyTo(msPlain);
                            return msPlain.ToArray();
                        }
                    }
                }
            }
        }
    }

    private static byte[] ConvertStringToKey(string _stringToConvert)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(_stringToConvert);

            foreach (byte b in sha256.ComputeHash(inputBytes))
            {
                Console.Write($"{b}");
            }
            Console.WriteLine();

            return sha256.ComputeHash(inputBytes);
        }
    }
}