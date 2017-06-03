using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebuch_project
{
    class BMP_PARSER
    {
        private byte[] word;
        private byte[] newBMPData;
        private byte[] oldbmpData;
        private byte[] oldData;
        private byte[] newData;
        private byte[] dword;
        public int position;
        private UInt16 bfType;
        private UInt32 bfSize;
        private UInt16 bfReserved1;
        private UInt16 bfReserved2;
        private UInt32 bfOffBits;
        private UInt32 biSize;
        private UInt32 biWidth;
        private UInt32 biHeaight;
        private UInt16 biPlanes;
        private UInt16 biBitCount;
        private UInt32 biCompression;
        private UInt32 biSizeImage;
        private UInt32 biXPelsPerMeter;
        private UInt32 biYPelsPerMeter;
        private UInt32 biClrUsed;
        private UInt32 biClrImportant;
        private byte[] bfTypeByte;
        private byte[] bfSizeByte;
        private byte[] bfReserved1Byte;
        private byte[] bfReserved2Byte;
        private byte[] bfOffBitsByte;
        private byte[] biSizeByte;
        private byte[] biWidthByte;
        private byte[] biHeaightByte;
        private byte[] biPlanesByte;
        private byte[] biBitCountByte;
        private byte[] biCompreseeionByte;
        private byte[] biSizeImageByte;
        private byte[] biXPelsPerMeterByte;
        private byte[] biYPelsPerMeterByte;
        private byte[] biClrUsedByte;
        private byte[] biClrImportantByte;
        public byte[] BFTYPEBYTE
        { 
            get
            {
                return bfTypeByte;
            }
            set
            {
                bfTypeByte = value;
            }
        }
        public byte[] BFSIZEBYTE
        {
            get
            {
                return bfSizeByte;
            }
            set
            {
                bfSizeByte = value;
            }
        }
        public byte[] BFRESERVED1BYTE
        {
            get
            {
                return bfReserved1Byte;
            }
            set
            {
                bfReserved1Byte = value;
            }
        }
        public byte[] BFRESERVED2BYTE
        {
            get
            {
                return bfReserved2Byte;
            }
            set
            {
                bfReserved2Byte = value;
            }
        }
        public byte[] BFOFFBITSBYTE
        {
            get
            {
                return bfOffBitsByte;
            }
            set
            {
                bfOffBitsByte = value;
            }
        }
        public byte[] BISIZEBYTE
        {
            get 
            {
                return biSizeByte;
            }
            set
            {
                biSizeByte = value;
            }
        }
        public byte[] BIWIDTHBYTE
        {
            get
            {
                return biWidthByte;
            }
            set
            {
                biWidthByte = value;
            }
        }
        public byte[] BIHEIGHTBYTE
        {
            get
            {
                return biHeaightByte;
            }
            set
            {
                biHeaightByte = value;
            }
        }
        public byte[] BIPLANESBYTE
        {
            get
            {
                return biPlanesByte;
            }
            set
            {
                biPlanesByte = value;
            }
        }
        public byte[] BIBITCOUNTBYTE
        {
            get
            {
                return biBitCountByte;
            }
            set
            {
                biBitCountByte = value;
            }
        }
        public byte[] BICOMPRESSIONBYTE
        {
            get
            {
                return biCompreseeionByte;
            }
            set
            {
                biCompreseeionByte = value;
            }
        }
        public byte[] BISIZEIMAGEBYTE
        {
            get
            {
                return biSizeImageByte;
            }
            set
            {
                biSizeImageByte = value;
            }
        }
        public byte[] BIXPELSPERMETERBYTE
        {
            get 
            {
                return biXPelsPerMeterByte;
            }
            set 
            {
                biXPelsPerMeterByte = value;
            }
        }
        public byte[] BIYPELSPERMETERBYTE
        {
            get
            {
                return biYPelsPerMeterByte;
            }
            set
            {
                biYPelsPerMeterByte = value;
            }
        }
        public byte[] BICLRUSEDBYTE
        {
            get
            {
                return biClrUsedByte;
            }
            set
            {
                biClrUsedByte = value;
            }
        }
        public byte[] BICLRIMPORTANTBYTE
        {
            get
            {
                return biClrImportantByte;
            }
            set
            {
                biClrImportantByte = value;
            }
        }
        public UInt16 BFTYPE
        {
            get
            {
                return bfType;
            }
            set
            {
                bfType = value;
            }
        }
        public UInt32 BFSIZE
        {
            get 
            {
                return bfSize;
            }
            set
            {
                bfSize = value;
            }
        }
        public UInt16 BFRESERVED1
        {
            get
            {
                return bfReserved1;
            }
            set
            {
                bfReserved1 = value;
            }
        }
        public UInt16 BFRESERVED2
        {
            get 
            {
                return bfReserved2;
            }
            set
            {
                bfReserved2 = value;
            }
        }
        public UInt32 BFOFFBITS
        {
            get
            {
                return bfOffBits;
            }
            set
            {
                bfOffBits = value;
            }
        }
        public UInt32 BISIZE
        {
            get 
            {
                return biSize;
            }
            set
            {
                biSize = value;
            }
        }
        public UInt32 BIWIDTH
        {
            get 
            {
                return biWidth;
            }
            set
            {
                biWidth = value;
            }
        }
        public UInt32 BIHEIGHT
        {
            get 
            {
                return biHeaight;
            }
            set
            {
                biHeaight = value;
            }
        }
        public UInt16 BIPLANES
        {
            get
            {
                return biPlanes;
            }
            set
            {
                biPlanes = value;
            }
        }
        public UInt16 BIBITCOUNT
        {
            get
            {
                return biBitCount;
            }
            set
            {
                biBitCount = value;
            }
        }
        public UInt32 BiCOMPRESSION
        {
            get
            {
                return biCompression;
            }
            set
            {
                biCompression = value;
            }
        }
        public UInt32 BISIZEIMAGE
        {
            get
            {
                return biSizeImage;
            }
            set
            {
                biSizeImage = value;
            }
        }
        public UInt32 BIXPELSPERMETER
        {
            get
            {
                return biXPelsPerMeter;
            }
            set
            {
                biXPelsPerMeter = value;
            }
        }
        public UInt32 BIYPELSPERMETER
        {
            get
            {
                return biYPelsPerMeter;
            }
            set
            {
                biYPelsPerMeter = value;
            }
        }
        public UInt32 BICLRUSED
        {
            get
            {
                return biClrUsed;
            }
            set
            {
                biClrUsed = value;
            }
        }
        public UInt32 BICLRIMPORTANT
        {
            get
            {
                return biClrImportant;
            }
            set
            {
                biClrImportant = value;
            }
        }
        public byte[] OLDBMPDATA
        {
            get { return oldbmpData; }
        }
        public byte[] NEWDATA
        {
            get
            {
                return newData;
            }
            set
            {
                newData = value;
            }
        }
        public byte[] OLDDATA
        {
            get 
            {
                return oldData;
            }
            set
            {
                oldData = value;
            }
        }

        public BMP_PARSER(byte[] oldbmpData)
        {
            this.oldbmpData = oldbmpData;
            word = new byte[2];
            dword = new byte[4];
            OLDDATA = new byte[oldbmpData.Length - 54];
            Parse_BMP_Faile();
        }
        public BMP_PARSER(byte[] bfTypeByte, byte[] bfSizeByte, byte[] bfReserved1Byte, byte[] bfReserved2Byte, byte[] bfOffBitsByte, byte[] biSizeByte, byte[] biWidthByte, byte[] biHeaightByte, byte[] biPlanesByte, byte[] biBitCountByte, byte[] biCompressionByte, byte[] biSizeImageByte, byte[] biXPelsPerMeterByte, byte[] biYPelsPerMeterByte, byte[] biClrUsedByte, byte[] biClrImportantByte, byte[] newData)
        {
            this.bfTypeByte = bfTypeByte;
            this.bfSizeByte = bfSizeByte;
            this.bfReserved1Byte = bfReserved1Byte;
            this.bfReserved2Byte = bfReserved2Byte;
            this.bfOffBitsByte = bfOffBitsByte;
            this.biSizeByte = biSizeByte;
            this.biWidthByte = biWidthByte;
            this.biHeaightByte = biHeaightByte;
            this.biPlanesByte = biPlanesByte;
            this.biBitCountByte = biBitCountByte;
            this.biCompreseeionByte = biCompressionByte;
            this.biSizeImageByte = biSizeImageByte;
            this.biXPelsPerMeterByte = biXPelsPerMeterByte;
            this.biYPelsPerMeterByte = biYPelsPerMeterByte;
            this.biClrUsedByte = biClrUsedByte;
            this.biClrImportantByte = biClrImportantByte;
            this.newData = newData;
            newBMPData = new byte[NEWDATA.Length + 54];
        }
        public byte[] NEWBMPDATA
        {
            get 
            { 
                return newBMPData; 
            }
            set
            {
                newBMPData = value;
            }
        }
        public byte[] BMP
        {
            get
            {
                position = 0;
                
                Array.Copy(BFTYPEBYTE, 0, NEWBMPDATA, position, 2);
                position += 2;

                Array.Copy(BFSIZEBYTE, 0, NEWBMPDATA, position, 4);
                position += 4;

                Array.Copy(BFRESERVED1BYTE, 0, NEWBMPDATA, position, 2);
                position += 2;

                Array.Copy(BFRESERVED2BYTE, 0, NEWBMPDATA, position, 2);
                position += 2;

                Array.Copy(BFOFFBITSBYTE, 0, NEWBMPDATA, position, 4);
                position += 4;

                Array.Copy(BISIZEBYTE, 0, NEWBMPDATA, position, 4);
                position += 4;

                Array.Copy(BIWIDTHBYTE, 0, NEWBMPDATA, position, 4);
                position += 4;

                Array.Copy(BIHEIGHTBYTE, 0, NEWBMPDATA, position, 4);
                position += 4;

                Array.Copy(BIPLANESBYTE, 0, NEWBMPDATA, position, 2);
                position += 2;

                Array.Copy(BIBITCOUNTBYTE,0,NEWBMPDATA,position,2);
                position += 2;

                Array.Copy(BICOMPRESSIONBYTE, 0, NEWBMPDATA, position, 4);
                position += 4;

                Array.Copy(BISIZEIMAGEBYTE, 0, NEWBMPDATA, position, 4);
                position += 4;

                Array.Copy(BIXPELSPERMETERBYTE, 0, NEWBMPDATA, position, 4);
                position += 4;

                Array.Copy(BIYPELSPERMETERBYTE, 0, NEWBMPDATA, position, 4);
                position += 4;

                Array.Copy(BICLRUSEDBYTE,0,NEWBMPDATA,position,4);
                position += 4;

                Array.Copy(BICLRIMPORTANTBYTE,0,NEWBMPDATA,position,4);
                position += 4;

                Array.Copy(NEWDATA, 0, NEWBMPDATA, position, NEWDATA.Length);

                return NEWBMPDATA;
            }
        }
        private void Parse_BMP_Faile()
        {
            Array.Copy(OLDBMPDATA, position, word, 0, 2);
            position += 2;
            BFTYPEBYTE = new byte[2];
            Array.Copy(word,BFTYPEBYTE,2);
            Array.Reverse(word);
            BFTYPE = BitConverter.ToUInt16(word, 0);

            Array.Copy(OLDBMPDATA, position, dword, 0, 4);
            position += 4;
            BFSIZEBYTE = new byte[4];
            Array.Copy(dword, BFSIZEBYTE, 4);
            Array.Reverse(dword);
            BFSIZE = BitConverter.ToUInt32(dword, 0);

            Array.Copy(OLDBMPDATA, position, word, 0, 2);
            position += 2;
            BFRESERVED1BYTE = new byte[2];
            Array.Copy(word, BFRESERVED1BYTE, 2);
            Array.Reverse(word);
            BFRESERVED1 = BitConverter.ToUInt16(word, 0);

            Array.Copy(OLDBMPDATA, position, word, 0, 2);
            position += 2;
            BFRESERVED2BYTE = new byte[2];
            Array.Copy(word, BFRESERVED2BYTE, 2);
            Array.Reverse(word);
            BFRESERVED2 = BitConverter.ToUInt16(word, 0);

            Array.Copy(OLDBMPDATA, position, dword, 0, 4);
            position += 4;
            BFOFFBITSBYTE = new byte[4];
            Array.Copy(dword,BFOFFBITSBYTE,4);
            Array.Reverse(dword);
            BFOFFBITS = BitConverter.ToUInt32(dword, 0);

            Array.Copy(OLDBMPDATA, position, dword, 0, 4);
            position += 4;
            BISIZEBYTE = new byte[4];
            Array.Copy(dword, BISIZEBYTE, 4);
            Array.Reverse(dword);
            BISIZE = BitConverter.ToUInt32(dword, 0);

            Array.Copy(OLDBMPDATA, position, dword, 0, 4);
            position += 4;
            BIWIDTHBYTE = new byte[4];
            Array.Copy(dword,BIWIDTHBYTE,4);
            Array.Reverse(dword);
            BIWIDTH = BitConverter.ToUInt32(dword, 0);

            Array.Copy(OLDBMPDATA, position, dword, 0, 4);
            position += 4;
            BIHEIGHTBYTE = new byte[4];
            Array.Copy(dword,BIHEIGHTBYTE,4);
            Array.Reverse(dword);
            BIHEIGHT = BitConverter.ToUInt32(dword,0);

            Array.Copy(OLDBMPDATA, position, word, 0, 2);
            position += 2;
            BIPLANESBYTE = new byte[2];
            Array.Copy(word, BIPLANESBYTE, 2);
            Array.Reverse(word);
            BIPLANES = BitConverter.ToUInt16(word, 0);

            Array.Copy(OLDBMPDATA, position, word, 0, 2);
            position += 2;
            BIBITCOUNTBYTE = new byte[2];
            Array.Copy(word,BIBITCOUNTBYTE,2);
            Array.Reverse(word);
            BIBITCOUNT = BitConverter.ToUInt16(word,0);

            Array.Copy(OLDBMPDATA, position, dword, 0, 4);
            position += 4;
            BICOMPRESSIONBYTE = new byte[4];
            Array.Copy(dword, BICOMPRESSIONBYTE, 4);
            Array.Reverse(dword);
            BiCOMPRESSION = BitConverter.ToUInt32(dword,0);

            Array.Copy(OLDBMPDATA, position, dword, 0, 4);
            position += 4;
            BISIZEIMAGEBYTE = new byte[4];
            Array.Copy(dword,BISIZEIMAGEBYTE,4);
            Array.Reverse(dword);
            BISIZEIMAGE = BitConverter.ToUInt32(dword,0);

            Array.Copy(OLDBMPDATA, position, dword, 0, 4);
            position += 4;
            BIXPELSPERMETERBYTE = new byte[4];
            Array.Copy(dword,BIXPELSPERMETERBYTE,4);
            Array.Reverse(dword);
            BIXPELSPERMETER = BitConverter.ToUInt32(dword,0);

            Array.Copy(OLDBMPDATA, position, dword, 0, 4);
            position += 4;
            BIYPELSPERMETERBYTE = new byte[4];
            Array.Copy(dword, BIYPELSPERMETERBYTE, 4);
            Array.Reverse(dword);
            BIYPELSPERMETER = BitConverter.ToUInt32(dword, 0);

            Array.Copy(OLDBMPDATA, position, dword, 0, 4);
            position += 4;
            BICLRUSEDBYTE = new byte[4];
            Array.Copy(dword,BICLRUSEDBYTE,4);
            Array.Reverse(dword);
            BICLRUSED = BitConverter.ToUInt32(dword,0);

            Array.Copy(OLDBMPDATA, position, dword, 0, 4);
            position += 4;
            BICLRIMPORTANTBYTE = new byte[4];
            Array.Copy(dword,BICLRIMPORTANTBYTE,4);
            Array.Reverse(dword);
            biClrImportant = BitConverter.ToUInt32(dword,0);

            Array.Copy(OLDBMPDATA, position, OLDDATA, 0, OLDBMPDATA.Length - position);
        }
    }
}
