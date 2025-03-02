namespace ProjectManagement.services
{
    public static class ErrorCodes
    {
        public static readonly Dictionary<int, string> Messages = new()
        {
            { 400, "bad_request" },
            { 401, "unauthorized" },
            { 402, "payment_required" },
            { 403, "forbidden" },
            { 404, "not_found" },
            { 405, "method_not_allowed" },
            { 500, "internal_server_error" },
            { 600, "password_incorrect" },
            { 601, "user_not_found" },
            { 602, "account_locked" },
            { 603, "token_expired" },
            { 604, "invalid_captcha" },
            { 605, "email_not_verified" },
            { 606, "phone_not_verified" },
            { 607, "session_expired" },
            { 608, "invalid_two_factor_code" },
            { 609, "account_suspended" },
            { 700, "invalid_input_data" },
            { 701, "missing_required_fields" },
            { 702, "duplicate_entry" },
            { 703, "invalid_file_format" },
            { 704, "file_too_large" },
            { 705, "unsupported_file_type" },
            { 706, "data_conflict" },
            { 707, "invalid_date_format" },
            { 708, "username_taken" },
            { 709, "email_taken" },
            { 710, "phone_number_taken" }
        };
    }
}
