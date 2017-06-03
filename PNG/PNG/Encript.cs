using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNG
{
    abstract class Encriptor
    {
        public abstract byte[] encript();
    }

    class Png_Encriptor : Encriptor
    {
        public byte[] png_data;
        public Png_Encriptor(byte[] png_data)
        {
            this.png_data = png_data;
        }
        public Png_Encriptor() { }
        public override byte[] encript()
        {
            PngParser pngParser = new PngParser(png_data);
            List<Chunk> newChunks = new List<Chunk>();
            foreach (var chunk in pngParser.Chunks)
            {
                chunk.DATA = Chunk.Pack((Change2Bits(chunk.Unpack())));
                newChunks.Add(chunk);
            }
            PngParser newPngParser = new PngParser(pngParser.Png_signature, pngParser._IHDR, newChunks, pngParser.IEND_);
            return newPngParser.Png_DATA;
        }
        public byte[] Change2Bits(byte[] array)
        { return array; }
    }
}
