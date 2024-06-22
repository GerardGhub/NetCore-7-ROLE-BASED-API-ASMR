using LearnAPI.Repos;
using LearnAPI.Repos.Models;
using LearnAPI.Service;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace LearnAPI.Container
{
    // Implementation of the refresh token handler interface
    public class RefreshHandler : IRefreshHandler
    {
        // Constructor to initialize the context
        private readonly LearndataContext context;
        public RefreshHandler(LearndataContext context) { 
            this.context = context;
        }

        // Method to generate a new refresh token for a given username
        public async Task<string> GenerateToken(string username)
        {
            // Create a byte array to hold the random number
            var randomnumber = new byte[32];

            // Use a random number generator to fill the byte array with random numbers
            using (var randomnumbergenerator= RandomNumberGenerator.Create())
            {
                randomnumbergenerator.GetBytes(randomnumber);
                // Convert the byte array to a base64 string to use as the refresh token
                string refreshtoken =Convert.ToBase64String(randomnumber);

                // Check if a refresh token already exists for the given username
                var Existtoken = this.context.TblRefreshtokens.FirstOrDefaultAsync(item=>item.Userid==username).Result;
                if (Existtoken != null)
                {
                    // If a token already exists, update it with the new refresh token
                    Existtoken.Refreshtoken = refreshtoken;
                }
                else
                {
                    // If no token exists, create a new refresh token entry in the database
                    await this.context.TblRefreshtokens.AddAsync(new TblRefreshtoken
                    {
                       Userid=username,
                       Tokenid= new Random().Next().ToString(),
                       Refreshtoken=refreshtoken
                   });
                }
                // Save the changes to the database
                await this.context.SaveChangesAsync();

                // Return the new refresh token
                return refreshtoken;

            }
        }
    }
}
