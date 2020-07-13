using RimWorld;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Verse;

namespace Libraries.Buildings
{
    public class Building_Bookcase : Building_InternalStorage
    {
        public float dust = 0f;

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            map.GetComponent<MapComponent_Library>().RegisterBookshelf(this);
        }

        public override void DeSpawn(DestroyMode mode = DestroyMode.Vanish)
        {
            Map.GetComponent<MapComponent_Library>().DeRegisterBookshelf(this);
            base.DeSpawn(mode);
        }
        public void AddDust(float dustAmount)
        {
            dust += dust * Libraries.Settings.DustIncrementMultiplier;
        }

        public void CleanDust(out float timeTaken)
        {
            // 5 ticks per full number of dust, whatever that ends up being
            timeTaken = dust * 5;
        }

        public bool Dusty()
        {
            return !Libraries.Settings.DustyBookshelves || dust > Libraries.Settings.DustyBookshelfThreshold;
        }

        public override string GetInspectString()
        {
            StringBuilder s = new StringBuilder();
            string baseStr = base.GetInspectString();
            if (baseStr != "")
                s.AppendLine(baseStr);

            if (innerContainer.Count > 0)
                s.AppendLine($"Contains {innerContainer.Count}");

            s.AppendLine($"Capacity of {CompStorageGraphic.Props.fullthreshold}");
            return s.ToString().TrimEndNewlines();
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            foreach (Gizmo g in base.GetGizmos())
                yield return g;

            if (innerContainer.Count > 0)
            {
                yield return new Command_Action()
                {
                    defaultLabel = "Grab Book",
                    icon = ContentFinder<Texture2D>.Get("UI/Commands/LaunchReport", true),
                    defaultDesc = "Retrieve a book from a bookshelf",
                    action = delegate
                    {
                        ProcessInput();
                    }
                };
            }
        }

        public void ProcessInput()
        {
            List<FloatMenuOption> list = new List<FloatMenuOption>();
            if (innerContainer.Count != 0)
            {
                foreach (Book current in innerContainer)
                {
                    Func<Rect, bool> extraPartOnGUI = (Rect rect) => Widgets.InfoCardButton(rect.x + 5f, rect.y + (rect.height - 24f) / 2f, current);

                    list.Add(new FloatMenuOption(current.Label, () => TryDrop(current), MenuOptionPriority.Default, null, null, 29f, extraPartOnGUI, null));
                }
            }
            Find.WindowStack.Add(new FloatMenu(list));
        }
    }

}