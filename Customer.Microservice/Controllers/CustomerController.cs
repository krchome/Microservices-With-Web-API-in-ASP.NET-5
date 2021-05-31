using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customer.Microservice.Models;
using System.Data;
using System.Data.SqlClient;
using Dapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Customer.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly string connectionString = "Data Source=LAPTOP-LHM7GE2K\\SQLEXPRESS;Initial Catalog=CustomerDb;Integrated Security=True";

        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<IEnumerable<Models.Customer>> GetAllCustomers()
        {
            IEnumerable<Models.Customer> customers; 
           
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var sqlQuery = "Select * From Customer";
                customers = await connection.QueryAsync<Models.Customer>(sqlQuery);
            }
            return customers; 

        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<Models.Customer> GetCustomerById(int id)
        {
            Models.Customer customer = new Models.Customer();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var sqlQuery = "Select * From Customer Where Id = @Id";
                customer = await connection.QuerySingleAsync<Models.Customer>(sqlQuery, new {Id = id });
            }

            return customer;
        }

        // POST api/<CustomerController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Models.Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var sqlQuery = "Insert Into Customer (Name, Address, Telephone, Email) Values(@Name, @Address, @Telephone, @Email)";
                await connection.ExecuteAsync(sqlQuery, customer);
            }
            return Ok();

        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Models.Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var sqlQuery = "UPDATE Customer SET Name = @Name, Address = @Address, Email = @Email WHERE Id = @Id";
                await connection.ExecuteAsync(sqlQuery, customer);
            }
            return Ok();
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var sqlQuery = "Delete Customer Where Id = @Id";
                await connection.ExecuteAsync(sqlQuery, new { Id = id });
            }
            return Ok();
        }
    }
}
