using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;

namespace SendGrid
{
    public abstract class AttachmentBase
    {
        public string ContentType { get; set; }
        public abstract Stream GetStream();
        public string Name { get; set; }
        public string ContentId { get; set; }
    }
    
    public class StreamAttachment : AttachmentBase
    {
        public Stream Stream { get; set; }

        public StreamAttachment(Stream stream, string name)
        {
            Name = name;
            Stream = stream;
            ContentType = MediaTypeNames.Application.Octet;
        }

        public override Stream GetStream()
        {
            return Stream;
        }
    }

    public class FileAttachment : AttachmentBase
    {
        public string FilePath { get; set; }

        public FileAttachment(string filePath)
        {
            FilePath = filePath;
            Name = Path.GetFileName(filePath);
            ContentType = MediaTypeNames.Application.Octet;
        }

        public override Stream GetStream()
        {
            return new FileStream(FilePath, FileMode.Open, FileAccess.Read);
        }
    }
}
