
using System.Collections.Generic;

namespace CoreWebApi.Model
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime Date { get; set; }
        public string Description { get; set; }
        public string Lien { get; set; }
        public List<byte []> Ima { get; set; }
        public byte[] Vid { get; set; }
    }
}