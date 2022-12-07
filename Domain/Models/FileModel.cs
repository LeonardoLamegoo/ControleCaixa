namespace Domain.Models
{
    public class FileModel
    {
        public byte[] MediaFIle { get; private set; }
        public string FileName { get; private set; }
        public bool IsCreated { get; private set; }

        public FileModel(byte[] file, string fileName)
        {
            MediaFIle = file;
            FileName = fileName;
            IsCreated = true;
        }

        public FileModel(bool isCreated)
        {
            IsCreated = isCreated;
        }
    }
}
