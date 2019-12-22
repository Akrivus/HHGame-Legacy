using SFML.Audio;
using SFML.Graphics;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;

namespace Hammerhand
{
    public class Assets
    {
        private ConcurrentDictionary<string, Sound> _sounds = new ConcurrentDictionary<string, Sound>();
        private ConcurrentDictionary<string, Texture> _images = new ConcurrentDictionary<string, Texture>();
        private ConcurrentDictionary<string, Music> _musics = new ConcurrentDictionary<string, Music>();
        private Assembly _assembly = Assembly.GetExecutingAssembly();
        public Assets()
        {
            foreach (string name in _assembly.GetManifestResourceNames()) {
                string path = name.Substring(24, name.Length - (24 + 4));
                string type = name.Substring(0, 23);
                Stream stream = GetStream(name);
                switch (type)
                {
                    case "Hammerhand.Assets.Sound":
                        _sounds.TryAdd(path, new Sound(new SoundBuffer(stream)));
                        break;
                    case "Hammerhand.Assets.Image":
                        _images.TryAdd(path, new Texture(new Image(stream)));
                        break;
                    case "Hammerhand.Assets.Music":
                        _musics.TryAdd(path, new Music(stream));
                        break;
                }
            }
        }
        public Sound GrabSound(string name, Sound value = null)
        {
            _sounds.TryGetValue(name, out value);
            return value;
        }
        public Music GrabMusic(string name, Music value = null)
        {
            _musics.TryGetValue(name, out value);
            return value;
        }
        public Texture GrabImage(string name, Texture value = null)
        {
            _images.TryGetValue(name, out value);
            return value;
        }
        private Stream GetStream(string path)
        {
            return _assembly.GetManifestResourceStream(path);
        }
    }
}
