using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an behemoth corpse")] 
    public class Behemoth : BaseCreature
    {
        [Constructable]
        public Behemoth()   // using this mob as an template right now (All stats/skills and everything is taken from Fantasia scripts)
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4) // 0.2 is standard apparently
        //  : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.4, 0.4) // not sure about 0.4 or 02, havent had time to test
        {
            this.Name = "an behemoth";
            this.Body = 0x0e;
            this.BaseSoundID = 268;

            this.Hue = 0x488;

            this.SetStr(2250);
            this.SetDex(400);
            this.SetInt(55);

            this.SetHits(2250);
            this.SetMana(55);
            this.SetStam(400);

          //  this.SetDamage(50);
            this.SetDiceDmg(10, 6);

            this.SetResistanceLevel(ResistanceType.Physical, 3);
            this.SetResistanceLevel(ResistanceType.Earth, 3);
            this.SetResistanceLevel(ResistanceType.Fire, 3);
            this.SetResistanceLevel(ResistanceType.Cold, 3); // Cold == Water
            this.SetResistanceLevel(ResistanceType.Necro, 3);

            //this.SetResistanceImmunity(ResistanceType.Poison); // just found out this existed: public override Poison PoisonImmune

            // this.SetResistanceLevel(ResistanceType.Magic, 3); // JustZH not done yet, Behemoth in fantasia scripts have (PermMagicProtection i6)

            /*
            this.SetDamageType(ResistanceType.Physical, 100);

            this.SetResistance(ResistanceType.Physical, 50, 60);
            this.SetResistance(ResistanceType.Fire, 50, 60);
            this.SetResistance(ResistanceType.Cold, 50, 60);
            this.SetResistance(ResistanceType.Poison, 50, 60);
            this.SetResistance(ResistanceType.Energy, 50, 60);
             */

            this.SetSkill(SkillName.DetectHidden, 100);
            this.SetSkill(SkillName.MagicResist, 200);
            this.SetSkill(SkillName.Tactics, 175);
            this.SetSkill(SkillName.Wrestling, 175);

            this.Fame = 3500; // Fame and Karma are not defined in npcdesc.cfg (the cfg file which contains all mobs), gotta find the script for this somewhere.
            this.Karma = -3500;

            // saw an awesome "AI Setting" in the scripts for behemoth (	AISetting	OpenDoors	i1 ), awesoooome, i guess the bastard can open doors

            this.VirtualArmor = 50; 
          //  this.ControlSlots = 2;
        }

        public Behemoth(Serial serial)
            : base(serial)
        {
        }

        public override double DispelDifficulty
        {
            get
            {
                return 117.5;
            }
        }
        public override double DispelFocus
        {
            get
            {
                return 45.0;
            }
        }
        public override bool BleedImmune
        {
            get
            {
                return true;
            }
        }
        public override Poison PoisonImmune // hmmm didnt know this existed, added
        {
            get
            {
                return Poison.Deadly;
            }
        }
        public override int TreasureMapLevel // not done (dunno what it even means)
        {
            get
            {
                return 1;
            }
        }
        public override void GenerateLoot() // not done
        {
            this.AddLoot(LootPack.Average);
            this.AddLoot(LootPack.AosUltraRich);
            this.AddLoot(LootPack.Gems);
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