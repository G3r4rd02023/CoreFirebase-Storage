using Firebase.Auth;
using Firebase.Storage;

namespace CoreFirebase.Helpers
{
    public class UploadStorage : IUploadStorage
    {
        public async Task<string> SubirStorage(Stream archivo, string nombre)
        {
            //INGRESA AQUÍ TUS PROPIAS CREDENCIALES
            string email = "tecnologershn@gmail.com";
            string clave = "Tecno.2020";
            string ruta = "corefirebase-a5382.appspot.com";
            string api_key = "AIzaSyCUORv3pc2jheUNSco8Qj_ZJYIULKbafW0";


            var auth = new FirebaseAuthProvider(new FirebaseConfig(api_key));
            var a = await auth.SignInWithEmailAndPasswordAsync(email, clave);

            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(
                ruta,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                })
                .Child("Fotos_Perfil")
                .Child(nombre)
                .PutAsync(archivo, cancellation.Token);


            var downloadURL = await task;


            return downloadURL;

        }
    }
}
