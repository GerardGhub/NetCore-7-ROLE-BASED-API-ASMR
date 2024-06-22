using LearnAPI.Repos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace LearnAPI.Helper
{
    // Custom authentication handler for basic authentication
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly LearndataContext context;

        // Constructor to initialize the handler with necessary dependencies
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            LearndataContext context) : base(options, logger, encoder, clock)
        {
            this.context = context;
        }

        // Method to handle the authentication process
        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Check if the request contains an Authorization header
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("No header found"); // Fail if no header is found
            }

            // Parse the Authorization header
            var headervalue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            if (headervalue != null)
            {
                // Decode the base64 encoded credentials
                var bytes = Convert.FromBase64String(headervalue.Parameter);
                string credentials=Encoding.UTF8.GetString(bytes);
                string[] array = credentials.Split(":");
                string username = array[0];
                string password = array[1];


                // Verify the user credentials from the database
                var user =await this.context.TblUsers.FirstOrDefaultAsync(item => item.Username == username && item.Password == password);
                if (user != null)
                {
                    // Create claims and identity for the authenticated user
                    var claim = new[] { new Claim(ClaimTypes.Name, user.Username) };
                    var identity = new ClaimsIdentity(claim, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);
                    return AuthenticateResult.Success(ticket); // Authentication successful
                }
                else
                {
                    return AuthenticateResult.Fail("UnAutorized"); // Fail if credentials are invalid
                }
            }
            else
            {
                return AuthenticateResult.Fail("Empty header"); // Fail if the header is empty
            }
        }
    }
}
