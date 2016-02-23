using Server.Spells;
using Server.Spells.Fourth;
using Server.Spells.Third;
using Server.Spells.Second;
using Server.Spells.First;
using Server.Spells.Seventh;
using Server.Spells.Sixth;
using Server.Spells.Eighth;
using Server.Spells.Fifth;
using System;
using Server.Spells.Zulu.EarthSpells;

namespace Server.Mobiles.AI
{
    class SpellGroups
    {

        public Spell getSpellByGroup(int grp, BaseCreature mobile, Mobile target)
        {

            switch (grp) // Add moar
            {
                case 1:
                    return grp1(mobile, target);
            }
            return new TeleportSpell(mobile, null);
        }

        public Spell getRandomBeneficial(BaseCreature mobile) // cast a random benificial spell on self (monster)
        {
            Spell spell = null;

            if (mobile.Poisoned)
            {
                mobile.Say("");
                spell = new CureSpell(mobile, null);
            }

            switch (Utility.Random(0, 7)) // more chance to bless
            {
                case 0:
                    mobile.Say("Flam Sanct");
                    spell = new ReactiveArmorSpell(mobile, null);
                    break;
                case 1:
                    mobile.Say("Vas Uus Sanct");
                    spell = new ArchProtectionSpell(mobile, null);
                    break;
                case 2:
                    mobile.Say("Uus Mani");
                    spell = new StrengthSpell(mobile, null);
                    break;
                case 3:
                    mobile.Say("Uus Mani");
                    spell = new StrengthSpell(mobile, null);
                    break;
                case 4:
                    if (mobile.EarthMob)
                        spell = new EarthBless(mobile, null);

                    mobile.Say("Rel Sanct");
                    spell = new BlessSpell(mobile, null);
                    break;
                case 5:
                    mobile.Say("In Jux Sanct");
                    spell = new MagicReflectSpell(mobile, null);
                    break;
                case 6:
                    if (mobile.EarthMob)
                        spell = new EarthBless(mobile, null);

                    mobile.Say("Rel Sanct");
                    spell = new BlessSpell(mobile, null);
                    break;
                case 7:
                    if (mobile.EarthMob)
                        spell = new EarthBless(mobile, null);

                    mobile.Say("Rel Sanct");
                    spell = new BlessSpell(mobile, null);
                    break;
            }

            

            return spell;
        }

        public Spell getRandomDetrimental(BaseCreature mobile) // cast a random benificial spell on enemy (player)
        {
            Spell spell = null;

            switch (Utility.Random(0, 4))
            {
                case 0:
                    mobile.Say("Ort Rel");
                    spell = new ManaDrainSpell(mobile, null);
                    break;
                case 1:
                    mobile.Say("Des Sanct");
                    spell = new CurseSpell(mobile, null);
                    break;
                case 2:
                    mobile.Say("An Ex Por");
                    spell = new ParalyzeSpell(mobile, null);
                    break;
                case 3:
                    mobile.Say("Ort Sanct");
                    spell = new ManaVampireSpell(mobile, null);
                    break;
                case 4:
                    mobile.Say("An Ort");
                    spell = new DispelSpell(mobile, null);
                    break;
                case 5:
                    mobile.Say("In Nox");
                    spell = new PoisonSpell(mobile, null);
                    break;
            }

            return spell;

        }

        public Spell getRandomMiscSpell() // dunno yet
        {
            return null;
        }

        public virtual Spell getRandomLowLevelDamageSpell(BaseCreature mobile) // Casts a low level damage spell on enemy
        {
            Spell spell = null;

            switch (Utility.Random(0, 2))
            {
                case 0:
                    mobile.Say("In Por Ylem");
                    spell = new HarmSpell(mobile, null);
                    break;
                case 1:
                    mobile.Say("An Mani");
                    spell = new HarmSpell(mobile, null);
                    break;
                case 2:
                    mobile.Say("Vas Flam");
                    spell = new FireballSpell(mobile, null);
                    break;
            }
            return spell;
        }

