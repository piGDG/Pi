using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Model
{
    public class Event
    {
        public DateTime Date { get; internal set; }
        public int Id { get; internal set; }
        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public string Lien { get; internal set; }
        public List<byte[]> Ima { get; internal set; }
    }
}
