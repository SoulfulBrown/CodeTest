using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace CakeApp
{
    public static class NetworkAPI
    {
        private static readonly string endpoint = "https://gist.githubusercontent.com/hart88/198f29ec5114a3ec3460/raw/8dd19a88f9b8d24c23d9960f3300d0c917a4f07c/cake.json";
      //  private static readonly string endpoint = "cake.json";

        public static IEnumerator GetJson(System.Action<string> callback)
        {
            UnityWebRequest request = UnityWebRequest.Get(endpoint);

            request.chunkedTransfer = false;

            //Create Handlers
            request.downloadHandler = new DownloadHandlerBuffer();

            //Send Request 
            yield return request.SendWebRequest();


            //Error checking
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError("Error getting file from endpoint server, unable to proceed!");
            }
            else
            {
                //Write json to file
                var downloadedJson = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
                callback(downloadedJson);
            }
        }

        public static IEnumerator GetImage(string url, System.Action<Sprite> callback)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);

            //Send Request 
            yield return request.SendWebRequest();

            //Error checking
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError(string.Format("Error getting file from {0}, unable to proceed!", url));
            }
            else
            {
                //Write json to file
                var tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
                var sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
            
                callback(sprite);
            }
        }
    }
}
