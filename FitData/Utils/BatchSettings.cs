using System;
using System.IO;
using System.Text.Json;

namespace FitData.Utils
{
    public class BatchSettings
    {
        public string BatchId { get; set; } = "";
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string CenterCode { get; set; } = "";
        public string User { get; set; } = "";
        public string Type { get; set; } = "";

        private static string GetPath()
        {
            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config");
            Directory.CreateDirectory(dir);
            return Path.Combine(dir, "batchsettings.json");
        }

        public static BatchSettings Load()
        {
            var path = GetPath();
            if (!File.Exists(path)) return new BatchSettings();
            try
            {
                var txt = File.ReadAllText(path);
                return JsonSerializer.Deserialize<BatchSettings>(txt) ?? new BatchSettings();
            }
            catch
            {
                return new BatchSettings();
            }
        }

        public void Save()
        {
            var path = GetPath();
            var txt = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, txt);
        }
    }
}

