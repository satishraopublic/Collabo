using System;

namespace Collabo.Common
{
public class User{
    public Guid ID     { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public string EmailId { get; set; }
    
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }

    public string SecretQuestion { get; set; }
    public string SecretAnswer { get; set; }
}
}