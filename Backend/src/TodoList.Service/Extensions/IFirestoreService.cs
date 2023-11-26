using Google.Cloud.Firestore;
using TodoList.Service.Schema;

namespace TodoList.Service.Extensions
{
    public interface IFirestoreService
    {
        //public void GetData<T>(string collectionName, string documentName);


        Task<List<T>> GetAllAsync<T>(string ownerName) where T : FirestoreTaskItemConfig;
   
        Task<object> GetAsync<T>(string documentName) where T : FirestoreTaskItemConfig;
       
        Task<T> AddAsync<T>(T entity) where T : FirestoreTaskItemConfig;
        
        Task<T> UpdateAsync<T>(T entity, string documentName) where T : FirestoreTaskItemConfig;

        Task DeleteAsync<T>(string documentName) where T : FirestoreTaskItemConfig;

        Task<List<T>> QueryRecordsAsync<T>(Query query) where T : FirestoreTaskItemConfig;
    }
}
