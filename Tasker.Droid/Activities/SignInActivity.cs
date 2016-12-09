using Android.App;
using Android.Widget;
using Android.OS;

using Android.Content;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Views;
using Android.Gms.Auth.Api;
using Android.Support.V7.App;
using Android.Gms.Auth.Api.SignIn;
using Android.Runtime;
using Android.Util;
using Android.Gms.Tasks;
using Firebase.Auth;
using Android.Gms.Auth.Api.SignIn;

namespace Tasker.Droid.Activities
{
    [Activity]
    public class SignInActivity : AppCompatActivity, GoogleApiClient.IOnConnectionFailedListener, IOnCompleteListener, View.IOnClickListener
    {

        private const string TAG = "SignInActivity";
        private const int RC_SIGN_IN = 9001;

        private SignInButton mSignInButton;

        private GoogleApiClient mGoogleApiClient;

        // Firebase instance variables
        private FirebaseAuth mFirebaseAuth;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.activity_sign_in);
            mFirebaseAuth = FirebaseAuth.Instance;
            // Assign fields
            mSignInButton = FindViewById<SignInButton>(Resource.Id.sign_in_button);

            // Set click listeners
            mSignInButton.SetOnClickListener(this);

            // Configure Google Sign In
            var id = GetString(Resource.String.default_web_client_id);
            GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                    .RequestIdToken(GetString(Resource.String.default_web_client_id))
                    .RequestEmail()
                    .Build();
            mGoogleApiClient = new GoogleApiClient.Builder(this)
                    .EnableAutoManage(this, this)
                    .AddApi(Auth.GOOGLE_SIGN_IN_API, gso)
                    .Build();
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            // An unresolvable error has occurred and Google APIs (including Sign-In) will not
            // be available.
            Log.Debug(TAG, "onConnectionFailed:" + result);
            Toast.MakeText(this, "Google Play Services error.", ToastLength.Long).Show();
        }

        public void OnClick(View v)
        {
            switch (v.Id)
            {
                case Resource.Id.sign_in_button:
                    SignIn();
                    break;
            }
        }

        private void SignIn()
        {
            Intent signInIntent = Auth.GoogleSignInApi.GetSignInIntent(mGoogleApiClient);
            StartActivityForResult(signInIntent, RC_SIGN_IN);
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            // Result returned from launching the Intent from GoogleSignInApi.getSignInIntent(...);
            if (requestCode == RC_SIGN_IN)
            {
                GoogleSignInResult result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
                if (result.IsSuccess)
                {
                    // Google Sign In was successful, authenticate with Firebase
                    GoogleSignInAccount account = result.SignInAccount;
                    FirebaseAuthWithGoogle(account);
                }
                else
                {
                    // Google Sign In failed
                    Log.Error(TAG, "Google Sign In failed.");
                }
            }
        }

        private void FirebaseAuthWithGoogle(GoogleSignInAccount account)
        {
            Log.Debug(TAG, "firebaseAuthWithGooogle:" + account.Id);
            AuthCredential credential = GoogleAuthProvider.GetCredential(account.IdToken, null);
            mFirebaseAuth.SignInWithCredential(credential)
                    .AddOnCompleteListener(this, this);

        }


        public void OnComplete(Task task)
        {
            Log.Debug(TAG, "signInWithCredential:onComplete:" + task.IsSuccessful);

            // If sign in fails, display a message to the user. If sign in succeeds
            // the auth state listener will be notified and logic to handle the
            // signed in user can be handled in the listener.
            if (!task.IsSuccessful)
            {
                Log.WriteLine(LogPriority.Error, "signInWithCredential", task.Exception.Message);
                Toast.MakeText(this, "Authentication failed.", ToastLength.Long).Show();
            }
            else
            {
                StartActivity(new Intent(this, typeof(MainTaskListActivity)));
                Finish();
            }
        }


    }
}