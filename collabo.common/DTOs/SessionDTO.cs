using System;
using Collabo.Common;

namespace Collabo.API.DTOs{
    public class SessionDTO{
        public Guid ID     { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public DateTime LoggedOn { get; set; }

    public SessionDTO()
    {
        
    }
    public SessionDTO(Session session)
    {
        if(session == null)throw new Exception("Session not available.");
        ID = session.ID;
        FirstName = session.FirstName;
        LastName = session.LastName;
        UserName = session.UserName;
        LoggedOn = session.LoggedOn;
    }
}
}