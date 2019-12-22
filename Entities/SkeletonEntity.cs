using Hammerhand.Client;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Hammerhand.Entities
{
    public class SkeletonEntity : PhysicalEntity
    {
        public int FullHeight { get { return TorsoHeight * 2 + HeadHeight; } }
        public float HeightOffset { get { return -0.5F * TorsoHeight + 6.5F; } }
        public int CeliacPoint { get { return TorsoHeight / 2 + 1; } }
        public const int HeadHeight = 13;
        public int TorsoHeight;
        public Vector2f Facing;
        public bool Walking = true;
        protected float RightArmRotation;
        protected float LeftArmRotation;
        protected float HeadRotation;
        protected float RightLegRotation;
        protected float LeftLegRotation;
        protected Sprite RightArm;
        protected Sprite RightLeg;
        protected Sprite Head;
        protected Sprite Torso;
        protected Sprite LeftLeg;
        protected Sprite LeftArm;
        private bool LeftLeading;
        public SkeletonEntity(Texture texture, Game _game, int torsoHeight) : base(_game)
        {
            TorsoHeight = torsoHeight;
            Bounds = new FloatRect(0, 0, 16, FullHeight);
            RightArm = new Sprite(texture, new IntRect(11, HeadHeight + 1, 4, TorsoHeight));
            RightArm.Origin = new Vector2f(-1, 0);
            RightLeg = new Sprite(texture, new IntRect(8, HeadHeight + TorsoHeight + 1, 5, TorsoHeight));
            RightLeg.Origin = new Vector2f(1, 0);
            Head = new Sprite(texture, new IntRect(0, 0, 16, HeadHeight));
            Head.Origin = new Vector2f(7, HeadHeight);
            Torso = new Sprite(texture, new IntRect(4, HeadHeight, 7, TorsoHeight + 1));
            Torso.Origin = new Vector2f(4, CeliacPoint);
            LeftLeg = new Sprite(texture, new IntRect(2, HeadHeight + TorsoHeight + 1, 5, TorsoHeight));
            LeftLeg.Origin = new Vector2f(3, 0);
            LeftArm = new Sprite(texture, new IntRect(0, HeadHeight + 1, 4, TorsoHeight));
            LeftArm.Origin = new Vector2f(4, 0);
            CanBounce = true;
        }
        protected override void OnUpdate(GameWindow.Priority priority, ConcurrentQueue<QueuedEntity> queue)
        {
            base.OnUpdate(priority, queue);
            if (Walking)
            {
                Accelerate(GetScaleFromFace() * GetWalkingSpeed(), 0);
                RightArmRotation = -RightLegRotation;
                LeftArmRotation = -LeftLegRotation;
                if (LeftLeading)
                {
                    RightLegRotation -= Game.FrameDelta;
                    LeftLegRotation = -RightLegRotation;
                    if (RightLegRotation < -15)
                    {
                        LeftLeading = false;
                    }
                }
                else
                {
                    RightLegRotation += Game.FrameDelta;
                    LeftLegRotation = -RightLegRotation;
                    if (RightLegRotation > 15)
                    {
                        LeftLeading = true;
                    }
                }
            }
            else
            {
                if (RightLegRotation > 1)
                {
                    RightLegRotation -= Game.FrameDelta;
                }
                if (RightLegRotation < 1)
                {
                    RightLegRotation += Game.FrameDelta;
                }
                if (LeftLegRotation > 1)
                {
                    LeftLegRotation -= Game.FrameDelta;
                }
                if (LeftLegRotation < 1)
                {
                    LeftLegRotation += Game.FrameDelta;
                }
            }
        }
        protected override void OnDraw(GameWindow window, GameWindow.Priority priority)
        {
            RightArm.Position = new Vector2f(Position.X + 2, Position.Y - CeliacPoint + HeightOffset + 2);
            RightArm.Rotation = RightArmRotation;
            RightLeg.Position = new Vector2f(Position.X + 1, Position.Y + CeliacPoint + HeightOffset);
            RightLeg.Rotation = RightLegRotation;
            if (RightLeg.Rotation != 0)
            {
                RightLeg.Scale = new Vector2f(GetScaleFromFace(), 1.0F);
                if (RightLeg.Scale.X < 0.0F)
                {
                    RightLeg.Position = new Vector2f(Position.X + 2, Position.Y + CeliacPoint + HeightOffset);
                }
            }
            else
            {
                RightLeg.Position = new Vector2f(Position.X + 1, Position.Y + CeliacPoint + HeightOffset);
            }
            Head.Position = new Vector2f(Position.X - 1, Position.Y - CeliacPoint + HeightOffset + 1);
            Head.Rotation = HeadRotation;
            Head.Scale = new Vector2f(GetScaleFromFace(), 1.0F);
            if (Head.Scale.X < 0.0F)
            {
                Head.Position = new Vector2f(Position.X, Position.Y - CeliacPoint + HeightOffset + 1);
            }
            Torso.Position = new Vector2f(Position.X, Position.Y + 1 + HeightOffset);
            LeftLeg.Position = new Vector2f(Position.X - 3, Position.Y + CeliacPoint + HeightOffset);
            LeftLeg.Rotation = LeftLegRotation;
            if (LeftLeg.Rotation != 0)
            {
                LeftLeg.Scale = new Vector2f(GetScaleFromFace() * -1.0F, 1.0F);
                if (LeftLeg.Scale.X > 0.0F)
                {
                    LeftLeg.Position = new Vector2f(Position.X - 3, Position.Y + CeliacPoint + HeightOffset);
                }
            }
            LeftArm.Position = new Vector2f(Position.X - 4, Position.Y - CeliacPoint + HeightOffset + 2);
            LeftArm.Rotation = LeftArmRotation;
            if (GetScaleFromFace() > 0)
            {
                window.Draw(RightArm);
                window.Draw(RightLeg);
            }
            else
            {
                window.Draw(LeftLeg);
                window.Draw(LeftArm);
            }
            window.Draw(Head);
            window.Draw(Torso);
            if (GetScaleFromFace() > 0)
            {
                window.Draw(LeftArm);
                window.Draw(LeftLeg);
            }
            else
            {
                window.Draw(RightLeg);
                window.Draw(RightArm);
            }
        }
        protected virtual float GetWalkingSpeed()
        {
            return 0.03F;
        }
        private float GetScaleFromFace()
        {
            if (Torso.Position.X > Facing.X)
            {
                return -1.0F;
            }
            else
            {
                return 1.0F;
            }
        }
    }
}
