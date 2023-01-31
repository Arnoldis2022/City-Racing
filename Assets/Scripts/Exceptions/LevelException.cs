using System;

public class LevelException : Exception
{
    public LevelException(string message = "{ui_error_level}") : base(message)
    {
    }
}
