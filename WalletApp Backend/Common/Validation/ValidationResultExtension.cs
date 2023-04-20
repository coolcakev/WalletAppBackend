using FluentValidation.Results;

namespace WalletApp_Backend.Common.Validation
{
    public static class ValidationResultExtension
    {
        public static string[] ErrorMessages(this ValidationResult validationResult)
        {
            return validationResult.Errors.Select(x => x.ErrorMessage).ToArray();
        }
    }
}