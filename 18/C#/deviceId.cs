using System.Security.Cryptography;


static T[] CombineTwoArrays<T>(T[] a1, T[] a2)
{
    T[] arrayCombined = new T[a1.Length + a2.Length];
    Array.Copy(a1, 0, arrayCombined, 0, a1.Length);
    Array.Copy(a2, 0, arrayCombined, a1.Length, a2.Length);
    return arrayCombined;
}


static byte[] StringToByteArray(string hex)
{
    return Enumerable.Range(0, hex.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                     .ToArray();
}

static string deviceId()
{
    string prefix = "18";
    string key = "d19d2cb8468aac9b0ae16be4a6fa464be63760ce";

    Random rnd = new Random();
    byte[] identifier = new byte[20];
    rnd.NextBytes(identifier);
    HMACSHA1 hmac = new HMACSHA1(StringToByteArray(key));
    byte[] buffer = CombineTwoArrays(StringToByteArray(prefix), identifier);
    string result = BitConverter.ToString(hmac.ComputeHash(buffer)).Replace("-", "").ToLower();
    return prefix + BitConverter.ToString(identifier).Replace("-", "").ToLower() + result;
}


string device = deviceId();
Console.WriteLine(device);