using System;
using System.Collections.Generic;


namespace Collabo.API.DTOs{
public class CreateConversationDTO{
    public Guid Channel { get; set; }
    public List<Guid>  UserSet  { get; set; }
}
}