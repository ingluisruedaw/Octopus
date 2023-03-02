namespace System.Security.Cryptography;

public class RandomSecureVersion
{
    public int Next()
    {
        var randomBuffer = new byte[4];
        using (var provider = RandomNumberGenerator.Create())
        {
            provider.GetBytes(randomBuffer);
        }
        
        var result = BitConverter.ToInt32(randomBuffer, default);
        return result;
    }

    public int Next(int maximumValue)
    {
        return Next(default, maximumValue);
    }

    public int Next(int minimumValue, int maximumValue)
    {
        var seed = Next();
        return new Random(seed).Next(minimumValue, maximumValue);
    }
}
