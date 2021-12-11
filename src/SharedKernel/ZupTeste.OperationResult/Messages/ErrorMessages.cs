namespace ZupTeste.OperationResult.Messages
{
    public static class ErrorMessages
    {
        public static string EmptyErrorOnFailureResult => "Errors cannot be empty on a failure result!";
        public static string EmptyValueOnFailureResult => "Value cannot be empty on a success result!";
        public static string ValueAccessOnFailureResult => "You cannot access value on a failure result!";
        public static string ErrorsAccessOnSuccessResult => "You cannot access Errors on a success result!";
    }
}