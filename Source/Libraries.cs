using UnityEngine;
using Verse;

namespace Libraries
{
    public class Libraries : Mod
    {
        public static Settings Settings;


        private HarmonyLib.Harmony harmony;

        public HarmonyLib.Harmony Instance
        {
            get
            {
                if (harmony == null)
                    harmony = new HarmonyLib.Harmony("wiri.libraries");
                return harmony;
            }
        }

        public Libraries(ModContentPack modContent) : base(modContent)
        {
            Settings = GetSettings<Settings>();

            Instance.PatchAll();
        }

        public override string SettingsCategory()
        {
            return "Rim Libraries";
        }

        public override void DoSettingsWindowContents(Rect rect)
        {
            Settings.ShowWindowContents(rect);
        }
    }
}