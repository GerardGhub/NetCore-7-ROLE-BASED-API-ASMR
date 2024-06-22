using AutoMapper;
using LearnAPI.Helper;
using LearnAPI.Modal;
using LearnAPI.Repos;
using LearnAPI.Repos.Models;
using LearnAPI.Service;
using Microsoft.EntityFrameworkCore;

namespace LearnAPI.Container
{
    // Implementation of the customer service interface
    public class CustomerService : ICustomerService
    {
        private readonly LearndataContext context;
        private readonly IMapper mapper;
        private readonly ILogger<CustomerService> logger;

        // Constructor to initialize the service with necessary dependencies
        public CustomerService(LearndataContext context,IMapper mapper,ILogger<CustomerService> logger) { 
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }


        // Method to create a new customer
        public async Task<APIResponse> Create(Customermodal data)
        {
            APIResponse response = new APIResponse();
            try
            {
                this.logger.LogInformation("Create Begins");
                // Map the Customermodal to TblCustomer
                TblCustomer _customer = this.mapper.Map<Customermodal, TblCustomer>(data);
                // Add the customer to the database
                await this.context.TblCustomers.AddAsync(_customer);
                await this.context.SaveChangesAsync(); // Created
                response.ResponseCode = 201;
                response.Result = "pass";
            }
            catch(Exception ex)
            {
                response.ResponseCode = 400;
                response.Message = ex.Message;
                this.logger.LogError(ex.Message,ex);
            }
            return response;
        }

        // Method to get all customers
        public async Task<List<Customermodal>> Getall()
        { 
            List<Customermodal> _response=new List<Customermodal>();
            // Retrieve all customers from the database
            var _data = await this.context.TblCustomers.ToListAsync();
            if(_data != null )
            {
                // Map the list of TblCustomer to list of Customermodal
                _response = this.mapper.Map<List<TblCustomer>,List<Customermodal>>(_data);
            }
            return _response;
        }


        // Method to get a customer by code
        public async Task<Customermodal> Getbycode(string code)
        {
            Customermodal _response = new Customermodal();
            // Find the customer by code
            var _data = await this.context.TblCustomers.FindAsync(code);
            if (_data != null)
            {
                // Map the TblCustomer to Customermodal
                _response = this.mapper.Map<TblCustomer, Customermodal>(_data);
            }
            return _response;
        }


        // Method to remove a customer by code
        public async Task<APIResponse> Remove(string code)
        {
            APIResponse response = new APIResponse();
            try
            {
                var _customer = await this.context.TblCustomers.FindAsync(code);
                if(_customer != null)
                {
                    // Remove the customer from the database

                    this.context.TblCustomers.Remove(_customer);
                    await this.context.SaveChangesAsync();
                    response.ResponseCode = 200; // OK
                    response.Result = "pass";
                }
                else
                {
                    response.ResponseCode = 404; // Not found
                    response.Message = "Data not found";
                }
               
            }
            catch (Exception ex)
            {
                response.ResponseCode = 400; // Bad request
                response.Message = ex.Message;
            }
            return response;
        }


        // Method to update a customer's details
        public async Task<APIResponse> Update(Customermodal data, string code)
        {
            APIResponse response = new APIResponse();
            try
            {
                var _customer = await this.context.TblCustomers.FindAsync(code);
                if (_customer != null)
                {
                    // Update the customer's details
                    _customer.Name = data.Name;
                    _customer.Email = data.Email;
                    _customer.Phone=data.Phone;
                    _customer.IsActive=data.IsActive;
                    _customer.Creditlimit = data.Creditlimit;
                    await this.context.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Result = "pass";
                }
                else
                {
                    response.ResponseCode = 404;
                    response.Message = "Data not found";
                }

            }
            catch (Exception ex)
            {
                response.ResponseCode = 400;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
