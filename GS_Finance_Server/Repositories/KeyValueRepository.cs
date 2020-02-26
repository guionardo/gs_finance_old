using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using GS_Finance_Server.Interfaces.Repositories;

namespace GS_Finance_Server.Repositories
{
    public class KeyValueRepository : IKeyValueRepository
    {
        Dictionary<string, Dictionary<string, KeyData>> contexts =
            new Dictionary<string, Dictionary<string, KeyData>>();

        private const string file_repo = "key_value_repository.json";
        private int setCount = 0;

        public KeyValueRepository()
        {
            Load();
        }

        ~KeyValueRepository()
        {
            Save();
        }

        public bool Set(string key, string value, string context = null, long expiresIn = 0)
        {
            context ??= "_";
            if (!contexts.ContainsKey(context))
                contexts[context] = new Dictionary<string, KeyData>();
            expiresIn = expiresIn > 0 ? DateTimeOffset.UtcNow.ToUnixTimeSeconds() + expiresIn : 0;

            contexts[context][key] = new KeyData(value, expiresIn);
            setCount++;
            if (setCount > 0)
            {
                Save();
                setCount = 0;
            }

            return true;
        }

        public string Get(string key, string context = null, string defaultValue = null)
        {
            context ??= "_";
            if (contexts.ContainsKey(context) &&
                contexts[context].ContainsKey(key))
            {
                var data = contexts[context][key];
                if (data.IsValid())
                    return data.value;
            }

            return defaultValue;
        }

        public void Purge()
        {
            foreach (var context in contexts.Keys)
            {
                var removed = new List<string>();
                foreach (var data in contexts[context])
                {
                    if (!data.Value.IsValid())
                        removed.Add(data.Key);
                }

                foreach (var key in removed)
                {
                    contexts[context].Remove(key);
                }
            }
        }

        void Load()
        {
            if (File.Exists(file_repo))
            {
                contexts.Clear();
                try
                {
                    foreach (var line in File.ReadLines(file_repo))
                    {
                        var data = line.Split('|');
                        if (data.Length == 4)
                        {
                            var context = data[0];
                            var key = data[1];
                            var validUntil = long.Parse(data[2]);
                            var value = data[3];

                            if (!contexts.ContainsKey(context))
                                contexts[context] = new Dictionary<string, KeyData>();

                            contexts[context][key] = new KeyData(value, validUntil);
                        }
                    }
                }
                catch
                {
                    //
                }
            }
        }

        void Save()
        {
            using var f = File.CreateText(file_repo);
            foreach (var context in contexts.Keys)
            {
                foreach (var data in contexts[context])
                {
                    var line = context + "|" + data.Key + "|" + data.Value.validUntil.ToString() + "|" +
                               data.Value.value;
                    f.WriteLine(line);
                }
            }
        }

        [Serializable]
        public class KeyData
        {
            public long validUntil;
            public string value;

            public KeyData(string value, long validUntil)
            {
                this.value = value;
                this.validUntil = Math.Max(0, validUntil);
            }

            public bool IsValid() =>
                validUntil <= 0 || validUntil < DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }
}