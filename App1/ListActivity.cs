using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;

namespace App1
{
    [Activity(Label = "Rehber", Theme = "@style/MyTheme")]
    public class ListActivity : AppCompatActivity
    {
        private PeopleListAdapter adapter;
        private SupportToolbar mToolbar;
        private ListView ListPeople;
        private List<Person> People = new List<Person>();
        private EditText mSearchBar;
        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "defter.db3");

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.list_people);
            //Database connection set up
            var db = new SQLiteConnection(dbPath);

            mSearchBar = FindViewById<EditText>(Resource.Id.search_bar);
            ListPeople = FindViewById<ListView>(Resource.Id.listViewPeople);
            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbarList);
            mToolbar.SetNavigationIcon(Resource.Drawable.abc_ic_ab_back_material);


            adapter = new PeopleListAdapter(this, People);
            ListPeople.Adapter = adapter;


            mSearchBar.TextChanged += MSearchBar_TextChanged;


            SetSupportActionBar(mToolbar);           


            //Connect to the relevent table 
            var table = db.Table<Person>();

            try
            {
                People.Clear();
                foreach (var item in table)
                {
                    People.Add(item);                    
                }

            }
            catch (SQLiteException NoSuchTable)
            {

                Toast.MakeText(this, "Gösterilecek kişi yok", ToastLength.Short).Show();
                
                
            }


        }

        private void MSearchBar_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            List<Person> mPeople = (from mperson in People
                                    where mperson.FirstName.Contains(mSearchBar.Text, StringComparison.OrdinalIgnoreCase) ||
                                    mperson.LastName.Contains(mSearchBar.Text, StringComparison.OrdinalIgnoreCase)
                                    select mperson).ToList<Person>();
            adapter = new PeopleListAdapter(this, mPeople);
            ListPeople.Adapter = adapter;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
                this.OnBackPressed();
            return base.OnOptionsItemSelected(item);
        }

    }
}