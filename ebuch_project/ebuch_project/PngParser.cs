using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace ebuch_project
{
    class Chunk
    {
        private int lenght;
        private byte[] type = new byte[4];
        public byte[] data;
        private int crc;
        public Chunk(Int32 lenght, byte[] title, byte[] source, Int32 crc)
        {
            this.lenght = lenght;
            Array.Copy(title, type, 4);
            data = new byte[lenght];
            Array.Copy(source, data, lenght);
            this.crc = crc;
        }

        public Int32 Lenght
        {
            get { return lenght; }
            set { lenght = value; }
        }
        public byte[] Type
        {
            get { return type; }
            set { type = value; }
        }
        public byte[] DATA
        {
            get { return data; }
            set { data = value; }
        }
        public Int32 CRC
        {
            get { return crc; }
            set { crc = value; }
        }

        public static byte[] Pack(byte[] data)
        {
            Stream stream = new MemoryStream(data);
            MemoryStream result = new MemoryStream();
            DeflateStream deflate_stream = new DeflateStream(result, CompressionMode.Compress);
            stream.CopyTo(deflate_stream);
            return result.ToArray();
        }
        public byte[] Unpack()
        {
            Stream stream = new MemoryStream(DATA);
            MemoryStream result = new MemoryStream();
            DeflateStream deflate_stream = new DeflateStream(result, CompressionMode.Decompress);
            deflate_stream.CopyTo(stream);
            return result.ToArray();
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[Lenght + 12];
            byte[] lenght = BitConverter.GetBytes(Lenght);
            Array.Reverse(lenght);
            Array.Copy(lenght, 0, result, 0, 4);
            Array.Copy(Type, 0, result, 4, 4);
            Array.Copy(DATA, 0, result, 8, Lenght);
            Array.Copy(BitConverter.GetBytes(CRC), 0, result, Lenght + 8, 4);
            return result;
        }
    }
    class PngParser
    {
        private byte[] png_signature = new byte[8];
        public int position = 8;
        private byte[] png_data;
        private List<Chunk> chunks = new List<Chunk>();
        private Chunk IEND;
        private Chunk IHDR;
        public PngParser(byte[] png_data)
        {
            this.png_data = png_data;
            Array.Copy(png_data, png_signature, 8);
            Chunk chunk = ReadChunk();
            IHDR = chunk;

            do
            {
                chunk = ReadChunk();
                if (chunk.Lenght != 0)
                {
                    chunks.Add(chunk);
                }
                else
                {
                    IEND = chunk;
                }
            }
            while (chunk.Lenght != 0);
        }
        public PngParser(byte[] png_signature, Chunk IHDR, List<Chunk> new_Chunks, Chunk IEND)
        {
            this.png_signature = png_signature;
            this.IHDR = IHDR;
            this.IEND = IEND;
            this.chunks = new_Chunks;
            //List<Chunk>.Copy(new_Chunks, chunks,new_Chunks.Count);
        }
        public Chunk IEND_
        {
            get { return IEND; }
            set { IEND = value; }
        }
        public byte[] Png_signature
        {
            get
            {
                return png_signature;
            }
        }

        public byte[] Png_DATA
        {
            get
            {
                position = IHDR.ToByteArray().Length + IEND.ToByteArray().Length;
                foreach (Chunk chunk in chunks)
                {
                    position += chunk.ToByteArray().Length;
                }
                png_data = new byte[position];
                position = 0;
                Array.Copy(png_signature, 0, png_data, 0, 8);
                position += 8;
                Array.Copy(IHDR.ToByteArray(), 0, png_data, position, IHDR.ToByteArray().Length);
                position += IHDR.ToByteArray().Length;
                foreach (Chunk chunk in chunks)
                {
                    Array.Copy(chunk.ToByteArray(), 0, png_data, position, chunk.ToByteArray().Length);
                    position += chunk.ToByteArray().Length;
                }
                Array.Copy(IEND.ToByteArray(), 0, png_data, position, IEND.ToByteArray().Length);
                position += IEND.ToByteArray().Length;
                return png_data;
            }
            set { png_data = value; }
        }
        public Chunk _IHDR
        {
            get { return IHDR; }
            set { IHDR = value; }
        }
        public List<Chunk> Chunks
        {
            get { return chunks; }
        }
        private Chunk ReadChunk()
        {
            byte[] four = new byte[4];
            byte[] type = new byte[4];
            byte[] data;
            byte[] crc = new byte[4];
            Array.Copy(png_data, position, four, 0, 4);
            position += 4;
            int lenght = 0;
            Array.Reverse(four);
            lenght = BitConverter.ToInt32(four, 0);
            data = new byte[lenght];
            Array.Copy(png_data, position, type, 0, 4);
            position += 4;
            Array.Copy(png_data, position, data, 0, lenght);
            position += lenght;
            Array.Copy(png_data, position, crc, 0, 4);
            position += 4;
            return new Chunk(lenght, type, data, BitConverter.ToInt32(crc, 0));
        }
    }
}
