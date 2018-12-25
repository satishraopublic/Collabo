using System;

namespace Collabo.Common.DTOs
{
public class UserDTO{
    public UserDTO(User newUser)
    {
        ID = newUser.ID;
        FirstName = newUser.FirstName;
        LastName = newUser.LastName;
        UserName = newUser.UserName;
        EmailId = newUser.EmailId;        
        CreatedOn = newUser.CreatedOn;
        ModifiedOn = newUser.ModifiedOn;
    }

    public Guid ID {get; set;}
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string EmailId { get; set; }
    
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }

}
}