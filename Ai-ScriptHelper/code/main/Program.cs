using System;
using System.Security.Cryptography;

public static class Program
{
    public static EncryptData encryptData;
    public static FilesStorage filesStorage;

    public static void Main(string[] args)
    {
        filesStorage = new FilesStorage("Ai-ScriptHelper");
        encryptData = new EncryptData();
    }
}