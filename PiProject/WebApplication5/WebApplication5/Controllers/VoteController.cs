using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WebApplication5.Model;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication5.Controllers
{
    [Produces("application/json")]
    
    public class VoteController : Controller
    {
        private List<VoteEvent> events = new List<VoteEvent>();
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataReader reader = null;
        public void Connect()
        {
            conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FreeClub;Integrated Security=True;Pooling=False");

            conn.Open();
            cmd = new SqlCommand("select * from part");
            cmd.Connection = conn;
            reader = cmd.ExecuteReader();
            List<VoteEvent> ev = new List<VoteEvent>();
            while (reader.Read())
            {
                VoteEvent e = new VoteEvent
                {

                    Id = reader.GetInt32(reader.GetOrdinal("Id_event")),
                    Email = reader.GetString(reader.GetOrdinal("add_mail_client")),
                    Choix = reader.GetInt32(reader.GetOrdinal("choix")),
                };
                ev.Add(e);
            }
            events = ev;
            conn.Close();
        }
        public void ConnectUpdate(int id,string email,int choix)
        {
            conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FreeClub;Integrated Security=True;Pooling=False");
            conn.Open();
            cmd = new SqlCommand("UPDATE part SET choix="+choix+"WHERE (Id_event="+id+"AND add_mail_client='"+email+"');");
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void ConnectInsert(int id, string email, int choix)
        {
            conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FreeClub;Integrated Security=True;Pooling=False");
            conn.Open();
            cmd = new SqlCommand("INSERT INTO part VALUES(" + id + ",'" + email + "'," + choix + ");");
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public Boolean Test(List<VoteEvent> events, int id , string email)
        {
            foreach (VoteEvent ev in events)
            {
                if (ev.Id == id && ev.Email == email) return true;
            }
            return false;
            }
        [HttpGet("{id,email,choix}")]
        [Route("api/Vote")]
        [Authorize(Roles = "Admin,User,Mod")]
        public Boolean Get(int id, string email, int choix)
        {
            Connect();
            if (Test(events, id, email))
                ConnectUpdate(id, email, choix);
            else ConnectInsert(id, email, choix);
            return true;
        }
        [HttpGet("{id}")]
        [Route("api/VoteCount")]
        [Authorize(Roles = "Admin,User,Mod")]
        public int GetNumber(int id)
        {
            Connect();
            List<VoteEvent> myVote = new List<VoteEvent>();
            foreach(VoteEvent ve in events)
            {
                if (ve.Id == id && ve.Choix==1) myVote.Add(ve);
            }
            int count = myVote.Count;
            return count;
        }
    }
}