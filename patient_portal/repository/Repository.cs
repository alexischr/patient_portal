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

using Identity = System.Security.Principal.IIdentity;

namespace PatientPortal.BackEnd
{
    public abstract class Repo<T>
    {
        protected RepoCfg Cfg { get; set; }
        protected IPrincipal Claim { get; set; }  /* TODO: Change type */

        protected Repo(RepoCfg config, IPrincipal claim)
        {
            this.Cfg = config;
            this.Claim = claim;
        }

    }

    public class MongoRepoCfg
    {
        public  SafeMode SafeMode = SafeMode.FSyncTrue;
        public bool DebugPerformance = false;
        public string Address;
        public string DBName { get; set; }
    }

    public abstract class MongoRepo<T> : Repo<T>
    {
        protected MongoServer _server;
        protected MongoDatabase _db;
        protected MongoCollection _collection;
        protected MongoGridFS _gridFS;

        protected MongoRepoCfg _mongoConfig;

        protected MongoRepo(RepoCfg config, IPrincipal claim, MongoRepoCfg mongo_config, string collection)
            : base(config, claim)
        {
            _mongoConfig = mongo_config;

            _server = new MongoServer(new MongoServerSettings { Server = new MongoServerAddress(mongo_config.Address), SafeMode = mongo_config.SafeMode });
            _collection = _server.GetDatabase(mongo_config.DBName).GetCollection<PatientModel>(collection);
            _collection.EnsureIndex(IndexKeys.Ascending("_id"), IndexOptions.SetUnique(true));
            _gridFS = _server.GetDatabase(mongo_config.DBName).GridFS;
        }

        public virtual T Get(string id)
        {
            return _collection.FindOneByIdAs<T>(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            MongoCursor cursor = _collection.FindAllAs<PatientModel>();

            foreach (T result in cursor)
            {
                yield return result;
            }
        }

        public virtual void Add(T model)
        {
            var result = _collection.Insert<T>(model);
            if (result.ErrorMessage != null)
                throw new Exception("Problem inserting document");

        }

        public virtual void Update(T model)
        {
            _collection.Save<T>(model);
        }

        public virtual void Delete(string id)
        {
            var result = _collection.Remove(Query.EQ("_id", id));

            if (result.ErrorMessage != null)
                throw new MongoException(String.Format("Delete failed for _id {0}", id));

        }

    }

    public class RepoCfg
    {
        public virtual MongoRepoCfg MongoConfig { get; set; }

    }
}