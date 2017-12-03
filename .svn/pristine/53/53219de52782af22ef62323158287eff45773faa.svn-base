using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using CoreWebApi.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreWebApi.Controllers
{
    
    public class ClientController : Controller
    {
        // GET: /<controller>/
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataReader reader = null;
        private List<Client> clients = new List<Client>();
        public void Connect()
        {
            conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FreeClub;Integrated Security=True;Pooling=False");

            conn.Open();
            cmd = new SqlCommand("select * from Client");
            cmd.Connection = conn;
            reader = cmd.ExecuteReader();
            List<Client> cl = new List<Client>();
            while (reader.Read())
            {
                Client c = new Client
                {

                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    password = reader.GetString(reader.GetOrdinal("password"))
                };
                cl.Add(c);
            }
            clients = cl;
        }
        [HttpGet]
        [Route("api/[controller]")]
        public IEnumerable<Client> Get()
        {
            Connect();
            return clients;
        }
        [HttpGet("{Email,Password}")]
        [Route("api/verif")]
        public Boolean VerifAuth(string Email,string Password)
        {
            int i = 0;
            Client cl = new Client();
            cl.Email = Email;
            cl.password = Password;
            Connect();
            foreach (Client cli in clients)
            {
                if (cli.Email == cl.Email && cli.password == cl.password)
                {
                    i++;
                    return true;
                }
            }
            return false;
        }
    }
}
