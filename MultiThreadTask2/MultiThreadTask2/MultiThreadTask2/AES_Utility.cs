using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace MultiThreadTask2
{
    static class AES_Utility
    {
        private static String password;
        private static byte[] Salt = new byte[] { 10, 20, 30, 40, 50, 60, 70, 80 };
        private static byte[] raktas;
        private static byte[] romenas;

        public static void SetParams(String targetpassword, byte[] targetSalt)
        {

            password = targetpassword;
            Salt = targetSalt;
        }

        public static void AesEncryptas(FileInfo targetFile)
        {
            Thread.Sleep(2000);

            RijndaelManaged encdec2 = new RijndaelManaged();
            encdec2.BlockSize = 128;
            encdec2.KeySize = 256;
            var keygenerator = new Rfc2898DeriveBytes(password, Salt, 300);
            encdec2.Key = keygenerator.GetBytes(encdec2.KeySize / 8);
            raktas = encdec2.Key;
            encdec2.IV = keygenerator.GetBytes(encdec2.BlockSize / 8);
            romenas = encdec2.IV;

            // string filetext;
            // string ecnryptedfile;

            /* using (StreamReader src = new StreamReader(targetFile.FullName))
             {
                 filetext = src.ReadToEnd();
             }*/
            FileStream fsCrypt = new FileStream(targetFile.FullName + ".aes", FileMode.Create);
            fsCrypt.Write(Salt, 0, Salt.Length);

            // byte[] textbytes = ASCIIEncoding.ASCII.GetBytes(filetext);
            RijndaelManaged encdec = new RijndaelManaged();
            encdec.BlockSize = 128;
            encdec.KeySize = 256;
            encdec.Padding = PaddingMode.PKCS7;
            encdec.Mode = CipherMode.CBC;

            CryptoStream cs = new CryptoStream(fsCrypt, encdec.CreateEncryptor(raktas, romenas), CryptoStreamMode.Write);
            //fsCrypt.Close();
            FileStream fsIn = new FileStream(targetFile.FullName, FileMode.Open);


            byte[] buffer = new byte[1048576];
            int read;


            try
            {
                while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                {
                    //  Application.DoEvents(); // -> for responsive GUI, using Task will be better!
                    cs.Write(buffer, 0, read);
                }

                // Close up
                fsIn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                cs.Close();
                fsCrypt.Close();
                File.Delete(targetFile.FullName);
            }


            byte[] buffer2;
            int bytesRead;
            long size;
            long totalBytesRead = 0;
            using (Stream file = File.OpenRead(targetFile.FullName + ".aes"))
            {
                size = file.Length;

                using (HashAlgorithm hasher = MD5.Create())
                {
                    do
                    {
                        buffer2 = new byte[4096];
                        bytesRead = file.Read(buffer2, 0, buffer2.Length);
                        totalBytesRead += bytesRead;
                        hasher.TransformBlock(buffer2, 0, bytesRead, null, 0);

                    } while (bytesRead != 0);
                    hasher.TransformFinalBlock(buffer2, 0, 0);
                    string hashfilename = targetFile.FullName.Split('.')[0] + ".HASH";

                    File.WriteAllText(hashfilename, Convert.ToBase64String(hasher.Hash));
                    //For debugging purposes
                    //System.Windows.Forms.MessageBox.Show(Convert.ToBase64String(hasher.Hash));



                }

            }





        }


        public static void AesDecryptas(FileInfo targetFile)
        {
            Thread.Sleep(2000);
            string storedhash;
            string currenthash;
            string hashfilename;

            byte[] buffer2;
            int bytesRead;
            long size;
            long totalBytesRead = 0;
            using (Stream file = File.OpenRead(targetFile.FullName))
            {
                size = file.Length;

                using (HashAlgorithm hasher = MD5.Create())
                {
                    do
                    {
                        buffer2 = new byte[4096];
                        bytesRead = file.Read(buffer2, 0, buffer2.Length);
                        totalBytesRead += bytesRead;
                        hasher.TransformBlock(buffer2, 0, bytesRead, null, 0);

                    } while (bytesRead != 0);
                    hasher.TransformFinalBlock(buffer2, 0, 0);
                    hashfilename = targetFile.FullName.Split('.')[0] + ".HASH";

                    using (StreamReader sread = new StreamReader(hashfilename))
                    {
                        storedhash = sread.ReadToEnd();
                    }

                    // File.WriteAllText(hashfilename, Convert.ToBase64String(hasher.Hash));
                    // MessageBox.Show(Convert.ToBase64String(hasher.Hash));
                    currenthash = Convert.ToBase64String(hasher.Hash);
                    file.Close();
                    
                }
            }




                if (string.Compare(storedhash, currenthash) == 0)
                {
                    FileStream fsCrypt = new FileStream(targetFile.FullName, FileMode.Open);
                    fsCrypt.Read(Salt, 0, Salt.Length);

                    RijndaelManaged AES = new RijndaelManaged();
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    // var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
                    AES.Key = raktas;
                    AES.IV = romenas;
                    AES.Padding = PaddingMode.PKCS7;
                    AES.Mode = CipherMode.CBC;

                    CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateDecryptor(raktas, romenas), CryptoStreamMode.Read);

                    FileStream fsOut = new FileStream(targetFile.FullName.Replace(".aes", ""), FileMode.Create);

                    int read;
                    byte[] buffer = new byte[1048576];

                    try
                    {
                        while ((read = cs.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            //Application.DoEvents();
                            fsOut.Write(buffer, 0, read);
                        }
                    }
                    catch (CryptographicException ex_CryptographicException)
                    {
                        Console.WriteLine("CryptographicException error: " + ex_CryptographicException.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }

                    try
                    {
                        cs.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error by closing CryptoStream: " + ex.Message);
                    }
                    finally
                    {
                        fsOut.Close();
                        fsCrypt.Close();
                        File.Delete(targetFile.FullName);
                        File.Delete(hashfilename);
                    }
                }


            


        }
    }
}
