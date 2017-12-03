﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App1
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Lien { get; set; }
        public List<byte[]> Ima { get; set; }
        public byte[] Vid { get; set; }
        public override string ToString()
        {
            return Name + " " + Date;
        }
    }
}