using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace PNG
{
    //static class Utils
    //{ 
    //    public static byte ConvertToByte(BitArray bits)
    //    {
    //        if(bits.Count!=8)
    //        {
    //            throw new ArgumentException("bits");
    //        }
    //        byte[] bytes=new byte[1];
    //        bits.CopyTo(bytes,0);
    //        return bytes[0];
    //    }
    //}
    //class IHDR
    //{
    //    public byte[] weidth = new byte[4];
    //    public byte[] height = new byte[4];
    //    public byte bit_depth;
    //    public byte type_color;
    //    public byte type_compression;
    //    public byte method_filtr;
    //    public byte Interface;
    //    public IHDR(byte[] Data)
    //    {
    //        Array.Copy(Data, weidth, 4);
    //        Array.Copy(Data,4,height,0,4);
    //        bit_depth = Data[8];
    //        type_color = Data[9];
    //        type_compression = Data[10];
    //        method_filtr = Data[11];
    //        Interface = Data[12];
    //    }
    //}
    //abstract class Arhivator
    //{
    //    abstract byte[] Compress(byte[] array);
    //    abstract byte[] Decompress(byte[] array);

    //}
    //class Haffman : Arhivator
    //{
    //    public byte[] Compress(byte[] array)
    //    {
    //        int[] bytesCount = MakeBytesCount(array);
    //        HaffmanTree haffmanTree = new HaffmanTree(bytesCount);
    //        BitArray[] haffmanDictionary = haffmanTree.MakeDictionary();
    //        BitArray result = new BitArray(256 * 8 + GetHaffmanLength(array, haffmanDictionary, bytesCount));
    //        byte[] arrayHafmanDictionary;
    //        for (int i = 0; i < 256; i++)
    //        {
    //            arrayHafmanDictionary[i] = Utils.ConvertToByte(haffmanDictionary[i]);

    //        }
    //        for (int i = 0; i < array.Length; i++)
    //        {

    //        }

    //    }

    //    public byte[] Decompress(byte[] array)
    //    {

    //    }

    //    private int[] MakeBytesCount(byte[] array)
    //    {
    //        int[] bytesCount = new int[256];
    //        for (int i = 0; i < array.Length; i++)
    //        {
    //            bytesCount[(int)array[i]]++;
    //        }
    //        return bytesCount;
    //    }

    //    private int GetHaffmanLength(byte[] array, BitArray[] haffmanDictionary, int[] bytesCount)
    //    {
    //        int sum = 0;
    //        for (int i = 0; i < array.Length; ++i)
    //        {
    //            sum += haffmanDictionary[array[i]].Length * bytesCount[array[i]];
    //        }
    //        return sum;
    //    }
    //    class HaffmanTree
    //    {

    //    }
    //}

    class Program
    {
        static void Main(string[] args)
        {
            string path = @"720d7c16.png";
            Stream stderr = Console.OpenStandardError();
            byte[] DATA;
            FileStream fsSource = new FileStream(path, FileMode.Open, FileAccess.Read);
            DATA = new byte[fsSource.Length];
            fsSource.Read(DATA, 0, DATA.Length);
            Png_Encriptor encriptor = new Png_Encriptor(DATA);
            DATA=encriptor.encript();
            Console.ReadKey();
        }
    }
}
