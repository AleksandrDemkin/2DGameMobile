using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Bundles
{
    public class AssetBundleViewBase : MonoBehaviour
    {
        private const string UrlAssetBundleSprite =
            "https://drive.google.com/uc?export=download&id=19qENdr_Slmg9NNdxV3ko36Qc9tFjQ0Wp";

        private const string UrlAssetBundleAudio =
            "https://drive.google.com/uc?export=download&id=1X9hCVQivbvcpwXI3oIF2phHhnnksEpoD";

        [SerializeField] private DataSpriteBundle _dataSpriteBundle;

        [SerializeField] private DataAudioBundle _dataAudioBundle;

        private AssetBundle _spritesAssetBundle;
        private AssetBundle _audioAssetBundle;

        protected IEnumerator DowloadAndSetAssetBundle()
        {
            yield return GetSpriteAssteBundle();
        }

        private IEnumerator GetSpriteAssteBundle()
        {
            var request = UnityWebRequestAssetBundle.GetAssetBundle(UrlAssetBundleSprite);

            yield return request.SendWebRequest();

            while (!request.isDone)
                yield return null;

            StateRequest(request, _dataSpriteBundle);
        }

        private void StateRequest(UnityWebRequest request, DataSpriteBundle dataSpriteBundle)
        {
            
        }
    }
}