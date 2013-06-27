using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PatientPortal.Models
{
    //class for the
    public class SiteModel
    {
        [BsonId]
        [Required]
        [Display(Name = "ID", GroupName = "Information")]
        BsonObjectId ID { get; set; }
       

        public string Name { get; set; }
        public string URL { get; set; }
        
    }
}