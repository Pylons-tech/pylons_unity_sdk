using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PylonsSdk.Editor
{
    public class WalletLogViewer : EditorWindow
    {
        public enum LogSort
        {
            OldestToNewest,
            NewestToOldest
        }

        [Flags]
        public enum LogTag
        {
            INFO = 1,
            CORE_ERROR = 1 << 1,
            WALLET_ERROR = 1 << 2,
            EXTERNAL_ERROR = 1 << 3,
            UNREGISTERED_LOG_TAG = 1 << 31
        }


        private Vector2 scrollPos;
        private LogSort logSort = LogSort.NewestToOldest;
        private LogTag logTag = LogTag.INFO | LogTag.CORE_ERROR | LogTag.WALLET_ERROR | LogTag.EXTERNAL_ERROR;
        private DateTime oldest;
        private DateTime newest = DateTime.Now;
        private AnimBool showOldestDateField;
        private AnimBool showNewestDateField;

        void OnEnable()
        {
            oldest = Util.UnixEpoch;
            titleContent = new GUIContent("Wallet Log Viewer");
            showOldestDateField = new AnimBool(false);
            showOldestDateField.valueChanged.AddListener(Repaint);
            showNewestDateField = new AnimBool(false);
            showNewestDateField.valueChanged.AddListener(Repaint);
        }

        [MenuItem("Pylons/Wallet/Log Viewer")]
        static void Init() => GetWindow<WalletLogViewer>().Show();

        const int SINGLE_ENTRY_HEIGHT = 64;

        void OnGUI()
        {
            GUILayout.Label("Wallet Log Viewer", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal(); // 1
            GUILayout.Label("Sort by: ");
            logSort = (LogSort)EditorGUILayout.EnumPopup(logSort);
            GUILayout.EndHorizontal(); // 0
            GUILayout.BeginHorizontal(); // 1
            GUILayout.Label("Include tags: ");
            logTag = (LogTag)EditorGUILayout.EnumFlagsField(logTag);
            GUILayout.EndHorizontal(); // 0
            showOldestDateField.target = EditorGUILayout.ToggleLeft("Filter for newer than: ", showOldestDateField.target);
            if (EditorGUILayout.BeginFadeGroup(showOldestDateField.faded)) oldest = LayoutDateFields(oldest, oldest); // 1
            EditorGUILayout.EndFadeGroup(); // 0
            showNewestDateField.target = EditorGUILayout.ToggleLeft("Filter for older than: ", showNewestDateField.target);
            if (EditorGUILayout.BeginFadeGroup(showNewestDateField.faded)) newest = LayoutDateFields(newest, DateTime.Now); // 1
            EditorGUILayout.EndFadeGroup(); // 0
            EditorGUILayout.Separator();
            scrollPos = GUILayout.BeginScrollView(scrollPos); // 1
            var entries = WalletLog.Entries;
            var scrollBase = (scrollPos.y / SINGLE_ENTRY_HEIGHT) - 1;
            if (scrollBase < 0) scrollBase = 0;
            var scrollEdge = (scrollPos.y / SINGLE_ENTRY_HEIGHT) + 12;
            if (scrollEdge > entries.Count * SINGLE_ENTRY_HEIGHT) scrollEdge = entries.Count * SINGLE_ENTRY_HEIGHT;
            for (int i = 0; i < entries.Count; i++)
                {
                    if (i >= scrollBase && i <= scrollEdge)
                    {
                        if (logSort == LogSort.NewestToOldest) LayoutSingleLogEntry(entries[entries.Count - i - 1]);
                        else LayoutSingleLogEntry(entries[i]);
                    }
                    else GUILayout.Space(SINGLE_ENTRY_HEIGHT);
                }
            GUILayout.EndScrollView(); // 0
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Top")) scrollPos = Vector2.zero;
            if (GUILayout.Button("Bottom")) scrollPos = new Vector2(0, (scrollEdge + 1) * SINGLE_ENTRY_HEIGHT);
            GUILayout.EndHorizontal();
        }

        private DateTime LayoutDateFields(DateTime date, DateTime defaultVal)
        {
            GUILayout.BeginVertical(); // 1
                                       // Begin time area
            GUILayout.BeginVertical(); // 2

            GUILayout.Label("Time", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal(); // 1
            var inputHour = EditorGUILayout.IntField(date.Hour, GUILayout.Width(28));
            GUILayout.Label(":", GUILayout.Width(16));
            var inputMin = EditorGUILayout.IntField(date.Minute, GUILayout.Width(28));
            GUILayout.Label(":", GUILayout.Width(16));
            var inputSec = EditorGUILayout.IntField(date.Second, GUILayout.Width(28));
            GUILayout.Label(":", GUILayout.Width(16));
            var inputMilli = EditorGUILayout.IntField(date.Millisecond, GUILayout.Width(42));
            GUILayout.EndHorizontal(); // 0
                                       // End time area
            GUILayout.EndVertical();  // 1
                                      // Separate time and date areas
            EditorGUILayout.Separator();
            // Begin date area
            GUILayout.BeginVertical(); // 2
            GUILayout.Label("Date", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal(); // 1
                                         // Days field
            GUILayout.Label("dd: ", GUILayout.Width(28));
            var inputDay = EditorGUILayout.IntField(date.Day, GUILayout.Width(28));
            // Months field
            GUILayout.Label("mm: ", GUILayout.Width(28));
            var inputMonth = EditorGUILayout.IntField(date.Month, GUILayout.Width(28));
            // Years field
            GUILayout.Label("yyyy: ", GUILayout.Width(42));
            var inputYear = EditorGUILayout.IntField(date.Year, GUILayout.Width(42));
            // End date area
            GUILayout.EndHorizontal(); // 0
            GUILayout.EndVertical(); // 1
                                     // Finished w/ ui section
            GUILayout.EndVertical(); // 0
            try { return new DateTime(inputYear, inputMonth, inputDay, inputHour, inputMin, inputSec, inputMilli); }
            catch { return defaultVal; }
        }

        private void LayoutSingleLogEntry(WalletLog.LogEntry entry)
        {
            var rect = EditorGUILayout.BeginVertical(GUILayout.Height(SINGLE_ENTRY_HEIGHT));
            if (!Enum.TryParse(entry.Tag, out LogTag entryTag)) entry = FallbackLogAttempt(entry);
            var dateTime = Util.GetDateTimeFromUnixTimestamp(entry.Timestamp);
            // Filter logic
            if (showOldestDateField.value && dateTime < oldest) return;
            if (showNewestDateField.value && dateTime > newest) return;
            if (!logTag.HasFlag(entryTag)) return;
            // UI
            GUILayout.BeginVertical(GUILayout.Height(SINGLE_ENTRY_HEIGHT)); // 1
            GUILayout.Label($"{entry.Tag} | {dateTime}", EditorStyles.boldLabel);
            GUILayout.TextArea(JsonConvert.SerializeObject(entry.Message));
            GUILayout.EndVertical();  // 0
            GUILayout.EndVertical();
            EditorGUILayout.Separator();
        }

        private WalletLog.LogEntry FallbackLogAttempt (WalletLog.LogEntry entry) => 
            new WalletLog.LogEntry(entry.Event, JsonConvert.SerializeObject(entry), Enum.GetName(typeof(LogTag), LogTag.UNREGISTERED_LOG_TAG), entry.Timestamp);
    }
}