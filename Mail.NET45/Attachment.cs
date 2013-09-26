using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;

namespace SendGrid
{
    public abstract class AttachmentBase
    {
        public string ContentType { get; set; }
    }
    
    public class StreamAttachment : AttachmentBase
    {
        public MemoryStream Stream { get; set; }
        public string Name { get; set; }

        public StreamAttachment()
        {
            ContentType = MediaTypeNames.Application.Octet;
        }
    }

    public class FileAttachment : AttachmentBase
    {
        public string FilePath { get; set; }

        public FileAttachment()
        {
            ContentType = MediaTypeNames.Application.Octet;
        }
    }
}
