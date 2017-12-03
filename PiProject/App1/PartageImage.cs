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
using Android.Graphics;
using Android.Graphics.Drawables;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using App1;
using Android.Util;

namespace FreeClub
{
    [Activity(Label = "PartageImage", MainLauncher = false)]
    public class PartageImage : Activity
    {
        List<Event> events;
        int id;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.partageImage);
            string Email = Intent.GetStringExtra("Email");
            string password = Intent.GetStringExtra("Password");
            HttpContent content = new FormUrlEncodedContent(
                new List<KeyValuePair<string, string>> {
                   new KeyValuePair<string, string>("Email", Email),
                   new KeyValuePair<string, string>("Password", password),
                   new KeyValuePair<string, string>("RememberMe", "true")
        });
             var client = new HttpClient();
             HttpResponseMessage resposne = await client.PostAsync(new Uri("http://192.168.0.78:63835/Account/Login/"), content);
            var json = await client.GetStringAsync("http://192.168.0.78:63835/api/allevents");
            var taskModels = JsonConvert.DeserializeObject<List<Event>>(json);
            DateTime date = DateTime.Today;
            List<string> eventss = new List<string>();
            events = taskModels;
            foreach (Event ev in taskModels)
            {
                string eve = ev.Name + "\n" + ev.Date;
                eventss.Add(eve);
            }
            ImageView image = FindViewById<ImageView>(Resource.Id.myImage);
            Button button = FindViewById<Button>(Resource.Id.myButton);
            Button but = FindViewById<Button>(Resource.Id.myBut);
            Spinner spin = FindViewById<Spinner>(Resource.Id.mySpin);
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, eventss);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spin.Adapter = adapter;
            button.Click += delegate
            {
                var imageIntent = new Intent();
                imageIntent.SetType("image/*");
                imageIntent.SetAction(Intent.ActionGetContent);
                StartActivityForResult(
                    Intent.CreateChooser(imageIntent, "Select photo"), 0);
            };
            but.Click += async delegate
            {
                image.BuildDrawingCache(true);
                Bitmap bmap = image.GetDrawingCache(true);
                Bitmap newBitmap = Bitmap.CreateScaledBitmap(bmap, 215,135, true);
                MemoryStream stream = new MemoryStream();
                newBitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);
                string ff = id.ToString();
                byte[] byteArray = stream.ToArray();
                String base64Str = Base64.EncodeToString(byteArray, Base64.Default);
                HttpContent cont = new FormUrlEncodedContent(
                new List<KeyValuePair<string, string>> {
            new KeyValuePair<string, string>("id", ff),
                new KeyValuePair<string, string>("ima", base64Str)
        });
                HttpResponseMessage resp = await client.PostAsync(new Uri("http://192.168.0.78:63835/api/partageimage"), cont);
                };
            spin.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            
            void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
            {
                Spinner spinner = (Spinner)sender;

                id = events[e.Position].Id;
            }
        }
        
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok)
            {
                var imageView =
                    FindViewById<ImageView>(Resource.Id.myImage);
                imageView.SetImageURI(data.Data);
            }
        }
    }
}
