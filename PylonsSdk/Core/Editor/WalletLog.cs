using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PylonsSdk.Editor
{
    public static class WalletLog
    {
        public readonly struct LogEntry
        {
            [JsonProperty("evt")]
            public readonly string Event;
            [JsonProperty("msg")]
            public readonly string Message;
            [JsonProperty("tag")]
            public readonly string Tag;
            [JsonProperty("timestamp")]
            public readonly long Timestamp;

            public LogEntry(string @event, string message, string tag, long timestamp)
            {
                Event = @event;
                Message = message;
                Tag = tag;
                Timestamp = timestamp;
            }
        }

        static readonly string logPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\pylons\\log.txt";
        public static IReadOnlyList<LogEntry> Entries { get { PopulateList(); return _entries; } }
        private static long logFileStreamPosition;

        private static List<LogEntry> _entries = new List<LogEntry>();

        private static void PopulateList()
        {

            using (var filestream = File.Open(logPath, FileMode.Open)) using (var sr = new StreamReader(filestream))
            {
                // Log can potentially get lengthy, so we don't want to have to process log entries more than once
                filestream.Position = logFileStreamPosition;
                var newLines = sr.ReadToEnd();
                logFileStreamPosition = filestream.Position;
                var split = newLines.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in split)
                {
                    try { _entries.Add(JsonConvert.DeserializeObject<LogEntry>(line)); }
                    catch (Exception e)
                    {
                        Debug.LogWarning($"WARNING: Failed to deserialize entry in pylons\\log.txt: {line}" + Environment.NewLine +
                            $"Threw exception: {e.Message}" + Environment.NewLine +
                            $"Stack trace: {e.StackTrace}");
                    }
                }
            }
        }
    }
}