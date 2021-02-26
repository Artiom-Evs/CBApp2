using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Net;

using Xamarin.Forms;
using Xamarin.Essentials;

[assembly: Dependency(typeof(CBApp2.Droid.Services.ConnectionInfo))]
namespace CBApp2.Droid.Services
{
    public class ConnectionInfo : CBApp2.Services.IConnectionInfo
    {
        
        public bool IsConnected
        {
            get
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    return true;
                }
                return false;
            }
        }
    }
}