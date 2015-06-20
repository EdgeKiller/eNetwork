using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

namespace eNetwork
{
    public static class eUtils
    {

        // Serialize object
        public static byte[] Serialize(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        // Deserialize byte array to T
        public static T Deserialize<T>(byte[] data)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                return (T)bf.Deserialize(ms);
            }
        }

        // Deserialize byte array to ePacket
        public static ePacket Deserialize(byte[] data)
        {
            if (data == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                return (ePacket)bf.Deserialize(ms);
            }
        }

        // Compress byte array
        public static byte[] Compress(byte[] data)
        {
            if (data == null)
                return null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (BufferedStream bs = new BufferedStream(new GZipStream(ms, CompressionMode.Compress), 2048))
                {
                    bs.Write(data, 0, data.Length);
                }
                return ms.ToArray();
            }
        }

        // Decompress byte array
        public static byte[] Decompress(byte[] data)
        {
            if (data == null)
                return null;
            using (var compressedMs = new MemoryStream(data))
            {
                using (var decompressedMs = new MemoryStream())
                {
                    using (var bs = new BufferedStream(new GZipStream(compressedMs, CompressionMode.Decompress), 2048))
                    {
                        bs.CopyTo(decompressedMs);
                    }
                    return decompressedMs.ToArray();
                }
            }
        }

        // Check if byte array is ePacket
        public static bool IsPacket(byte[] data)
        {
            if (data == null)
                return false;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                try
                {
                    bf.Deserialize(ms);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        // Check if byte array is compressed ePacket
        public static bool IsPacketCompressed(byte[] data)
        {
            if (data == null)
                return false;
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                using (MemoryStream ms = new MemoryStream(Decompress(data)))
                {
                    bf.Deserialize(ms);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}
