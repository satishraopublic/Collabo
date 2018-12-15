using System;

namespace Collabo.Common
{
public class Message{
    public Guid ID     { get; set; }
    public string MessageText { get; set; }
    public DateTime SentDateTime { get; set; }
    public Session SentBy { get; set; }
    public Guid SentTo { get; set; }
}
}