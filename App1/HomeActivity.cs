using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V4.Widget;
using Android.Views;
using System.Collections.Generic;
using System;
using Android.Content;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/MyTheme", MainLauncher = true)]
    public class HomeActivity : AppCompatActivity, ListView.IOnItemClickListener
    {
        private SupportToolbar mToolbar;
        private ActionBarDrawerToggle mDrawerToggle;
        private DrawerLayout mDrawerLayout;
        private ListView mListView;
        private ArrayAdapter<string> mCategoryAdapter;
        private List<string> mCategory;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Home);

            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            mListView = FindViewById<ListView>(Resource.Id.left_drawer);

            mCategory = new List<string>
            {
                "Yeni Kayıt",
                "Rehber"
            };

            mCategoryAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mCategory);
            mListView.Adapter = mCategoryAdapter;
            mListView.OnItemClickListener = this;

            SetSupportActionBar(mToolbar);

            mDrawerToggle = new ActionBarDrawerToggle(
                this,
                mDrawerLayout,
                Resource.String.openDrawer,
                Resource.String.closeDrawer
                );
            mDrawerLayout.AddDrawerListener(mDrawerToggle);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            mDrawerToggle.SyncState();

            
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            mDrawerToggle.OnOptionsItemSelected(item);
            return base.OnOptionsItemSelected(item);
        }


        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            mDrawerToggle.SyncState();
        }

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
                        
            switch (position)
            {
                case 0:
                    StartActivity(typeof(MainActivity));
                    mDrawerLayout.CloseDrawer((int)GravityFlags.Left);
                    break;

                case 1:
                    StartActivity(typeof(ListActivity));
                    mDrawerLayout.CloseDrawer((int)GravityFlags.Left);
                    break;
                default:
                    break;
            }
        }

       
    }
}

