using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Hammerhand.Data
{
    public class Options
    {
        private bool _verticalSyncEnabled = true;
        private uint _frameRateLimit = 256;
        private bool _fullscreenEnabled = false;
        private uint _width = 960;
        private uint _height = 640;
        public bool VerticalSyncEnabled
        {
            get { return _verticalSyncEnabled; }
            set { Save(_verticalSyncEnabled = value); }
        }
        public uint FrameRateLimit
        {
            get { return _frameRateLimit; }
            set { Save(_frameRateLimit = value); }
        }
        public bool FullscreenEnabled {
            get { return _fullscreenEnabled; }
            set { Save(_fullscreenEnabled = value); }
        }
        public uint Width
        {
            get { return _width; }
            set { Save(_width = value); }
        }
        public uint Height
        {
            get { return _height; }
            set { Save(_height = value); }
        }
        public Options(bool verticalSyncEnabled = true, uint frameRateLimit = 256, bool fullscreenEnabled = false, uint width = 960, uint height = 640)
        {
            _verticalSyncEnabled = verticalSyncEnabled;
            _frameRateLimit = frameRateLimit;
            _fullscreenEnabled = fullscreenEnabled;
            _width = width; _height = height;
        }
        public T Save<T>(T value)
        {
            string json = JsonConvert.SerializeObject(this);
            File.WriteAllText("./options.json", json);
            return value;
        }
        public static Options Load()
        {
            if (File.Exists("./options.json"))
            {
                string json = File.ReadAllText("./options.json");
                return JsonConvert.DeserializeObject<Options>(json);
            }
            else
            {
                return new Options();
            }
        }
    }
}
