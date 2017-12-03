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
using System.Net.Http;
using Newtonsoft.Json;

namespace FreeClub
{
    [Activity(Label = "Detail de vote")]
    public class VoteDetail : Activity
    {
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            string Email = Intent.GetStringExtra("Email");
            string password = Intent.GetStringExtra("Password");
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.userRow);
            int id = Intent.GetIntExtra("id",0);
            string name = Intent.GetStringExtra("name");
            string date = Intent.GetStringExtra("date");
            string description = Intent.GetStringExtra("description");
            string lien = Intent.GetStringExtra("Lien");
            byte [] Ima = Intent.GetByteArrayExtra("Image");
            TextView nameTxt = FindViewById<TextView>(Resource.Id.name);
            TextView dateTxt = FindViewById<TextView>(Resource.Id.date);
            TextView descriptionTxt = FindViewById<TextView>(Resource.Id.description);
            TextView lienTxt = FindViewById<TextView>(Resource.Id.lien);
            TextView nbpart = FindViewById<TextView>(Resource.Id.nbPart);
            Button yesBut = FindViewById<Button>(Resource.Id.yesBut);
            Button NoBut = FindViewById<Button>(Resource.Id.NoBut);
            nameTxt.Text = name;
            dateTxt.Text = date;
            descriptionTxt.Text = description;
            lienTxt.Text = lien;
            HttpContent content = new FormUrlEncodedContent(
              new List<KeyValuePair<string, string>> {
                  new KeyValuePair<string, string>("Email", Email),
                  new KeyValuePair<string, string>("Password", password),
                  new KeyValuePair<string, string>("RememberMe", "true")
      });
            var client = new HttpClient();
            HttpResponseMessage resposne = await client.PostAsync(new Uri("http://192.168.0.78:63835/Account/Login/"), content);
            var jsons = await client.GetStringAsync("http://192.168.0.78:63835/api/voteCount/?id=" + id);
            var count = JsonConvert.DeserializeObject<string>(jsons);
            nbpart.Text = count;
            yesBut.Click += async delegate
            {
                var json = await client.GetStringAsync("http://192.168.0.78:63835/api/vote/?id=" + id+"&&email="+Email+"&&choix=1");
                var taskModels = JsonConvert.DeserializeObject<Boolean>(json);
                jsons = await client.GetStringAsync("http://192.168.0.78:63835/api/voteCount/?id=" + id);
                count = JsonConvert.DeserializeObject<string>(jsons);
                nbpart.Text = count;
            };
            NoBut.Click += async delegate
            {
                var json = await client.GetStringAsync("http://192.168.0.78:63835/api/vote/?id=" + id + "&&email=" + Email + "&&choix=2");
                var taskModels = JsonConvert.DeserializeObject<Boolean>(json);
                jsons = await client.GetStringAsync("http://192.168.0.78:63835/api/voteCount/?id=" + id);
                count = JsonConvert.DeserializeObject<string>(jsons);
                nbpart.Text = count;
            };
        }
    }
}