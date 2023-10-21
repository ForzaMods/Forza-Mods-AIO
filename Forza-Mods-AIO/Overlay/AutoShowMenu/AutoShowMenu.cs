﻿using System.Collections.Generic;

namespace Forza_Mods_AIO.Overlay.AutoShowMenu
{
    public class AutoShowMenu
    {
        public static readonly List<Overlay.MenuOption> AutoShowOptions = new List<Overlay.MenuOption>()
        {
            new Overlay.MenuOption("Autoshow Filters", "MenuButton"),
            new Overlay.MenuOption("Garage Modifications", "MenuButton"),
            new Overlay.MenuOption("Others Modifications", "MenuButton"),
        };

        public void InitiateSubMenu()
        {
            var AS_F = new SubMenus.AutoshowFilters();
            var GA_M = new SubMenus.GarageModifications();
            var OT_M = new SubMenus.OthersModifications();

            AS_F.Initalize();
            GA_M.Initalize();
            OT_M.Initalize();
        }
    }
}