        public virtual Spell getRandomMediumLevelDamageSpell(BaseCreature mobile, Mobile target) // Casts a medium level damage spell on enemy
        {
            Spell spell = null;

            switch (Utility.Random(0, 4))
            {
                case 1:
                    mobile.Say("Por Ort Grav");
                    spell = new LightningSpell(mobile, null);
                    break;
                case 2:
                    if (mobile.Int > target.Int) // check if int is higher than targets, else cast lightningspell
                    {
                        mobile.Say("Por Corp Wis");
                        spell = new MindBlastSpell(mobile, null);
                        break;
                    }
                    goto case 1;
                case 3:
                    mobile.Say("Corp Por");
                    spell = new EnergyBoltSpell(mobile, null);
                    break;
                case 4:
                    mobile.Say("Vas Ort Flam");
                    spell = new ExplosionSpell(mobile, null);
                    break;
            }
            return spell;
        }

        public virtual Spell EvilStuff(BaseCreature mobile, Mobile target) // Mohahahahaha...
        {
            Spell spell = null;

            if (target.Paralyzed)
            {
                mobile.Say("Rel Por");
                spell = new TeleportSpell(mobile, null);
            }
            if (!target.Paralyzed)
            {
                mobile.Say("An Ex Por");
                spell = new ParalyzeSpell(mobile, null);
            }
            if (!target.Poisoned)
            {
                mobile.Say("In Nox");
                spell = new PoisonSpell(mobile, null);
            }
            if (target.Hidden)
            {
                mobile.Say("I see you!");
                spell = new RevealSpell(mobile, null);
            }

            return spell;
        }

        public virtual Spell getRandomHighLevelDamageSpell(BaseCreature mobile, Mobile target) // Casts a high level damage spell on enemy
        {
            Spell spell = null;

            switch (Utility.Random(0, 8))
            {
                case 1:
                    mobile.Say("Por Ort Grav");
                    spell = new LightningSpell(mobile, null);
                    break;
                case 2:
                    if (mobile.Int > target.Int) // check if int is higher than targets, else cast lightningspell
                    {
                        mobile.Say("Por Corp Wis");
                        spell = new MindBlastSpell(mobile, null);
                        break;
                    }
                    goto case 1;
                case 3:
                    mobile.Say("Corp Por");
                    spell = new EnergyBoltSpell(mobile, null);
                    break;
                case 4:
                    mobile.Say("Vas Ort Flam");
                    spell = new ExplosionSpell(mobile, null);
                    break;
                case 5:
                    mobile.Say("Kal Vas Flam");
                    spell = new FlameStrikeSpell(mobile, null);
                    break;
                case 6:
                    mobile.Say("Vas Ort Grav");
                    spell = new ChainLightningSpell(mobile, null);
                    break;
                case 7:
                    mobile.Say("Flam Kal Des Ylem");
                    spell = new MeteorSwarmSpell(mobile, null);
                    break;
                case 8:
                    mobile.Say("In Vas Por");
                    spell = new EarthquakeSpell(mobile, null);
                    break;
            }
            return spell;
        }

        public virtual Spell getRandomEarthSpell(BaseCreature mobile)
        {
            Spell spell = null;

            switch (Utility.Random(0, 4))
            {
                case 0:
                    mobile.Say("Esmagamento Con Pedra");
                    spell = new ShiftingEarth(mobile, null);
                    break;
                case 1:
                    mobile.Say("Batida Do Deus");
                    spell = new CallLightning(mobile, null);
                    break;
                case 2:
                    mobile.Say("Gust Do Ar");
                    spell = new Gustofair(mobile, null);
                    break;
                case 3:
                    mobile.Say("Batida Do Fogo");
                    spell = new RisingFire(mobile, null);
                    break;
                case 4:
                    mobile.Say("Geada Com Inverno");
                    spell = new IceStrike(mobile, null);
                    break;
            }
            return spell;
        }

        public virtual Spell getRandomNecroSpell(BaseCreature mobile)
        {
            return null;
        }

        public virtual Spell EarthBless(BaseCreature mobile)
        {
            return new EarthBless(mobile, null);
        }

