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
using App1;
using Newtonsoft.Json;
using System.IO;
using Android.Webkit;
using Android.Media;

namespace FreeClub
{
    [Activity(Label = "Videos", MainLauncher =false)]
    public class Videos : Activity, MediaPlayer.IOnPreparedListener, ISurfaceHolderCallback
    {
        VideoView videoView;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.Videos);
            base.OnCreate(savedInstanceState);
            videoView = FindViewById<VideoView>(Resource.Id.video);
            play("TransData/test.3gp");
        }
        MediaPlayer player;
        void play(string fullPath)
        {
            ISurfaceHolder holder = videoView.Holder;
            holder.SetType(SurfaceType.PushBuffers);
            holder.AddCallback(this);
            player = new MediaPlayer();
            Android.Content.Res.AssetFileDescriptor afd = this.Assets.OpenFd(fullPath);
            if (afd != null)
            {
                player.SetDataSource(afd.FileDescriptor, afd.StartOffset, afd.Length);
                player.Prepare();
                player.Start();
            }
        }
        public void SurfaceCreated(ISurfaceHolder holder)
        {
            Console.WriteLine("SurfaceCreated");
            player.SetDisplay(holder);
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            Console.WriteLine("SurfaceDestroyed");
        }

        public void SurfaceChanged(ISurfaceHolder holder, Android.Graphics.Format format, int w, int h)
        {
            Console.WriteLine("SurfaceChanged");
        }

        public void OnPrepared(MediaPlayer mp)
        {
            throw new NotImplementedException();
        }
    }
}