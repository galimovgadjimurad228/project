using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNG
{
    abstract class Encriptor
    {
        public abstract byte[] encript(byte[] array);
    }

    class Png_Encriptor : Encriptor
    {
        public byte[] encript(byte[] png_data)
        {
            PngParser pngParser = new PngParser(png_data);
            List<Chunk> newChunks;
            foreach (var chunk in pngParser.Chunks)
            {
                chunk.DATA = Chunk.Pack((Change2Bits(chunk.unpack()));
                newChunks.Add(chunk);
            }
            PngParser newPngParser = new PngParser(pngParser.signature, pngParser.IHDR..., newChunks);
            return newPngParser.PngData;
        }
    }
}
