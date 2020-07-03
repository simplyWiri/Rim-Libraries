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
            Map map = Map;
            if (innerContainer.Count != 0)
            {
                foreach (Book current in innerContainer)
                {
                    string text = current.Label;
                    List<FloatMenuOption> arg_121_0 = list;
                    Func<Rect, bool> extraPartOnGUI = (Rect rect) => Widgets.InfoCardButton(rect.x + 5f, rect.y + (rect.height - 24f) / 2f, current);
                    arg_121_0.Add(new FloatMenuOption(text, delegate
                    {
                        TryDrop(current);
                    }, MenuOptionPriority.Default, null, null, 29f, extraPartOnGUI, null));
                }
            }
            Find.WindowStack.Add(new FloatMenu(list));
        }
    }

}