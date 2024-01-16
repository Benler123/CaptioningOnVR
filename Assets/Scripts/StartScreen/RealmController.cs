using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;
using Realms.Sync;
using MongoDB.Bson;
using Realms.Exceptions;
using System.Threading.Tasks;
using Unity.VisualScripting;
using System.Linq;

public class RealmController : MonoBehaviour
{

    private const string EMAIL = "tkwok123@gmail.com";
    private const string PASSWORD = "Boris@123";
    public Realm realm;
    static public RealmController Instance;

    private App realmApp;
    private User realmUser;
    [SerializeField] private string realmAppId = "application-0-qhtul";
    async void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        if(realm == null) {
            realmApp = App.Create(new AppConfiguration(realmAppId));
            if(realmApp.CurrentUser == null) {
                await realmApp.LogInAsync(Credentials.EmailPassword(EMAIL, PASSWORD)).ContinueWith(task => {
                    if (task.IsFaulted)
                    {
                        Debug.Log("Failed to log in");
                               Debug.Log("Exception: " + task.Exception.Message);
        Debug.Log("Stack Trace: " + task.Exception.StackTrace);
                    }
                    else
                    {
                        Debug.Log("Successfully logged in!");
                        realmUser = task.Result;
                    }
                });
            } else {
                Debug.Log("ALREADY LOGGED");
                realmUser = realmApp.CurrentUser;
            }
        }
        
        
    var config = new FlexibleSyncConfiguration(realmApp.CurrentUser)
    {
        PopulateInitialSubscriptions = (realm) =>
        {
            var paramItems = realm.All<ParametersDataObject>().Where(n => true);
            var quitItems = realm.All<QuitDataObject>().Where(n => true);
            realm.Subscriptions.Add(paramItems);
            realm.Subscriptions.Add(quitItems);
        }
    };
    realm = await Realm.GetInstanceAsync(config);
        
    Debug.Log("REALM CONTROLLER SUBS " + realm.Subscriptions.Count);

    }

    void OnDisable(){
        if (realm != null)
        {
            realm.Dispose();
        }
    }

    public IQueryable<ParametersDataObject> GetParametersDataObjects()
    {
        if (realm == null) return null;
        return realm.All<ParametersDataObject>();
    }

}

    