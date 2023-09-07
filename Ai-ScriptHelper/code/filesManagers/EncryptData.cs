using System.Security.Cryptography;
using System.Text;

public class EncryptData
{
    public byte[] EncryptObject(string _objectName,byte[] _dataToEncrypt)
    {
        string selectedPassword;

        Console.WriteLine($"Choose a password (remember it) to encrypt the file {_objectName}");
        Console.Write("Password: ");

        selectedPassword = Console.ReadLine();

        byte[] selectedKey = ConvertStringToKey(selectedPassword);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = selectedKey;
            aesAlg.Mode = CipherMode.CFB; // CFB encryption

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

    public byte[] DecryptObject(string _objectName, byte[] _dataToDecrypt)
    {
        string selectedPassword;

        Console.WriteLine($"Write the password to decrypt the file {_objectName}");
        Console.Write("Password: ");

        selectedPassword = Console.ReadLine();

        byte[] selectedKey = ConvertStringToKey(selectedPassword);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = selectedKey;
            aesAlg.Mode = CipherMode.CFB; // Assurez-vous d'utiliser le même mode de chiffrement qu'à l'encryption.

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

    private byte[] ConvertStringToKey(string _stringToConvert)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(_stringToConvert);
            return sha256.ComputeHash(inputBytes);
        }
    }
}