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

namespace PatientPortal.BackEnd
{

    public class PatientRepository
    {
        private MongoServer _server;
        private MongoDatabase _db;
        private MongoCollection _patients;
        private MongoGridFS _gridFS;

        readonly string _DBNAME = ConfigurationManager.AppSettings["dbname"];
        readonly string _DBHOST = ConfigurationManager.AppSettings["dbhost"];
        readonly string _REPORTINTERNALNAME = "genomic_report.pptx";

        public PatientModel GetPatient(string id)
        {
            return _patients.FindOneAs<PatientModel>(Query.EQ("_id", id));
            
        }

        public bool TriggerReportGeneration(string id)
        {
            var bin = ConfigurationManager.AppSettings["reportgen"];
            var resource_path = ConfigurationManager.AppSettings["reportgendir"];
            var host = ConfigurationManager.AppSettings["dbhost"];
            var db = ConfigurationManager.AppSettings["dbname"];

            var cmd_line = string.Format(@"-jar {0} I={1}/gemm_main2.jrxml " + 
                @"O={2} outputType=pptx Q=""test"" s={3} p=patient " +
                @"m=mongodb://{4}:27017/su2c " +
                @"wd={1}",
                                                             bin,
                                                             resource_path,
                                                             _REPORTINTERNALNAME,
                                                             id, host);

            //launch the process
            var process = System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "java",
                Arguments = cmd_line,
                CreateNoWindow = false,
                WorkingDirectory = resource_path,
                ErrorDialog = true
            });



            if (!process.Start())
                throw new Exception(
                    String.Format("problem starting process with parameters '{}' and '{}'"
                    , process.StartInfo.FileName, process.StartInfo.Arguments));

            return true;
        }




        public PatientRepository()
        {
            _server = new MongoServer(new MongoServerSettings { Server = new MongoServerAddress(_DBHOST), SafeMode = SafeMode.FSyncTrue});

            //patients
            _patients = _server.GetDatabase(_DBNAME).GetCollection<PatientModel>("patients");
            _patients.EnsureIndex(IndexKeys.Ascending("_id"), IndexOptions.SetUnique(true));
            _gridFS = _server.GetDatabase(_DBNAME).GridFS;

            new MongoDB.Web.Providers.MongoDBMembershipProvider();
        }

        public PatientViewModel GetPatientWithFiles(string id)
        {
            var model = GetPatient(id);
            //TODO: create hash map of files with filename as key

            var viewmodel = new PatientViewModel( model, GetFilesForPatient(model).ToList());
            viewmodel.IsReportAvailable = IsReportGenerated(id);
            return viewmodel;
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
                throw new Exception("Problem inserting document");
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

        public Stream DownloadReport(string patient_id)
        {
            return DownloadFile(new FileModel { PatientID = patient_id, Filename = "genomic_report.pptx" });
        }

        public bool IsReportGenerated(string patient_id)
        {
            var result = _gridFS.FindOne(Query.And(
                        Query.EQ("PatientID", patient_id), Query.EQ("Filename", _REPORTINTERNALNAME)
                        ));

            if (result == null)
                return false;
            return true;
        }

        public FileModel UploadReport(Stream data, string patient_id)
        {
            return UploadFile(data, new FileModel { PatientID = patient_id, Filename = "genomic_report.pptx" });
        }

        public Stream DownloadFile(FileModel file)
        {
            var result = _gridFS.FindOne( Query.And(
                        Query.EQ("PatientID", file.PatientID), Query.EQ("Filename", file.Filename)
                        ));


            if (result == null)
                throw new Exception("Could not find file by that ID.");

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