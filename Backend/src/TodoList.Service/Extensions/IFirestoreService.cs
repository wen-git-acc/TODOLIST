using Google.Cloud.Firestore;
using TodoList.Service.Schema;

namespace TodoList.Service.Extensions
{
    public interface IFirestoreService
    {
        public void AddData(string collectionName);
        //public void GetData<T>(string collectionName, string documentName);


        Task<List<T>> GetAllAsync<T>(string collectionName) where T : FirestoreTaskItemConfig;
   
        Task<object> GetAsync<T>(string collectionName, string documentName) where T : TaskItemConfig;
       
        Task<T> AddAsync<T>(T entity, string collectionName, string documentName) where T : TaskItemConfig;
        
        Task<T> UpdateAsync<T>(T entity, string collectionName, string documentName) where T : TaskItemConfig;

        Task DeleteAsync<T>(string collectionName, string documentName) where T : TaskItemConfig;

        Task<List<T>> QueryRecordsAsync<T>(Query query) where T : TaskItemConfig;
    }
}
