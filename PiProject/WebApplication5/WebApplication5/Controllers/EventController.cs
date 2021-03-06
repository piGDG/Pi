using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WebApplication5.Model;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using System.Drawing;

namespace WebApplication5.Controllers
{

    public class EventsController : Controller
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataReader reader = null;
        public static byte[] ToByteArray(string path)
        {
            byte[] buffer = System.IO.File.ReadAllBytes(path);
            return buffer;
        }
        private static byte[] ImageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }


        private List<byte[]> photos = new List<byte[]>
        {

    };
        private List<Event> events = new List<Event>();
        private List<Project> projects = new List<Project>();
        private List<Images> imagees = new List<Images>();
        /*{
            new Event {Id = 1,Name = "soir�e ramadhanesque",Date = "10/06/2017",Description="Ramadan est l� ! C�est parti pour les soir�es entre amis ou familles ! Qui n'aime pas passer une soir�e ramadanesque � papoter, rigoler autour d'un caf� turc, un bon th� au pignon,... ",Lien="www.soir�eRamadanesque.freedomofdev.tn",Vid=ToByteArray(@"Videos\test.mp4")},
            new Event {Id = 2,Name = "randonn�e",Date = "22/07/2017",Description="La randonn�e p�destre est une activit� de plein air qui s'effectue � pied en suivant un itin�raire",Lien="www.randonn�e.freedomofdev.tn",Ima = photos},
           new Event { Id = 3,Name = "festival",  Date = "19/08/2017",Description="Un festival, organis�e � �poque fixe et r�currente autour d'une activit� li�e au spectacle, aux arts, aux loisirs, etc.",Lien="www.festival.freedomofdev.tn",Ima= photos }
     };*/
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
        public void ConnectProject()
        {
            conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FreeClub;Integrated Security=True;Pooling=False");

            conn.Open();
            cmd = new SqlCommand("select * from project");
            cmd.Connection = conn;
            reader = cmd.ExecuteReader();
            List<Project> ev = new List<Project>();
            while (reader.Read())
            {
                Project e = new Project
                {

                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    NameEquipe = reader.GetString(reader.GetOrdinal("name_eq")),
                    NameProject = reader.GetString(reader.GetOrdinal("name_project")),
                };
                ev.Add(e);
            }
            projects = ev;

        }
        public void ConnectImage()
        {
            conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FreeClub;Integrated Security=True;Pooling=False");

            conn.Open();
            cmd = new SqlCommand("select * from imageTab");
            cmd.Connection = conn;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                Images e = new Images
                {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                image = (Byte[])reader.GetSqlBinary(reader.GetOrdinal("image"))

            };
                imagees.Add(e);
            }
        }
        public void ImageRead()
        {
            foreach (Event e in events)
            {
                e.Ima = new List<byte[]>();
                foreach (Images im in imagees)
                {
                    if (e.Id == im.Id) e.Ima.Add(im.image);
                }
            }
        }
        [HttpGet]
        [Route("api/allevents")]
       [Authorize(Roles = "Admin,User,Mod")]
        public IEnumerable<Event> GetAll()
        {
            Connect();
            ConnectImage();
            ImageRead();
            return events;
        }
        [HttpGet]
        [Route("api/projects")]
        [Authorize(Roles = "Admin,User,Mod")]
        public IEnumerable<Project> GetProject()
        {
            ConnectProject();
            return projects;
        }

        // GET: api/values
        [HttpGet]
        [Route("api/events")]
        [Authorize(Roles = "Admin,User,Mod")]
        public IEnumerable<Event> Get()
        {
            Connect();
            ConnectImage();
            ImageRead();
            List<Event> aVenir = new List<Event>();
            DateTime date = DateTime.Today;
            foreach (Event ev in events)
            {
                if (date <= ev.Date) aVenir.Add(ev);
            }
            
            return aVenir;
        }
        [HttpGet]
        [Route("api/eventsmois")]
        [Authorize(Roles = "Admin,User,Mod")]
        public IEnumerable<Event> GetEventMois()
        {
            Connect();
            ConnectImage();
            ImageRead();
            List<Event> mois = new List<Event>();
            DateTime date = DateTime.Today;
            foreach (Event ev in events)
            {
                if ((date.Month == ev.Date.Month) && (date.Year == ev.Date.Year)) mois.Add(ev);
            }
            return mois;
        }


        [Route("api/eventsTmois")]
        [Authorize(Roles = "Admin,User,Mod")]
        public IEnumerable<Event> GetEventTMois()
        {

            Connect();
            ConnectImage();
            ImageRead();
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
        [Authorize(Roles = "Admin,User,Mod")]
        public IEnumerable<Event> GetEvenYear()
        {

            Connect();
            ConnectImage();
            ImageRead();
            List<Event> mois = new List<Event>();
            DateTime date = DateTime.Today;
            foreach (Event ev in events)
            {
                if (date.Year == ev.Date.Year) mois.Add(ev);
            }
            return mois;

        }
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
