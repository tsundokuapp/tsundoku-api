namespace TsundokuTraducoes.Helpers.Imagens
{
    public static class UtilidadeImagem
    {
        // some magic bytes for the most important image formats, see Wikipedia for more
        static readonly List<byte> jpg = new List<byte> { 0xFF, 0xD8 };
        static readonly List<byte> bmp = new List<byte> { 0x42, 0x4D };
        static readonly List<byte> gif = new List<byte> { 0x47, 0x49, 0x46 };
        static readonly List<byte> png = new List<byte> { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
        static readonly List<byte> svg_xml_small = new List<byte> { 0x3C, 0x3F, 0x78, 0x6D, 0x6C }; // "<?xml"
        static readonly List<byte> svg_xml_capital = new List<byte> { 0x3C, 0x3F, 0x58, 0x4D, 0x4C }; // "<?XML"
        static readonly List<byte> svg_small = new List<byte> { 0x3C, 0x73, 0x76, 0x67 }; // "<svg"
        static readonly List<byte> svg_capital = new List<byte> { 0x3C, 0x53, 0x56, 0x47 }; // "<SVG"
        static readonly List<byte> intel_tiff = new List<byte> { 0x49, 0x49, 0x2A, 0x00 };
        static readonly List<byte> motorola_tiff = new List<byte> { 0x4D, 0x4D, 0x00, 0x2A };

        static readonly List<(List<byte> magic, string extension)> imageFormats = new List<(List<byte> magic, string extension)>()
        {
            (jpg, "jpg"),
            (bmp, "bmp"),
            (gif, "gif"),
            (png, "png"),
            (svg_small, "svg"),
            (svg_capital, "svg"),
            (intel_tiff,"tif"),
            (motorola_tiff, "tif"),
            (svg_xml_small, "svg"),
            (svg_xml_capital, "svg")
        };

        public static bool ValidaImagem(byte[] array)
        {
            // check for simple formats first
            foreach (var imageFormat in imageFormats)
            {
                if (array.EhImagem(imageFormat.magic))
                {
                    if (imageFormat.magic != svg_xml_small && imageFormat.magic != svg_xml_capital)
                        return true;

                    // special handling for SVGs starting with XML tag
                    int readCount = imageFormat.magic.Count; // skip XML tag
                    int maxReadCount = 1024;

                    do
                    {
                        if (array.EhImagem(svg_small, readCount) || array.EhImagem(svg_capital, readCount))
                        {
                            return true;
                        }
                        readCount++;
                    }
                    while (readCount < maxReadCount && readCount < array.Length - 1);

                    return false;
                }
            }
            return false;
        }

        private static bool EhImagem(this byte[] array, List<byte> comparer, int offset = 0)
        {
            int arrayIndex = offset;
            foreach (byte c in comparer)
            {
                if (arrayIndex > array.Length - 1 || array[arrayIndex] != c)
                    return false;
                ++arrayIndex;
            }
            return true;
        }

        public static byte[] ConverteStreamParaByteArray(Stream stream)
        {
            byte[] byteArray = new byte[16 * 1024];
            using (MemoryStream mStream = new MemoryStream())
            {
                int bit;
                while ((bit = stream.Read(byteArray, 0, byteArray.Length)) > 0)
                {
                    mStream.Write(byteArray, 0, bit);
                }
                return mStream.ToArray();
            }
        }

        public static bool SalvaArquivoImagem(byte[] array, string caminhoArquivoImagem)
        {
            try
            {
                using MemoryStream ms = new(array);
                FileStream file = new(caminhoArquivoImagem, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
                file.Close();
                ms.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool ValidaImagemPorContentType(string contentType)
        {
            var imagemValida = contentType.ToLower().Contains("png") ||
                contentType.ToLower().Contains("jpg") ||
                contentType.ToLower().Contains("jpeg");

            return imagemValida;
        }
    }
}