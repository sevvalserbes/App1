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

namespace App1
{
    
    class PeopleListAdapter : BaseAdapter<Person>
    {

        List<Person> mPeople;
        Activity mContext;

        public PeopleListAdapter(Activity Context, List<Person> People) : base()
        {
            mPeople = People;
            mContext = Context;
        }

        public override Person this[int position]
        {
            get
            {
                return mPeople[position];
            }
        }

        public override int Count
        {
            get
            {
                return mPeople.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if(view == null)
            {
                view = mContext.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            }

            TextView txtName = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            txtName.Text = mPeople[position].ToString();
            return view;
        }
    }
}