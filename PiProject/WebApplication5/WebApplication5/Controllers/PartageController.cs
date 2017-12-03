using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using WebApplication5.Model;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebApplication5.Controllers
{
    [Produces("application/json")]
    public class PartageController : Controller
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataReader reader = null;
        private static byte[] ImageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
        [HttpPost]
        [Route("api/Partageimage")]
        [Authorize(Roles = "Admin,User,Mod")]
        public void PartageImage(int id, String ima)
        {
            SqlConnection CN = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FreeClub;Integrated Security=True;Pooling=False");
            string qry = "insert into imageTab (Id, image) values("+id + ",@image);";
            cmd = new SqlCommand(qry, CN);
            byte[] image = Convert.FromBase64String(ima);
            cmd.Parameters.AddWithValue("@image",image);
            CN.Open();
           cmd.ExecuteNonQuery();
            CN.Close();
        }

        [HttpPost]
        [Route("api/Partagevideo")]
        [Authorize(Roles = "Admin,User,Mod")]
        public void PartageVideo(int id, String ima)
        {
            SqlConnection CN = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FreeClub;Integrated Security=True;Pooling=False");
            string qry = "insert into videoTab (Id, image) values(" + id + ",@image);";
            cmd = new SqlCommand(qry, CN);
            byte[] image = Convert.FromBase64String(ima);
            cmd.Parameters.AddWithValue("@image", image);
            CN.Open();
            cmd.ExecuteNonQuery();
            CN.Close();
        }
    }
}