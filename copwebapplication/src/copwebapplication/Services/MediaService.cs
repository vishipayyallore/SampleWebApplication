using System.IO;

namespace copwebapplication.Services
{
    public class MediaService 
    {
        public MemoryStream GetImageStream(string handle)
        {
            MemoryStream mStream = new BlobService().GetImage(handle);
            mStream.Seek(0, SeekOrigin.Begin);
            return mStream;
        }
    }
}
