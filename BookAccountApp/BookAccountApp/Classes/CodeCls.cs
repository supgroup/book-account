using System;
using System.Collections;
using System.IO;
using System.Text;
using BookAccountApp.ApiClasses;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
namespace BookAccountApp.Classes
{
    class CodeCls
    {
        public ActivateModel convertToModel(string activeCode)
        {
            try
            {
            ActivateModel activemode = new ActivateModel();
            if (!string.IsNullOrEmpty(activeCode))
            {
                string orginalkeydec = CodeCls.FinalDecode(activeCode.Trim());

               activemode = JsonConvert.DeserializeObject<ActivateModel>(orginalkeydec, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
                return activemode;
            }
            else
            {
                return null;
            }
            }
            catch
            {
                return null;
            }
        }

        public bool encodefile(string source, string dest)
        {
            try
            {

                byte[] arr = File.ReadAllBytes(source);

                arr = Encrypt(arr);

                File.WriteAllBytes(dest, arr);
                return true;
            }
            catch
            {
                return false;
            }

        }
        public static string Encrypt(string Text)
        {
            byte[] b = ConvertToBytes(Text);
            b = Encryptcode(b);
            return ConvertToText(b);
        }
        private static byte[] ConvertToBytes(string text)
        {
            return System.Text.Encoding.Unicode.GetBytes(text);
        }
        private static string ConvertToText(byte[] ByteAarry)
        {
            return System.Text.Encoding.Unicode.GetString(ByteAarry);
        }
        public bool encodestring(string sourcetext, string dest)
        {
            try
            {
                byte[] arr = ConvertToBytes(sourcetext);
                //  byte[] arr = File.ReadAllBytes(source);

                arr = Encrypt(arr);

                File.WriteAllBytes(dest, arr);
                return true;
            }
            catch
            {
                return false;
            }

        }





        public static byte[] Encrypt(byte[] ordinary)
        {
            BitArray bits = ToBits(ordinary);
            BitArray LHH = SubBits(bits, 0, bits.Length / 2);
            BitArray RHH = SubBits(bits, bits.Length / 2, bits.Length / 2);
            BitArray XorH = LHH.Xor(RHH);
            RHH = RHH.Not();
            XorH = XorH.Not();
            BitArray encr = ConcateBits(XorH, RHH);
            byte[] b = new byte[encr.Length / 8];
            encr.CopyTo(b, 0);
            return b;
        }
        public static byte[] Encryptcode(byte[] ordinary)
        {
            BitArray bits = ToBits(ordinary);
            bits = bits.Not();
            byte[] b = new byte[bits.Length / 8];
            bits.CopyTo(b, 0);

            // BitArray LHH = SubBits(bits, 0, bits.Length / 2);
            // BitArray RHH = SubBits(bits, bits.Length / 2, bits.Length / 2);
            // BitArray XorH = LHH.Xor(RHH);
            // RHH = RHH.Not();
            // XorH = XorH.Not();
            // BitArray encr = ConcateBits(XorH, RHH);
            //// byte[] b = new byte[encr.Length / 8];
            // encr.CopyTo(b, 0);
            return b;
        }
        public static byte[] Decrypt(byte[] Encrypted)
        {
            BitArray enc = ToBits(Encrypted);
            BitArray XorH = SubBits(enc, 0, enc.Length / 2);
            XorH = XorH.Not();
            BitArray RHH = SubBits(enc, enc.Length / 2, enc.Length / 2);
            RHH = RHH.Not();
            BitArray LHH = XorH.Xor(RHH);
            BitArray bits = ConcateBits(LHH, RHH);
            byte[] decr = new byte[bits.Length / 8];
            bits.CopyTo(decr, 0);
            return decr;
        }
        public static byte[] Decryptcode(byte[] Encrypted)
        {
            BitArray enc = ToBits(Encrypted);

            enc = enc.Not();
            byte[] decr = new byte[enc.Length / 8];
            enc.CopyTo(decr, 0);

            //BitArray XorH = SubBits(enc, 0, enc.Length / 2);
            //XorH = XorH.Not();
            //BitArray RHH = SubBits(enc, enc.Length / 2, enc.Length / 2);
            //RHH = RHH.Not();
            //BitArray LHH = XorH.Xor(RHH);
            //BitArray bits = ConcateBits(LHH, RHH);
            //byte[] decr = new byte[bits.Length / 8];
            //bits.CopyTo(decr, 0);
            return decr;
        }
        private static BitArray ToBits(byte[] Bytes)
        {
            BitArray bits = new BitArray(Bytes);
            return bits;
        }
        private static BitArray SubBits(BitArray Bits, int Start, int Length)
        {
            BitArray half = new BitArray(Length);
            for (int i = 0; i < half.Length; i++)
                half[i] = Bits[i + Start];
            return half;
        }
        private static BitArray ConcateBits(BitArray LHH, BitArray RHH)
        {
            BitArray bits = new BitArray(LHH.Length + RHH.Length);
            for (int i = 0; i < LHH.Length; i++)
                bits[i] = LHH[i];
            for (int i = 0; i < RHH.Length; i++)
                bits[i + LHH.Length] = RHH[i];
            return bits;
        }
        public void DelFile(string fileName)
        {

            bool inuse = false;

            inuse = IsFileInUse(fileName);
            if (inuse == false)
            {
                File.Delete(fileName);
            }






        }

        private bool IsFileInUse(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                //throw new ArgumentException("'path' cannot be null or empty.", "path");
                return true;
            }


            try
            {
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite)) { }
            }
            catch (IOException)
            {
                return true;
            }

            return false;
        }

        public static string Decrypt(string EncryptedText)
        {
            byte[] b = ConvertToBytes(EncryptedText);
            b = Decryptcode(b);
            return ConvertToText(b);
        }
        public static string DeCompressThenDecrypt(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            //  text = Encoding.UTF8.GetString(bytes);

            return (Decrypt(text));
        }
        public static string EncryptThenCompress(string text)
        {
            string str1 = Encrypt(text);

            var bytes = Encoding.UTF8.GetBytes(str1);
            //   return (Encoding.UTF8.GetString(bytes));
            return str1;
        }
        //////////
        public bool decodefile(string Source, string DestPath)
        {
            try
            {
                byte[] restorearr = File.ReadAllBytes(Source);

                restorearr = Decrypt(restorearr);
                File.WriteAllBytes(DestPath, restorearr);
                return true;

            }
            catch
            {
                return false;
            }
        }



        public static string StringTobase64(string textstring)
        {
            try
            {
                //from string to base64
                string code = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(textstring));
                return code;
            }
            catch
            {
                return "0";
            }
        }


        public static string Base64ToString(string basestring)
        {
            try
            {
                //from base64 to string
                string code = (string)Encoding.UTF8.GetString(Convert.FromBase64String(basestring));
                //
                return code;
            }
            catch
            {
                return "0";
            }
        }

        public static string FinalEncode(string textvalue)
        {
            try
            {
                string encripted = EncryptThenCompress(textvalue);
                encripted = StringTobase64(encripted);
                return encripted;
            }
            catch
            {
                return "0";
            }
        }

        public static string FinalDecode(string textvalue)
        {
            try
            {
                string Decripted = Base64ToString(textvalue);
                Decripted = DeCompressThenDecrypt(Decripted);
                return Decripted;
            }
            catch
            {
                return "0";
            }
        }


    }
}

