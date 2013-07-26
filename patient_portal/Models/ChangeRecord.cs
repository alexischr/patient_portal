using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientPortal.Models
{
    public class ChangeRecord
    {
        [BsonId]
        public ObjectId _id;

        public DateTime timestamp;



    }
}