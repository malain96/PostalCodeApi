using System;
using System.Collections.Generic;

namespace PostalCodeApi.Resources
{
    /// <summary>
    /// Error message returned by the API
    /// </summary>
    [Serializable]
    public class ErrorResource
    {
        public ErrorResource(int status, string message)
        {
            Status = status;
            Messages = new List<string>();

            if (!string.IsNullOrWhiteSpace(message)) Messages.Add(message);
        }
        public ErrorResource(int status, List<string> messages)
        {
            Messages = messages ?? new List<string>();
            Status = status;
        }

        public int Status { get; }

        public bool Success => false;
        public List<string> Messages { get; }
    }
}