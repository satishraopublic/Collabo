using System;

namespace Collabo.Common
{
public class Session{
    public Guid ID     { get; set; }
    public Guid User     { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public DateTime LoggedOn { get; set; }
    public DateTime LoggedOffOn { get; set; }
    public Session()
    {
    
    }
    public Session(User user)
    {
        ID = Guid.NewGuid();
        FirstName = user.FirstName;
        LastName = user.LastName;
        UserName = user.UserName;
        LoggedOn = DateTime.UtcNow;
        User = user.ID;
    }
}
}