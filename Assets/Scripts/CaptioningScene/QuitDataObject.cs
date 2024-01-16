using System;
using System.Collections.Generic;
using Realms;
using MongoDB.Bson;

public partial class QuitDataObject : IRealmObject
{
    [MapTo("_id")]
    [PrimaryKey]
    public ObjectId Id { get; set; }

    public QuitDataObject()
    {
        this.Id = ObjectId.GenerateNewId();
    }
}
