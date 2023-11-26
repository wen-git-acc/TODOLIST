using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Microsoft.AspNetCore.Antiforgery;
using Newtonsoft.Json;
using TodoList.Service.Schema;

namespace TodoList.Service.Extensions
{
    public class FirestoreService : IFirestoreService
    {
        private readonly IConfiguration _configuration;
        public FirestoreDb firestoreDb;
        private readonly string _projectId = "todolistdatabase-ef822";
        public FirestoreService(IConfiguration configuration)
        {
            _configuration = configuration;
            InitializeFirestoreDb();
            
            
        }

        private void InitializeFirestoreDb()
        {
            var firebaseCredentialsJson = "./Extensions/todolistdatabase-ef822-firebase-adminsdk-1cmms-ea35a0dfd3.json";
            var credentials = File.ReadAllText(firebaseCredentialsJson);
            var builder = new FirestoreClientBuilder{JsonCredentials = credentials};
            firestoreDb = FirestoreDb.Create(_projectId, builder.Build());
        }


        public void AddData(string collectionName)
        {
            var collection = firestoreDb.Collection(collectionName);
            collection.AddAsync(new { Name = new { First = "Ada", Last = "Lovelace" }, Born = 1815 });
        }

        //public async void GetData<T>(string collectionName, string documentName)
        //{
        //    var collection = firestoreDb.Collection(collectionName);
        //    var snapshots = await collection.GetSnapshotAsync();
        //    var taskItemDocument = snapshots.Documents.Select(s => s.ConvertTo<T>()).ToList();
        //    var documentReference = collection.Document(documentName);
        //    var snapShot = await documentReference.GetSnapshotAsync();
        //    var x = snapShot.ToDictionary();
        //    Console.WriteLine(item);

        //}


        public async Task<List<T>> GetAllAsync<T>(string collectionName) where T : FirestoreTaskItemConfig
        {
            Query query = firestoreDb.Collection(collectionName);
            var querySnapshot = await query.GetSnapshotAsync();
            var list = new List<T>();
            foreach (var documentSnapshot in querySnapshot.Documents)
            {
                if (!documentSnapshot.Exists) continue;
                var data = documentSnapshot.ConvertTo<T>();
                if (data == null) continue;
                //data.Id = documentSnapshot.Id;
                list.Add(data);
            }

            return list;
        }

        /// <inheritdoc />
        public async Task<object> GetAsync<T>(string collectionName, string documentName) where T : TaskItemConfig
        {
            var docRef = firestoreDb.Collection(collectionName).Document(documentName);
            var snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                var usr = snapshot.ConvertTo<T>();
                //usr.Id = snapshot.Id;
                return usr;
            }

            return null;
        }

        /// <inheritdoc />
        public async Task<T> AddAsync<T>(T item, string collectionName, string documentName) where T : TaskItemConfig
        {
            var colRef = firestoreDb.Collection(collectionName);
            var doc = await colRef.AddAsync(item);
            // GO GET RECORD FROM DATABASE:
            // return (T) await GetAsync(entity); 
            return item;
        }

        /// <inheritdoc />
        public async Task<T> UpdateAsync<T>(T item, string collectionName, string documentName) where T : TaskItemConfig
        {
            var recordRef = firestoreDb.Collection(collectionName).Document(documentName);
            await recordRef.SetAsync(item, SetOptions.MergeAll);
            // GO GET RECORD FROM DATABASE:
            // return (T)await GetAsync(entity);
            return item;
        }

        /// <inheritdoc />
        public async Task DeleteAsync<T>(string collectionName, string documentName) where T : TaskItemConfig
        {
            var recordRef = firestoreDb.Collection(collectionName).Document(documentName);
            await recordRef.DeleteAsync();
        }

        /// <inheritdoc />
        public async Task<List<T>> QueryRecordsAsync<T>(Query query) where T : TaskItemConfig
        {
            var querySnapshot = await query.GetSnapshotAsync();
            var list = new List<T>();
            //foreach (var documentSnapshot in querySnapshot.Documents)
            //{
            //    if (!documentSnapshot.Exists) continue;
            //    var data = documentSnapshot.ConvertTo<T>();
            //    if (data == null) continue;
            //    data.Id = documentSnapshot.Id;
            //    list.Add(data);
            //}

            return list;
        }



    }
}
