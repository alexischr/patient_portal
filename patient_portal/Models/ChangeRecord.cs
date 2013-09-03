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

        public DateTime Timestamp;


        public string UserID { get; set; }
        public string PatientID { get; set; }
        public string PatientName { get; set; }
        public string Action { get; set; }
        public string UserName { get; set; }
    }

    /* view models */

    public class ChangeHistoryVM
    {
        public DateTime timestamp;
        public string patientName;
        public string action;
        public string userName;
    }
}