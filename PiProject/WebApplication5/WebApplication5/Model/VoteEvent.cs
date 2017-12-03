using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Model
{
    public class VoteEvent
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int Choix { get; set; }
    }
}
