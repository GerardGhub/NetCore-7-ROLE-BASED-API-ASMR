namespace LearnAPI.Helper
{
    // This class represents a standardized response structure for the API.
    public class APIResponse
    {
        // Property to store the response code (e.g., 200 for success, 404 for not found).
        public int ResponseCode { get; set; }
        // Property to store the result of the API call (could be data or status).
        public string Result { get; set; }
        // Property to store a message, usually used for providing additional information or error details.
        public string Message { get; set; }
    }
}
