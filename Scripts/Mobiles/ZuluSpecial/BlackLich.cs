using Server.Items;
using Server.Spells.Fifth;
using Server.Spells.Seventh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Mobiles.ZuluSpecial
{
    class BlackLich : BaseCreature
    {
        [Constructable]
        public BlackLich()
            : base(AIType.AI_NecroMage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            this.SpeechHue = Utility.RandomDyedHue();
            this.Hue = 0x2C3;
            this.Female = false;
            this.BodyValue = 24;
            this.BaseSoundID = 1001;
            this.Name = "a black lich";

            this.SetStr(416, 505);
            this.SetDex(146, 165);
            this.SetInt(566, 655);

            this.SetHits(2000);
            this.SetMana(2000);

            this.SetDamage(10, 15);

            this.SetDamageType(ResistanceType.Physical, 70);
            this.SetDamageType(ResistanceType.Cold, 15);
            this.SetDamageType(ResistanceType.Energy, 15);

            this.SetResistance(ResistanceType.Physical, 40, 50);
            this.SetResistance(ResistanceType.Fire, 30, 40);
            this.SetResistance(ResistanceType.Cold, 50, 60);
            this.SetResistance(ResistanceType.Poison, 50, 60);
            this.SetResistance(ResistanceType.Energy, 40, 50);

            this.SetSkill(SkillName.EvalInt, 77.6, 87.5);
            this.SetSkill(SkillName.Necromancy, 100.6, 120.5);
            this.SetSkill(SkillName.SpiritSpeak, 110.1, 120.5);
            this.SetSkill(SkillName.Magery, 90.1, 100.1);
            this.SetSkill(SkillName.Poisoning, 80.5);
            this.SetSkill(SkillName.Meditation, 110.0);
            this.SetSkill(SkillName.MagicResist, 80.1, 85.0);
            this.SetSkill(SkillName.Parry, 90.1, 95.1);
            this.SetSkill(SkillName.Tactics, 120.0);
            this.SetSkill(SkillName.Wrestling, 70.1, 80.0);

            this.Fame = 24000;
            this.Karma = -24000;

            this.VirtualArmor = 65;

            this.PackNecroReg(50, 100);
            this.PackGold(5500, 8000);
            this.PackItem(new BlackLichStaff());

        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.MedScrolls, 4);
        }

        public override bool CanRummageCorpses { get { return true; } }
        public override bool AlwaysMurderer { get { return true; } }
        public override bool Unprovokable { get { return true; } }
        public override bool Uncalmable { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Lethal; } }
        public override int TreasureMapLevel { get { return 5; } }

        public void Polymorph(Mobile m)
        {
            if (!m.CanBeginAction(typeof(PolymorphSpell)) || !m.CanBeginAction(typeof(IncognitoSpell)) || m.IsBodyMod)
                return;

            IMount mount = m.Mount;

            if (mount != null)
                mount.Rider = null;

            if (m.Mounted)
                return;

            if (m.BeginAction(typeof(PolymorphSpell)))
            {
                Item disarm = m.FindItemOnLayer(Layer.OneHanded);

                if (disarm != null && disarm.Movable)
                    m.AddToBackpack(disarm);

                disarm = m.FindItemOnLayer(Layer.TwoHanded);

                if (disarm != null && disarm.Movable)
                    m.AddToBackpack(disarm);

                m.BodyMod = 50;
                m.HueMod = 0;

                new ExpirePolymorphTimer(m).Start();
            }
        }

        private class ExpirePolymorphTimer : Timer
        {
            private Mobile m_Owner;

            public ExpirePolymorphTimer(Mobile owner)
                : base(TimeSpan.FromMinutes(3.0))
            {
                m_Owner = owner;

                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                if (!m_Owner.CanBeginAction(typeof(PolymorphSpell)))
                {
                    m_Owner.BodyMod = 0;
                    m_Owner.HueMod = -1;
                    m_Owner.EndAction(typeof(PolymorphSpell));
                }
            }
        }

        public void SpawnWraiths(Mobile target)
        {
            Map map = this.Map;

            if (map == null)
                return;

            int newWraiths = Utility.RandomMinMax(5, 8);

            for (int i = 0; i < newWraiths; ++i)
            {
                Wraith wraith = new Wraith();

                wraith.Team = this.Team;
                wraith.FightMode = FightMode.Weakest; // changed from Closest

                bool validLocation = false;
                Point3D loc = this.Location;

                for (int j = 0; !validLocation && j < 10; ++j)
                {
                    int x = X + Utility.Random(3) - 1;
                    int y = Y + Utility.Random(3) - 1;
                    int z = map.GetAverageZ(x, y);

                    if (validLocation = map.CanFit(x, y, this.Z, 16, false, false))
                        loc = new Point3D(x, y, Z);
                    else if (validLocation = map.CanFit(x, y, z, 16, false, false))
                        loc = new Point3D(x, y, z);
                }

                wraith.MoveToWorld(loc, map);
                wraith.Combatant = null;
            }
        }

        public void DoSpecialAbility(Mobile target)
        {
            if (0.6 >= Utility.RandomDouble())
                Polymorph(target);

            if (0.2 >= Utility.RandomDouble())
                SpawnWraiths(target);
        }

        public override void OnGaveMeleeAttack(Mobile defender)
        {
            base.OnGaveMeleeAttack(defender);

            DoSpecialAbility(defender);

            defender.Damage(Utility.Random(20, 10), this);
            defender.Mana -= Utility.Random(20, 10);
        }

        public override void OnGotMeleeAttack(Mobile attacker)
        {
            base.OnGotMeleeAttack(attacker);

            DoSpecialAbility(attacker);

        }

        public override OppositionGroup OppositionGroup
        {
            get
            {
                return OppositionGroup.FeyAndUndead;
            }
        }

        public BlackLich(Serial serial)
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