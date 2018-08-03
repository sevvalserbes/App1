using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;

namespace App1
{
    [Activity(Label = "HomeActivity", Theme = "@style/MyTheme", MainLauncher = true)]
    public class HomeActivity : AppCompatActivity
    {
        private SupportToolbar mToolBar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Home);

            mToolBar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            SetSupportActionBar(mToolBar);
            
        }
    }
}