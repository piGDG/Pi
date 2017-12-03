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
using System.Net.Http;
using Newtonsoft.Json;
using App1;
using Android.Provider;
using Android.Database;

using Android.Util;
using System.IO;
using Java.IO;

namespace FreeClub
{
    [Activity(Label = "PartageVideo", MainLauncher = false)]
    public class PartageVideo : Activity
    {
        byte[] videoBytes = null;
        int id;
        List<Event> events;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var client = new HttpClient();
            var json = await client.GetStringAsync("http://192.168.1.5:63835/api/allevents");
            var taskModels = JsonConvert.DeserializeObject<List<Event>>(json);
            events = taskModels;
            List<string> eventss = new List<string>();
            foreach (Event ev in taskModels)
            {
                string eve = ev.Name + "\n" + ev.Date;
                eventss.Add(eve);
            }
            SetContentView(Resource.Layout.PartageVideo);
            VideoView image = FindViewById<VideoView>(Resource.Id.myVideo);
            Button button = FindViewById<Button>(Resource.Id.myButton);
            Button but = FindViewById<Button>(Resource.Id.myBut);
            Spinner spin = FindViewById<Spinner>(Resource.Id.mySpin);
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, eventss);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spin.Adapter = adapter;
            button.Click += delegate
            {
                var intent = new Intent();
                intent.SetType("video/*");
                intent.SetAction(Intent.ActionGetContent);
                intent.PutExtra(Android.Provider.MediaStore.ExtraVideoQuality, 0);
                intent.PutExtra(Android.Provider.MediaStore.ExtraDurationLimit,5000);
                this.StartActivityForResult(Intent.CreateChooser(intent, "Select video"), 200);
            };
            but.Click += async delegate
            {
                string ff = id.ToString();
                String base64Str = Base64.EncodeToString(videoBytes, Base64.Default);
                using (var cl = new HttpClient())
                {
                    cl.BaseAddress = new Uri("http://192.168.1.5:63835");
                    var content = new FormUrlEncodedContent(new[]
                    {
                new KeyValuePair<string, string>("id", ff),
                new KeyValuePair<string, string>("ima", base64Str)
            });
                    var result = await cl.PostAsync("/api/PartageVideo", content);

                }
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

            if (resultCode == Result.Ok && data.Data != null)
            {
                var imageView =
                    FindViewById<VideoView>(Resource.Id.myVideo);
                imageView.SetVideoURI(data.Data);
                imageView.Start();
            }
        }
    }
}