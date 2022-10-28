[System.Serializable]
public class ContractException : System.Exception
{
    public ContractException() { }
    public ContractException(string message) : base(message) { }
    public ContractException(string message, System.Exception inner) : base(message, inner) { }
    protected ContractException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}