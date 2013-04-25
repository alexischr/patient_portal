using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using PatientPortal.Models;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using System.IO;

namespace PatientPortal.BackEnd
{

    public class Repository
    {
        private MongoServer _server;
        private MongoDatabase _db;
        private MongoCollection _patients;
        private MongoGridFS _gridFS;

        const string _DBNAME = "su2c_test";

        public PatientModel GetPatient(string id)
        {
            return _patients.FindOneAs<PatientModel>(Query.EQ("_id", id));
        }

        public Repository()
        {
            _server = new MongoServer(new MongoServerSettings { Server = new MongoServerAddress("localhost"), SafeMode = SafeMode.True });

            //patients
            _patients = _server.GetDatabase(_DBNAME).GetCollection<PatientModel>("patients");
            _patients.EnsureIndex(IndexKeys.Ascending("_id"), IndexOptions.SetUnique(true));
            _gridFS = _server.GetDatabase(_DBNAME).GridFS;

            new MongoDB.Web.Providers.MongoDBMembershipProvider();
        }

        public PatientViewModel GetPatientWithFiles(string id)
        {
            var model = GetPatient(id);
            return new PatientViewModel( model, GetFilesForPatient(model).ToList());
        }

        public IEnumerable<PatientModel> GetAllPatients()
        {
            MongoCursor cursor =  _patients.FindAllAs<PatientModel>();

            foreach (PatientModel result in cursor)
            {
                yield return result;
            }
        }

        public IEnumerable<PatientViewModel> GetAllPatientsWithFileInfo()
        {
            foreach (PatientModel result in GetAllPatients())
            {
                yield return new PatientViewModel(result, GetFilesForPatient(result).ToList());
            }
        }


        public void AddPatient(PatientModel patient)
        {
            var result =_patients.Insert<PatientModel>(patient);
            if (result.ErrorMessage != null)
                throw new Exception("Problem insterting document");
        }

        public IEnumerable<FileModel> GetFilesForPatient(PatientModel patient)
        {
            var cursor = _gridFS.Find(Query.EQ("metadata.PatientID", patient.ID));

            foreach (var result in cursor)
            {
                yield return FileModelFromGridFSMetadata(result);
            }
        }

        public static FileModel FileModelFromGridFSMetadata(MongoGridFSFileInfo gfs_entry)
        {
            var file_model = new FileModel();

            file_model.Filename = gfs_entry.Metadata["Filename"].AsString;
            file_model.PatientID = gfs_entry.Metadata["PatientID"].AsString;
            file_model.ID = gfs_entry.Id.AsObjectId;  

            return file_model;
        }

        public Stream DownloadFile(ref FileModel file)
        {
            var result = _gridFS.FindOneById(file.ID);

            if (result == null)
                throw new Exception("Could not find file by that ID.");

            file.Filename = result.Name;

            return result.OpenRead();
        }

        public FileModel UploadFile(Stream data, FileModel fileinfo)
        {
            //if (fileinfo.ID != null)
             //   throw new Exception("File already has id!?!?!?");

            var info = _gridFS.Upload(data, fileinfo.Filename, new MongoGridFSCreateOptions { Metadata = fileinfo.ToBsonDocument() });
            fileinfo.ID = info.Id.AsObjectId;
            return fileinfo;
        }


        internal void UpdatePatient(PatientViewModel model)
        {
            _patients.Save<PatientModel>(model.Patient);
        }

        internal void DeletePatient(string id)
        {
            var result = _patients.Remove(Query.EQ("_id", id));
            //TODO: Check!

        }
    }
}