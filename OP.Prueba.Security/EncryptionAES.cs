using OP.Prueba.Security.Resources;
using System.Security.Cryptography;
using System.Text;

namespace OP.Prueba.Security
{
    public class EncryptionAES
    {
        private static readonly string EncryptionKey = Base64Decode(ImportantData.KeyEncryipt);
        private static readonly string PDB = Base64Decode(ImportantData.PDB);

        /// <summary>
        /// Metodo Para Cifrar Cadenas con AES128
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public static string Encrypt(string encryptString)
        {

            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                byte[] bytes = Encoding.Default.GetBytes(PDB);
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, bytes);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        /// <summary>
        /// Metodo Para Descifrar Cadenas con AES128
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static string Decrypt(string cipherText)
        {
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                byte[] bytes = Encoding.Default.GetBytes(PDB);
                Rfc2898DeriveBytes pdb = new(EncryptionKey, bytes);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        /// <summary>
        /// Codifica en Base 64
        /// </summary>
        /// <param name="base64EncodedData"></param>
        /// <returns></returns>
        private static string Base64Decode(string base64EncodedData)
        {
            byte[] base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private static string GetSecretHash(string username, string appClientId, string appSecretKey)
        {
            string dataString = username + appClientId;
            byte[] data = Encoding.UTF8.GetBytes(dataString);
            byte[] key = Encoding.UTF8.GetBytes(appSecretKey);
            return Convert.ToBase64String(HmacSHA256(data, key));
        }

        private static byte[] HmacSHA256(byte[] data, byte[] key)
        {
            using HMACSHA256 shaAlgorithm = new(key);
            byte[] result = shaAlgorithm.ComputeHash(data);
            return result;
        }
    }
}
