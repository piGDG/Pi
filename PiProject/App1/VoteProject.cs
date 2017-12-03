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

namespace FreeClub
{
    [Activity(Label = "VoteProject")]
    public class VoteProject : Activity
    {
        ListView myList;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            string Email = Intent.GetStringExtra("Email");
            string password = Intent.GetStringExtra("Password");
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.VoteProject);
            ImageButton home = FindViewById<ImageButton>(Resource.Id.IB1);
            ImageButton events = FindViewById<ImageButton>(Resource.Id.IB2);
            ImageButton history = FindViewById<ImageButton>(Resource.Id.IB3);
            ImageButton vote = FindViewById<ImageButton>(Resource.Id.IB4);
            ImageButton partage = FindViewById<ImageButton>(Resource.Id.IB5);
            ImageButton deco = FindViewById<ImageButton>(Resource.Id.IB6);
            List<string> eventss = new List<string>();
            myList = FindViewById<ListView>(Resource.Id.mylistView);
            HttpContent content = new FormUrlEncodedContent(
               new List<KeyValuePair<string, string>> {
                  new KeyValuePair<string, string>("Email", Email),
                  new KeyValuePair<string, string>("Password", password),
                  new KeyValuePair<string, string>("RememberMe", "true")
       });
            var client = new HttpClient();
            HttpResponseMessage resposne = await client.PostAsync(new Uri("http://192.168.0.78:63835/Account/Login/"), content);
            var json = await client.GetStringAsync("http://192.168.0.78:63835/api/projects");
            List<Project> gnaoui = JsonConvert.DeserializeObject<List<Project>>(json);
            foreach (Project ev in gnaoui)
            {
                string eve = ev.NameEquipe ;
                eventss.Add(eve);
            }
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, eventss);
            myList.Adapter = adapter;
            home.Click += delegate
            {
                this.Finish();
                Intent nextAct = new Intent(this, typeof(Home));
                nextAct.PutExtra("Email", Email);
                nextAct.PutExtra("Password", password);
                StartActivity(nextAct);
            };
            deco.Click += delegate
            {
                this.Finish();
                Intent nextAct = new Intent(this, typeof(Auth));
                StartActivity(nextAct);
            };
            vote.Click += delegate
            {
                this.Finish();
                Intent nextAct = new Intent(this, typeof(Vote));
                nextAct.PutExtra("Email", Email);
                nextAct.PutExtra("Password", password);
                StartActivity(nextAct);
            };
            partage.Click += delegate
            {
                this.Finish();
                Intent nextAct = new Intent(this, typeof(Partage));
                nextAct.PutExtra("Email", Email);
                nextAct.PutExtra("Password", password);
                StartActivity(nextAct);
            };
            events.Click += delegate
            {
                this.Finish();
                Intent nextAct = new Intent(this, typeof(MainActivity));
                nextAct.PutExtra("Email", Email);
                nextAct.PutExtra("Password", password);
                StartActivity(nextAct);
            };
            history.Click += delegate
            {
                this.Finish();
                Intent nextAct = new Intent(this, typeof(History));
                nextAct.PutExtra("Email", Email);
                nextAct.PutExtra("Password", password);
                StartActivity(nextAct);
            };
            myList = FindViewById<ListView>(Resource.Id.listView);
            void listViewItemClick(object sender, AdapterView.ItemClickEventArgs e)
            {
                Intent nextAct = new Intent(this, typeof(VoteDetail));
                nextAct.PutExtra("id", gnaoui[e.Position].Id);
                nextAct.PutExtra("NameEquipe", gnaoui[e.Position].NameEquipe);
                nextAct.PutExtra("NameProject", gnaoui[e.Position].NameProject);
                nextAct.PutExtra("Email", Email);
                nextAct.PutExtra("Password", password);
                StartActivity(nextAct);
            }
            myList.ItemClick += listViewItemClick;
        }
    }
}