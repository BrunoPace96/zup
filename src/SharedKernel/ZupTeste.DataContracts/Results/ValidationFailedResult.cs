namespace ZupTeste.DataContracts.Results
{
    public record ValidationFailedResult(
        string Message, 
        IList<ValidationErrorResult> Errors
    );
}