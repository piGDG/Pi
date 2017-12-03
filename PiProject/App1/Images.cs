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
using App1;
using static Android.App.ActionBar;

namespace FreeClub
{
    [Activity(Label = "Images")]
    public class Images : Activity
    {
        private string chemin = "http://192.168.0.78:63835/api/allevents";
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            string Email = Intent.GetStringExtra("Email");
            string password = Intent.GetStringExtra("Password");
            SetContentView(Resource.Layout.Images);
            base.OnCreate(savedInstanceState);
            HttpContent content = new FormUrlEncodedContent(
               new List<KeyValuePair<string, string>> {
                  new KeyValuePair<string, string>("Email", Email),
                  new KeyValuePair<string, string>("Password", password),
                  new KeyValuePair<string, string>("RememberMe", "true")
       });
            var client = new HttpClient();
            HttpResponseMessage resposne = await client.PostAsync(new Uri("http://192.168.0.78:63835/Account/Login/"), content);
            var json = await client.GetStringAsync(chemin);
            List<Event> taskModels = JsonConvert.DeserializeObject<List<Event>>(json);
            string nom = Intent.GetStringExtra("nom");
            int n = taskModels.Count;
            List<byte[]> photos = null;
            int i;
            for (i = 0; i <n ; i++)
            {
                if (taskModels[i].Name==nom){
                    photos = taskModels[i].Ima;
                    }
            }
            int longueur = photos.Count;
            for(int j = 0; j < longueur; j++)
            {
                LinearLayout layoutBase = FindViewById<LinearLayout>(Resource.Id.layoutBase);
                ImageView img = new ImageView(this);
                img.LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
                img.Visibility = ViewStates.Visible;
                img.LayoutParameters.Height = 500;
                img.LayoutParameters.Width = 800;
                int longe = photos[j].Length;
                Android.Graphics.Bitmap bmp = await Android.Graphics.BitmapFactory.DecodeByteArrayAsync(photos[j], 0, longe);
                Android.Graphics.Bitmap newBitmap = Android.Graphics.Bitmap.CreateScaledBitmap(bmp, 500, 500, true);
                img.SetImageBitmap(bmp);
                layoutBase.AddView(img);
            }
           }
    }
}