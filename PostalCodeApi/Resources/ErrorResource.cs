using System.Collections.Generic;

namespace PostalCodeApi.Resources
{
    public class ErrorResource
    {
        public int Status { get; private set; }
        
        public bool Success => false;
        public List<string> Messages { get; private set; }
        
        public ErrorResource(int status, List<string> messages)
        {
            Messages = messages ?? new List<string>();
            Status = status;
        }

        public ErrorResource(int status, string message)
        {
            Status = status;
            Messages = new List<string>();

            if(!string.IsNullOrWhiteSpace(message))
            {
                Messages.Add(message);
            }
        }
    }
}