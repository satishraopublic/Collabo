using System;
namespace Collabo.Common
{
public class Conversation{
    
    public Guid ID {get; set;}
    public Guid InitiatedBy { get; set; }
    public DateTime InitiatedOn {get; set; }
    public Guid Channel { get; set; }
    public DateTime TerminatedOn {get; set;}
}
}