using System;
using System.Collections.Generic;
using Realms;
using MongoDB.Bson;

public partial class ParametersDataObject : IRealmObject
{
    [MapTo("_id")]
    [PrimaryKey]
    public ObjectId Id { get; set; }

    public long FOV { get; set; }

    public long Video { get; set; }

    public long Offset { get; set; }
    public long CaptioningMethod { get; set; }

    public ParametersDataObject()
    {
        this.Id = ObjectId.GenerateNewId();
    }
}
