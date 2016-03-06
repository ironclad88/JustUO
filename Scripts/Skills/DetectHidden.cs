using System;
using Server.Factions;
using Server.Mobiles;
using Server.Multis;
using Server.Targeting;

namespace Server.SkillHandlers
{
    public class DetectHidden
    {
        public static void Initialize()
        {
            SkillInfo.Table[(int)SkillName.DetectHidden].Callback = new SkillUseCallback(OnUse);
        }

        public static TimeSpan OnUse(Mobile src)
        {
            src.SendLocalizedMessage(500819);//Where will you search?
            src.Target = new InternalTarget();

            return TimeSpan.FromSeconds(7.0);
        }

        private class InternalTarget : Target
        {
            private double trg;

            public InternalTarget()
                : base(12, true, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile src, object targ)
            {
                bool foundAnyone = false;

                Point3D p;
                if (targ is Mobile)
                    p = ((Mobile)targ).Location;
                else if (targ is Item)
                    p = ((Item)targ).Location;
                else if (targ is IPoint3D)
                    p = new Point3D((IPoint3D)targ);
                else 
                    p = src.Location;

                int range;
                double srcSkill = src.Skills[SkillName.DetectHidden].Value;
                if(src.SpecClasse == SpecClasse.Thief) // better range for specced theifs
                {
                    range = (int)(srcSkill / 8.0) + 5;
                }
                range = (int)(srcSkill / 9.0);

                if (!src.CheckSkill(SkillName.DetectHidden, 0.0, 130.0))
                    range /= 2;

                BaseHouse house = BaseHouse.FindHouseAt(p, src.Map, 16);

                bool inHouse = (house != null && house.IsFriend(src));

                if (inHouse)
                    range = 22;

                if (range > 0)
                {
                    IPooledEnumerable inRange = src.Map.GetMobilesInRange(p, range);

                    foreach (Mobile trg in inRange)
                    {
                        if (trg.Hidden && src != trg)
                        {
                            double ss;
                            double ts;

                            ss = srcSkill + Utility.Random(21) - 10;
                            ts = trg.Skills[SkillName.Hiding].Value + Utility.Random(21) - 10;

                            if (src.SpecClasse == SpecClasse.Thief) // better range for specced theifs
                            {
                                switch (src.SpecLevel)
                                {
                                    case 1:
                                        ss = srcSkill + Utility.Random(25) - 10;
                                        break;
                                    case 2:
                                        ss = srcSkill + Utility.Random(27) - 10;
                                        break;
                                    case 3:
                                        ss = srcSkill + Utility.Random(29) - 10;
                                        break;
                                    case 4:
                                        ss = srcSkill + Utility.Random(31) - 10;
                                        break;
                                    case 5:
                                        ss = srcSkill + Utility.Random(35) - 10;
                                        break;
                                    case 6:
                                        ss = srcSkill + Utility.Random(40) - 10;
                                        break;
                                }
                               // ss = srcSkill + Utility.Random(21) - 10;
                            }
                            if (trg.SpecClasse == SpecClasse.Thief) // better range for specced theifs
                            {
                                switch (trg.SpecLevel)
                                {
                                    case 1:
                                        this.trg = srcSkill + Utility.Random(25) - 10;
                                        break;
                                    case 2:
                                        this.trg = srcSkill + Utility.Random(27) - 10;
                                        break;
                                    case 3:
                                        this.trg = srcSkill + Utility.Random(29) - 10;
                                        break;
                                    case 4:
                                        this.trg = srcSkill + Utility.Random(31) - 10;
                                        break;
                                    case 5:
                                        this.trg = srcSkill + Utility.Random(35) - 10;
                                        break;
                                    case 6:
                                        this.trg = srcSkill + Utility.Random(40) - 10;
                                        break;
                                }
                            }

                            

                            if (src.AccessLevel >= trg.AccessLevel && (ss >= ts || (inHouse && house.IsInside(trg))))
                            {
                                if (trg is ShadowKnight && (trg.X != p.X || trg.Y != p.Y))
                                    continue;

                                trg.RevealingAction();
                                trg.SendLocalizedMessage(500814); // You have been revealed!
                                foundAnyone = true;
                            }
                        }
                    }

                    inRange.Free();

                    if (Faction.Find(src) != null)
                    {
                        IPooledEnumerable itemsInRange = src.Map.GetItemsInRange(p, range);

                        foreach (Item item in itemsInRange)
                        {
                            if (item is BaseFactionTrap)
                            {
                                BaseFactionTrap trap = (BaseFactionTrap)item;

                                if (src.CheckTargetSkill(SkillName.DetectHidden, trap, 80.0, 100.0))
                                {
                                    src.SendLocalizedMessage(1042712, true, " " + (trap.Faction == null ? "" : trap.Faction.Definition.FriendlyName)); // You reveal a trap placed by a faction:

                                    trap.Visible = true;
                                    trap.BeginConceal();

                                    foundAnyone = true;
                                }
                            }
                        }

                        itemsInRange.Free();
                    }
                }

                if (!foundAnyone)
                {
                    src.SendLocalizedMessage(500817); // You can see nothing hidden there.
                }
            }
        }
    }
}