using System;

namespace Collabo.Common.DTOs
{
public class UserViewDTO{
    public UserViewDTO(User newUser)
    {
        FirstName = newUser.FirstName;
        LastName = newUser.LastName;
        UserName = newUser.UserName;
        EmailId = newUser.EmailId;        
        CreatedOn = newUser.CreatedOn;
        ModifiedOn = newUser.ModifiedOn;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string EmailId { get; set; }
    
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }

}
}