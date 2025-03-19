namespace Tutorial_03.exceptions;

public class OverloadException : Exception
{
    public OverloadException() : base("Load exceeds maximum capacity.") { }
}