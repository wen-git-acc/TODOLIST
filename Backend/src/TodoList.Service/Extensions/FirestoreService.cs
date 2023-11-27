using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Microsoft.AspNetCore.Antiforgery;
using Newtonsoft.Json;
using TodoList.Service.ConfigKey;
using TodoList.Service.Schema;

namespace TodoList.Service.Extensions
{
    public class FirestoreService : IFirestoreService
    {
        private readonly IConfiguration _configuration;
        public FirestoreDb firestoreDb;
        private readonly string _projectId;
        private readonly string _collectionName;
        public FirestoreService(IConfiguration configuration)
        {
            _configuration = configuration;
            _collectionName = _configuration[ConfigKeys.firebaseCollectionName]!;
            _projectId = _configuration[ConfigKeys.firebaseProjectId]!;
            InitializeFirestoreDb();
            
            
        }

        private void InitializeFirestoreDb()
        {
            var firebaseCredentialsJson = "./Extensions/todolistdatabase-ef822-firebase-adminsdk-1cmms-ea35a0dfd3.json";
            var credentials = File.ReadAllText(firebaseCredentialsJson);
            var builder = new FirestoreClientBuilder{JsonCredentials = credentials};
            firestoreDb = FirestoreDb.Create(_projectId, builder.Build());
        }

        public async Task<List<T>> GetAllAsync<T>(string ownerName) where T : FirestoreTaskItemConfig
        {
            var query = firestoreDb.Collection(_collectionName).WhereArrayContains("Owner",ownerName);
            var querySnapshot = await query.GetSnapshotAsync();
            var list = new List<T>();
            foreach (var documentSnapshot in querySnapshot.Documents)
            {
                if (!documentSnapshot.Exists) continue;
                var data = documentSnapshot.ConvertTo<T>();
                if (data == null) continue;
                list.Add(data);
            }
            return list;
        }

        public async Task<List<T>> GetAllByQueryAsync<T>(string ownerName, string? status = null, string? name = null) where T : FirestoreTaskItemConfig
        {
            var query = firestoreDb.Collection(_collectionName).WhereArrayContains("Owner",ownerName);
            if (status != null)
            {
                query = query.WhereEqualTo("Status", status);
            }  
            
            if (name != null)
            {
                query = query.WhereEqualTo("Name", name);
            }

            var querySnapshot = await query.GetSnapshotAsync();
            var list = new List<T>();
            foreach (var documentSnapshot in querySnapshot.Documents)
            {
                if (!documentSnapshot.Exists) continue;
                var data = documentSnapshot.ConvertTo<T>();
                if (data == null) continue;
                list.Add(data);
            }
            return list;
        }

        public async Task<T> GetAsync<T>(string documentName) where T : FirestoreTaskItemConfig
        {
            var docRef = firestoreDb.Collection(_collectionName).Document(documentName);
            var snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                var data = snapshot.ConvertTo<T>();
                return data;
            }

            return null;
        }

        public async Task<T> AddAsync<T>(T item) where T : FirestoreTaskItemConfig
        {
            try
            {
                var colRef = firestoreDb.Collection(_collectionName).Document(item.DocumentId);
                await colRef.SetAsync(item);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            return item;
        }

        public async Task<T> UpdateAsync<T>(T item, string documentName) where T : FirestoreTaskItemConfig
        {
            try
            {
                var recordRef = firestoreDb.Collection(_collectionName).Document(documentName);
                await recordRef.SetAsync(item, SetOptions.MergeAll);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            
            return item;
        }

        public async Task DeleteAsync<T>(string documentName) where T : FirestoreTaskItemConfig
        {
            var recordRef = firestoreDb.Collection(_collectionName).Document(documentName);
            await recordRef.DeleteAsync();
        }

    }
}
