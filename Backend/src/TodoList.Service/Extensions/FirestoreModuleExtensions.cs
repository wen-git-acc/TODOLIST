using System.Diagnostics;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;

namespace TodoList.Service.Extensions;

public static class FirestoreModuleExtensions
{

    public static IServiceCollection AddFirestoreExtension(this IServiceCollection services)
    {
        services.AddSingleton<IFirestoreService, FirestoreService>();
        return services;
    }
}