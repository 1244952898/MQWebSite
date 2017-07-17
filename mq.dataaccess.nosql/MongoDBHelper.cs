using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.IO;
using mq.application.common;


namespace mq.dataaccess.nosql
{
    public class MongoDBHelper
    {
        private static MongoServer server = null;
        private static string dbConnectionString = MemcachedHelper.GetConfigValue("MongoDBServer");

        private static MongoServer ServerInstance()
        {
            if (server == null)
            {
                //推荐使用的最新的
                MongoClient mc = new MongoClient(dbConnectionString);
                server = mc.GetServer();
            }
            return server;
        }

        public static bool ExistsFile(string dbName, string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return false;
            MongoDatabase db = ServerInstance().GetDatabase(dbName);
            return db.GridFS.Exists(filename.ToUpper());
        }

        public static MemoryStream GetFileByName(string dbName, string filename)
        {
            MemoryStream ms = new MemoryStream();
            try
            {
                MongoDatabase db = ServerInstance().GetDatabase(dbName);
                db.GridFS.Download(ms, filename.ToUpper());
            }
            catch
            {
                // ignored
            }
            return ms;
        }

        public static byte[] GetFileByteArrayByName(string dbName, string filename)
        {
            byte[] fileByte = new byte[0];
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    MongoDatabase db = ServerInstance().GetDatabase(dbName);
                    db.GridFS.Download(ms, filename.ToUpper());
                    fileByte = new byte[ms.Length];
                    fileByte = ms.GetBuffer();
                }
            }
            catch
            {
                // ignored
            }
            return fileByte;
        }

        public static void SetFileByName(string dbName, string filepath, string filename)
        {
            if (!File.Exists(filepath))
                return;
            MongoDatabase db = ServerInstance().GetDatabase(dbName);
            if (db.GridFS.Exists(filename.ToUpper()))
                db.GridFS.Delete(filename.ToUpper());
            MongoGridFSFileInfo fileInfo = db.GridFS.Upload(filepath, filename.ToUpper());
        }

        public static void SetFileByName(string dbName, Stream filestream, string filename)
        {
            if (filestream == null)
            {
                return;
            }

            MongoDatabase db = ServerInstance().GetDatabase(dbName);
            if (db.GridFS.Exists(filename.ToUpper()))
                db.GridFS.Delete(filename.ToUpper());
            filestream.Seek(0, SeekOrigin.Begin);//将当前流的位置设置为初始位置。很重要，很重要，很重要！！！（ add on 2016/5/4 by wangqi）
            MongoGridFSFileInfo fileInfo = db.GridFS.Upload(filestream, filename.ToUpper(), new MongoGridFSCreateOptions() { ContentType = GetContentType(filename) });

        }

        /// <summary>
        /// 根据文件名，获取一个ContentType
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetContentType(string fileName)
        {
            string contentType = "";
            string extensionName = Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(extensionName))
            {
                contentType = "";
            }
            else
            {
                contentType = ContentTypeHelper.GetValue(extensionName);
            }
            return contentType;
        }

        private static MongoCredential GetCredential(bool isWriteRight)
        {
            MongoCredential credential = null;
            if (isWriteRight)
            {
                credential = MongoCredential.CreateGssapiCredential("writeuser", "password");
            }
            else
            {
                credential = MongoCredential.CreateGssapiCredential("readuser", "password");
            }
            return credential;
        }

        public static MongoCollection GetCollection(string dbName, string collectionName)
        {
            MongoDatabase db = MongoDBHelper.ServerInstance().GetDatabase(dbName);
            MongoCollection collection = db.GetCollection(collectionName);
            return collection;
        }

        public static bool Insert<T>(string dbName, string collectionName, T model)
        {
            bool isInsert = false;
            if (model == null)
                return isInsert;
            MongoCollection collection = GetCollection(dbName, collectionName);
            WriteConcernResult result = collection.Insert<T>(model);
            return result.UpdatedExisting;
        }

        public static bool Update(string dbName, string collectionName, MongoDB.Driver.IMongoQuery query, MongoDB.Driver.Builders.UpdateBuilder update)
        {
            MongoCollection collection = GetCollection(dbName, collectionName);
            WriteConcernResult result = collection.Update(query, update);
            return result.UpdatedExisting;
        }
    }
}
