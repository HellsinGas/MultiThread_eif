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

        public static void SetParams(String targetpassword)
        {

            password = targetpassword;
           
        }

        public static void AesEncryptas(FileInfo targetFile)
        {
            Thread.Sleep(2000);

            RijndaelManaged encdec = new RijndaelManaged();
            encdec.BlockSize = 128;
            encdec.KeySize = 256;
            var keygenerator = new Rfc2898DeriveBytes(password, Salt, 3000);
            encdec.Key = keygenerator.GetBytes(encdec.KeySize / 8);
            raktas = encdec.Key;
            encdec.IV = keygenerator.GetBytes(encdec.BlockSize / 8);
            romenas = encdec.IV;
            encdec.Mode = CipherMode.CBC;

           
            FileStream fsCrypt = new FileStream(targetFile.FullName + ".aes", FileMode.Create);
            fsCrypt.Write(Salt, 0, Salt.Length);           
            

            CryptoStream cs = new CryptoStream(fsCrypt, encdec.CreateEncryptor(raktas, romenas), CryptoStreamMode.Write);
            
            FileStream fsIn = new FileStream(targetFile.FullName, FileMode.Open);


            byte[] buffer = new byte[1048576];
            int read;


            try
            {
                while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                {                    
                    cs.Write(buffer, 0, read);
                }

                
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



                }

            }





        }


        public static void AesDecryptas(FileInfo targetFile)
        {


            RijndaelManaged encdec = new RijndaelManaged();
            encdec.BlockSize = 128;
            encdec.KeySize = 256;
            var keygenerator = new Rfc2898DeriveBytes(password, Salt, 3000);
            encdec.Key = keygenerator.GetBytes(encdec.KeySize / 8);
            raktas = encdec.Key;
            encdec.IV = keygenerator.GetBytes(encdec.BlockSize / 8);
            romenas = encdec.IV;
            encdec.Mode = CipherMode.CBC;

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
                    if (File.Exists(hashfilename) == true)
                    {
                        using (StreamReader sread = new StreamReader(hashfilename))
                        {
                            storedhash = sread.ReadToEnd();
                        }
                    }
                    else storedhash = " ";                    
                    currenthash = Convert.ToBase64String(hasher.Hash);
                    file.Close();
                    
                }
            }




                if (string.Compare(storedhash, currenthash) == 0)
                {
                    FileStream fsCrypt = new FileStream(targetFile.FullName, FileMode.Open);
                    fsCrypt.Read(Salt, 0, Salt.Length);
                                 

                    CryptoStream cs = new CryptoStream(fsCrypt, encdec.CreateDecryptor(raktas, romenas), CryptoStreamMode.Read);

                    FileStream fsOut = new FileStream(targetFile.FullName.Replace(".aes", ""), FileMode.Create);

                    int read;
                    byte[] buffer = new byte[1048576];

                    try
                    {
                        while ((read = cs.Read(buffer, 0, buffer.Length)) > 0)
                        {
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
