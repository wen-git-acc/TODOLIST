using System.Text.Json.Serialization;
using Google.Cloud.Firestore;

namespace TodoList.Service.Schema
{
    [FirestoreData]
    public class FirestoreTaskItemConfig
    {
        [FirestoreDocumentId]
        public string DocumentId { get; set; }

        [FirestoreProperty]
        public string UniqueId { get; set; }

        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public string Description { get; set; }

        [FirestoreProperty]
        public DateTime DueDate { get; set; }

        [FirestoreProperty]
        public string Status { get; set; }

        [FirestoreProperty]
        public List<string> Owner { get; set; }
    }
}
