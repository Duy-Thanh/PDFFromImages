using System.IO;

namespace PDFFromImages
{
    internal class ImageItem
    {
        public string FilePath { get; set; }
        public string FileName => Path.GetFileName(FilePath);

        public override string ToString() => FileName;
    }
}