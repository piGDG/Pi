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
using FreeClub;

namespace App1
{
    [Activity(Label = "Partage")]
    public class Partage : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            string Email = Intent.GetStringExtra("Email");
            string Password = Intent.GetStringExtra("Password");
            SetContentView(Resource.Layout.Partage);
            ImageButton home = FindViewById<ImageButton>(Resource.Id.IB1);
            ImageButton events = FindViewById<ImageButton>(Resource.Id.IB2);
            ImageButton history = FindViewById<ImageButton>(Resource.Id.IB3);
            ImageButton vote = FindViewById<ImageButton>(Resource.Id.IB4);
            ImageButton partage = FindViewById<ImageButton>(Resource.Id.IB5);
            ImageButton deco = FindViewById<ImageButton>(Resource.Id.IB6);
            Button partIm = FindViewById<Button>(Resource.Id.MyBut);
            events.Click += delegate
            {
                this.Finish();
                Intent nextAct = new Intent(this, typeof(MainActivity));
                nextAct.PutExtra("Email", Email);
                nextAct.PutExtra("Password", Password);
                StartActivity(nextAct);
            };
            vote.Click += delegate
            {
                this.Finish();
                Intent nextAct = new Intent(this, typeof(Vote));
                nextAct.PutExtra("Email", Email);
                nextAct.PutExtra("Password", Password);
                StartActivity(nextAct);
            };
            home.Click += delegate
            {
                this.Finish();
                Intent nextAct = new Intent(this, typeof(Partage));
                nextAct.PutExtra("Email", Email);
                nextAct.PutExtra("Password", Password);
                StartActivity(nextAct);
            };
            history.Click += delegate
            {
                this.Finish();
                Intent nextAct = new Intent(this, typeof(History));
                nextAct.PutExtra("Email", Email);
                nextAct.PutExtra("Password", Password);
                StartActivity(nextAct);
            };
            deco.Click += delegate
            {
                this.Finish();
                Intent nextAct = new Intent(this, typeof(Auth));
                StartActivity(nextAct);
            };
            partIm.Click += delegate
            {
                Intent nextAct = new Intent(this, typeof(PartageImage));
                nextAct.PutExtra("Email", Email);
                nextAct.PutExtra("Password", Password);
                StartActivity(nextAct);
            };

            // Create your application here
        }
    }
}