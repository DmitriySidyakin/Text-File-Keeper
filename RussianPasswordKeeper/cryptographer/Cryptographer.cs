
namespace Cryptographer;
using System.IO;
using System.Text;
using Cryptonix.Security;

public class Cryptographer {
    public static byte[] Crypt(string pass, string input, bool isEncryption)
    {
        byte[] inputFileBytes = Encoding.Unicode.GetBytes(input);
        byte[] passBytes = Encoding.Unicode.GetBytes(pass);
        byte[] outputFileBytes;
        if (isEncryption)
        {
            outputFileBytes = Xorer.Xor4Encrypt(inputFileBytes, passBytes);
            outputFileBytes = Spiralizer.Spiralize(outputFileBytes);
        }
        else
        {
            outputFileBytes = Spiralizer.Despiralize(inputFileBytes);
            outputFileBytes = Xorer.Xor4Dencrypt(outputFileBytes, passBytes);
        }

        return outputFileBytes;
    }
}