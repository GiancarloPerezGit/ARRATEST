using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;


public class speechgeneratorscript : MonoBehaviour
{
    public string voicePath;
    public string textScript;

    void Start()
    {
        SynthesizeAudioAsync(voicePath, textScript);
    }

    // Update is called once per frame
    static async Task SynthesizeAudioAsync(string path, string text)
    {
        var config = SpeechConfig.FromSubscription("1818d7657940467c9111342381af1bf2", "eastus");
        
        var audioConfig = AudioConfig.FromWavFileOutput(path);
        var synthesizer = new SpeechSynthesizer(config, audioConfig);
        await synthesizer.SpeakSsmlAsync(text);
    }
}
