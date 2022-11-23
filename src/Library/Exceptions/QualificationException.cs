[System.Serializable]
public class QualificationException : System.Exception
{
    public QualificationException() { }
    public QualificationException(string message) : base(message) { }
    public QualificationException(string message, System.Exception inner) : base(message, inner) { }
    protected QualificationException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}