        public virtual Spell CheckIfHealingNeeded(BaseCreature mobile)
        {
            Spell spell = null;
            var maxHits = mobile.HitsMax;
            var currentHits = mobile.Hits;
            var checkForHeal = currentHits / maxHits;

            if (mobile.Poisoned)
            {
                if (mobile.EarthMob)
                    spell = new Antidote(mobile, null);

                spell = new CureSpell(mobile, null);
                return spell;
            }

            if (checkForHeal <= 0.4)
            {
                if (mobile.EarthMob)
                    spell = new NaturesTouch(mobile, null);

                spell = new GreaterHealSpell(mobile, null);
            }

            return spell;
        }

        private const double HealChance = 0.10;// 10% chance to heal at gm magery //  only used in CheckCastHealingSpell right now
        private DateTime m_NextHealTime;
        private Spell CheckCastHealingSpell(BaseCreature mobile) // stolen function from MageAI, havent checked it out
        {
            // If I'm poisoned, always attempt to cure.
            if (mobile.Poisoned)
                return new CureSpell(mobile, null);

            // Summoned creatures never heal themselves.
            if (mobile.Summoned)
                return null;

            if (mobile.Controlled)
            {
                if (DateTime.UtcNow < m_NextHealTime)
                    return null;
            }


            if (Utility.Random(0, 4 + (mobile.Hits == 0 ? mobile.HitsMax : (mobile.HitsMax / mobile.Hits))) < 3)
                return null;


            Spell spell = null;

            if (mobile.Hits < (mobile.HitsMax - 50))
            {

                spell = new GreaterHealSpell(mobile, null);

                if (spell == null)
                    spell = new HealSpell(mobile, null);

            }
            else if (mobile.Hits < (mobile.HitsMax - 10))
            {
                spell = new HealSpell(mobile, null);
            }

            double delay;

            if (mobile.Int >= 500)
                delay = Utility.RandomMinMax(7, 10);
            else
                delay = Math.Sqrt(600 - mobile.Int);

            this.m_NextHealTime = DateTime.UtcNow + TimeSpan.FromSeconds(delay);

            return spell;
        }

        public virtual Spell GetRandomDamageSpell(BaseCreature mobile) // Casts a random damage spell on enemy
        {

            var maxHits = mobile.HitsMax;
            var currentHits = mobile.Hits;
            var checkForHeal = currentHits / maxHits;

            if (checkForHeal <= 0.3)
            { // test
                CheckIfHealingNeeded(mobile);
            }

            switch (Utility.Random(11))
            {
                case 0:
                case 1:
                    mobile.Say("In Por Ylem");
                    return new MagicArrowSpell(mobile, null);
                case 2:
                case 3:
                    mobile.Say("An Mani");
                    return new HarmSpell(mobile, null);
                case 4:
                case 5:
                    mobile.Say("Vas Flam");
                    return new FireballSpell(mobile, null);
                case 6:
                case 7:
                    mobile.Say("Por Ort Grav");
                    return new LightningSpell(mobile, null);
                case 8:
                case 9:
                    mobile.Say("Por Corp Wis");
                    return new MindBlastSpell(mobile, null);
                case 10:
                    mobile.Say("Corp Por");
                    return new EnergyBoltSpell(mobile, null);
                case 11:
                    mobile.Say("Vas Ort Flam");
                    return new ExplosionSpell(mobile, null);
                default:
                    mobile.Say("Kal Vas Flam");
                    return new FlameStrikeSpell(mobile, null);
            }
        }

        public Spell grp1(BaseCreature mobile, Mobile target) // spells for group 1 enemies
        {
            Spell spell = null;
            switch (Utility.Random(16))
            {
                case 0:
                    if (target.Poisoned)
                        goto default;

                    mobile.DebugSay("Attempting to poison");
                    mobile.Say("In Nox");
                    spell = new PoisonSpell(mobile, null);
                    break;
                case 1:
                    if (!target.Hidden)
                        goto default;

                    mobile.DebugSay("Attempting to cast reveal");
                    mobile.Say("Wis Quas");
                    spell = new RevealSpell(mobile, null);
                    break;

                default:
                    spell = GetRandomDamageSpell(mobile);
                    break;


            }
            return spell;
        }


    }


}
