using LearnAPI.Helper;
using LearnAPI.Modal;

namespace LearnAPI.Service
{
    // Interface representing operations related to customers
    public interface ICustomerService
    {
        // Retrieves all customers asynchronously
        Task<List<Customermodal>> Getall();

        // Retrieves a customer by their unique code asynchronously
        Task<Customermodal> Getbycode(string code);
        // Removes a customer by their code asynchronously
        Task<APIResponse> Remove(string code);

        // Creates a new customer using provided data asynchronously
        Task<APIResponse> Create(Customermodal data);
        // Updates an existing customer identified by their code with new data asynchronously
        Task<APIResponse> Update(Customermodal data,string code);
    }
}
