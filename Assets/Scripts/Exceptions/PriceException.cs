using System;

public class PriceException : Exception
{
    public PriceException(string message = "{ui_error_price}") : base(message)
    {
    }
}
