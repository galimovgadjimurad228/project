using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ebuch_project
{
    class Utils
    {
        private byte[] Data;
        private string bit_str_length;
        private string Message;
        public string MESSAGE
        {
            get 
            {
                return Message; 
            }
            set
            {
                Message = value;
            }
        }
        public byte[] DATA
        {
            get 
            {
                return Data;
            }
            set
            {
                Data = value;
            }
        }
        public string BIT_STR_LENGTH
        {
            get 
            {
                return bit_str_length;
            }
            set
            {
                bit_str_length = value;
            }
        }
        public Utils(byte[] Data,string Message)
        {
            this.Data = Data;
            this.Message = Message;
            BIT_STR_LENGTH = MESSAGE.Length.ToString();

            while (BIT_STR_LENGTH.Length < 8)
            {
                BIT_STR_LENGTH += 's';
            }
        }
        public Utils(byte[] Data)
        {
            this.Data = Data;
        }
        public byte Bit_To_Byte(byte bytes, byte text)
        {
            return Convert.ToByte(((bytes >> 2) << 2) | ((text) % 4));
        }
        public byte[] Bytes_Fulled_Bits(string text)
        {
            byte[] lenght = new byte[4 * text.Length];
            int i = 0;
            while (i < lenght.Length)
            {
                for (int j = 0; j < 4; ++j)
                {
                    lenght[3 + i - j] = Convert.ToByte((text[i / 4] >> 2 * j) % 4);
                }
                i += 4;
            }
            return lenght;
        }
        public string FourBitToStr(byte[] bit_str)
        {
            int i = 0;
            string str = "";
            while (i < bit_str.Length)
            {
                byte sym = 0;
                for (int j = 0; j < 4; ++j)
                {
                    sym |= (byte)(bit_str[i] % 4);
                    if (j < 3)
                    {
                        sym <<= 2;
                    }
                    ++i;
                }
                str += (char)(sym);
            }
            return str;
        }
        public byte[] Change2Bits()
        {
            byte[] messageToByte = Bytes_Fulled_Bits(MESSAGE);
            for (int i = 0; i < 4*MESSAGE.Length + 32; i++)
            {
                if (i >= 32)
                {
                    DATA[i] = Bit_To_Byte(DATA[i], messageToByte[i-32]);
                }
                else
                {
                    DATA[i] = Bit_To_Byte(DATA[i], Bytes_Fulled_Bits(BIT_STR_LENGTH)[i]);
                }
            }
            return DATA;
        }
        public byte Give2Bits(byte bytes)
        {
            return Convert.ToByte(bytes % 4);    
        }
        public string Take2Bits()
        {
            string str_res = " ";
            byte[] bit_res;
            string size_ = "";
            byte[] bit_size = new byte[32];
            Array.Copy(DATA, 54, bit_size, 0, 32);
            for (int j = 0; j < bit_size.Length; j++)
            {
                bit_size[j] = Give2Bits(bit_size[j]);
            }
            string str_size = FourBitToStr(bit_size);
            size_ = str_size.Substring(0, str_size.IndexOf('s'));

            bit_res = new byte[4*Convert.ToUInt32(size_)];
            Array.Copy(DATA, 86, bit_res, 0, 4*Convert.ToUInt32(size_));
            str_res = FourBitToStr(bit_res);
            return str_res;
        }
    }
    interface IEncriptor
    {
        byte[] Encript(string text);
        string Decript();
    }

    class Png_Encriptor : IEncriptor
    {
        public byte[] png_data;
        public Png_Encriptor(byte[] png_data)
        {
            this.png_data = png_data;
        }
        public Png_Encriptor() { }
        public byte[] Encript(string text)
        {
            PngParser pngParser = new PngParser(png_data);
            List<Chunk> newChunks = new List<Chunk>();
            foreach (var chunk in pngParser.Chunks)
            {
                Utils ToEncrypt = new Utils(chunk.Unpack());
                chunk.DATA = Chunk.Pack((ToEncrypt.Change2Bits()));
                newChunks.Add(chunk);
            }
            PngParser newPngParser = new PngParser(pngParser.Png_signature, pngParser._IHDR, newChunks, pngParser.IEND_);
            return newPngParser.Png_DATA;
        }
        public string Decript()
        {
            return "sda";
        }
    }
    class BMP_Encriptor : IEncriptor
    {
        public byte[] bmp_data;
        public BMP_Encriptor(byte[] bmp_data)
        {
            this.bmp_data = bmp_data;
        }
        public byte[] Encript(string text)
        {
            BMP_PARSER bmp_to_parse = new BMP_PARSER(bmp_data);
            byte[] newData = new byte[bmp_data.Length - 54];
            Utils toEncrypt = new Utils(bmp_to_parse.OLDDATA,text);
            newData=toEncrypt.Change2Bits();
            BMP_PARSER new_bmp_to_collect = new BMP_PARSER(bmp_to_parse.BFTYPEBYTE, bmp_to_parse.BFSIZEBYTE, bmp_to_parse.BFRESERVED1BYTE, bmp_to_parse.BFRESERVED2BYTE, bmp_to_parse.BFOFFBITSBYTE, bmp_to_parse.BISIZEBYTE, bmp_to_parse.BIWIDTHBYTE, bmp_to_parse.BIHEIGHTBYTE, bmp_to_parse.BIPLANESBYTE, bmp_to_parse.BIBITCOUNTBYTE, bmp_to_parse.BICOMPRESSIONBYTE, bmp_to_parse.BISIZEIMAGEBYTE, bmp_to_parse.BIXPELSPERMETERBYTE, bmp_to_parse.BIYPELSPERMETERBYTE, bmp_to_parse.BICLRUSEDBYTE, bmp_to_parse.BICLRIMPORTANTBYTE, newData);
            return new_bmp_to_collect.BMP;
        }
        public string Decript()
        {
            Utils toDecrypt = new Utils(bmp_data);
            return toDecrypt.Take2Bits();
        }
    }
    class JPG_Encriptor : IEncriptor
    {
        public byte[] jpg_data;
        public JPG_Encriptor(byte[] jpg_data)
        {
            this.jpg_data = jpg_data;
        }
        public byte[] Encript(string text)
        {
            JPEG_Comments jpg_to_parse = new JPEG_Comments(jpg_data, text);
            return jpg_to_parse.NEW_DATA;
        }
        public string Decript()
        {
            JPEG_Comments jpg_to_parse = new JPEG_Comments(jpg_data);
            return jpg_to_parse.Answer;
        }
    }
}
