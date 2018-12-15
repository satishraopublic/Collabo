using System;

namespace Collabo.Common.DTOs
{
public class MessageCreateDTO{
    public string MessageText { get; set; }
    public Guid SentTo { get; set; }
}
}