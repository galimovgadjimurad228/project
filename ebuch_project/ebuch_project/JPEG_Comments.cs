using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebuch_project
{
    class JPEG_Comments
    {
        private byte[] jpeg_data;
        private string message;
        //Вот здесь ВОПРОС??????
        private byte[] data_for_jpegcomments;
        private byte[] header = new byte[2];
        private byte[] lenght = new byte[2];
        string answer="";
        public int position = 0;
        public int new_position = 0;
        byte[] title_for_comments = new byte[2];
        public JPEG_Comments(byte[] jpeg_data)
        {
            this.jpeg_data = jpeg_data;
        }
        public JPEG_Comments(byte[] jpeg_data, string message)
        {
            this.jpeg_data = jpeg_data;
            this.message = message;
            title_for_comments[0] = (byte)0XFF;
            title_for_comments[1] = (byte)0XFE;
            if (message.Length >= 32768)
            {
                if (message.Length % 32768 == 0)
                {
                    data_for_jpegcomments = new byte[jpeg_data.Length + 32768 * message.Length / 32768 + 4 * message.Length / 32768];
                }
                else
                {
                    data_for_jpegcomments = new byte[jpeg_data.Length + 32768 * (message.Length / 32768) + 4 * message.Length / 32768 + 4 + (message.Length - 32768 * (message.Length / 32768))];
                }
            }
            else
            {
                data_for_jpegcomments = new byte[jpeg_data.Length + message.Length + 2 + 2];
            }
        }

        public byte[] JPEG_DATA
        {
            get { return jpeg_data; }
        }
        public string MESSAGE
        {
            get { return message; }
        }
        public byte[] Data_for_jpegcomments
        {
            get { return data_for_jpegcomments; }
            set { data_for_jpegcomments = value; }
        }
        public byte[] Lenght
        {
            get { return lenght; }
            set { lenght = value; }
        }
        public byte[] Header
        {
            get { return header; }
            set { header = value; }
        }
        public byte[] NEW_DATA
        {
            get
            {
                bool flag_for_comments = true;
                int i = MESSAGE.Length / 32768;
                int p = 0;
                byte[] lenght_mass = new byte[2];

                if ((MESSAGE.Length / JPEG_DATA.Length) * 100 > 20)
                {
                    Console.WriteLine("Warning: the size of comment is very big");
                }
                Array.Copy(JPEG_DATA, Header, 2);
                Array.Copy(Header, Data_for_jpegcomments, 2);
                new_position += 2;
                position += 2;

                if (BitConverter.ToUInt16(Header, 0) != 0XD8FF)
                {
                    //problem = "It is not JPEG";
                    //stderr.Write(Encoding.ASCII.GetBytes(problem), 0, problem.Length);
                    //Environment.Exit(1);
                }

                while (new_position != Data_for_jpegcomments.Length)
                {
                    Array.Copy(JPEG_DATA, position, Header, 0, 2);
                    Array.Copy(header, 0, Data_for_jpegcomments, new_position, 2);
                    position += 2;
                    new_position += 2;

                    Array.Copy(JPEG_DATA, position, Lenght, 0, 2);
                    Array.Copy(Lenght, 0, Data_for_jpegcomments, new_position, 2);
                    position += 2;
                    new_position += 2;
                    if (position == Data_for_jpegcomments.Length)
                        break;
                    Array.Reverse(Lenght);
                    UInt16 L = (UInt16)(BitConverter.ToUInt16(Lenght, 0) - 2);

                    Array.Copy(JPEG_DATA, position, Data_for_jpegcomments, new_position, L);
                    position += L;
                    new_position += L;
                    if (BitConverter.ToUInt16(Header, 0) != 0xFEFF)
                    {
                        if (flag_for_comments == true)
                        {
                            if (MESSAGE.Length > 32768)
                            {
                                while (p != i)
                                {
                                    Array.Copy(title_for_comments, 0, data_for_jpegcomments, new_position, 2);
                                    new_position += 2;
                                    UInt16 text_of_lenght_to_byte = (UInt16)(32768 - 2);
                                    lenght_mass = BitConverter.GetBytes(text_of_lenght_to_byte);
                                    Array.Reverse(lenght_mass);
                                    Array.Copy(lenght_mass, 0, Data_for_jpegcomments, new_position, 2);
                                    new_position += 2;
                                    Array.Copy(Encoding.ASCII.GetBytes(MESSAGE), p * 32678, Data_for_jpegcomments, new_position, text_of_lenght_to_byte);
                                    new_position += text_of_lenght_to_byte;
                                }
                                if (MESSAGE.Length % 32768 != 0)
                                {
                                    Array.Copy(title_for_comments, 0, Data_for_jpegcomments, new_position, 2);
                                    new_position += 2;
                                    UInt16 text_of_lenght_to_byte = (UInt16)(MESSAGE.Length - (32768 * i) + 2);
                                    lenght_mass = BitConverter.GetBytes(text_of_lenght_to_byte);
                                    Array.Reverse(lenght_mass);
                                    Array.Copy(lenght_mass, 0, Data_for_jpegcomments, new_position, 2);
                                    new_position += 2;
                                    Array.Copy(Encoding.ASCII.GetBytes(MESSAGE), i * 32678, Data_for_jpegcomments, new_position, text_of_lenght_to_byte);
                                    new_position += text_of_lenght_to_byte;
                                }
                            }
                            else
                            {
                                Array.Copy(title_for_comments, 0, Data_for_jpegcomments, new_position, 2);
                                new_position += 2;
                                UInt16 text_of_lenght_to_byte = (UInt16)(MESSAGE.Length + 2);
                                lenght_mass = BitConverter.GetBytes(text_of_lenght_to_byte);
                                Array.Reverse(lenght_mass);
                                Array.Copy(lenght_mass,0,Data_for_jpegcomments,new_position,2);
                                new_position += 2;
                                Array.Copy(Encoding.ASCII.GetBytes(MESSAGE),0,Data_for_jpegcomments,new_position,MESSAGE.Length);
                                new_position += MESSAGE.Length;
                            }
                        }
                        flag_for_comments = false;
                    }
                    if (BitConverter.ToUInt16(Header, 0) == 0XDAFF)
                    {
                        byte bytes;
                        while (new_position != Data_for_jpegcomments.Length)
                        {
                            bytes = jpeg_data[position];
                            data_for_jpegcomments[new_position] = bytes;
                            new_position += 1;
                            position += 1;
                        }
                    }
                }
                return Data_for_jpegcomments;
            }
        }
        public string Answer
        {
            get 
            {
                Array.Copy(JPEG_DATA,header,2);
                position += 2;

                byte[] comments;

                while (position < JPEG_DATA.Length)
                {
                    Array.Copy(JPEG_DATA, position, header, 0, 2);
                    position += 2;

                    Array.Copy(JPEG_DATA, position, Lenght, 0, 2);
                    position += 2;
                    Array.Reverse(Lenght);

                    UInt16 L = (UInt16)(BitConverter.ToUInt16(Lenght, 0) - 2);
                    comments = new byte[L];
                    Array.Copy(JPEG_DATA, position, comments, 0, L);

                    position += L;
                    if (BitConverter.ToUInt16(Header, 0) == 0xFEFF)
                    {
                        answer += Encoding.ASCII.GetString(comments);
                    }

                    if (BitConverter.ToUInt16(Header, 0) == 0XDAFF)
                    {
                        while (position != JPEG_DATA.Length)
                        {
                            position += 1;
                        }
                    }
                }
                return answer;
            }
            set { }
        }
    }
}
