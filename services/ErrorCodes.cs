namespace ProjectManagement.Services
{
    public static class ErrorCodes
    {
        public static class Success
        {
            public const int AddSuccess = 201;
            public const int UpdateSuccess = 200;
        }
        public static class General
        {
            public const int BadRequest = 400;
            public const int Unauthorized = 401;
            public const int PaymentRequired = 402;
            public const int Forbidden = 403;
            public const int NotFound = 404;
            public const int MethodNotAllowed = 405;
            public const int InvalidToken = 498;
            public const int InternalServerError = 500;
        }

        public static class Authentication
        {
            public const int PasswordIncorrect = 600;
            public const int UserNotFound = 601;
            public const int AccountLocked = 602;
            public const int TokenExpired = 603;
            public const int InvalidCaptcha = 604;
            public const int EmailNotVerified = 605;
            public const int PhoneNotVerified = 606;
            public const int SessionExpired = 607;
            public const int InvalidTwoFactorCode = 608;
            public const int AccountSuspended = 609;
        }

        public static class Validation
        {
            public const int InvalidInputData = 700;
            public const int MissingRequiredFields = 701;
            public const int DuplicateEntry = 702;
            public const int InvalidFileFormat = 703;
            public const int FileTooLarge = 704;
            public const int UnsupportedFileType = 705;
            public const int DataConflict = 706;
            public const int InvalidDateFormat = 707;
            public const int UsernameTaken = 708;
            public const int EmailTaken = 709;
            public const int PhoneNumberTaken = 710;
        }

        public static class Resource
        {
            public const int EmailDoesNotExist = 801;
            public const int ProjectNotFound = 802;
            public const int TokenNotFound = 803;
            public const int EmailAlreadyExists = 804;

        }

        public static readonly Dictionary<int, string> Messages = new()
        {
            // Add Success
            { Success.AddSuccess, "add_success"},
            {Success.UpdateSuccess, "update_success"},
            // General Errors
            { General.BadRequest, "bad_request" },
            { General.Unauthorized, "unauthorized" },
            { General.PaymentRequired, "payment_required" },
            { General.Forbidden, "forbidden" },
            { General.NotFound, "not_found" },
            { General.MethodNotAllowed, "method_not_allowed" },
            { General.InternalServerError, "internal_server_error" },
            {General.InvalidToken,"invalid_token"},

            // Authentication Errors
            { Authentication.PasswordIncorrect, "password_incorrect" },
            { Authentication.UserNotFound, "user_not_found" },
            { Authentication.AccountLocked, "account_locked" },
            { Authentication.TokenExpired, "token_expired" },
            { Authentication.InvalidCaptcha, "invalid_captcha" },
            { Authentication.EmailNotVerified, "email_not_verified" },
            { Authentication.PhoneNotVerified, "phone_not_verified" },
            { Authentication.SessionExpired, "session_expired" },
            { Authentication.InvalidTwoFactorCode, "invalid_two_factor_code" },
            { Authentication.AccountSuspended, "account_suspended" },

            // Validation Errors
            { Validation.InvalidInputData, "invalid_input_data" },
            { Validation.MissingRequiredFields, "missing_required_fields" },
            { Validation.DuplicateEntry, "duplicate_entry" },
            { Validation.InvalidFileFormat, "invalid_file_format" },
            { Validation.FileTooLarge, "file_too_large" },
            { Validation.UnsupportedFileType, "unsupported_file_type" },
            { Validation.DataConflict, "data_conflict" },
            { Validation.InvalidDateFormat, "invalid_date_format" },
            { Validation.UsernameTaken, "username_taken" },
            { Validation.EmailTaken, "email_taken" },
            { Validation.PhoneNumberTaken, "phone_number_taken" },

            // Resource Errors
            { Resource.EmailDoesNotExist, "email_does_not_exist" },
            { Resource.ProjectNotFound, "project_not_found" },
            { Resource.TokenNotFound, "token_not_found" },
            {Resource.EmailAlreadyExists, "email_already_exists"}

        };
    }
}
