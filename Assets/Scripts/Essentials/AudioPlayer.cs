using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AudioPlayer : MonoBehaviour
{
    private static AudioPlayer instance;

    public static AudioPlayer _instance
    {
        get { return instance ?? (instance = (new GameObject("AudioPlayer").AddComponent<AudioPlayer>())); }
    }

    private List<Channel> channels = new List<Channel>();
    private List<AudioSource> allAudioSources = new List<AudioSource>();

    private void Awake()
    {
        // If we're a prefab, we set our instance
        instance = this;

        // Make sure we don't get destroyed
        DontDestroyOnLoad(gameObject);
    }

    #region Public functions
    #region Channels
    private class Audio
    {
        public Channel channel;
        public AudioSource audioSource;
        public float volume;
        public float pitch;
        public bool loop;

        public Audio(Channel channel, AudioSource audioSource, float volume, float pitch, bool loop)
        {
            this.channel = channel;
            this.audioSource = audioSource;
            this.volume = volume;
            this.pitch = pitch;
            this.loop = loop;
        }
    }

    private class Channel
    {
        public List<Audio> audioSources = new List<Audio>();

        public string name;
        public float volume;
        public float pitch;

        public Channel(string name, float volume = 1f, float pitch = 1f)
        {
            this.name = name;
            this.volume = volume;
            this.pitch = pitch;
        }
        
        public void AddAudioSource(AudioSource audioSource, float volume = 1f, float pitch = 1, bool loop = false)
        {
            foreach (Audio audio in audioSources)
                if (audio.audioSource == audioSource)
                    return;

            Audio newAudio = new Audio(this, audioSource, volume, pitch, loop);
            SetAudioValues(newAudio);
            audioSources.Add(newAudio);
        }

        public void RemoveAudioSource(AudioSource audioSource)
        {
            foreach (Audio audio in audioSources)
                if (audio.audioSource == audioSource)
                {
                    audioSources.Remove(audio);
                    break;
                }
        }

        public void UpdateChannel(float volume, float pitch)
        {
            this.volume = volume;
            this.pitch = pitch;

            foreach (Audio audio in audioSources)
                SetAudioValues(audio);
        }

        public void UpdateVolume(float volume)
        {
            UpdateChannel(volume, pitch);
        }

        public void UpdatePitch(float pitch)
        {
            UpdateChannel(volume, pitch);
        }

        public void SetAudioValues(Audio audio)
        {
            audio.audioSource.volume = audio.volume * volume;
            audio.audioSource.pitch = audio.pitch * pitch;
            audio.audioSource.loop = audio.loop;
        }

        public Audio GetAudio(AudioClip clip)
        {
            if (clip == null) return null;

            foreach (Audio audio in audioSources)
                if (audio.audioSource.clip == clip)
                    return audio;

            return null;
        }
    }

    private Channel GetChannel(string name)
    {
        // See if the channel does not already exist
        foreach (Channel channel in channels)
            if (channel.name == name)
                return channel;

        Channel newChannel = new Channel(name);
        channels.Add(newChannel);
        return newChannel;
    }

    public void CreateChannel(string name, float volume, float pitch)
    {
        Channel channel = GetChannel(name);
        channel.volume = volume;
        channel.pitch = pitch;
    }

    public void UpdateChannel(string name, float volume, float pitch)
    {
        GetChannel(name).UpdateChannel(volume, pitch);
    }

    public void UpdateChannelVolume(string name, float volume)
    {
        GetChannel(name).UpdateVolume(volume);
    }

    public void UpdateChannelPitch(string name, float pitch)
    {
        GetChannel(name).UpdatePitch(pitch);
    }

    public void RemoveChannel(string name)
    {
        channels.Remove(GetChannel(name));
    }
    #endregion

    #region Play Audio
    public void PlayAudio(string channelName, AudioClip clip, float volume, float pitch, bool loop = false)
    {
        // Null check
        if (clip == null) return;

        // Prepare audioSource
        AudioSource audioSource = GetAudioSource();
        audioSource.clip = clip;
        
        // Get the correct channel, and add the correct data
        Channel channel = GetChannel(channelName);
        channel.AddAudioSource(audioSource, volume, pitch, loop);

        // Play the audio
        audioSource.Play();
    }

    public void PlayAudio(string channelName, string audioName, float volume, float pitch, bool loop = false)
    {
        PlayAudio(channelName, ResourceLoader._instance.GetAsset<AudioClip>(audioName), volume, pitch, loop);
    }

    public void PlayAudio(AudioClip clip, float volume, float pitch, bool loop = false)
    {
        PlayAudio("_MiscAudioChannel", clip, volume, pitch, loop);
    }

    public void PlayAudio(string audioName, float volume, float pitch, bool loop = false)
    {
        PlayAudio(ResourceLoader._instance.GetAsset<AudioClip>(audioName), volume, pitch, loop);
    }

    public void PlayAudio(string channelName, AudioClip clip, float volume, bool loop = false)
    {
        PlayAudio(channelName, clip, volume, 1f, loop);
    }

    public void PlayAudio(string channelName, string audioName, float volume, bool loop = false)
    {
        PlayAudio(channelName, ResourceLoader._instance.GetAsset<AudioClip>(audioName), volume, loop);
    }

    public void PlayAudio(AudioClip clip, float volume, bool loop = false)
    {
        PlayAudio(clip, volume, 1f, loop);
    }

    public void PlayAudio(string audioName, float volume, bool loop = false)
    {
        PlayAudio(ResourceLoader._instance.GetAsset<AudioClip>(audioName), volume, loop);
    }

    public void PlayAudio(string channelName, AudioClip clip, bool loop = false)
    {
        PlayAudio(channelName, clip, 1f, 1f, loop);
    }

    public void PlayAudio(string channelName, string audioName, bool loop = false)
    {
        PlayAudio(channelName, ResourceLoader._instance.GetAsset<AudioClip>(audioName), loop);
    }

    public void PlayAudio(AudioClip clip, bool loop = false)
    {
        PlayAudio(clip, 1f, 1f, loop);
    }

    public void PlayAudio(string audioName, bool loop = false)
    {
        PlayAudio(ResourceLoader._instance.GetAsset<AudioClip>(audioName), loop);
    }
    #endregion
    #region Update Audio
    private void UpdateAudio(Channel channel, AudioClip clip, float volume, float pitch, bool loop)
    {
        // Null check
        if (channel == null) return;

        // Get variables
        Audio audio = channel.GetAudio(clip);
        if (audio == null) return;
        AudioSource audioSource = audio.audioSource;
        if (audioSource == null) return;

        // Update variables
        audio.volume = volume;
        audio.pitch = pitch;
        audioSource.loop = loop;

        // Update via channel
        channel.SetAudioValues(audio);
    }

    public void UpdateAudio(string channelName, AudioClip clip, float volume, float pitch, bool loop)
    {
        UpdateAudio(GetChannelByName(channelName), clip, volume, pitch, loop);
    }

    public void UpdateAudio(string channelName, string name, float volume, float pitch, bool loop)
    {
        AudioClip clip = ResourceLoader._instance.GetAsset<AudioClip>(name);
        UpdateAudio(GetChannelByName(channelName), clip, volume, pitch, loop);
    }

    public void UpdateAudio(AudioClip clip, float volume, float pitch, bool loop)
    {
        UpdateAudio(GetChannelFromAudio(clip), clip, volume, pitch, loop);
    }

    public void UpdateAudio(string name, float volume, float pitch, bool loop)
    {
        AudioClip clip = ResourceLoader._instance.GetAsset<AudioClip>(name);
        UpdateAudio(GetChannelFromAudio(clip), clip, volume, pitch, loop);
    }
    #endregion
    #region Stop Audio
    private void StopAudio(Channel channel, AudioClip clip)
    {
        // Get audioSource
        AudioSource audioSource = channel.GetAudio(clip).audioSource;

        // Null checks
        if (channel == null || clip == null || audioSource == null) return;

        // See if we should disable it
        if (audioSource.isPlaying)
            audioSource.Stop();

        // Remove the audio from the channel
        channel.RemoveAudioSource(audioSource);
    }

    public void StopAudio(string channelName, AudioClip clip)
    {
        StopAudio(GetChannelByName(channelName), clip);
    }

    public void StopAudio(string channelName, string audioName)
    {
        StopAudio(channelName, ResourceLoader._instance.GetAsset<AudioClip>(audioName));
    }

    public void StopAudio(AudioClip clip)
    {
        StopAudio(GetChannelFromAudio(clip), clip);        
    }

    public void StopAudio(string audioName)
    {
        StopAudio(ResourceLoader._instance.GetAsset<AudioClip>(audioName));
    }
    #endregion
    #endregion

    private AudioSource GetAudioSource()
    {
        // Loop through all available sources, and send one back if it's not playing
        for (int i = 0; i < allAudioSources.Count; i++)
            if (!allAudioSources[i].isPlaying)
                return allAudioSources[i];

        // Since none are available we add a new one and add it to the list
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        allAudioSources.Add(newSource);

        return newSource;
    }

    private Channel GetChannelByName(string name)
    {
        foreach (Channel channel in channels)
            if (channel.name == name)
                return channel;

        return null;
    }

    private Channel GetChannelFromAudio(AudioClip clip)
    {
        foreach (Channel channel in channels)
            if (channel.GetAudio(clip) != null)
                return channel;

        return null;
    }

    //private Audio GetAudioFromChannel(string channelName, AudioClip clip)
    //{
    //    foreach (Channel channel in channels)
    //        if (channel.name == channelName)
    //            return channel.GetAudio(clip);

    //    return null;        
    //}

    //private Audio GetAudioFromChannel(AudioClip clip)
    //{
    //    foreach (Channel channel in channels)
    //        if (channel.GetAudio(clip) != null)
    //            return channel.GetAudio(clip);

    //    return null;
    //}

    //private AudioSource GetSourceOfName(string name)
    //{
    //    // Loop through all audio sources, and return the one with the matching name
    //    for (int i = 0; i < allAudioSources.Count; i++)
    //        if (allAudioSources[i].clip.name == name)
    //            return allAudioSources[i];

    //    return null;
    //}

    //private AudioSource GetSourceOfClip(AudioClip clip)
    //{
    //    // Loop through all audio sources, and return the one with the matching clip
    //    for (int i = 0; i < allAudioSources.Count; i++)
    //        if (allAudioSources[i].clip == clip)
    //            return allAudioSources[i];

    //    return null;
    //}
}