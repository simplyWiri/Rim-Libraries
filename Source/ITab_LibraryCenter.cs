using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Libraries
{
    class ITab_LibraryCenter : ITab
    {
        int CurrentTab = 0;
        public override void UpdateSize()
        {
            base.UpdateSize();
            size = new UnityEngine.Vector2(500, 500);
        }

        public ITab_LibraryCenter()
        {
            labelKey = "RL_LibraryCenter";
        }

        public override void FillTab()
        {
            UpdateSize();
            Draw(new Rect(0f, 0f, 500, 485));
        }

        public void Draw(Rect rect)
        {
            rect.y += 35f;

            var list = new List<TabRecord>();
            list.Add(new TabRecord("studyallocation".TranslateSimple(), () => CurrentTab = 0, CurrentTab == 0));
            list.Add(new TabRecord("policies".TranslateSimple(), () => CurrentTab = 1, CurrentTab == 1));

            TabDrawer.DrawTabs(rect.ContractedBy(15f), list, rect.width * .9f);

            if (CurrentTab == 0) DrawStudyAllocation(rect);
            else DrawPolicy(rect);
        }

        public void DrawStudyAllocation(Rect rect)
        {

        }

        public void DrawPolicy(Rect rect)
        {

        }
    }
}
