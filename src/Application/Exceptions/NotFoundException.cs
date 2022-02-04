using System;

namespace Bcan.Backend.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key) : 
            base($"{name} ({key}) is not found.")
        {
            
        }
    }
}