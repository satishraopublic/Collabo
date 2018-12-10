using System;

public class Session{
    public Guid ID     { get; set; }
    public Guid User     { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public DateTime LoggedOn { get; set; }
    public DateTime LoggedOffOn { get; set; }

}