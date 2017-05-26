using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BMP_
{

    class Fields
    {
        public BMP_values_for_2_bytes bfType = new BMP_values_for_2_bytes(0);
        public BMP_values_for_2_bytes bfReserved1 = new BMP_values_for_2_bytes(1);
        public BMP_values_for_2_bytes bfReserved2 = new BMP_values_for_2_bytes(2);
        public BMP_values_for_2_bytes biPlanes = new BMP_values_for_2_bytes(3);
        public BMP_values_for_2_bytes biBitCount = new BMP_values_for_2_bytes(4);


        public BMP_values_for_4_bytes bfSize = new BMP_values_for_4_bytes(0);
        public BMP_values_for_4_bytes bfOffBits = new BMP_values_for_4_bytes(1);
        public BMP_values_for_4_bytes biSize = new BMP_values_for_4_bytes(2);
        public BMP_values_for_4_bytes biWidth = new BMP_values_for_4_bytes(3);
        public BMP_values_for_4_bytes biHeight = new BMP_values_for_4_bytes(4);
        public BMP_values_for_4_bytes biCompression = new BMP_values_for_4_bytes(5);
        public BMP_values_for_4_bytes biSizeImage = new BMP_values_for_4_bytes(6);
        public BMP_values_for_4_bytes biXPelsPerMeter = new BMP_values_for_4_bytes(7);
        public BMP_values_for_4_bytes biYPelsPerMeter = new BMP_values_for_4_bytes(8);
        public BMP_values_for_4_bytes biClrUsed = new BMP_values_for_4_bytes(9);
        public BMP_values_for_4_bytes biClrImportant = new BMP_values_for_4_bytes(10);
    }
    class CHECK
    {
        Fields checking = new Fields();
        public void Check_the_title(ERRORS eror)
        {
            if (checking.bfType.value != 0X4D42)
                eror.index_for_errors = 1;
        }
        public void Check_the_compression(ERRORS eror)
        {
            if (checking.biCompression.znach != 0)
                eror.index_for_errors = 2;
        }
        public void Check_the_Planes(ERRORS eror)
        {
            if (checking.biPlanes.value >= 1)
                eror.index_for_errors = 3;
        }
        public void Check_the_Pixels(ERRORS eror)
        {
            if (((checking.biHeight.znach * checking.biWidth.znach * checking.biBitCount.value) / 8) != checking.biSizeImage.znach)
                eror.index_for_errors = 4;
        }
        public void Check_size_image(ERRORS eror)
        {
            if (checking.biSizeImage.znach + 54 != checking.bfSize.znach + 2)
                eror.index_for_errors = 5;
        }

        public void Reverse1(ERRORS eror)
        {
            if (checking.bfReserved1.value != 0)
                eror.index_for_errors = 6;
        }
        public void Reverse2(ERRORS eror)
        {
            if (checking.bfReserved2.value != 0)
                eror.index_for_errors = 6;
        }
        public void Check_the_equality_between_size_Image_and_BYTES_FOR_THE_LSB(uint size, ERRORS eror)
        {
            if (checking.biSizeImage.znach - 36 != size - 52)
                eror.index_for_errors = 7;
        }
    }
    class ERRORS
    {
        public string comments;
        public int index_for_errors;
        public ERRORS()
        {
        }
        public void The_Reasons()
        {
            if (index_for_errors == 1)
            {
                comments = "It is not BMP";
            }
            if (index_for_errors == 2)
            {
                comments = "Type the Compression is bad";
            }
            if (index_for_errors == 3)
            {
                comments = "Problem with the amount planes";
            }
            if (index_for_errors == 4)
            {
                comments = "Amount the Pixels";
            }
            if (index_for_errors == 5)
            {
                comments = "Size the image ";
            }
            if (index_for_errors == 6)
            {
                comments = "Reserve is not 0";
            }
            if (index_for_errors == 7)
            {
                //comments = "BYTES_FOR_LSB.Length is not size of image";
            }
        }
    }

    class BMP_values_for_2_bytes
    { 
        public int poses=0;
        public UInt16 value=0;
        public BMP_values_for_2_bytes(int poses)
        {
            this.poses=poses;
        }
    }
    class BMP_values_for_4_bytes
    {
        public int location;
        public UInt32 znach;
        public BMP_values_for_4_bytes(int location)
        {
            this.location=location;
        }
    }
    class Program
    {
        
        static byte[] WORD = new byte[2];
        static byte[] DWORD=new byte[4];
        public static UInt16 Transform_to_two_bytes()
        {
            return BitConverter.ToUInt16(WORD, 0);
        }
        public static UInt32 Transform_to_four_bytes()
        {
            return BitConverter.ToUInt32(DWORD,0);
        }
        static void Main(string[] args)
        {
            const string text = "Cat_dog_rog_fog_female_marker_kreker";

            Stream input = Console.OpenStandardInput();
            Stream output = Console.OpenStandardOutput();
            Stream stderr = Console.OpenStandardError();

            uint size=0;//for the LSB

            Fields PEMP = new Fields();

            input.Read(WORD, 0, 2);
            output.Write(WORD,0,2);
            WORD.Reverse();
            PEMP.bfType.value = Transform_to_two_bytes();

            input.Read(DWORD, 0, 4);
            output.Write(DWORD,0,4);
            DWORD.Reverse();
            PEMP.bfSize.znach = Transform_to_four_bytes();

            size = PEMP.bfSize.znach - 2;

            input.Read(WORD,0,2);
            output.Write(WORD, 0, 2);
            WORD.Reverse();
            PEMP.bfReserved1.value = Transform_to_two_bytes();

            input.Read(WORD,0,2);
            output.Write(WORD, 0, 2);
            WORD.Reverse();
            PEMP.bfReserved2.value = Transform_to_two_bytes();

            input.Read(DWORD,0,4);
            output.Write(DWORD, 0, 4);
            DWORD.Reverse();
            PEMP.bfOffBits.znach = Transform_to_four_bytes();

            input.Read(DWORD, 0, 4);
            output.Write(DWORD, 0, 4);
            DWORD.Reverse();
            PEMP.biSize.znach = Transform_to_four_bytes();

            input.Read(DWORD, 0, 4);
            output.Write(DWORD, 0, 4);
            DWORD.Reverse();
            PEMP.biWidth.znach = Transform_to_four_bytes();

            input.Read(DWORD,0,4);
            output.Write(DWORD, 0, 4);
            DWORD.Reverse();
            PEMP.biHeight.znach = Transform_to_four_bytes();

            input.Read(WORD, 0, 2);
            output.Write(WORD, 0, 2);
            WORD.Reverse();
            PEMP.biPlanes.value = Transform_to_two_bytes();

            input.Read(WORD,0,2);
            output.Write(WORD, 0, 2);
            WORD.Reverse();
            PEMP.biBitCount.value = Transform_to_two_bytes();

            input.Read(DWORD, 0, 4);
            output.Write(DWORD, 0, 4);
            DWORD.Reverse();
            PEMP.biCompression.znach = Transform_to_four_bytes();

            input.Read(DWORD,0,4);
            output.Write(DWORD, 0, 4);
            DWORD.Reverse();
            PEMP.biSizeImage.znach = Transform_to_four_bytes();

            input.Read(DWORD,0,4);
            output.Write(DWORD, 0, 4);
            DWORD.Reverse();
            PEMP.biXPelsPerMeter.znach = Transform_to_four_bytes();

            input.Read(DWORD, 0, 4);
            output.Write(DWORD, 0, 4);
            DWORD.Reverse();
            PEMP.biYPelsPerMeter.znach = Transform_to_four_bytes();

            input.Read(DWORD, 0, 4);
            output.Write(DWORD, 0, 4);
            DWORD.Reverse();
            PEMP.biClrUsed.znach = Transform_to_four_bytes();

            input.Read(DWORD,0,4);
            output.Write(DWORD, 0, 4);
            DWORD.Reverse();
            PEMP.biClrImportant.znach = Transform_to_four_bytes();

            byte[] BYTES_FOR_LSB=new byte[size-52];

            input.Read(BYTES_FOR_LSB, 0, BYTES_FOR_LSB.Length);

            ERRORS erer = new ERRORS();
            CHECK checking = new CHECK();

            checking.Check_the_title(erer);
            checking.Reverse1(erer);
            checking.Reverse2(erer);
            checking.Check_size_image(erer);
            checking.Check_the_compression(erer);
            checking.Check_the_equality_between_size_Image_and_BYTES_FOR_THE_LSB(size, erer);
            checking.Check_the_Pixels(erer);
            checking.Check_the_Planes(erer);

            byte[] for_problems=new byte[4];

            erer.The_Reasons();
            stderr.WriteByte((byte)(erer.index_for_errors));
            string strings = "Not problems";
            strings = erer.comments;
            if (strings != null)
            {
                stderr.Write(Encoding.ASCII.GetBytes(strings), 0, strings.Length);
                Console.Write(PEMP.biSizeImage.znach);
                Console.Write(" ");
                Console.Write(PEMP.biSize.znach);
                Environment.Exit(1);
            }

            

            byte[] leght = new byte[4];
            string str_length;
            byte[] bit_str_length;
            byte[] bit_lok;
            bit_lok = Utilits.Bytes_Fulled_Bits(text);
            str_length = BYTES_FOR_LSB.Length.ToString();

            while (str_length.Length < 8)
            {
                str_length += 's';
            }

            bit_str_length = Utilits.Bytes_Fulled_Bits(str_length);

            if (args[0] == "-Enc")
            {

                for (int i = 0; i < bit_lok.Length + 32; i++)
                {
                    if (i >= 32)
                    {
                        BYTES_FOR_LSB[i] = Utilits.Bit_To_Byte(BYTES_FOR_LSB[i], bit_lok[i - 32]);
                    }
                    else
                    {
                        BYTES_FOR_LSB[i] = Utilits.Bit_To_Byte(BYTES_FOR_LSB[i], bit_str_length[i]);
                    }
                }

                output.Write(BYTES_FOR_LSB, 0, BYTES_FOR_LSB.Length);
            }
            if (args[0] == "-Dec")
            {
                for (int i = 0; i < BYTES_FOR_LSB.Length; i++)
                {
                    if (i >= 2)
                    {
                        byte[] bit_res = new byte[bit_lok.Length];
                        Array.Copy(BYTES_FOR_LSB, 32, bit_res, 0, bit_lok.Length);
                        string str_res = Utilits.FourBitToStr(bit_res);
                        break;
                    }
                    else
                    {
                        byte[] bit_size = new byte[32];
                        Array.Copy(bit_str_length, bit_size, 32);
                        string str_size = Utilits.FourBitToStr(bit_size);
                        string size_ = str_size.Substring(0, str_size.IndexOf('s'));

                    }
                }}
            }
        }
    static class Utilits
    {
        public static byte Bit_To_Byte(byte bytes, byte text)
        {
            return Convert.ToByte(((bytes >> 2) << 2) | ((text) % 4));
        }
        public static byte[] Bytes_Fulled_Bits(string text)
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
        public static string FourBitToStr(byte[] bit_str)
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
    }
}
