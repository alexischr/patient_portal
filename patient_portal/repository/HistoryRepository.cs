using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using PatientPortal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;

namespace PatientPortal.BackEnd
{
    using T = ChangeRecord;
    using Fields = Fields<ChangeRecord>;
    using SortBy = SortBy<ChangeRecord>;
    using Query = MongoDB.Driver.Builders.Query<ChangeRecord>;

    public class HistoryRepository : MongoRepo<T>
    {
        const string _COLLECTION = "history";

        public HistoryRepository(RepoCfg config, IPrincipal claim) :
            base(config, claim, config.MongoConfig, _COLLECTION)
        {
        }

        public IEnumerable<T> GetChangeHistory(string patientID)
        {
            var cursor = _collection.FindAs<T>( 
                Query.EQ(r => r.PatientID, patientID));

            cursor.SetFields(Fields.Include(
                    r => r.Action,
                    r => r.UserName,
                    r => r.Timestamp))
                .SetSortOrder(SortBy.Ascending(
                    r => r.Timestamp));

            foreach (var record in cursor)
            {
                yield return record;
            }
        }

        public IEnumerable<object> GetByUser(string userID)
        {
            var cursor = _collection.FindAs<T>(Query.EQ(
                r => r.UserID, userID));

            cursor.SetFields(Fields.Include(
                    r => r.Action,
                    r => r.PatientName,
                    r => r.Timestamp))
                .SetSortOrder(SortBy.Ascending(
                    r => r.Timestamp));

            foreach (var record in cursor)
            {
                yield return record;
            }
        }



    }
       
}