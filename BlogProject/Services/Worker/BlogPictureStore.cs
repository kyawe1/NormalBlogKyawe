using BlogProject.Services.Interfaces;

namespace BlogProject.Services.Worker
{
    public class BlogPictureStore : IPictureStore
    {
        public byte[]? store(IFormFile file)
        {
            byte[]? data;

            using (MemoryStream stream = new MemoryStream())
            {
                file.CopyTo(stream);
                data = stream.ToArray();
            }
            return data;


        }
    }
}
