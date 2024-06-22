using LearnAPI.Repos;
using LearnAPI.Repos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LearnAPI.Controllers
{
    // Route for the API controller, setting the base path as "api/associate"
    [Route("api/[controller]")]
    [ApiController]
    public class AssociateController : ControllerBase
    {
        // Database context for accessing the data
        private readonly LearndataContext learndata;

        // Constructor to initialize the database context
        public AssociateController(LearndataContext context) { 
            this.learndata = context;
        }

        // GET: api/associate/getall
        // Method to get all customers using a stored procedure
        [HttpGet("getall")]
        public async Task<ActionResult> Getall()
        {
            string sqlquery = "exec sp_getcustomer";
            var data = await this.learndata.TblCustomers.FromSqlRaw(sqlquery).ToListAsync();
            if(data==null)
            {
                return NotFound();
            }
            return Ok(data);

        }

        // GET: api/associate/Getallcustom
        // Method to get all custom customers using a stored procedure
        [HttpGet("Getallcustom")]
        public async Task<ActionResult> Getallcustom()
        {
            string sqlquery = "exec sp_getcustomer_custom";
            var data = await this.learndata.customerdetail.FromSqlRaw(sqlquery).ToListAsync();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);

        }


        // GET: api/associate/Getallcustomq
        // Method to get all custom customers with an additional status field
        [HttpGet("Getallcustomq")]
        public async Task<ActionResult> Getallcustomq()
        {
            string sqlquery = "Select *,'Active' as Statusname from tbl_customer";
            var data = await this.learndata.customerdetail.FromSqlRaw(sqlquery).ToListAsync();
            if (data == null)
            {
                return NotFound();  
            }
            return Ok(data);

        }



        // GET: api/associate/getbycode?code=value
        // Method to get a customer by code, includes a status field
        [HttpGet("getbycode")]
        public async Task<ActionResult> getbycode(string code)
        {
            string sqlquery = "Select *,'Active' as Statusname from tbl_customer where code=@code";
            SqlParameter parameter = new SqlParameter("@code", code);
            var data = await this.learndata.customerdetail.FromSqlRaw(sqlquery, parameter).FirstOrDefaultAsync();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);

        }



        // POST: api/associate/create
        // Method to create a new customer
        [HttpPost("create")]
        public async Task<ActionResult> create(TblCustomer tblCustomer)
        {
            string sqlquery = "Insert Into tbl_customer values(@code,@name,@email,@phone,@creditlimit,@active,@taxcode)";
            SqlParameter[] parameter =
            {
                new SqlParameter("@code",tblCustomer.Code),
                new SqlParameter("@name",tblCustomer.Name),
                new SqlParameter("@email",tblCustomer.Email),
                new SqlParameter("@phone",tblCustomer.Phone),
                new SqlParameter("@creditlimit",tblCustomer.Creditlimit),
                new SqlParameter("@active",tblCustomer.IsActive),
                new SqlParameter("@taxcode",tblCustomer.Taxcode)
            };
            var data = await this.learndata.Database.ExecuteSqlRawAsync(sqlquery, parameter);
            return Ok(data);

        }


        // PUT: api/associate/update
        // Method to update an existing customer using a stored procedure
        [HttpPut("Update")]
        public async Task<ActionResult> Update(string code,TblCustomer tblCustomer)
        {
            string sqlquery = "exec sp_createcustomer @code,@name,@email,@phone,@creditlimit,@active,@taxcode,@type";
            SqlParameter[] parameter =
            {
                new SqlParameter("@code",code),
                new SqlParameter("@name",tblCustomer.Name),
                new SqlParameter("@email",tblCustomer.Email),
                new SqlParameter("@phone",tblCustomer.Phone),
                new SqlParameter("@creditlimit",tblCustomer.Creditlimit),
                new SqlParameter("@active",tblCustomer.IsActive),
                new SqlParameter("@taxcode",tblCustomer.Taxcode),
                new SqlParameter("@type","update")
            };
            var data = await this.learndata.Database.ExecuteSqlRawAsync(sqlquery, parameter);
            return Ok(data);

        }

        // DELETE: api/associate/delete
        // Method to delete a customer by code using a stored procedure

        [HttpDelete("delete")]
        public async Task<ActionResult> delete(string code)
        {
            string sqlquery = "exec sp_deletecustomer @code";
            SqlParameter[] parameter =
            {
                new SqlParameter("@code",code)
            };
            var data = await this.learndata.Database.ExecuteSqlRawAsync(sqlquery, parameter);
            return Ok(data);

        }


    }
}
