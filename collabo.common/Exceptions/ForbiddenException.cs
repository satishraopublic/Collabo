using System;

public class ForbiddenException : CollaboExceptionBase
{
    public ForbiddenException(string message) : base(401, message)
    {

    }
}