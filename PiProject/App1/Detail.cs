using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using static Android.Provider.Contacts;
using System.Net.Http;
using Newtonsoft.Json;
using FreeClub;
using System.DrawingCore;
using System.IO;
using Android.Graphics.Drawables;

namespace App1
{
    [Activity(Label = "Detail")]
    public class Detail : Activity
    {
             protected async override void OnCreate(Bundle savedInstanceState)
        {
            string Email = Intent.GetStringExtra("Email");
            string password = Intent.GetStringExtra("Password");
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Detail);
            string name = Intent.GetStringExtra("name");
            string date = Intent.GetStringExtra("date");
            string description = Intent.GetStringExtra("description");
            string lien = Intent.GetStringExtra("Lien");
            byte [] Ima = Intent.GetByteArrayExtra("Image");
            TextView nameTxt = FindViewById<TextView>(Resource.Id.name);
            TextView dateTxt = FindViewById<TextView>(Resource.Id.date);
            TextView descriptionTxt = FindViewById<TextView>(Resource.Id.description);
            TextView lienTxt = FindViewById<TextView>(Resource.Id.lien);
            ImageButton image = FindViewById<ImageButton>(Resource.Id.IB1);
            ImageButton video = FindViewById<ImageButton>(Resource.Id.IB2);

            nameTxt.Text = name;
            dateTxt.Text = date;
            descriptionTxt.Text = description;
            lienTxt.Text = lien;
            
            image.Click += delegate
            {
                Intent nextAct = new Intent(this, typeof(Images));
                nextAct.PutExtra("nom", name);
                nextAct.PutExtra("Email", Email);
                nextAct.PutExtra("Password", password);
                StartActivity(nextAct);
            };
            video.Click += delegate
            {
                /*Intent nextAct = new Intent(this, typeof(Videos));
                StartActivity(nextAct);*/
                Toast.MakeText(this, "en cours de construction", ToastLength.Long).Show();
            };
           
        }
    }
}