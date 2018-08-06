using Android.App;
using Android.Widget;
using Android.OS;
using System.IO;
using SQLite;
using System.Collections.Generic;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Views;

namespace App1
{
    [Activity(Label = "Yeni Kayıt", Theme = "@style/MyTheme")]
    public class MainActivity : AppCompatActivity
    {
        private SupportToolbar mToolbar;
        EditText PersonName;
        EditText PersonLName;
        TextView TName;
        TextView TLastName;
        Button SaveButton;
        Button ShowButton;
        ListView ListPeople;
        List<Person> People = new List<Person>();

        //Path for the database file
        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "defter.db3");

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);


            PersonName = FindViewById<EditText>(id: Resource.Id.personName);
            PersonLName = FindViewById<EditText>(id: Resource.Id.personLName);
            TName = FindViewById<TextView>(id: Resource.Id.txtName);
            TLastName = FindViewById<TextView>(id: Resource.Id.txtLastName);
            SaveButton = FindViewById<Button>(id: Resource.Id.btnSave);
            ShowButton = FindViewById<Button>(id: Resource.Id.btnShow);

            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbarMain);
            mToolbar.SetNavigationIcon(Resource.Drawable.abc_ic_ab_back_material);

            SetSupportActionBar(mToolbar);


            SaveButton.Click += SaveButton_Click;
            ShowButton.Click += ShowButton_Click;

        }

        private void SaveButton_Click(object sender, System.EventArgs e)
        {
            //Database connection set up
            var db = new SQLiteConnection(dbPath);
            //Creating tables
            db.CreateTable<Person>();

            //Create new object 
            Person person = new Person(PersonName.Text, PersonLName.Text);


            //Store the object into the table
            db.Insert(person);


            Toast.MakeText(this, "Kaydedildi", ToastLength.Short).Show();

        }

        private void ShowButton_Click(object sender, System.EventArgs e)
        {

            //Database connection set up
            var db = new SQLiteConnection(dbPath);

            ListPeople = FindViewById<ListView>(Resource.Id.listViewPeople);
            ListPeople.Adapter = new PeopleListAdapter(this, People);

            //Connect to the relevent table 
            var table = db.Table<Person>();
            //int count = 0;

            try
            {
                People.Clear();
                foreach (var item in table)
                {
                    People.Add(item);
                    //count++;
                }

            }
            catch (SQLiteException NoSuchTable)
            {

                Toast.MakeText(this, "Gösterilecek kişi yok", ToastLength.Short).Show();
            }           
           

        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
                this.OnBackPressed();
            return base.OnOptionsItemSelected(item);
        }
    }
}

