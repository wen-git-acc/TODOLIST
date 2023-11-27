using Google.Cloud.Firestore;
using TodoList.Service.Schema;

namespace TodoList.Service.Extensions
{
    public interface IFirestoreService
    {
        //public void GetData<T>(string collectionName, string documentName);

        public Task<List<T>> GetAllByQueryAsync<T>(string ownerName, string? status = null, string? name = null) where T : FirestoreTaskItemConfig;

       Task<List<T>> GetAllAsync<T>(string ownerName) where T : FirestoreTaskItemConfig;
   
        Task<T> GetAsync<T>(string documentName) where T : FirestoreTaskItemConfig;
       
        Task<T> AddAsync<T>(T entity) where T : FirestoreTaskItemConfig;
        
        Task<T> UpdateAsync<T>(T entity, string documentName) where T : FirestoreTaskItemConfig;

        Task DeleteAsync<T>(string documentName) where T : FirestoreTaskItemConfig;

    }
}
