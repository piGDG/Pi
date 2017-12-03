using System.Collections.Generic;
using CoreWebApi.Model;
using Microsoft.AspNetCore.Mvc;
using System.DrawingCore;
using System.IO;
using System.Data.SqlClient;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using Newtonsoft.Json;

namespace CoreWebApi.Controllers
{
    
    public class EventsController : Controller
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataReader reader = null;
        private List<Client> clients = new List<Client>();
        Boolean verif;
        public static byte[] ToByteArray(string path)
        {
            byte[] buffer = System.IO.File.ReadAllBytes(path);
            return buffer;
        }
        private static byte[] imageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.DrawingCore.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
    

private static List<byte[]> photos = new List<byte[]>
        {

          {imageToByteArray(Image.FromFile(@"Images\panda.jpg")) },
          {imageToByteArray(Image.FromFile(@"Images\skate.jpg")) },
          {imageToByteArray(Image.FromFile(@"Images\lune.jpg")) },
          {imageToByteArray(Image.FromFile(@"Images\parachute.jpg"))}

    };
        private List<Event> events = new List<Event>();
        public void Connect()
        {
            conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FreeClub;Integrated Security=True;Pooling=False");

          conn.Open();
            cmd = new SqlCommand("select * from events");
            cmd.Connection = conn;
            reader = cmd.ExecuteReader();
            List<Event> ev = new List<Event>();
            while (reader.Read())
            {
                Event e = new Event
                {

                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Nom")),
                    Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                    Description = reader.GetString(reader.GetOrdinal("Description")),
                    Lien = reader.GetString(reader.GetOrdinal("Lien"))
                };
                ev.Add(e);
            }
            events = ev;
        }
        public void Connect2()
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
        public Boolean Verify(string Email, string Password)
        {
            int i = 0;
            Client cl = new Client();
            cl.Email = Email;
            cl.password = Password;
            Connect2();
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
        // GET: api/values
        [HttpGet("{Email,Password}")]
        [Route("api/events")]
        public IEnumerable<Event> Get(string Email, string Password)
        {
            if (Verify(Email, Password))
            {
                Connect();
                events[0].Ima = photos;
                events[1].Ima = photos;
                events[2].Ima = photos;
                events[3].Ima = photos;
                return events;
            }
            else return events;
        }
        
        [HttpGet("{Email,Password}")]
        [Route("api/eventsmois")]
        public IEnumerable<Event> GetEventMois(string Email, string Password)
        {
            if(Verify(Email,Password))
            {
                Connect();
                events[0].Ima = photos;
                events[1].Ima = photos;
                events[2].Ima = photos;
                events[3].Ima = photos;
                List<Event> mois = new List<Event>();
                DateTime date = DateTime.Today;
                foreach (Event ev in events)
                {
                    if ((date.Month == ev.Date.Month) && (date.Year == ev.Date.Year)) mois.Add(ev);
                }
                return mois;
            }
            else return null;
        }
     
    [Route("api/eventsTmois")]
public IEnumerable<Event> GetEventTMois()
{

    Connect();
    events[0].Ima = photos;
    events[1].Ima = photos;
    events[2].Ima = photos;
    events[3].Ima = photos;
    List<Event> mois = new List<Event>();
    DateTime date = DateTime.Today;
    foreach (Event ev in events)
    {
        if (((date.Month == ev.Date.Month) || (date.Month - 1 == ev.Date.Month) || (date.Month - 2 == ev.Date.Month)) && (date.Year == ev.Date.Year)) mois.Add(ev);
    }
    return mois;

}
[HttpGet]
        
        [Route("api/eventsyear")]

public IEnumerable<Event> GetEvenYear()
{

    Connect();
    events[0].Ima = photos;
    events[1].Ima = photos;
    events[2].Ima = photos;
    events[3].Ima = photos;
    List<Event> mois = new List<Event>();
    DateTime date = DateTime.Today;
    foreach (Event ev in events)
    {
        if (date.Year == ev.Date.Year) mois.Add(ev);
    }
    return mois;

}
/*[HttpGet]
public Event GetById(int id)
{
    Event Ev = null;
    for (int i = 0; i < 3; i++)
    {
        if (events[i].Id == id) Ev = events[i];
    }
    return Ev;
}*/
[HttpPost]
        public IActionResult Create([FromBody] Event item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            events.Add(item);
            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }
       
    }
}
