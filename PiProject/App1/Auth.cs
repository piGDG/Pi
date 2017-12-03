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

namespace FreeClub
{
    [Activity(Label = "Pi GDG", MainLauncher =true, Icon = "@drawable/logo")]
    public class Auth : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Auth);
            EditText email = FindViewById<EditText>(Resource.Id.EmailTxt);
            EditText passwd = FindViewById<EditText>(Resource.Id.passwordTxt);
            Button connBut = FindViewById<Button>(Resource.Id.connbut);
            TextView oubli = FindViewById<TextView>(Resource.Id.oubliTxt);


            connBut.Click += async delegate
            {
                string emaill = email.Text;
                string password = passwd.Text;
                using (var cl = new HttpClient())
                {
                    cl.BaseAddress = new Uri("http://192.168.0.78:63835");
                    var content = new FormUrlEncodedContent(new[]
                    {
                new KeyValuePair<string, string>("Email", emaill),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("RememberMe", "false")
            });
                    var result = await cl.PostAsync("/Account/Login", content);
                    var json = await cl.GetStringAsync("http://192.168.0.78:63835/api/ConnectMobile");
                    

                    if (json == "true")
                    {
                        this.Finish();
                        Intent nextAct = new Intent(this, typeof(Home));
                        nextAct.PutExtra("Email", emaill);
                        nextAct.PutExtra("Password", password);
                        StartActivity(nextAct);
                    }
                    else oubli.Visibility=ViewStates.Visible;

                }
            };
            TextView motPass = FindViewById<TextView>(Resource.Id.motPass);
            motPass.Click += delegate
            {
                Toast.MakeText(this, "en cours de construction", ToastLength.Long).Show();
            };
        }
    }
}