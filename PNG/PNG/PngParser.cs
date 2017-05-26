using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNG
{
    class Chunk
    {
        private Int32 lenght;
        private byte[] type;
        private byte[] data;
        private Int32 crc;
        public Chunk(Int32 lenght,byte[] title,byte[] source,Int32 crc)
        {
            this.lenght = lenght;
            Array.Copy(title, type, 4);
            Array.Copy(source,data,lenght);
            this.crc = crc;
        }

        public Int32 Lenght
        {
            get { return lenght; }
            set { lenght=value; }
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
    }
    class PngParser
    {
        private byte[] png_signature;
        public int position=8;
        private byte[] png_data;
        private List<Chunk> chunks;
        private Chunk IEND;
        private Chunk IHDR;
        public PngParser(byte[] png_data)
        {
            this.png_data = png_data;
            Array.Copy(png_data,png_signature,8);
            Chunk chunk = ReadChunk();
            IHDR = chunk;

            do{
                chunk = ReadChunk();
                if (chunk != IEND)
                {
                    chunks.Add(chunk);
                }
            }
            while(chunk != IEND);
        }
        public PngParser(Chunk png_signature,Chunk IHDR,Chunk new_Chunks,Chunk IEND)
        { 
            
        }
        public List<Chunk> Chunks
        {
            get { return chunks; }
        }
        private Chunk ReadChunk()
        {
            byte[] four = new byte[4];
            byte[] type=new byte[4];
            byte[] data;
            byte[] crc=new byte[4];
            Array.Copy(png_data, position, four, 0, 4);
            position += 4;
            int lenght = 0;
            Array.Reverse(four);
            lenght = BitConverter.ToInt32(four,0);
            data = new byte[lenght];
            Array.Copy(png_data,position,four,0,4);
            position += 4;
            type = four;
            Array.Copy(png_data,position,data,0,lenght);
            position += lenght;
            Array.Copy(png_data,position,four,0,4);
            position += 4;
            return new Chunk(lenght,type,data,BitConverter.ToInt32(crc,0));
        }
    }
}
