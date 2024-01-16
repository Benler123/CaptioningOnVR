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

public class CaptioningRealmController : MonoBehaviour
{

    private const string EMAIL = "tkwok123@gmail.com";
    private const string PASSWORD = "Boris@123";
    public Realm realm;
    static public CaptioningRealmController Instance;

    private App realmApp;
    private User realmUser;
    [SerializeField] private string realmAppId = "application-0-qhtul";
    async void Awake()
    {
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
                realmUser = realmApp.CurrentUser;
            }
        }
        
    var config = new FlexibleSyncConfiguration(realmApp.CurrentUser);
    realm = await Realm.GetInstanceAsync(config);
    Debug.Log("SUBS " + realm.Subscriptions.Count);
    realm.Subscriptions.Update(() =>
    {
        var quitDataObjects = realm.All<QuitDataObject>().Where(n => true);
        realm.Subscriptions.Add(quitDataObjects);
    });

    await realm.Subscriptions.WaitForSynchronizationAsync();

    }

    void OnDisable(){
        if (realm != null)
        {
            realm.Dispose();
        }
    }

    public IQueryable<QuitDataObject> GetQuitDataObjects()
    {
        if (realm == null) return null;
        return realm.All<QuitDataObject>();
    }

}

    