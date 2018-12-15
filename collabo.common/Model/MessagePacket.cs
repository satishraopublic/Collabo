using System;
using System.Collections.Generic;

namespace Collabo.Common
{
public class MessagePacket{
    public Guid ID { get; set; }
    public Message Message { get; set; }
    public List<Guid> SentTo { get; set; }
    public Guid Group { get; set; }

}
}