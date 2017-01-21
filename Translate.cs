using System;
using System.Linq;
using Google.Apis.CloudSpeechAPI.v1beta1;
using Google.Cloud.Speech.V1Beta1;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using System.IO;

namespace GoogleCloudSamples
{
    public class Transcribe
    {
        
        static public CloudSpeechAPIService CreateAuthorizedClient()
        {
            GoogleCredential credential =
                GoogleCredential.GetApplicationDefaultAsync().Result;
            if (credential.IsCreateScopedRequired)
            {
                credential = credential.CreateScoped(new[]
                {
                    CloudSpeechAPIService.Scope.CloudPlatform
                });
            }
            return new CloudSpeechAPIService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "wordidentify",
            });
        }

        static public void translate(string args)
        {
            if (args == "")
            {
                Console.WriteLine("Usage:\nTranscribe audio_file");
                return;
            }
            var service = CreateAuthorizedClient();
            string audio_file_path = args;
            var request = new Google.Apis.CloudSpeechAPI.v1beta1.Data.SyncRecognizeRequest()
            {
                Config = new Google.Apis.CloudSpeechAPI.v1beta1.Data.RecognitionConfig()
                {
                    Encoding = "LINEAR16",
                    SampleRate = 44100,
                    LanguageCode = "ro-RO"
                },
                Audio = new Google.Apis.CloudSpeechAPI.v1beta1.Data.RecognitionAudio()
                {
                    Content = Convert.ToBase64String(File.ReadAllBytes(audio_file_path))
                }
            };
            var response = service.Speech.Syncrecognize(request).Execute();
            foreach (var result in response.Results)
            {
                foreach (var alternative in result.Alternatives)
                    Console.WriteLine(alternative.Transcript);
            }
        }
    }
}