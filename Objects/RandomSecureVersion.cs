namespace System.Security.Cryptography;

public class RandomSecureVersion
{
    //Never ever ever never use Random() in the generation of anything that requires true security/randomness
    //and high entropy or I will hunt you down with a pitchfork!! Only RNGCryptoServiceProvider() is safe.
    //private readonly RNGCryptoServiceProvider provider = new();

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
        // Do not use Next() % maximumValue because the distribution is not OK
        return Next(default, maximumValue);
    }

    public int Next(int minimumValue, int maximumValue)
    {
        var seed = Next();

        //  Generate uniformly distributed random integers within a given range.
        return new Random(seed).Next(minimumValue, maximumValue);
    }
}
