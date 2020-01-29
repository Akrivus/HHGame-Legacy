using SFML.Audio;
using SFML.Graphics;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;

namespace HHGame
{
    public class Assets
    {
        private ConcurrentDictionary<string, Sound> _sounds = new ConcurrentDictionary<string, Sound>();
        private ConcurrentDictionary<string, Texture> _images = new ConcurrentDictionary<string, Texture>();
        private ConcurrentDictionary<string, Music> _musics = new ConcurrentDictionary<string, Music>();
        private Assembly _assembly = Assembly.GetExecutingAssembly();
        public Font Font;
        public Assets()
        {
            foreach (string name in _assembly.GetManifestResourceNames()) {
                if (name.Equals("HHGame.Assets.Font.ttf"))
                {
                    Font = new Font(GetStream(name));
                }
                else
                {
                    string path = name.Substring(20, name.Length - (20 + 4));
                    string type = name.Substring(0, 19);
                    Stream stream = GetStream(name);
                    switch (type)
                    {
                        case "HHGame.Assets.Sound":
                            _sounds.TryAdd(path, new Sound(new SoundBuffer(stream)));
                            break;
                        case "HHGame.Assets.Image":
                            _images.TryAdd(path, new Texture(new Image(stream)));
                            break;
                        case "HHGame.Assets.Music":
                            _musics.TryAdd(path, new Music(stream));
                            break;
                    }
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
