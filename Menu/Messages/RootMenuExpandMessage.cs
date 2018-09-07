// <copyright file="RootMenuExpand.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Messages
{
    public class RootMenuExpandMessage
    {
        public RootMenuExpandMessage(string mainMenuName)
        {
            this.MainMenuName = mainMenuName;
        }

        public string MainMenuName { get; }
    }
}