// <copyright file="CrazyEventHub.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Plugins
{
    using System;

    using Ensage.SDK.Helpers;
    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;

    [ExportPlugin(
        "Crazy Event Hub",
        StartupMode.Manual,
        description: "CAN lead to high FPS reduction! - Publishes tons of data from Ensages event layer to Messenger<(EventName)EventArgs")]
    public class CrazyEventHub : Plugin
    {
        protected override void OnActivate()
        {
            Game.OnFireEvent += this.OnFireEvent;
            Game.OnGCMessageReceive += this.OnGCMessageReceive;
            Game.OnMessage += this.OnMessage;
            Game.OnUIStateChanged += this.OnUIStateChanged;
            Game.OnWndProc += this.OnWndProc;

            ObjectManager.OnAddEntity += this.OnAddEntity;
            ObjectManager.OnRemoveEntity += this.OnRemoveEntity;
            ObjectManager.OnAddLinearProjectile += this.OnAddLinearProjectile;
            ObjectManager.OnRemoveLinearProjectile += this.OnRemoveLinearProjectile;
            ObjectManager.OnAddTrackingProjectile += this.OnAddTrackingProjectile;
            ObjectManager.OnRemoveTrackingProjectile += this.OnRemoveTrackingProjectile;

            Player.OnExecuteOrder += this.OnExecuteOrder;
            Unit.OnModifierAdded += this.OnModifierAdded;
            Unit.OnModifierRemoved += this.OnModifierRemoved;
            Entity.OnAnimationChanged += this.OnAnimationChanged;
            Entity.OnParticleEffectAdded += this.OnParticleEffectAdded;
            Entity.OnHandlePropertyChange += this.OnHandlePropertyChange;
            Entity.OnBoolPropertyChange += this.OnBoolPropertyChange;
            Entity.OnFloatPropertyChange += this.OnFloatPropertyChange;
            Entity.OnInt32PropertyChange += this.OnInt32PropertyChange;
            Entity.OnInt64PropertyChange += this.OnInt64PropertyChange;
            Entity.OnStringPropertyChange += this.OnStringPropertyChange;
        }

        protected override void OnDeactivate()
        {
            Game.OnFireEvent -= this.OnFireEvent;
            Game.OnGCMessageReceive -= this.OnGCMessageReceive;
            Game.OnMessage -= this.OnMessage;
            Game.OnUIStateChanged -= this.OnUIStateChanged;
            Game.OnWndProc -= this.OnWndProc;

            ObjectManager.OnAddEntity -= this.OnAddEntity;
            ObjectManager.OnRemoveEntity -= this.OnRemoveEntity;
            ObjectManager.OnAddLinearProjectile -= this.OnAddLinearProjectile;
            ObjectManager.OnRemoveLinearProjectile -= this.OnRemoveLinearProjectile;
            ObjectManager.OnAddTrackingProjectile -= this.OnAddTrackingProjectile;
            ObjectManager.OnRemoveTrackingProjectile -= this.OnRemoveTrackingProjectile;

            Player.OnExecuteOrder -= this.OnExecuteOrder;
            Unit.OnModifierAdded -= this.OnModifierAdded;
            Unit.OnModifierRemoved -= this.OnModifierRemoved;
            Entity.OnAnimationChanged -= this.OnAnimationChanged;
            Entity.OnParticleEffectAdded -= this.OnParticleEffectAdded;
            Entity.OnHandlePropertyChange -= this.OnHandlePropertyChange;
            Entity.OnBoolPropertyChange -= this.OnBoolPropertyChange;
            Entity.OnFloatPropertyChange -= this.OnFloatPropertyChange;
            Entity.OnInt32PropertyChange -= this.OnInt32PropertyChange;
            Entity.OnInt64PropertyChange -= this.OnInt64PropertyChange;
            Entity.OnStringPropertyChange -= this.OnStringPropertyChange;
        }

        private void OnAddEntity(EntityEventArgs args)
        {
            Messenger<EntityEventArgs>.Publish(args);
        }

        private void OnAddLinearProjectile(LinearProjectileEventArgs args)
        {
            Messenger<LinearProjectileEventArgs>.Publish(args);
        }

        private void OnAddTrackingProjectile(TrackingProjectileEventArgs args)
        {
            Messenger<TrackingProjectileEventArgs>.Publish(args);
        }

        private void OnAnimationChanged(Entity sender, EventArgs args)
        {
            Messenger<Animation>.Publish(sender.Animation);
        }

        private void OnBoolPropertyChange(Entity sender, BoolPropertyChangeEventArgs args)
        {
            Messenger<BoolPropertyChangeEventArgs>.Publish(args);
        }

        private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            Messenger<ExecuteOrderEventArgs>.Publish(args);
        }

        private void OnFireEvent(FireEventEventArgs args)
        {
            Messenger<FireEventEventArgs>.Publish(args);
        }

        private void OnFloatPropertyChange(Entity sender, FloatPropertyChangeEventArgs args)
        {
            Messenger<FloatPropertyChangeEventArgs>.Publish(args);
        }

        private void OnGCMessageReceive(GCMessageEventArgs args)
        {
            Messenger<GCMessageEventArgs>.Publish(args);
        }

        private void OnHandlePropertyChange(Entity sender, HandlePropertyChangeEventArgs args)
        {
            Messenger<HandlePropertyChangeEventArgs>.Publish(args);
        }

        private void OnInt32PropertyChange(Entity sender, Int32PropertyChangeEventArgs args)
        {
            Messenger<Int32PropertyChangeEventArgs>.Publish(args);
        }

        private void OnInt64PropertyChange(Entity sender, Int64PropertyChangeEventArgs args)
        {
            Messenger<Int64PropertyChangeEventArgs>.Publish(args);
        }

        private void OnMessage(MessageEventArgs args)
        {
            Messenger<MessageEventArgs>.Publish(args);
        }

        private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
        {
            Messenger<ModifierChangedEventArgs>.Publish(args);
        }

        private void OnModifierRemoved(Unit sender, ModifierChangedEventArgs args)
        {
            Messenger<ModifierChangedEventArgs>.Publish(args);
        }

        private void OnParticleEffectAdded(Entity sender, ParticleEffectAddedEventArgs args)
        {
            Messenger<ParticleEffectAddedEventArgs>.Publish(args);
        }

        private void OnRemoveEntity(EntityEventArgs args)
        {
            Messenger<EntityEventArgs>.Publish(args);
        }

        private void OnRemoveLinearProjectile(LinearProjectileEventArgs args)
        {
            Messenger<LinearProjectileEventArgs>.Publish(args);
        }

        private void OnRemoveTrackingProjectile(TrackingProjectileEventArgs args)
        {
            Messenger<TrackingProjectileEventArgs>.Publish(args);
        }

        private void OnStringPropertyChange(Entity sender, StringPropertyChangeEventArgs args)
        {
            Messenger<StringPropertyChangeEventArgs>.Publish(args);
        }

        private void OnUIStateChanged(UIStateChangedEventArgs args)
        {
            Messenger<UIStateChangedEventArgs>.Publish(args);
        }

        private void OnWndProc(WndEventArgs args)
        {
            Messenger<WndEventArgs>.Publish(args);
        }
    }
}