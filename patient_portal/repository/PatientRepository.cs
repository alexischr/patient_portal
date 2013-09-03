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


    public class AppSettingsConfig : RepoCfg
    {
        public AppSettingsConfig()
        {
            this.MongoConfig = new MongoRepoCfg
            {
                Address = ConfigurationManager.AppSettings["dbhost"],
                DBName = ConfigurationManager.AppSettings["dbname"],
            };

        }
    }

    public class PatientRepository : MongoRepo<PatientModel>
    {
        protected HistoryRepository _history;
        const string _REPORTINTERNALNAME = "genomic_report";
        const string _REPORTINTERNALEXT = "genomic_report.pptx";
        const string _COLLECTION = "patients";

        public PatientRepository(RepoCfg config, IPrincipal claim) :
            base(config, claim, config.MongoConfig, _COLLECTION)
        {
            _history = new HistoryRepository(config, claim);
        }



        public void TriggerReportGeneration(string id)
        {
            var bin = ConfigurationManager.AppSettings["reportgen"];
            var resource_path = ConfigurationManager.AppSettings["reportgendir"];
            var host = Cfg.MongoConfig.Address;
            var db = Cfg.MongoConfig.DBName;

            var cmd_line = string.Format(@"-jar {0} I={1}\gemm_main2.jrxml " + 
                @"O=.\{2} outputType=pptx Q=""test"" s={3} p=patient " +
                @"m=mongodb://{4}:27017/{5} " +
                @"wd={1} g=true",
                                                             bin,
                                                             resource_path,
                                                             _REPORTINTERNALNAME,
                                                             id, host, db);

            //launch the process
            var process = new Process();
            
            process.StartInfo = new ProcessStartInfo 
             {
                 FileName = "java.exe",
                 Arguments = cmd_line,
                 WorkingDirectory = resource_path, 
                 UseShellExecute = true,
                 LoadUserProfile = false,
                 
             };

            try
            {
//                throw new Exception("here");
                if (!process.Start())
                    throw new Exception("process.Start() returned false.");

            }
            catch (Exception e)
            {
                throw new Exception(
                        String.Format("problem starting process with parameters '{0}' and '{1}' in {2}:\n {3}'"
                        , process.StartInfo.FileName, process.StartInfo.Arguments
                        , process.StartInfo.WorkingDirectory, e.Message));
            }

        }

  
        public PatientViewModel GetPatientWithFiles(string id)
        {
            var model = Get(id);
            //TODO: create hash map of files with filename as key

            var viewmodel = new PatientViewModel( model, GetFilesForPatient(model).ToList());
            viewmodel.IsReportAvailable = IsReportGenerated(id);
            return viewmodel;
        }



        public IEnumerable<PatientViewModel> GetAllPatientsWithFileInfo()
        {
            foreach (PatientModel result in GetAll())
            {
                yield return new PatientViewModel(result, GetFilesForPatient(result).ToList());
            }
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

            file_model.Filename = gfs_entry.Name;
            file_model.PatientID = gfs_entry.Metadata["PatientID"].AsString;
            file_model.ID = gfs_entry.Id.AsObjectId;  

            return file_model;
        }

        public Stream DownloadReport(string patient_id)
        {
            return DownloadFile(new FileModel { PatientID = patient_id, Filename = _REPORTINTERNALEXT });
        }

        public bool IsReportGenerated(string patient_id)
        {

            var result = _gridFS.FindOne(Query.And(
                        Query.EQ("metadata.PatientID", patient_id), Query.EQ("filename", _REPORTINTERNALEXT)
                        ));

            if (result == null)
                return false;
            else
                return true;

        }

        public FileModel UploadReport(Stream data, string patient_id)
        {
            return UploadFile(data, new FileModel { PatientID = patient_id, Filename = "genomic_report.pptx" });
        }

        public Stream DownloadFile(FileModel file)
        {
            var result = _gridFS.FindOne( Query.And(
                        Query.EQ("metadata.PatientID", file.PatientID), Query.EQ("filename", file.Filename)
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

        public override void Update(PatientModel model)
        {
            base.Update(model);
            _history.Add(new ChangeRecord
                    {
                        Action = "modify",
                        PatientID = model.ID,
                        PatientName = model.ID,
                        Timestamp = DateTime.Now,
                        UserName = Claim.Identity.Name
                    });

        }

        public override void Add(PatientModel model)
        {
            base.Add(model);
            _history.Add(new ChangeRecord
                {
                    Action = "add",
                    PatientID = model.ID,
                    PatientName = model.ID, /*todo: separate string ID from mongo _id */
                    Timestamp = DateTime.Now,
                    UserName = Claim.Identity.Name
                });
        }

        public override void Delete(string id)
        {
            base.Delete(id);
            _history.Add(new ChangeRecord
            {
                Action = "delete",
                PatientID = id,
                PatientName = id, /*todo: separate string ID from mongo _id */
                Timestamp = DateTime.Now,
                UserName = Claim.Identity.Name
            });
        }

    }
}