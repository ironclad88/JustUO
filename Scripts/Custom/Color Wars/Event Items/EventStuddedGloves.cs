////////////////////////////////////////////////////////////////////////////////////////
/////                                                                            ////////
//////    Version: 1.0   Original Author: Pillzan                                 ////////
///////                                                                            ////////
////////  Email: oscar.thernstrom@hotmail.com                                       ////////
////////                                                                            ////////
////////                                                                             ////////
////////                                                                            ////////
////////    Distribution: This script can be freely distributed, as long as the     ////////
////////                  credit notes are left intact.	This script can also be     ////////
////////                  modified, as long as the credit notes are left intact.    ///////
////////                                                                            //////
/////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////

using System;
using Server.Mobiles;
using Server;

namespace Server.Items
{
    public class EventStuddedGloves : StuddedGloves
    {
        public override int BasePhysicalResistance { get { return 5; } }
        public override int BaseFireResistance { get { return 5; } }
        public override int BaseColdResistance { get { return 5; } }
        public override int BasePoisonResistance { get { return 5; } }
        public override int BaseEnergyResistance { get { return 5; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }


        [Constructable]
        public EventStuddedGloves(int hue)
        {

            Hue = hue;
            Name = "Event Studded Leather Gloves";
            LootType = LootType.Blessed;
        }

        // Event Item Steal Protection
        public override bool OnEquip(Mobile from)
        {
            if (from.Player)
            {
                PlayerMobile pm = from as PlayerMobile;

                if (pm.IsInEvent != true)
                {
                    if (pm.AccessLevel >= AccessLevel.GameMaster)
                    {
                        return true;
                    }
                    else
                        from.SendMessage("Oh cool one of these event Items!");
                    this.Delete();

                    return false;
                }
                else
                {
                    return true;
                }


            }
            else
            {
                return false;
            }
        }

        public override bool OnDroppedToMobile(Mobile from, Mobile target)
        {
            if (from.Player)
            {
                PlayerMobile pm = from as PlayerMobile;

                if (pm.IsInEvent != true)
                {
                    from.SendMessage("Oh cool one of these Event Items!");
                    this.Delete();
                    return false;
                }
                else
                {
                    return true;
                }


            }
            else
            {
                return false;
            }
        }

        public override bool OnDroppedToWorld(Mobile from, Point3D p)
        {
            if (from.Player)
            {
                PlayerMobile pm = from as PlayerMobile;

                if (pm.IsInEvent != true)
                {
                    if (pm.AccessLevel >= AccessLevel.GameMaster)
                    {
                        return true;
                    }
                    else
                        from.SendMessage("Oh cool one of these Event Items!");
                    this.Delete();
                    return false;

                }
                else
                {
                    return true;
                }


            }
            else
            {
                return false;
            }
        }

        // And to really make sure nothing will ever happen....

        public override bool OnDragLift(Mobile from)
        {

            if (from.Player)
            {
                PlayerMobile pm = from as PlayerMobile;

                if (pm.IsInEvent != true)
                {
                    if (pm.AccessLevel >= AccessLevel.GameMaster)
                    {
                        return true;
                    }
                    else
                        from.SendMessage("Oh cool one of these Event Items!");
                    this.Delete();
                    return false;

                }
                else
                {
                    return true;
                }


            }
            else
            {
                return false;
            }

        }
        // End :>

        public EventStuddedGloves(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
