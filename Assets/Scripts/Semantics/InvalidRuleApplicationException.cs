using System;

public class InvalidRuleApplicationException : Exception {
    public InvalidRuleApplicationException() {}
    public InvalidRuleApplicationException(string message) : base(String.Format("Cannot apply rule {0} to this proposition", message)) {}
}