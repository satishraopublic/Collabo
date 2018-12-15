using System;
using System.Collections.Generic;
using System.Linq;
using Collabo.Common;

namespace Collabo.API.Services{
public class LoginService : ILoginService
{
    List<Session> _loggedUsers=new List<Session>();
    public void AddUserContext(Session uinfo)
    {
        _loggedUsers.Add(uinfo);
    }

    public void RemoveUserContext(Session uinfo)
    {
        _loggedUsers.Remove(uinfo);
    }

    public Session ValidateUserContext(Guid token)
    {
        Session session = _loggedUsers.FirstOrDefault(c=> c.ID == token);
        if(session == null) throw new Exception("Access Denied.");
        return session;
    }
}
}